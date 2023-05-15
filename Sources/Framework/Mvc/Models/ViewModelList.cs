namespace Pingvinius.Framework.Mvc.Models
{
    using System.Collections.Generic;

    public abstract class ViewModelList<TViewModel>
        where TViewModel : ViewModel
    {
        public List<TViewModel> Items { get; private set; }

        public string SearchKey { get; set; }

        public int TotalItemCount { get; set; }

        public PagingInfo PagingInfo { get; set; }

        protected ViewModelList()
        {
            this.Items = new List<TViewModel>();
            this.TotalItemCount = 0;
            this.PagingInfo = new PagingInfo();
        }
    }
}