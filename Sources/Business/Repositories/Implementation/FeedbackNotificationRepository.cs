namespace ClubbyBook.Business.Repositories.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using ClubbyBook.Business.Repositories.Interfaces;
    using ClubbyBook.Business.Repositories.SelectCriteria;
    using ClubbyBook.Data.Models;
    using Pingvinius.Framework.Repositories.SelectCriteria;
    using Pingvinius.Framework.Utilities;

    internal sealed class FeedbackNotificationRepository : EntityRepository<FeedbackNotification>, IFeedbackNotificationRepository
    {
        protected override DbSet<FeedbackNotification> EntityList
        {
            get { return this.Context.FeedbackNotifications; }
        }

        protected override DbQuery<FeedbackNotification> ConfigureQueryForGet(DbQuery<FeedbackNotification> query)
        {
            return base.ConfigureQueryForGet(query)
                .Include("Notification")
                .Include("User");
        }

        protected override DbQuery<FeedbackNotification> ConfigureQueryForSelect(DbQuery<FeedbackNotification> query)
        {
            return base.ConfigureQueryForSelect(query)
                .Include("Notification")
                .Include("User");
        }

        protected override IList<ISelectCriteria<FeedbackNotification>> GetDefaultSelectCriteriaForSelect()
        {
            var selectCriteria = base.GetDefaultSelectCriteriaForSelect();
            selectCriteria.Add(new FeedbackNotificationSelectCriteria.CreatedDateSort());
            return selectCriteria;
        }

        protected override FeedbackNotification CreateInstanceInternal()
        {
            // Create notification
            var notification = new Notification();
            notification.CreatedDate = DateTimeHelper.Now;
            this.Context.Notifications.Add(notification);

            // Create feedback notification
            var entity = base.CreateInstanceInternal();
            entity.IsNew = true;
            entity.Notification = notification;

            return entity;
        }

        protected override void DeleteInternal(FeedbackNotification entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            // Remove notification
            this.Context.Notifications.Remove(entity.Notification);

            // Remove feedback notification
            base.DeleteInternal(entity);
        }
    }
}