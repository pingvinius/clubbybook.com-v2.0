namespace Pingvinius.Framework.Repositories.Resolving
{
    using System;

    /// <summary>
    /// The repository resolver which helps to link repository interface with repository interface implementation.
    /// </summary>
    public interface IRepositoryResolver
    {
        /// <summary>
        /// Resolves repository type which implements specified interface by specified interface type.
        /// </summary>
        /// <param name="interfaceType">Type of the interface.</param>
        /// <returns>The repository type which implements specified interface.</returns>
        Type Resolve(Type interfaceType);
    }
}