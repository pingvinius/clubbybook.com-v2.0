namespace ClubbyBook.WindowsService
{
    using System;
    using System.Configuration.Install;
    using System.Reflection;
    using System.ServiceProcess;
    using NLog;

    public sealed class ClubbyBookService : ServiceBase
    {
        private Logger Logger { get; set; }

        public ClubbyBookService()
        {
            this.CanHandlePowerEvent = false;
            this.CanHandleSessionChangeEvent = false;
            this.CanPauseAndContinue = false;

            this.ServiceName = "ClubbyBook";
            this.AutoLog = true;
            this.CanStop = true;
            this.CanShutdown = true;

            this.Logger = LogManager.GetLogger(this.GetType().Name);
        }

        protected override void OnStart(string[] args)
        {
            this.Logger.Info("The ClubbyBook service is preparing to be started.");

            base.OnStart(args);

            this.Logger.Info("The ClubbyBook service has been successfully started.");
        }

        protected override void OnStop()
        {
            this.Logger.Info("The ClubbyBook service is preparing to be stopped.");

            base.OnStop();

            this.Logger.Info("The ClubbyBook service has been successfully stopped.");
        }

        protected override void OnShutdown()
        {
            base.OnShutdown();

            this.Logger.Info("The ClubbyBook service has been handled shutdown event.");
        }

        #region Main Method Routine

        public static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                try
                {
                    if (args != null && args.Length == 1)
                    {
                        if (args[0].Equals("/run", StringComparison.OrdinalIgnoreCase))
                        {
                            using (var service = new ClubbyBookService())
                            {
                                service.OnStart(null);

                                Console.WriteLine("Press any key to stop ClubbyBook service.");
                                Console.ReadKey();

                                service.OnStop();
                            }
                            return;
                        }
                        else if (args[0].Equals("/install", StringComparison.OrdinalIgnoreCase))
                        {
                            ClubbyBookService.InstallService();
                            return;
                        }
                        else if (args[0].Equals("/uninstall", StringComparison.OrdinalIgnoreCase))
                        {
                            ClubbyBookService.UninstallService();
                            return;
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Wrong command line:");
                    Console.WriteLine("    /install - Install the windows service.");
                    Console.WriteLine("    /uninstall - Uninstall the windows service.");
                    Console.WriteLine("    /run - Run the windows service as a console application.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("An erro has occurred while performing requested operation. Details: ");
                    Console.WriteLine(ex.ToString());
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                finally
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine();
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                }
            }
            else
            {
                using (var service = new ClubbyBookService())
                {
                    ServiceBase.Run(service);
                }
            }
        }

        #endregion

        #region Run/Install/Uninstall Routine

        private static void InstallService()
        {
            ManagedInstallerClass.InstallHelper(new string[] { Assembly.GetExecutingAssembly().Location });
        }

        private static void UninstallService()
        {
            ManagedInstallerClass.InstallHelper(new string[] { "/u", Assembly.GetExecutingAssembly().Location });
        }

        #endregion
    }
}
