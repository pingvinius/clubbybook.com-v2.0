namespace Pingvinius.Framework.Attributes.DataAnnotations
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class IntegerAttribute : DataTypeAttribute
    {
        public IntegerAttribute()
            : base("integer")
        {
        }

        public override string FormatErrorMessage(string name)
        {
            if (this.ErrorMessage == null && this.ErrorMessageResourceName == null)
            {
                this.ErrorMessage = "The entered value has wrong format. It should be integer value.";
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

            return int.TryParse(Convert.ToString(value), out retNum);
        }
    }
}