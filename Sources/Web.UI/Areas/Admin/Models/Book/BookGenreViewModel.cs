namespace ClubbyBook.Web.UI.Areas.Admin.Models.Book
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;
    using ClubbyBook.Business.Repositories.Interfaces;
    using ClubbyBook.Data.Models;
    using ClubbyBook.Web.UI.Utilities;
    using Pingvinius.Framework.Mvc.Models;
    using Pingvinius.Framework.Repositories;
    using Pingvinius.Framework.Tree;
    using Pingvinius.Framework.Utilities;

    public sealed class BookGenreViewModel : ViewModel
    {
        private BookGenre bookGenre;
        private Genre selectedGenreCache;

        public string DisplayString
        {
            get
            {
                if (this.SelectedGenre != null)
                {
                    return this.SelectedGenre.FullName;
                }
                return UIHelper.NotSpecifiedString;
            }
        }

        public Genre SelectedGenre
        {
            get
            {
                if (this.selectedGenreCache == null)
                {
                    if (this.bookGenre != null && this.bookGenre.Genre != null)
                    {
                        this.selectedGenreCache = this.bookGenre.Genre;
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(this.SelectedGenreId))
                        {
                            var selectedGenreId = ParsingHelper.ToNullableInt(this.SelectedGenreId);
                            if (selectedGenreId.HasValue)
                            {
                                this.selectedGenreCache = RepositoryFactory.Get<IGenreRepository>().Get(selectedGenreId.Value);
                            }
                        }
                    }
                }
                return this.selectedGenreCache;
            }
        }

        [Required(ErrorMessage = "Жанр является обязательным полем.")]
        public string SelectedGenreId { get; set; }

        public BookGenreViewModel()
            :this(null)
        {
        }

        public BookGenreViewModel(BookGenre bookGenre)
        {
            this.bookGenre = bookGenre;
            if (this.bookGenre != null && this.bookGenre.Genre != null)
            {
                this.SelectedGenreId = this.bookGenre.Genre.Id.ToString();
            }
        }

        public SelectList GetSelectList()
        {
            var genresFlatTree = RepositoryFactory.Get<IGenreRepository>().GetFlatTree();

            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem()
            {
                Selected = this.SelectedGenre == null,
                Text = UIHelper.SelectString,
                Value = string.Empty
            });

            foreach (var genreFlatTreeItem in genresFlatTree)
            {
                list.Add(new SelectListItem()
                {
                    Selected = this.SelectedGenre != null && this.SelectedGenre.Id == genreFlatTreeItem.Item.Id,
                    Text = BookGenreViewModel.GetDisplayName(genreFlatTreeItem),
                    Value = genreFlatTreeItem.Item.Id.ToString()
                });
            }

            var selectedValueId = this.SelectedGenre != null ? this.SelectedGenre.Id.ToString() : string.Empty;
            return new SelectList(list, "Value", "Text", selectedValueId);
        }

        private static string GetDisplayName(FlatTreeItem<Genre> flatTreeItem)
        {
            if (flatTreeItem == null)
            {
                throw new ArgumentNullException("flatTreeItem");
            }

            string indent = string.Empty;
            for (int i = flatTreeItem.Level - 1; i > 0; i--)
            {
                indent += "-";
            }

            return indent + (string.IsNullOrEmpty(indent) ? string.Empty : " ") + flatTreeItem.Item.Name;
        }
    }
}