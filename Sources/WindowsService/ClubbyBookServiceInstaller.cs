namespace ClubbyBook.WindowsService
{
    using System.Collections;
    using System.ComponentModel;
    using System.Configuration.Install;
    using System.ServiceProcess;
    using NLog;

    [RunInstaller(true)]
    public sealed class ClubbyBookServiceInstaller : Installer
    {
        private Logger Logger { get; set; }

        public ClubbyBookServiceInstaller()
        {
            this.Logger = LogManager.GetLogger(this.GetType().Name);

            ServiceProcessInstaller serviceProcessInstaller = new ServiceProcessInstaller();
            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            serviceProcessInstaller.Username = null;
            serviceProcessInstaller.Password = null;

            ServiceInstaller serviceInstaller = new ServiceInstaller();
            serviceInstaller.ServiceName = "ClubbyBook";
            serviceInstaller.StartType = ServiceStartMode.Automatic;
            serviceInstaller.Description = "The ClubbyBook web site service.";
            serviceInstaller.DisplayName = "ClubbyBook Service";

            this.Installers.Add(serviceProcessInstaller);
            this.Installers.Add(serviceInstaller);
        }

        public override void Install(IDictionary stateSaver)
        {
            this.Logger.Info("The ClubbyBook service is preparing to be installed.");

            base.Install(stateSaver);

            this.Logger.Info("The ClubbyBook service has been successfully installed.");
        }

        public override void Uninstall(IDictionary savedState)
        {
            this.Logger.Info("The ClubbyBook service is preparing to be uninstalled.");

            base.Uninstall(savedState);

            this.Logger.Info("The ClubbyBook service has been successfully uninstalled.");
        }
    }
}
