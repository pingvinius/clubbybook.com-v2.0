namespace Pingvinius.Framework.Utilities
{
    using System;
    using System.ComponentModel;
    using Pingvinius.Framework.Attributes;

    public static class AttributeHelper
    {
        public static string GetDescriptionValue(object obj)
        {
            DescriptionAttribute description = AttributeHelper.GetEnumValueAttribute<DescriptionAttribute>(obj);
            if (description != null)
            {
                return description.Description;
            }

            return string.Empty;
        }

        public static string GetUrlRewriteValue(object obj)
        {
            UrlRewriteAttribute urlRewrite = AttributeHelper.GetEnumValueAttribute<UrlRewriteAttribute>(obj);
            if (urlRewrite != null)
            {
                return urlRewrite.UrlRewrite;
            }

            return string.Empty;
        }

        public static object FindEnumValueByUrlRewrite<TEnumType>(string urlRewrite)
        {
            if (string.IsNullOrWhiteSpace(urlRewrite))
            {
                throw new ArgumentNullException("urlRewrite");
            }

            foreach (TEnumType value in Enum.GetValues(typeof(TEnumType)))
            {
                string valueUrlRewrite = AttributeHelper.GetUrlRewriteValue(value);
                if (!string.IsNullOrEmpty(valueUrlRewrite) && valueUrlRewrite.Equals(urlRewrite, StringComparison.OrdinalIgnoreCase))
                {
                    return value;
                }
            }

            return null;
        }

        private static AttributeType GetEnumValueAttribute<AttributeType>(object obj) where AttributeType : Attribute
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            object[] attributes = obj.GetType().GetField(obj.ToString()).GetCustomAttributes(typeof(AttributeType), false);
            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0] as AttributeType;
            }

            return null;
        }
    }
}