namespace ClubbyBook.Data.Models
{
    using Pingvinius.Framework.Entities;
    using Pingvinius.Framework.Utilities;

    public partial class Profile : IEntity, IUrlRewriteable
    {
        public Gender Gender
        {
            get
            {
                return sbGender.HasValue ? (Gender)sbGender.Value : Gender.NotSpecified;
            }
            set
            {
                sbGender = (sbyte)value;
            }
        }

        #region IUrlRewriteable Members

        public string GenerateUrlRewriteString()
        {
            string profileAlias = this.User.EMail;
            profileAlias = profileAlias.Substring(0, profileAlias.IndexOf('@'));
            profileAlias = profileAlias.Replace('.', '-');
            profileAlias = profileAlias.Replace('_', '-');

            return LatinizeHelper.Latinize(profileAlias);
        }

        #endregion IUrlRewriteable Members
    }
}