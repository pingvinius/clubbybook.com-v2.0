namespace ClubbyBook.Web.UI.Controllers.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using ClubbyBook.Web.UI.Models;
    using Pingvinius.Framework.Entities;
    using Pingvinius.Framework.Mvc.Controllers;

    [HandleError]
    public class ClubbyBookController : AbstractController
    {
        public string SearchKey
        {
            get
            {
                var searchKey = this.GetStringParameter("SearchKey");
                if (searchKey == null)
                {
                    return string.Empty;
                }

                return HttpUtility.HtmlDecode(searchKey).Trim();
            }
        }

        public int PageNumber
        {
            get
            {
                var pageNumber = this.GetIntParameter("page");
                if (!pageNumber.HasValue)
                {
                    return 0;
                }

                return pageNumber.Value;
            }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            base.OnException(filterContext);

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;

                filterContext.Result = this.CreateAjaxErrorResponse(filterContext.Exception);
                filterContext.ExceptionHandled = true;
            }
        }

        #region Ajax Related

        protected JsonResult CreateAjaxErrorResponse(Exception exception)
        {
            return this.Json(new AjaxResponseViewModel(exception), AbstractController.DefaultJsonContentType, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult CreateAjaxSuccessResponse(object obj)
        {
            return this.Json(new AjaxResponseViewModel(obj), AbstractController.DefaultJsonContentType, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult CreateAjaxSuccessResponse()
        {
            return this.Json(new AjaxResponseViewModel(), AbstractController.DefaultJsonContentType, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult CreateAjaxSuccessResponseForEntityList<TEntity>(IEnumerable<TEntity> entities,
            Func<TEntity, SelectListItem> converter = null) where TEntity : IEntity
        {
            List<SelectListItem> list = new List<SelectListItem>();
            string selectedValue = null;

            if (entities != null)
            {
                if (converter == null)
                {
                    converter = delegate(TEntity entity)
                    {
                        return new SelectListItem()
                        {
                            Text = entity.Id.ToString(),
                            Value = entity.Id.ToString()
                        };
                    };
                }

                list = entities.Select(e => converter(e)).ToList();
                selectedValue = list.Where(i => i.Selected).Select(i => i.Value.ToString()).FirstOrDefault();
            }

            SelectList selectList = new SelectList(list, "Value", "Text", selectedValue != null ? selectedValue : string.Empty);
            return this.CreateAjaxSuccessResponse(selectList);
        }

        #endregion
    }
}