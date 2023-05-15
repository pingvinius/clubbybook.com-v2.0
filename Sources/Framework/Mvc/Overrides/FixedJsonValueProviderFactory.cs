namespace Pingvinius.Framework.Mvc.Overrides
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;

    public sealed class FixedJsonValueProviderFactory : ValueProviderFactory
    {
        public int MaxJsonLength { get; set; }

        #region Overrides Routine

        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }

            object jsonData = FixedJsonValueProviderFactory.GetDeserializedObject(controllerContext, this.MaxJsonLength);
            if (jsonData == null)
            {
                return null;
            }

            Dictionary<string, object> backingStore = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            FixedJsonValueProviderFactory.AddToBackingStore(backingStore, string.Empty, jsonData);
            return new DictionaryValueProvider<object>(backingStore, CultureInfo.CurrentCulture);
        }

        #endregion

        #region Private Routine

        private static void AddToBackingStore(Dictionary<string, object> backingStore, string prefix, object value)
        {
            IDictionary<string, object> d = value as IDictionary<string, object>;
            if (d != null)
            {
                foreach (KeyValuePair<string, object> entry in d)
                {
                    FixedJsonValueProviderFactory.AddToBackingStore(backingStore, FixedJsonValueProviderFactory.MakePropertyKey(prefix, entry.Key), entry.Value);
                }
                return;
            }

            IList l = value as IList;
            if (l != null)
            {
                for (int i = 0; i < l.Count; i++)
                {
                    FixedJsonValueProviderFactory.AddToBackingStore(backingStore, FixedJsonValueProviderFactory.MakeArrayKey(prefix, i), l[i]);
                }
                return;
            }

            // primitive
            backingStore[prefix] = value;
        }

        private static object GetDeserializedObject(ControllerContext controllerContext, int maxJsonLength)
        {
            if (!controllerContext.HttpContext.Request.ContentType.StartsWith("application/json", StringComparison.OrdinalIgnoreCase))
            {
                // not JSON request
                return null;
            }

            StreamReader reader = new StreamReader(controllerContext.HttpContext.Request.InputStream);
            string bodyText = reader.ReadToEnd();
            if (string.IsNullOrEmpty(bodyText))
            {
                // no JSON data
                return null;
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = maxJsonLength;
            return serializer.DeserializeObject(bodyText);
        }


        private static string MakeArrayKey(string prefix, int index)
        {
            return prefix + "[" + index.ToString(CultureInfo.InvariantCulture) + "]";
        }

        private static string MakePropertyKey(string prefix, string propertyName)
        {
            return (string.IsNullOrEmpty(prefix)) ? propertyName : prefix + "." + propertyName;
        }

        #endregion
    }
}
