namespace ClubbyBook.Data.Models
{
    using System;

    [Flags]
    public enum UserBookStatuses : int
    {
        None = 0x0000,

        AlreadyRead = 0x0001,

        ReadingNow = 0x0010,

        WantToRead = 0x0100
    }
}