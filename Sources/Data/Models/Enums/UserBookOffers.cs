namespace ClubbyBook.Data.Models
{
    using System;
    using Pingvinius.Framework.Attributes;

    [Flags]
    public enum UserBookOffers : int
    {
        [UrlRewrite("any-offer")]
        None = 0x0000,

        [UrlRewrite("sell")]
        Sell = 0x00000001,

        [UrlRewrite("buy")]
        Buy = 0x00000010,

        [UrlRewrite("barter")]
        Barter = 0x00000100,

        [UrlRewrite("will-give-read")]
        WillGiveRead = 0x00001000,

        [UrlRewrite("will-grant-gratis")]
        WillGrantGratis = 0x00010000
    }
}