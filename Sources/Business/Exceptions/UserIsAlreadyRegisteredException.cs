namespace ClubbyBook.Business.Exceptions
{
    using System;

    [Serializable]
    public sealed class UserIsAlreadyRegisteredException : Exception
    {
        public UserIsAlreadyRegisteredException(string email)
            : base(string.Format("The user with '{0}' email already exists in the system.", email))
        {
        }
    }
}