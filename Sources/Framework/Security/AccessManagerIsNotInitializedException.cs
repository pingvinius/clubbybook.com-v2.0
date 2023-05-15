namespace Pingvinius.Framework.Security
{
    using System;

    [Serializable]
    public sealed class AccessManagerIsNotInitializedException : Exception
    {
        public AccessManagerIsNotInitializedException()
            : base("Access manager is not initialized.")
        {
        }
    }
}