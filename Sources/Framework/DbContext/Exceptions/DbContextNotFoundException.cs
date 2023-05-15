namespace Pingvinius.Framework.DbContext.Exceptions
{
    using System;

    [Serializable]
    public sealed class DbContextNotFoundException : Exception
    {
        public DbContextNotFoundException()
            : base("The database context is not found.")
        {
        }
    }
}