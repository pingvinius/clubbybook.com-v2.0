namespace Pingvinius.Framework.Repositories.Exceptions
{
    using System;

    [Serializable]
    public sealed class MapperIsNotInitializedException : Exception
    {
        public MapperIsNotInitializedException()
            : base("The mapper is not initialized.")
        {
        }
    }
}