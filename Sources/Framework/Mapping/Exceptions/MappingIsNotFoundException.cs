namespace Pingvinius.Framework.Repositories.Exceptions
{
    using System;

    [Serializable]
    public sealed class MappingIsNotFoundException : Exception
    {
        public MappingIsNotFoundException(Type sourceType, Type targetType)
            : base(string.Format("The proper mapping {0} -> {1} is not found.", sourceType.Name, targetType.Name))
        {
        }
    }
}