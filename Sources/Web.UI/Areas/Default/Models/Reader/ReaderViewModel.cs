namespace ClubbyBook.Web.UI.Areas.Default.Models.Reader
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ClubbyBook.Web.UI.Areas.Default.Models.Base;
    using ClubbyBook.Web.UI.Models;

    public sealed class ReaderViewModel : BaseEntityViewModel<ClubbyBook.Data.Models.Profile>
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string DisplayName { get; set; }

        public string Nickname { get; set; }

        public int TotalBookCount { get; set; }

        public int TotalReadBookCount { get; set; }

        [DataType("Gender")]
        public GenderViewModel Gender { get; set; }

        [DataType("City")]
        public CityViewModel City { get; set; }

        [DataType("Image")]
        public ImageViewModel Image { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UrlRewrite { get; set; }

        public IReadOnlyList<ReaderBookViewModel> Books { get; set; }
    }
}