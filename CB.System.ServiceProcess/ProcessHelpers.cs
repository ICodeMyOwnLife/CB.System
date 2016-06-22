using System.Diagnostics;
using System.Threading.Tasks;


namespace CB.System.ServiceProcess
{
    public static class ProcessHelpers
    {
        #region Fields
        private const string RUN_AS_ADMINISTRATOR = "runas";
        #endregion


        #region Methods
        public static Process RunAsAdministrator(string fileName, string arguments)
        {
            var info = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                Verb = RUN_AS_ADMINISTRATOR
            };
            return Process.Start(info);
        }

        public static async Task RunAsAdministratorAsync(string fileName, string arguments)
            => await Task.Run(() => RunAsAdministrator(fileName, arguments).WaitForExit());

        public static async Task<bool> RunAsAdministratorAsync(string fileName, string arguments, int milliseconds)
            => await Task.Run(() => RunAsAdministrator(fileName, arguments).WaitForExit(milliseconds));

        public static void RunAsAdministratorSync(string fileName, string arguments)
            => RunAsAdministrator(fileName, arguments).WaitForExit();

        public static bool RunAsAdministratorSync(string fileName, string arguments, int milliseconds)
            => RunAsAdministrator(fileName, arguments).WaitForExit(milliseconds);
        #endregion
    }
}