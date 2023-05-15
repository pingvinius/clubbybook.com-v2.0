namespace ClubbyBook.Web.UI.Areas.Admin.Models.Profile
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;
    using ClubbyBook.Business.Repositories.Interfaces;
    using ClubbyBook.Business.Repositories.SelectCriteria;
    using ClubbyBook.Common;
    using ClubbyBook.Data.Models;
    using ClubbyBook.Web.UI.Utilities;
    using Pingvinius.Framework.Mvc.Models;
    using Pingvinius.Framework.Repositories;
    using Pingvinius.Framework.Utilities;

    public sealed class ProfileRolesViewModel : ViewModel
    {
        private static readonly Dictionary<string, string> displayNames = new Dictionary<string, string>()
        {
            { RoleNames.Admin, "Администратор" },
            { RoleNames.Editor, "Редактор" },
            { RoleNames.Account, "Пользователь" }
        };

        private Profile profile;

        private IEnumerable<Role> selectedRolesCache;

        public IEnumerable<Role> SelectedRoles
        {
            get
            {
                if (this.selectedRolesCache == null)
                {
                    if (this.profile != null)
                    {
                        this.selectedRolesCache = RepositoryFactory.Get<IRoleRepository>().Select(new RoleSelectCriteria.UserRoles(this.profile.UserId));
                    }
                    else
                    {
                        var selectedIntValueIds = ParsingHelper.SplitToIntegers(this.SelectedValueIds);
                        if (selectedIntValueIds.Count() > 0)
                        {
                            this.selectedRolesCache = RepositoryFactory.Get<IRoleRepository>().Select(new RoleSelectCriteria.RoleIds(selectedIntValueIds));
                        }
                        else
                        {
                            return new List<Role>();
                        }
                    }
                }
                return this.selectedRolesCache;
            }
        }

        [Required(ErrorMessage = "Пользователь должен иметь хотя бы одну роль.")]
        public string SelectedValueIds { get; set; }

        public string DisplayString
        {
            get
            {
                if (this.SelectedRoles != null)
                {
                    return string.Join(", ", this.SelectedRoles.Select(r => ProfileRolesViewModel.GetDisplayName(r.Name)).OrderBy(n => n));
                }
                return UIHelper.NotSpecifiedString;
            }
        }

        public ProfileRolesViewModel()
            : this(null)
        {
        }

        public ProfileRolesViewModel(Profile profile)
        {
            this.profile = profile;
            this.selectedRolesCache = null;
            this.SelectedValueIds = this.profile != null ? string.Join(",", this.SelectedRoles.Select(r => r.Id)) : string.Empty;
        }

        public MultiSelectList GetSelectList()
        {
            var allRoles = RepositoryFactory.Get<IRoleRepository>().Select();

            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var role in allRoles)
            {
                list.Add(new SelectListItem()
                {
                    Selected = this.SelectedRoles != null && this.SelectedRoles.Contains(role),
                    Text = ProfileRolesViewModel.GetDisplayName(role.Name),
                    Value = role.Id.ToString()
                });
            }

            var selectedValueIds = this.SelectedRoles != null ? this.SelectedRoles.Select(r => r.Id.ToString()).ToList() : new List<string>();
            return new MultiSelectList(list.OrderBy(i => i.Text), "Value", "Text", selectedValueIds);
        }

        private static string GetDisplayName(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentNullException("roleName");
            }

            if (!ProfileRolesViewModel.displayNames.ContainsKey(roleName))
            {
                throw new NotSupportedException(string.Format("The role '{0}' is not supported in this version.", roleName));
            }

            return ProfileRolesViewModel.displayNames[roleName];
        }
    }
}