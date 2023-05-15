namespace Pingvinius.Framework.Attributes.DataAnnotations
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class YearAttribute : DataTypeAttribute
    {
        public YearAttribute()
            : base("year")
        {
        }

        public override string FormatErrorMessage(string name)
        {
            if (this.ErrorMessage == null && this.ErrorMessageResourceName == null)
            {
                this.ErrorMessage = "The entered value has has wrong format. It should have the following format: ####.";
            }

            return base.FormatErrorMessage(name);
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            int retNum;
            var parseSuccess = int.TryParse(Convert.ToString(value), out retNum);

            return parseSuccess && retNum >= 1 && retNum <= 9999;
        }
    }
}