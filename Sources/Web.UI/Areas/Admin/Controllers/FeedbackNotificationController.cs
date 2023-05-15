namespace ClubbyBook.Web.UI.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using ClubbyBook.Business.Repositories.Interfaces;
    using ClubbyBook.Business.Repositories.SelectCriteria;
    using ClubbyBook.Common;
    using ClubbyBook.Data.Models;
    using ClubbyBook.Web.UI.Areas.Admin.Controllers.Base;
    using ClubbyBook.Web.UI.Areas.Admin.Models.FeedbackNotification;
    using Pingvinius.Framework.Attributes;
    using Pingvinius.Framework.Repositories.SelectCriteria;

    [AccessPermission(RoleNames.Admin)]
    public sealed class FeedbackNotificationController : AdminEntityController<IFeedbackNotificationRepository, FeedbackNotification,
        FeedbackNotificationViewModel, FeedbackNotificationViewModelList>
    {
        public FeedbackNotificationController()
        {
            this.CurrentTabName = "tab-notifications";
        }

        protected override IList<ISelectCriteria<FeedbackNotification>> GetIndexSelectCriteria()
        {
            var selectCriteria = base.GetIndexSelectCriteria();
            selectCriteria.Add(new FeedbackNotificationSelectCriteria.CreatedDateSort());
            return selectCriteria;
        }

        public override ActionResult Edit(FeedbackNotificationViewModel viewModel)
        {
            throw new NotSupportedException("Edit action is not supported for feedback notifications.");
        }

        public override ActionResult Edit(int? id = null)
        {
            throw new NotSupportedException("Edit action is not supported for feedback notifications.");
        }

        public override ActionResult Details(int id)
        {
            throw new NotSupportedException("Details action is not supported for feedback notifications.");
        }
    }
}