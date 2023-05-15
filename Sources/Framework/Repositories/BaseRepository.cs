namespace Pingvinius.Framework.Repositories
{
    using NLog;

    /// <summary>
    /// The base repository implementation.
    /// All repositories should inherit this class.
    /// </summary>
    public abstract class BaseRepository : IRepository
    {
        #region Fields

        /// <summary>
        /// Current logger instance.
        /// </summary>
        private Logger logger;

        #endregion Fields

        /// <summary>
        /// Gets the current logger.
        /// </summary>
        /// <value>
        /// The current logger.
        /// </value>
        public Logger Logger
        {
            get
            {
                if (this.logger == null)
                {
                    this.logger = LogManager.GetLogger(this.GetType().Name);
                }
                return this.logger;
            }
        }
    }
}