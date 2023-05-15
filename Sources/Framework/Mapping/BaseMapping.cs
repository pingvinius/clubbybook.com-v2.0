namespace Pingvinius.Framework.Mapping
{
    public abstract class BaseMapping<TSource, TTarget>
        where TSource : class
        where TTarget : class
    {
        public TTarget Map(TSource source, TTarget targetBase)
        {
            return this.MapSourceToTarget(source, targetBase);
        }

        protected abstract TTarget MapSourceToTarget(TSource source, TTarget targetBase);
    }
}