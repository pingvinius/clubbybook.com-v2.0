namespace Pingvinius.Framework.DbContext.Exceptions
{
    using System;

    [Serializable]
    public sealed class DbContextNotInitializedException : Exception
    {
        public DbContextNotInitializedException()
            : base("The database context is not initialized.")
        {
        }
    }
}