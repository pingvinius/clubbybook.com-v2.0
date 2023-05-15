namespace ClubbyBook.Web.UI.Areas.Default.Controllers
{
    using System;
    using System.Web.Mvc;
    using ClubbyBook.Business.Exceptions;
    using ClubbyBook.Business.Repositories.Interfaces;
    using ClubbyBook.Common;
    using ClubbyBook.Web.UI.Areas.Default.Controllers.Base;
    using ClubbyBook.Web.UI.Areas.Default.Models.Account;
    using ClubbyBook.Web.UI.Extensions;
    using Pingvinius.Framework.Attributes;
    using Pingvinius.Framework.Repositories;
    using Pingvinius.Framework.Security;

    public sealed class AccountController : DefaultController
    {
        #region Registration Routine

        [HttpGet]
        public ActionResult Register()
        {
            // The user is already authenticated
            if (AccessManager.IsAuthenticated)
            {
                return this.RedirectToReturnUrlOrHome();
            }

            return this.View(new RegistrationInfo());
        }

        [HttpPost]
        public ActionResult Register(RegistrationInfo registrationInfo)
        {
            // Validate input parameters
            if (registrationInfo == null)
            {
                throw new ArgumentNullException("registrationInfo");
            }

            // Check that user agreed with terms
            if (!registrationInfo.UserAgreement)
            {
                this.ModelState.AddModelError("UserIsDeleted", "Для того что бы зарегистрироватся вам необходимо согласится с условиями пользовательского соглашения.");
            }

            // Try to register
            if (this.ModelState.IsValid)
            {
                try
                {
                    var user = RepositoryFactory.Get<IUserRepository>().Register(registrationInfo.Email, registrationInfo.Password);
                    if (user == null)
                    {
                        throw new InvalidOperationException("The user is not registered.");
                    }

                    AccessManager.SignIn(registrationInfo.Email, registrationInfo.Password, false);

                    return this.Redirect(this.Url.GoTo(DefaultUrlTemplates.ViewReader, user.Profile.UrlRewrite));
                }
                catch (UserIsDeletedException)
                {
                    this.ModelState.AddModelError("UserIsDeleted", "Данный пользователь был удален, напишите в тех поддержду для восcтановления пользователя.");
                }
                catch (UserIsAlreadyRegisteredException)
                {
                    this.ModelState.AddModelError("UserIsAlreadyRegistered", "Данный пользователь уже зарегистрирован.");
                }
                catch
                {
                    this.ModelState.AddModelError("UnexpectedError", "Во время регистрации произошла ошибка, попробуйте снова.");
                }
            }

            return this.View(registrationInfo);
        }

        #endregion Registration Routine

        #region SignIn Routine

        [HttpGet]
        public ActionResult SignIn()
        {
            // The user is already authenticated
            if (AccessManager.IsAuthenticated)
            {
                return this.RedirectToReturnUrlOrHome();
            }

            return this.View(new SignInInfo());
        }

        [HttpPost]
        public ActionResult SignIn(SignInInfo signInInfo)
        {
            if (signInInfo == null)
            {
                throw new ArgumentNullException("signInInfo");
            }

            // Try to sign in
            if (this.ModelState.IsValid)
            {
                try
                {
                    if (!AccessManager.SignIn(signInInfo.Email, signInInfo.Password, signInInfo.RememberMe))
                    {
                        this.ModelState.AddModelError("InvalidSignInInfo", "Ваши почта и пароль не опознаны системой авторизации.");
                    }
                }
                catch (UserIsDeletedException)
                {
                    this.ModelState.AddModelError("UserIsDeleted", "Данный пользователь был удален, напишите в тех поддержду для восcтановления пользователя.");
                }
            }

            // Redirect if possible to ReturnUrl
            if (this.ModelState.IsValid)
            {
                return this.RedirectToReturnUrlOrHome();
            }

            return this.View(signInInfo);
        }

        #endregion SignIn Routine

        #region SignOut Routine

        [HttpGet]
        [AccessPermission(RoleNames.Account, RoleNames.Editor, RoleNames.Admin)]
        public RedirectResult SignOut()
        {
            AccessManager.SignOut();

            return this.Redirect(this.Url.GoTo(CommonUrlTemplates.DefaultSite));
        }

        #endregion SignOut Routine

        public ActionResult RedirectToReturnUrlOrHome()
        {
            string returnUrl = this.GetStringParameter("ReturnUrl");
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                return this.Redirect(this.Url.GoTo(CommonUrlTemplates.DefaultSite));
            }
            else
            {
                return this.Redirect(returnUrl);
            }
        }
    }
}