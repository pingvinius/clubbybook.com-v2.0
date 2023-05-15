namespace Pingvinius.Framework.DbContext
{
    using System;
    using System.Web;

    public sealed class WebDbContextStore : IDbContextStore
    {
        private const string currentSessionKeyFormat = "context_session_key_{0}";

        private static string CurrentSessionKey
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    throw new InvalidOperationException("HttpContext.Current == null");
                }
                return string.Format(WebDbContextStore.currentSessionKeyFormat, HttpContext.Current.GetHashCode().ToString("x8"));
            }
        }

        #region IDbContextStore Members

        void IDbContextStore.Add(IDbContext context)
        {
            if (HttpContext.Current == null)
            {
                throw new InvalidOperationException("HttpContext.Current == null");
            }

            HttpContext.Current.Items.Add(WebDbContextStore.CurrentSessionKey, context);
        }

        void IDbContextStore.Remove()
        {
            if (HttpContext.Current == null)
            {
                throw new InvalidOperationException("HttpContext.Current == null");
            }

            HttpContext.Current.Items.Remove(WebDbContextStore.CurrentSessionKey);
        }

        bool IDbContextStore.Contains()
        {
            return HttpContext.Current != null ? HttpContext.Current.Items.Contains(WebDbContextStore.CurrentSessionKey) : false;
        }

        IDbContext IDbContextStore.Get()
        {
            return HttpContext.Current != null ? (IDbContext)HttpContext.Current.Items[WebDbContextStore.CurrentSessionKey] : null;
        }

        #endregion IDbContextStore Members
    }
}