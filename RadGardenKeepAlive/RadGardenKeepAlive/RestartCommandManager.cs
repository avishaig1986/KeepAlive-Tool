using System;
using System.Diagnostics;

namespace RadGardenKeepAlive
{
    class RestartCommandManager
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Restart()
        {
            log.Info("RESTARING WINDOWS NOW!");
            System.Diagnostics.Process.Start("shutdown.exe", "-r -t 0");
        }
    }
}
