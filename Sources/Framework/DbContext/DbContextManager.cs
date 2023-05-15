namespace Pingvinius.Framework.DbContext
{
    using System;
    using NLog;
    using Pingvinius.Framework.DbContext.Exceptions;

    public static class DbContextManager
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static object syncDbContext = new object();

        private static IDbContextStore contextStore { get; set; }

        private static IDbContextFactory contextFactory { get; set; }

        private static bool IsInitialized
        {
            get
            {
                return DbContextManager.contextStore != null;
            }
        }

        public static IDbContext GetCurrentContext()
        {
            if (DbContextManager.contextStore == null)
            {
                throw new DbContextNotInitializedException();
            }

            if (!DbContextManager.contextStore.Contains())
            {
                lock (DbContextManager.syncDbContext)
                {
                    if (!DbContextManager.contextStore.Contains())
                    {
                        DbContextManager.contextStore.Add(DbContextManager.contextFactory.Create());
                    }
                }
            }

            var dbContext = DbContextManager.contextStore.Get();
            if (dbContext == null)
            {
                throw new DbContextNotFoundException();
            }
            return dbContext;
        }

        public static void Initialize(IDbContextStore contextStore, IDbContextFactory contextFactory)
        {
            DbContextManager.logger.Trace("Initialize");

            if (contextStore == null)
            {
                throw new ArgumentNullException("contextStore");
            }

            if (contextFactory == null)
            {
                throw new ArgumentNullException("contextFactory");
            }

            if (DbContextManager.IsInitialized)
            {
                throw new DbContextAlreadyInitializedException();
            }

            DbContextManager.contextStore = contextStore;
            DbContextManager.contextFactory = contextFactory;
        }

        public static void Destroy()
        {
            DbContextManager.logger.Trace("Destroy");

            try
            {
                if (DbContextManager.contextStore != null && DbContextManager.contextStore.Contains())
                {
                    DbContextManager.GetCurrentContext().Destroy();
                    DbContextManager.contextStore.Remove();
                }
            }
            catch (Exception ex)
            {
                DbContextManager.logger.Error("An error has occurred while disposing database context manager: {0}.", ex);
            }
            finally
            {
                DbContextManager.contextStore = null;
                DbContextManager.contextFactory = null;
            }
        }

        public static void InitCurrent()
        {
            DbContextManager.logger.Trace("InitCurrent");
        }

        public static void DestroyCurrent()
        {
            DbContextManager.logger.Trace("DestroyCurrent");

            try
            {
                if (DbContextManager.contextStore != null && DbContextManager.contextStore.Contains())
                {
                    DbContextManager.GetCurrentContext().Destroy();
                    DbContextManager.contextStore.Remove();
                }
            }
            catch (Exception ex)
            {
                DbContextManager.logger.Error("An error has occurred while disposing database context: {0}.", ex);
            }
        }
    }
}