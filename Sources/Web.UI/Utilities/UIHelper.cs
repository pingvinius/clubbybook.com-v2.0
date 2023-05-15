namespace ClubbyBook.Web.UI.Utilities
{
    using System;
    using System.IO;
    using ClubbyBook.Common;
    using ClubbyBook.Data.Models;
    using ClubbyBook.Web.UI.Models;

    public static class UIHelper
    {
        #region Constants

        public const string NotSpecifiedString = "Не указано";
        public const string SelectString = "Выбрать";
        public const string NotSpecifiedMaleProfileFullNameString = "Незнакомец";
        public const string NotSpecifiedFemaleProfileFullNameString = "Незнакомка";
        public const string AnonymousProfileFullNameString = "Анонимус";

        public const int BookDescriptionMaxLength = 230;
        public const int AuthorDescriptionMaxLength = 230;
        public const int NewsDescriptionMaxLength = 390;

        #endregion Constants

        #region DateTime Routine

        public static string GetFullDateString(DateTime dateTime)
        {
            return dateTime.ToString("f");
        }

        public static string GetShortDateString(DateTime dateTime)
        {
            return dateTime.ToString("g");
        }

        #endregion DateTime Routine

        #region SEO Routine

        public static string GetProfileDescriptionForSEO(Profile profile)
        {
            if (profile == null)
            {
                return "Фото нового пользователя";
            }

            return string.Format("Фото пользователя {0}", UIHelper.GetProfileFullName(profile, false));
        }

        public static string GetBookDescriptionForSEO(Book book)
        {
            if (book == null)
            {
                return "Обложка новой книги";
            }

            return string.Format("Купить {0} | Продать {0} | Обменять {0}", book.Title);
        }

        public static string GetAuthorDescriptionForSEO(Author author)
        {
            if (author == null)
            {
                return "Фото нового автора";
            }

            return string.Format("Купить книгу автора {0} | Продать книгу автора {0} | Обменять книгу автора {0}", author.FullName);
        }

        #endregion SEO Routine

        #region Images Routine

        public static ImageViewModel BuildImageViewModel(Profile profile, bool useBig)
        {
            // The variable "profile" could be null in case of creating new entity

            return new ImageViewModel(UIHelper.GetValidProfileImagePath(profile, false), useBig, string.Empty, UIHelper.GetProfileDescriptionForSEO(profile),
                UIHelper.GetProfileDescriptionForSEO(profile), false);
        }

        public static ImageViewModel BuildImageViewModel(Book book, bool useBig)
        {
            // The variable "book" could be null in case of creating new entity

            return new ImageViewModel(UIHelper.GetValidBookImagePath(book), useBig, string.Empty, UIHelper.GetBookDescriptionForSEO(book),
                UIHelper.GetBookDescriptionForSEO(book), true);
        }

        public static ImageViewModel BuildImageViewModel(Author author, bool useBig)
        {
            // The variable "author" could be null in case of creating new entity

            return new ImageViewModel(UIHelper.GetValidAuthorImagePath(author), useBig, string.Empty, UIHelper.GetAuthorDescriptionForSEO(author),
                UIHelper.GetAuthorDescriptionForSEO(author), true);
        }

        private static string GetValidProfileImagePath(Profile profile, bool useAnonymousAsEmpty = false)
        {
            string fileName = profile != null ? profile.ImagePath : string.Empty;

            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = useAnonymousAsEmpty ? Settings.Images.AnonymousProfileFileName : Settings.Images.EmptyProfileFileName;
            }

            return Path.Combine(Settings.Images.ProfilePath, fileName);
        }

        private static string GetValidBookImagePath(Book book)
        {
            string fileName = book != null ? book.CoverPath : string.Empty;

            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = Settings.Images.EmptyBookFileName;
            }

            return Path.Combine(Settings.Images.BookPath, fileName);
        }

        private static string GetValidAuthorImagePath(Author author)
        {
            string fileName = author != null ? author.PhotoPath : string.Empty;

            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = Settings.Images.EmptyAuthorFileName;
            }

            return Path.Combine(Settings.Images.AuthorPath, fileName);
        }

        #endregion Images Routine

        #region User/Profile Fullname Routine

        public static string GetProfileFullName(Profile profile, bool buildAnyPossible = false)
        {
            if (profile == null)
            {
                throw new ArgumentNullException("profile");
            }

            string fullName = string.Empty;

            if (buildAnyPossible)
            {
                if (!string.IsNullOrWhiteSpace(profile.Name) || !string.IsNullOrWhiteSpace(profile.Surname))
                {
                    fullName = string.Format("{0} {1}", profile.Name, profile.Surname);
                    fullName = fullName.Trim();
                }

                if (!string.IsNullOrWhiteSpace(profile.Nickname))
                {
                    if (string.IsNullOrWhiteSpace(fullName))
                    {
                        fullName = profile.Nickname;
                    }
                    else
                    {
                        fullName += string.Format(" ({0})", profile.Nickname);
                    }
                }

                if (string.IsNullOrWhiteSpace(fullName))
                {
                    fullName = profile.User.EMail;
                }

                fullName = fullName.Trim();
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(profile.Name) && !string.IsNullOrWhiteSpace(profile.Surname))
                {
                    fullName = string.Format("{0} {1}", profile.Name, profile.Surname);
                }
                else if (string.IsNullOrWhiteSpace(profile.Name) && string.IsNullOrWhiteSpace(profile.Surname) && string.IsNullOrWhiteSpace(profile.Nickname))
                {
                    fullName = profile.Gender == Gender.Female ? UIHelper.NotSpecifiedFemaleProfileFullNameString : UIHelper.NotSpecifiedMaleProfileFullNameString;
                }
                else if (string.IsNullOrWhiteSpace(profile.Name) && string.IsNullOrWhiteSpace(profile.Surname) && !string.IsNullOrWhiteSpace(profile.Nickname))
                {
                    fullName = profile.Nickname;
                }
                else if (!string.IsNullOrWhiteSpace(profile.Name))
                {
                    fullName = profile.Name;
                }
                else if (!string.IsNullOrWhiteSpace(profile.Surname))
                {
                    fullName = profile.Surname;
                }
            }

            return fullName;
        }

        public static string GetUserFullName(User user, bool buildAnyPossible = false)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return UIHelper.GetProfileFullName(user.Profile, buildAnyPossible);
        }

        #endregion User/Profile Fullname Routine

        #region City Routine

        public static string GetCityName(City city, bool returnFullName = true)
        {
            if (city == null)
            {
                return UIHelper.NotSpecifiedString;
            }

            if (returnFullName)
            {
                if (city.District != null)
                {
                    return string.Format("{0}, {1}, {2}", city.Name, city.District.Name, city.Country.Name);
                }

                return string.Format("{0}, {1}", city.Name, city.Country.Name);
            }

            return city.Name;
        }

        #endregion City Routine

        #region Restrictions Routine

        public static string GetRestrictedString(string fullString, int restrictCount)
        {
            if (string.IsNullOrWhiteSpace(fullString))
            {
                return string.Empty;
            }

            if (restrictCount >= fullString.Length)
            {
                return fullString;
            }

            return string.Format("{0} ...", fullString.Substring(0, restrictCount));
        }

        #endregion Restrictions Routine

        #region Total Item Count String Routine

        public static string GetTotalItemCountString(int totalCount)
        {
            if (totalCount <= 0)
            {
                return string.Empty;
            }

            var totalCountToCheck = totalCount % 100;

            if (totalCountToCheck >= 5 && totalCountToCheck <= 20)
            {
                return string.Format("В результате поиска найдено {0} елементов.", totalCount);
            }
            else
            {
                totalCountToCheck = totalCountToCheck % 10;

                if (totalCountToCheck == 1)
                {
                    return string.Format("В результате поиска найден {0} елемент.", totalCount);
                }
                else if (totalCountToCheck >= 2 && totalCountToCheck <= 4)
                {
                    return string.Format("В результате поиска найдено {0} елемента.", totalCount);
                }
                else
                {
                    return string.Format("В результате поиска найдено {0} елементов.", totalCount);
                }
            }
        }

        #endregion Total Item Count String Routine

        #region Author Life Years Routine

        public static string GetAuthorLifeYearsString(Author author)
        {
            const string maleBirthText = "родился в {0} г.";
            const string femaleBirthText = "родилась в {0} г.";

            const string maleDeathText = "умер в {0} г.";
            const string femaleDeathText = "умерла в {0} г.";

            if (author != null)
            {
                if (author.BirthdayYear.HasValue && author.DeathYear.HasValue)
                {
                    return string.Format("{0} - {1} гг.", author.BirthdayYear.Value, author.DeathYear.Value);
                }
                else if (author.BirthdayYear.HasValue && !author.DeathYear.HasValue)
                {
                    return string.Format(author.Gender != Gender.Female ? maleBirthText : femaleBirthText, author.BirthdayYear.Value);
                }
                else if (!author.BirthdayYear.HasValue && author.DeathYear.HasValue)
                {
                    return string.Format(author.Gender != Gender.Female ? maleDeathText : femaleDeathText, author.DeathYear.Value);
                }
            }

            return UIHelper.NotSpecifiedString;
        }

        #endregion Author Life Years Routine
    }
}