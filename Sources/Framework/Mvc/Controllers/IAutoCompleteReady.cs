namespace Pingvinius.Framework.Mvc.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    /// <summary>
    /// The interface which is used to mark the controller that it is support AutoComplete feature.
    /// </summary>
    public interface IAutoCompleteReady
    {
        /// <summary>
        /// Gets the auto complete list by specified key.
        /// </summary>
        /// <param name="searchKey">The search key.</param>
        /// <returns>The items to be shown in auto complete box.</returns>
        [HttpPost]
        ActionResult GetAutoCompleteList(string searchKey);
    }
}