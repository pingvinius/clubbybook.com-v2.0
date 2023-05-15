namespace ClubbyBook.Web.UI.Exceptions
{
    using System;

    /// <summary>
    /// The exception which is raised when the user doesn't have enough rights to do something or view something.
    /// </summary>
    [Serializable]
    public sealed class AccessDenyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccessDenyException" /> class.
        /// </summary>
        public AccessDenyException()
            : base("The user doesn't have permissions to process current action.")
        {
        }
    }
}