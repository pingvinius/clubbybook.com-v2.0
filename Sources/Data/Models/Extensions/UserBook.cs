namespace ClubbyBook.Data.Models
{
    public partial class UserBook
    {
        public UserBookStatuses Status
        {
            get
            {
                return (UserBookStatuses)iStatus;
            }
            set
            {
                iStatus = (int)value;
            }
        }

        public UserBookOffers Offer
        {
            get
            {
                return (UserBookOffers)iOffer;
            }
            set
            {
                iOffer = (int)value;
            }
        }

        public UserBookVariants BookType
        {
            get
            {
                return (UserBookVariants)iBookType;
            }
            set
            {
                iBookType = (int)value;
            }
        }
    }
}