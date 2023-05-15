namespace ClubbyBook.Business.Repositories.Implementation
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using ClubbyBook.Business.Repositories.Interfaces;
    using ClubbyBook.Business.Repositories.SelectCriteria;
    using ClubbyBook.Data.Models;
    using Pingvinius.Framework.Repositories.SelectCriteria;
    using Pingvinius.Framework.Utilities;

    internal sealed class ConversationNotificationRepository : EntityRepository<ConversationNotification>, IConversationNotificationRepository
    {
        protected override DbSet<ConversationNotification> EntityList
        {
            get { return this.Context.ConversationNotifications; }
        }

        protected override DbQuery<ConversationNotification> ConfigureQueryForGet(DbQuery<ConversationNotification> query)
        {
            return base.ConfigureQueryForGet(query)
                .Include("Notification")
                .Include("FromUser")
                .Include("ToUser");
        }

        protected override DbQuery<ConversationNotification> ConfigureQueryForSelect(DbQuery<ConversationNotification> query)
        {
            return base.ConfigureQueryForSelect(query)
                .Include("Notification")
                .Include("FromUser")
                .Include("ToUser");
        }

        protected override IList<ISelectCriteria<ConversationNotification>> GetDefaultSelectCriteriaForSelect()
        {
            var selectCriteria = base.GetDefaultSelectCriteriaForSelect();
            selectCriteria.Add(new ConversationNotificationSelectCriteria.CreatedDateSort());
            return selectCriteria;
        }

        protected override ConversationNotification CreateInstanceInternal()
        {
            // Create notification
            var notification = new Notification();
            notification.CreatedDate = DateTimeHelper.Now;
            this.Context.Notifications.Add(notification);

            // Create conversation notification
            var entity = base.CreateInstanceInternal();
            entity.IsNew = true;
            entity.Notification = notification;

            return entity;
        }

        protected override void DeleteInternal(ConversationNotification entity)
        {
            // TODO: Do not remove notification
            // this.Context.Notifications.Remove(entity.Notification);

            // Remove conversation notification
            base.DeleteInternal(entity);
        }
    }
}