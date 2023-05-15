namespace Pingvinius.Framework.Mvc.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Pingvinius.Framework.Extensions;

    public abstract class EnumViewModel<T> where T : struct
    {
        public T Selected { get; set; }

        public string DisplayString
        {
            get
            {
                return this.DisplayNames[this.Selected];
            }
        }

        protected abstract Dictionary<T, string> DisplayNames { get; }

        public EnumViewModel(T selected)
        {
            if (!typeof(T).IsEnum)
            {
                throw new TypeLoadException("The generic type should be Enum type.");
            }

            this.Selected = selected;
        }

        public SelectList GetSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var enumValue in EnumExtension.GetValues<T>())
            {
                list.Add(new SelectListItem()
                {
                    Selected = enumValue.Equals(this.Selected),
                    Text = this.DisplayNames[enumValue],
                    Value = enumValue.ToString()
                });
            }

            return new SelectList(list, "Value", "Text", this.Selected);
        }
    }
}