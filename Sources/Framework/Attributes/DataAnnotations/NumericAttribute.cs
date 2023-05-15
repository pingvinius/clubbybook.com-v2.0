namespace Pingvinius.Framework.Attributes.DataAnnotations
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class NumericAttribute : DataTypeAttribute
    {
        public NumericAttribute()
            : base("numeric")
        {
        }

        public override string FormatErrorMessage(string name)
        {
            if (this.ErrorMessage == null && this.ErrorMessageResourceName == null)
            {
                this.ErrorMessage = "The entered number has wrong format. It should be numeric value.";
            }

            return base.FormatErrorMessage(name);
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            double retNum;

            return double.TryParse(Convert.ToString(value), out retNum);
        }
    }
}