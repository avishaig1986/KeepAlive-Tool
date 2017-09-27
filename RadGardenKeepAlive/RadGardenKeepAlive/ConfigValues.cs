using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;


namespace RadGardenKeepAlive
{
    public static class ConfigValues
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static string hostName;
        public static string ipAddress;
        public static int port;
        public static string fullPath;
        public static int testIntervalInMintues;
        public static int expectedHttpResponse;
        public static string serviceName;
        public static int failuresCounterBeforeRestaringService;
        public static int failuresCounterBeforeRestaringWindows;
        public static int localhostTestActionLevel;
        public static int ipOrHostNameActionLevel;
        public static int idleTimeOnstartupInMinutes;

        public static void SetValues()
        {

            try
            {

                hostName = ConfigurationManager.AppSettings["HostName"];
                log.Info("SETTING VALUE: Hostname set to " + hostName);

                ipAddress = ConfigurationManager.AppSettings["IP"];
                log.Info("SETTING VALUE: IpAddress set to " + ipAddress);

                port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                log.Info("SETTING VALUE: Port set to " + port);

                fullPath = ConfigurationManager.AppSettings["FullPath"];
                log.Info("SETTING VALUE: FullPath set to " + fullPath);

                testIntervalInMintues = Convert.ToInt32(ConfigurationManager.AppSettings["TestIntervalsInMinutes"]);
                log.Info("SETTING VALUE: Test Interval In Mintues set to " + testIntervalInMintues);

                expectedHttpResponse = Convert.ToInt32(ConfigurationManager.AppSettings["ExpectedHttpResponse"]);
                log.Info("SETTING VALUE: Expected Http Response set to " + expectedHttpResponse);

                serviceName = ConfigurationManager.AppSettings["ServiceName"];
                log.Info("SETTING VALUE: Process Name set to " + serviceName);

                failuresCounterBeforeRestaringService = Convert.ToInt32(ConfigurationManager.AppSettings["FailuresCounterBeforeRestaringService"]);
                log.Info("SETTING VALUE: Failures Counter Before Restaring Service set to " + failuresCounterBeforeRestaringService);

                failuresCounterBeforeRestaringWindows = Convert.ToInt32(ConfigurationManager.AppSettings["FailuresCounterBeforeRestaringWindows"]);
                log.Info("SETTING VALUE: Failures Counter Before Restaring Windows set to " + failuresCounterBeforeRestaringWindows);

                localhostTestActionLevel = Convert.ToInt32(ConfigurationManager.AppSettings["LocalhostTestActionLevel"]);
                log.Info("SETTING VALUE: Localhost Test Action Level set to " + localhostTestActionLevel);

                ipOrHostNameActionLevel = Convert.ToInt32(ConfigurationManager.AppSettings["IpOrHostNameActionLevel"]);
                log.Info("SETTING VALUE: Ip Or Hostname Action Level set to " + ipOrHostNameActionLevel);

                idleTimeOnstartupInMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["IdleTimeOnstartupInMinutes"]);
                log.Info("SETTING VALUE: Idle Time On startup In Minutes set to " + ipOrHostNameActionLevel);

                log.Info("All Configuration Loaded successfully");

            }
            catch (Exception ex)
            {
                log.Error("Could not load configuration! " + ex.Message);
                throw ex;
            }
        }
    }
}
