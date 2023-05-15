namespace Pingvinius.Framework.Attributes.DataAnnotations
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class DigitsAttribute : DataTypeAttribute
    {
        public DigitsAttribute()
            : base("digits")
        {
        }

        public override string FormatErrorMessage(string name)
        {
            if (this.ErrorMessage == null && this.ErrorMessageResourceName == null)
            {
                this.ErrorMessage = "The entered value has wrong format. It should be a string with digits only.";
            }

            return base.FormatErrorMessage(name);
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            long retNum;

            var parseSuccess = long.TryParse(Convert.ToString(value), out retNum);

            return parseSuccess && retNum >= 0;
        }
    }
}