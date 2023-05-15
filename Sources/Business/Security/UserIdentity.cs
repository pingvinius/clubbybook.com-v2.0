namespace ClubbyBook.Business.Security
{
    using System;
    using System.Collections.Generic;
    using Pingvinius.Framework.Security;

    internal sealed class UserIdentity : IUserIdentity
    {
        public int Id { get; private set; }

        public string Email { get; private set; }

        public IEnumerable<string> Roles { get; private set; }

        public UserIdentity(int id, string email, IEnumerable<string> roles)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException("email");
            }

            if (roles == null)
            {
                throw new ArgumentNullException("roles");
            }

            this.Id = id;
            this.Email = email;
            this.Roles = roles;
        }
    }
}