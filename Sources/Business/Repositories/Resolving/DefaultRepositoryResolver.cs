namespace ClubbyBook.Business.Repositories.Resolving
{
    using System;
    using System.Collections.Generic;
    using ClubbyBook.Business.Repositories.Implementation;
    using ClubbyBook.Business.Repositories.Interfaces;
    using Pingvinius.Framework.Repositories.Resolving;

    public sealed class DefaultRepositoryResolver : IRepositoryResolver
    {
        private readonly Dictionary<Type, Type> typesMap = new Dictionary<Type, Type>();

        public DefaultRepositoryResolver()
        {
            // User and Roles
            this.Bind<IRoleRepository, RoleRepository>();
            this.Bind<IUserRepository, UserRepository>();

            // Base entities
            this.Bind<IAuthorRepository, AuthorRepository>();
            this.Bind<IBookRepository, BookRepository>();
            this.Bind<IProfileRepository, ProfileRepository>();
            this.Bind<INewsRepository, NewsRepository>();
            this.Bind<IFeedbackNotificationRepository, FeedbackNotificationRepository>();
            this.Bind<IConversationNotificationRepository, ConversationNotificationRepository>();

            // Collections
            this.Bind<ICityRepository, CityRepository>();
            this.Bind<IDistrictRepository, DistrictRepository>();
            this.Bind<ICountryRepository, CountryRepository>();
            this.Bind<IGenreRepository, GenreRepository>();
        }

        private void Bind<TSource, TDestination>()
        {
            this.typesMap.Add(typeof(TSource), typeof(TDestination));
        }

        #region IRepositoryResolver Members

        public Type Resolve(Type interfaceType)
        {
            return this.typesMap.ContainsKey(interfaceType) ? this.typesMap[interfaceType] : null;
        }

        #endregion IRepositoryResolver Members
    }
}