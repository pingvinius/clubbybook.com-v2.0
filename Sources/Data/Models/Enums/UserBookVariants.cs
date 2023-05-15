namespace ClubbyBook.Data.Models
{
    using System;
    using Pingvinius.Framework.Attributes;

    [Flags]
    public enum UserBookVariants : int
    {
        None = 0x0000,

        [UrlRewrite("paper-book")]
        PaperBook = 0x0001,

        [UrlRewrite("e-book")]
        EBook = 0x0010,

        [UrlRewrite("audio-book")]
        Audiobook = 0x0100,
    }
}