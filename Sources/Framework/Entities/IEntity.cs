namespace Pingvinius.Framework.Entities
{
    /// <summary>
    /// The interface should be implemented if some class is entity class.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        int Id { get; }
    }
}