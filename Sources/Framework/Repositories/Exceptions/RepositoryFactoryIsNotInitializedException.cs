namespace Pingvinius.Framework.Repositories.Exceptions
{
    using System;

    /// <summary>
    /// The exception class which is used when the repository factory is not initialized but used.
    /// </summary>
    [Serializable]
    public sealed class RepositoryFactoryIsNotInitializedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryFactoryIsNotInitializedException"/> class.
        /// </summary>
        public RepositoryFactoryIsNotInitializedException()
            : base("The repository factory is not initialized.")
        {
        }
    }
}