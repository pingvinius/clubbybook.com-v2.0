namespace ClubbyBook.Business.Repositories.SelectCriteria
{
    using System.Linq;
    using ClubbyBook.Data.Models;
    using Pingvinius.Framework.Repositories.SelectCriteria;

    public static class FeedbackNotificationSelectCriteria
    {
        #region Where Routine

        #endregion Where Routine

        #region OrderBy Routine

        public sealed class CreatedDateSort : BaseOrderBySelectCriteria<FeedbackNotification>
        {
            public override IQueryable<FeedbackNotification> Apply(IQueryable<FeedbackNotification> query)
            {
                return query.OrderByDescending(entity => entity.Notification.CreatedDate);
            }
        }

        #endregion OrderBy Routine
    }
}