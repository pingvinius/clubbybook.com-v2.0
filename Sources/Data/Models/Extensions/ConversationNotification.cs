namespace ClubbyBook.Data.Models
{
    using Pingvinius.Framework.Entities;

    public partial class ConversationNotification : IEntity
    {
        public NotificationDirection Direction
        {
            get
            {
                return (NotificationDirection)sbDirection;
            }
            set
            {
                sbDirection = (sbyte)value;
            }
        }
    }
}