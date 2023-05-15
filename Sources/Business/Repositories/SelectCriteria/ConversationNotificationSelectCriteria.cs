namespace ClubbyBook.Business.Repositories.SelectCriteria
{
    using System.Linq;
    using ClubbyBook.Data.Models;
    using Pingvinius.Framework.Repositories.SelectCriteria;

    public static class ConversationNotificationSelectCriteria
    {
        #region Where Routine

        #endregion Where Routine

        #region OrderBy Routine

        public sealed class CreatedDateSort : BaseOrderBySelectCriteria<ConversationNotification>
        {
            public override IQueryable<ConversationNotification> Apply(IQueryable<ConversationNotification> query)
            {
                return query.OrderByDescending(entity => entity.Notification.CreatedDate);
            }
        }

        #endregion OrderBy Routine
    }
}