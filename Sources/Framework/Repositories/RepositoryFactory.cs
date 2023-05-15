namespace Pingvinius.Framework.Repositories
{
    using System;
    using System.Collections.Generic;
    using NLog;
    using Pingvinius.Framework.Repositories.Exceptions;
    using Pingvinius.Framework.Repositories.Resolving;

    /// <summary>
    /// The class represents repository factory.
    /// The class should be initialized before use.
    /// The factory has own cache to improve performance.
    /// </summary>
    public static class RepositoryFactory
    {
        #region Fields

        /// <summary>
        /// The repository resolver.
        /// </summary>
        private static IRepositoryResolver resolver;

        /// <summary>
        /// The repository factory cache.
        /// </summary>
        private static Dictionary<Type, IRepository> cache;

        /// <summary>
        /// Indicates if the repository factory is initialized.
        /// </summary>
        private static bool isInitialized;

        #endregion Fields

        /// <summary>
        /// Initializes repository factory by specified resolver.
        /// </summary>
        /// <param name="resolver">The repository resolver.</param>
        /// <exception cref="System.ArgumentNullException">resolver</exception>
        public static void Initialize(IRepositoryResolver resolver)
        {
            if (resolver == null)
            {
                throw new ArgumentNullException("resolver");
            }

            RepositoryFactory.resolver = resolver;
            RepositoryFactory.cache = new Dictionary<Type, IRepository>();

            RepositoryFactory.isInitialized = true;

            LogManager.GetCurrentClassLogger().Trace("The repository factory has been initialized successfully.");
        }

        /// <summary>
        /// Gets the instance of repository by type.
        /// </summary>
        /// <typeparam name="T">Repository type.</typeparam>
        /// <returns>The repository instance.</returns>
        /// <exception cref="Pingvinius.Framework.Repositories.Exceptions.RepositoryFactoryIsNotInitializedException">
        /// The repository factory is not initialized before use.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// The implementation class is not derived from IRespository.
        /// </exception>
        public static T Get<T>() where T : IRepository
        {
            if (!RepositoryFactory.isInitialized)
            {
                throw new RepositoryFactoryIsNotInitializedException();
            }

            Type type = typeof(T);

            if (!RepositoryFactory.cache.ContainsKey(type))
            {
                Type implementationType = RepositoryFactory.resolver.Resolve(type);
                if (implementationType == null)
                {
                    throw new InvalidOperationException(string.Format("The implementation type for '{0}' type is not found.", type.Name));
                }

                if (!typeof(IRepository).IsAssignableFrom(implementationType))
                {
                    throw new InvalidOperationException("The implementation class is not derived from IRespository.");
                }

                RepositoryFactory.cache.Add(type, (IRepository)Activator.CreateInstance(implementationType));
            }

            return (T)RepositoryFactory.cache[type];
        }
    }
}