namespace ClubbyBook.Business.Repositories.Interfaces
{
    using System.Collections.Generic;
    using ClubbyBook.Data.Models;
    using Pingvinius.Framework.Repositories;

    public interface IUserRepository : IEntityRepository<User>
    {
        void SetUserRoles(User user, IEnumerable<Role> roles);

        void UpdateActivity(User user);

        User Register(string email, string password);
    }
}