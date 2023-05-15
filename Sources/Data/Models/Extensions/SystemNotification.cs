namespace ClubbyBook.Data.Models
{
    using Pingvinius.Framework.Entities;

    public partial class SystemNotification : IEntity
    {
        public SystemNotificationType Type
        {
            get
            {
                return (SystemNotificationType)sbType;
            }
            set
            {
                sbType = (sbyte)value;
            }
        }
    }
}