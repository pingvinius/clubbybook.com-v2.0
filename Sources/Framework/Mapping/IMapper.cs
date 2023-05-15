namespace Pingvinius.Framework.Mapping
{
    public interface IMapper
    {
        TTarget Map<TSource, TTarget>(TSource source, TTarget targetBase)
            where TSource : class
            where TTarget : class;

        bool Contains<TSource, TTarget>()
            where TSource : class
            where TTarget : class;
    }
}