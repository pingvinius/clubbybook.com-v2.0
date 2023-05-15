namespace Pingvinius.Framework.Security
{
    using System.Collections.Generic;

    public interface IUserIdentity
    {
        int Id { get; }

        string Email { get; }

        IEnumerable<string> Roles { get; }
    }
}