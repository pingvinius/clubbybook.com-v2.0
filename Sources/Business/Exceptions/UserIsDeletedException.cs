namespace ClubbyBook.Business.Exceptions
{
    using System;

    [Serializable]
    public sealed class UserIsDeletedException : Exception
    {
        public UserIsDeletedException(string email)
            : base(string.Format("The user with '{0}' email was deleted.", email))
        {
        }
    }
}