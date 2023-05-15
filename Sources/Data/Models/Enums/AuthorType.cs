namespace ClubbyBook.Data.Models
{
    using Pingvinius.Framework.Attributes;

    public enum AuthorType : int
    {
        [UrlRewrite("type-none")]
        NotSpecified = 0,

        [UrlRewrite("type-man")]
        Person = 1,

        [UrlRewrite("type-publishing-house")]
        PublishingHouse = 2,
    }
}