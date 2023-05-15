namespace ClubbyBook.Web.UI.Areas.Admin.Models.Book
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using ClubbyBook.Business.Repositories.Interfaces;
    using ClubbyBook.Business.Repositories.SelectCriteria;
    using ClubbyBook.Data.Models;
    using ClubbyBook.Web.UI.Utilities;
    using Pingvinius.Framework.Mvc.Models;
    using Pingvinius.Framework.Repositories;
    using Pingvinius.Framework.Repositories.SelectCriteria;
    using Pingvinius.Framework.Utilities;

    public sealed class BookAuthorListViewModel : ViewModel
    {
        private IEnumerable<BookAuthor> bookAuthors;
        private IEnumerable<Author> selectedAuthorListCache;

        public string DisplayString
        {
            get
            {
                if (this.SelectedAuthorList.Count() > 0)
                {
                    return string.Join(", ", this.SelectedAuthorList.Select(a => a.FullName));
                }
                return UIHelper.NotSpecifiedString;
            }
        }

        public IEnumerable<Author> SelectedAuthorList
        {
            get
            {
                if (this.selectedAuthorListCache == null)
                {
                    IEnumerable<int> ids = new List<int>();

                    if (this.bookAuthors != null)
                    {
                        ids = this.bookAuthors.Select(ba => ba.AuthorId).ToList();
                    }
                    else if (!string.IsNullOrWhiteSpace(this.SelectedAuthorIds))
                    {
                        ids = ParsingHelper.SplitToIntegers(this.SelectedAuthorIds);
                    }

                    List<ISelectCriteria<Author>> selectCriteria = new List<ISelectCriteria<Author>>();
                    selectCriteria.Add(new AuthorSelectCriteria.ByIdList(ids));
                    selectCriteria.Add(new AuthorSelectCriteria.FullNameSort());

                    this.selectedAuthorListCache = RepositoryFactory.Get<IAuthorRepository>().Select(selectCriteria).ToList();
                }

                return this.selectedAuthorListCache;
            }
        }

        [Required(ErrorMessage = "Книга должна иметь хотябы одного автора.")]
        public string SelectedAuthorIds { get; set; }

        public BookAuthorListViewModel()
            : this(null)
        {
        }

        public BookAuthorListViewModel(IEnumerable<BookAuthor> bookAuthors)
        {
            this.bookAuthors = bookAuthors;
            this.SelectedAuthorIds = this.bookAuthors != null ? string.Join(",", this.bookAuthors.Select(r => r.AuthorId)) : string.Empty;
        }
    }
}