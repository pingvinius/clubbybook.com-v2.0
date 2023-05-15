namespace Pingvinius.Framework.DbContext.Exceptions
{
    using System;

    [Serializable]
    public sealed class DbContextAlreadyInitializedException : Exception
    {
        public DbContextAlreadyInitializedException()
            : base("The database context is already initialized.")
        {
        }
    }
}