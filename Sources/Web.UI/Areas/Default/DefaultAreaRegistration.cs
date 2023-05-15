namespace ClubbyBook.Web.UI.Areas.Default
 {
     using System;
     using System.Web.Mvc;

     public class DefaultAreaRegistration : AreaRegistration
     {
         public override string AreaName
         {
             get
             {
                 return "Default";
             }
         }

         public override void RegisterArea(AreaRegistrationContext context)
         {
             if (context == null)
             {
                 throw new ArgumentNullException("context");
             }

             // Registration, SignIn, SignOut, Reset password
             context.MapRoute(
                 name: "Default_Registration",
                 url: "registration",
                 defaults: new { controller = "Account", action = "Register" },
                 namespaces: new[] { "ClubbyBook.Web.UI.Areas.Default.Controllers" }
             );

             context.MapRoute(
                 name: "Default_SignIn",
                 url: "login",
                 defaults: new { controller = "Account", action = "SignIn" },
                 namespaces: new[] { "ClubbyBook.Web.UI.Areas.Default.Controllers" }
             );

             context.MapRoute(
                 name: "Default_SignOut",
                 url: "logout",
                 defaults: new { controller = "Account", action = "SignOut" },
                 namespaces: new[] { "ClubbyBook.Web.UI.Areas.Default.Controllers" }
             );

             context.MapRoute(
                 name: "Default_ResetPassword",
                 url: "resetpassword",
                 defaults: new { controller = "Account", action = "ResetPassword" },
                 namespaces: new[] { "ClubbyBook.Web.UI.Areas.Default.Controllers" }
             );

             // Books
             context.MapRoute(
                 name: "Default_Books",
                 url: "books",
                 defaults: new { controller = "Book", action = "Index" },
                 namespaces: new[] { "ClubbyBook.Web.UI.Areas.Default.Controllers" }
             );

             context.MapRoute(
                 name: "Default_ViewBook",
                 url: "viewbook/{urlRewrite}",
                 defaults: new { controller = "Book", action = "Details" },
                 namespaces: new[] { "ClubbyBook.Web.UI.Areas.Default.Controllers" }
             );

             // Authors
             context.MapRoute(
                 name: "Default_Authors",
                 url: "authors",
                 defaults: new { controller = "Author", action = "Index" },
                 namespaces: new[] { "ClubbyBook.Web.UI.Areas.Default.Controllers" }
             );

             context.MapRoute(
                 name: "Default_ViewAuthor",
                 url: "viewauthor/{urlRewrite}",
                 defaults: new { controller = "Author", action = "Details" },
                 namespaces: new[] { "ClubbyBook.Web.UI.Areas.Default.Controllers" }
             );

             // News
             context.MapRoute(
                 name: "Default_News",
                 url: "news",
                 defaults: new { controller = "News", action = "Index" },
                 namespaces: new[] { "ClubbyBook.Web.UI.Areas.Default.Controllers" }
             );

             context.MapRoute(
                 name: "Default_ViewNews",
                 url: "viewnews/{urlRewrite}",
                 defaults: new { controller = "News", action = "Details" },
                 namespaces: new[] { "ClubbyBook.Web.UI.Areas.Default.Controllers" }
             );

             // Readers
             context.MapRoute(
                 name: "Default_Readers",
                 url: "readers",
                 defaults: new { controller = "Reader", action = "Index" },
                 namespaces: new[] { "ClubbyBook.Web.UI.Areas.Default.Controllers" }
             );

             context.MapRoute(
                 name: "Default_ViewReaders",
                 url: "view-reader/{urlRewrite}",
                 defaults: new { controller = "Reader", action = "Details" },
                 namespaces: new[] { "ClubbyBook.Web.UI.Areas.Default.Controllers" }
             );

             context.MapRoute(
                 name: "Default_EditReaders",
                 url: "edit-reader/{urlRewrite}",
                 defaults: new { controller = "Reader", action = "Edit" },
                 namespaces: new[] { "ClubbyBook.Web.UI.Areas.Default.Controllers" }
             );

             context.MapRoute(
                 name: "Default_EditReaderAccounts",
                 url: "edit-reader-account/{urlRewrite}",
                 defaults: new { controller = "Reader", action = "EditAccount" },
                 namespaces: new[] { "ClubbyBook.Web.UI.Areas.Default.Controllers" }
             );

             // Messages
             context.MapRoute(
                 name: "Default_Messages",
                 url: "messages",
                 defaults: new { controller = "Conversation", action = "Index" },
                 namespaces: new[] { "ClubbyBook.Web.UI.Areas.Default.Controllers" }
             );

             // Static pages
             context.MapRoute(
                 name: "Default_UserAgreement",
                 url: "useragreement",
                 defaults: new { controller = "StaticPages", action = "UserAgreement" },
                 namespaces: new[] { "ClubbyBook.Web.UI.Areas.Default.Controllers" }
             );

             context.MapRoute(
                 name: "Default_About",
                 url: "about",
                 defaults: new { controller = "StaticPages", action = "About" },
                 namespaces: new[] { "ClubbyBook.Web.UI.Areas.Default.Controllers" }
             );

             context.MapRoute(
                 name: "Default_Contacts",
                 url: "contacts",
                 defaults: new { controller = "StaticPages", action = "Contacts" },
                 namespaces: new[] { "ClubbyBook.Web.UI.Areas.Default.Controllers" }
             );

             context.MapRoute(
                 name: "Default_SiteMap",
                 url: "sitemap",
                 defaults: new { controller = "StaticPages", action = "SiteMap" },
                 namespaces: new[] { "ClubbyBook.Web.UI.Areas.Default.Controllers" }
             );

             // Home page
             context.MapRoute(
                 name: "Default_Home",
                 url: "",
                 defaults: new { controller = "Home", action = "Index" },
                 namespaces: new[] { "ClubbyBook.Web.UI.Areas.Default.Controllers" }
             );
         }
     }
 }