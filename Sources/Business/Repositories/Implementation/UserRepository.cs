namespace ClubbyBook.Business.Repositories.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using ClubbyBook.Business.Exceptions;
    using ClubbyBook.Business.Repositories.Interfaces;
    using ClubbyBook.Business.Repositories.SelectCriteria;
    using ClubbyBook.Data.Models;
    using Pingvinius.Framework.Repositories;
    using Pingvinius.Framework.Utilities;

    internal sealed class UserRepository : EntityRepository<User>, IUserRepository
    {
        protected override DbSet<User> EntityList
        {
            get { return this.Context.Users; }
        }

        protected override DbQuery<User> ConfigureQueryForGet(DbQuery<User> query)
        {
            return base.ConfigureQueryForGet(query).Include("Profiles");
        }

        protected override DbQuery<User> ConfigureQueryForSelect(DbQuery<User> query)
        {
            return base.ConfigureQueryForSelect(query).Include("Profiles");
        }

        #region IUserRepository Members

        public void SetUserRoles(User user, IEnumerable<Role> roles)
        {
            // Validate input parameters
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (roles == null)
            {
                throw new ArgumentNullException("roles");
            }

            if (roles.Count() == 0)
            {
                throw new ArgumentException("The user should contain at least one role.");
            }

            // Get current user roles for user
            var currentUserRoles = this.Context.UserRoles.Where(ur => ur.UserId == user.Id).ToList();

            // Return in case the roles for user aren't changed
            if (currentUserRoles.Count == roles.Count() && roles.Count() == currentUserRoles.Select(ur => ur.RoleId).Intersect(roles.Select(r => r.Id)).Count())
            {
                return;
            }

            // Delete all exist user roles
            foreach (var userRole in currentUserRoles)
            {
                this.Context.UserRoles.Remove(userRole);
            }

            // Assignee new roles
            foreach (var role in roles.Distinct())
            {
                this.Context.UserRoles.Add(new UserRole()
                {
                    RoleId = role.Id,
                    UserId = user.Id
                });
            }

            // Save changes
            this.Context.SaveChanges();

            // Log action
            this.Logger.Info(string.Format("New roles {0} assigned for user {1}.", string.Join(", ", roles.Select(r => r.Name)), user.EMail));
        }

        public void UpdateActivity(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.LastAccessDate = DateTimeHelper.Now;

            this.SaveChanges(user);
        }

        public User Register(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException("email");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("password");
            }

            if (this.Count(new UserSelectCriteria.ByEmail(email)) != 0)
            {
                throw new UserIsAlreadyRegisteredException(email);
            }

            // Create user
            User newUser = this.CreateInstance();
            newUser.EMail = email.ToLower();
            newUser.Password = Md5Helper.Calculate(password);
            newUser.CreatedDate = DateTimeHelper.Now;
            newUser.LastAccessDate = DateTimeHelper.Now;

            UserRole userRoleRelation = new UserRole();
            userRoleRelation.User = newUser;
            userRoleRelation.Role = RepositoryFactory.Get<IRoleRepository>().GetAccountRole();
            this.Context.UserRoles.Add(userRoleRelation);

            this.SaveChanges(newUser);

            // Create profile
            var newUserProfile = RepositoryFactory.Get<IProfileRepository>().CreateInstance();
            newUserProfile.User = newUser;
            newUserProfile.Gender = Gender.NotSpecified;
            newUserProfile.Name = string.Empty;
            newUserProfile.Nickname = string.Empty;
            newUserProfile.Surname = string.Empty;
            newUserProfile.Birthday = null;
            RepositoryFactory.Get<IProfileRepository>().SaveChanges(newUserProfile);

            return newUser;
        }

        #endregion IUserRepository Members
    }
}