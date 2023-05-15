namespace ClubbyBook.Web.UI.Models
{
    using System;
    using Pingvinius.Framework.Mvc.Models;

    public sealed class AjaxResponseViewModel : ViewModel
    {
        public bool Result { get; set; }

        public string ErrorMessage { get; set; }

        public object Object { get; set; }

        public AjaxResponseViewModel()
        {
            this.Result = true;
            this.Object = null;
            this.ErrorMessage = string.Empty;
        }

        public AjaxResponseViewModel(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            this.Result = true;
            this.Object = obj;
            this.ErrorMessage = string.Empty;
        }

        public AjaxResponseViewModel(Exception exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException("exception");
            }

            this.Result = false;
            this.Object = exception.Message;
            this.ErrorMessage = exception.ToString();
        }
    }
}