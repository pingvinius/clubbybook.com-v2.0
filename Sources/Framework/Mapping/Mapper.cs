namespace Pingvinius.Framework.Mapping
{
    using System;
    using NLog;
    using Pingvinius.Framework.Repositories.Exceptions;

    public static class Mapper
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static IMapper mapper;
        private static bool isInitialized;

        public static void Initialize(IMapper mapper)
        {
            if (mapper == null)
            {
                throw new ArgumentNullException("mapper");
            }

            Mapper.mapper = mapper;
            Mapper.isInitialized = true;

            Mapper.logger.Trace("The mapper has been initialized successfully.");
        }

        public static TTarget Map<TSource, TTarget>(TSource source, TTarget target)
            where TSource : class
            where TTarget : class
        {
            if (!Mapper.isInitialized)
            {
                throw new MapperIsNotInitializedException();
            }

            if (!Mapper.mapper.Contains<TSource, TTarget>())
            {
                throw new MappingIsNotFoundException(typeof(TSource), typeof(TTarget));
            }

            return Mapper.mapper.Map<TSource, TTarget>(source, target);
        }
    }
}