namespace Pingvinius.Framework.Mapping
{
    using System.Collections.Generic;

    public abstract class BaseMapper : IMapper
    {
        private Dictionary<int, object> mappings = new Dictionary<int, object>();

        protected void Bind<TSource, TTarget>(BaseMapping<TSource, TTarget> mapping)
            where TSource : class
            where TTarget : class
        {
            this.mappings.Add(BaseMapper.GetKey<TSource, TTarget>(), mapping);
        }

        private static int GetKey<T1, T2>()
            where T1 : class
            where T2 : class
        {
            int hash = 23;
            hash = hash * 31 + typeof(T1).Name.GetHashCode() ^ typeof(T1).GetHashCode();
            hash = hash * 31 + typeof(T2).Name.GetHashCode() ^ typeof(T2).GetHashCode();
            return hash;
        }

        #region IMapper Members

        public TTarget Map<TSource, TTarget>(TSource source, TTarget targetBase)
            where TSource : class
            where TTarget : class
        {
            return ((BaseMapping<TSource, TTarget>)this.mappings[BaseMapper.GetKey<TSource, TTarget>()]).Map(source, targetBase);
        }

        public bool Contains<TSource, TTarget>()
            where TSource : class
            where TTarget : class
        {
            return this.mappings.ContainsKey(BaseMapper.GetKey<TSource, TTarget>());
        }

        #endregion IMapper Members
    }
}