using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading;

namespace RadGardenKeepAlive
{
    class MainFlow
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        KeepAliveTest keepAliveTest = new KeepAliveTest();
        ServiceControl serviceControl = new ServiceControl();
        RestartCommandManager restartCommandManager = new RestartCommandManager();

        enum TestingLevel
        {
            TurnOffTests = 0,
            RunTestDoNothingFails = 1,
            StopStartApplication = 2,
            StopStartApplicationAndRestartWindows = 3,
            OnlyRestartWindows = 4
        }
        public MainFlow()
        {
            log.Info("::: INITIALIZING :::");
            ConfigValues.SetValues();

        }

        string LocalhostKeepAliveTest()
        {

            return keepAliveTest.getResponseCodeFromLocalhost(ConfigValues.hostName, ConfigValues.port, ConfigValues.fullPath);

        }

        string IpKeepAliveTest()
        {
            return keepAliveTest.getResponseCodeFromIp(ConfigValues.ipAddress, ConfigValues.port, ConfigValues.fullPath);

        }


        private TestingLevel SelectedLevel(int level)
        {
            TestingLevel testLevel;
            testLevel = (TestingLevel)level;
            return testLevel;

        }

        public void FlowLogic()
        {
            int localhostFailureCounter=0;
            int ipHostnameFailureCounter=0;

            TestingLevel localhostTestLevel = SelectedLevel(ConfigValues.localhostTestActionLevel);
            TestingLevel ipHostnameTestLevel = SelectedLevel(ConfigValues.ipOrHostNameActionLevel);

            log.Info("Initial boot idle time: Sleeping for" + ConfigValues.idleTimeOnstartupInMinutes+" min");
            Thread.Sleep(ConfigValues.idleTimeOnstartupInMinutes * 60000);
            while (true)
            {

                switch (localhostTestLevel)
                {
                    case TestingLevel.TurnOffTests:
                        log.Info("localhost tests were turned off - no test executed");
                        break;
                    case TestingLevel.RunTestDoNothingFails:
                        LocalhostKeepAliveTest();
                        break;
                    case TestingLevel.StopStartApplication:
                         if (!LocalhostKeepAliveTest().Equals("200"))
                        {
                            localhostFailureCounter++;
                            if (localhostFailureCounter == ConfigValues.failuresCounterBeforeRestaringService)
                            {
                                serviceControl.KillService(ConfigValues.serviceName);
                                serviceControl.StartService(ConfigValues.serviceName);

                                //time after restarting process
                                Thread.Sleep(ConfigValues.testIntervalInMintues*60000);
                            }
                        }
                        break;
                    case TestingLevel.StopStartApplicationAndRestartWindows:
                       if (!LocalhostKeepAliveTest().Equals("200"))
                        {
                            localhostFailureCounter++;
                            if (localhostFailureCounter==ConfigValues.failuresCounterBeforeRestaringService)
                            {
                                serviceControl.KillService(ConfigValues.serviceName);
                                serviceControl.StartService(ConfigValues.serviceName);

                                //time after restarting process
                                Thread.Sleep(ConfigValues.testIntervalInMintues * 60000);
                            }
                            if (localhostFailureCounter == ConfigValues.failuresCounterBeforeRestaringWindows)
                            {
                                restartCommandManager.Restart();
                            }
                        }
                        break;
                    case TestingLevel.OnlyRestartWindows:
                          if (!LocalhostKeepAliveTest().Equals("200"))
                        {
                            localhostFailureCounter++;
                            if (localhostFailureCounter == ConfigValues.failuresCounterBeforeRestaringWindows)
                            {
                                restartCommandManager.Restart();
                            }
                        }
                        break;
                }

                switch (ipHostnameTestLevel)
                {
                    case TestingLevel.TurnOffTests:
                        log.Info("Ip or hostname tests were turned off - no test executed");
                        break;
                    case TestingLevel.RunTestDoNothingFails:
                        IpKeepAliveTest();
                        break;
                    case TestingLevel.StopStartApplication:
                        if (!IpKeepAliveTest().Equals("200"))
                        {
                            ipHostnameFailureCounter++;
                            if (ipHostnameFailureCounter == ConfigValues.failuresCounterBeforeRestaringService)
                            {
                                serviceControl.KillService(ConfigValues.serviceName);
                                serviceControl.StartService(ConfigValues.serviceName);

                                //time after restarting process
                                Thread.Sleep(ConfigValues.testIntervalInMintues*60000);
                            }
                        }
                        break;
                    case TestingLevel.StopStartApplicationAndRestartWindows:
                        if (!IpKeepAliveTest().Equals("200"))
                        {
                            ipHostnameFailureCounter++;
                            if (ipHostnameFailureCounter == ConfigValues.failuresCounterBeforeRestaringService)
                            {
                                serviceControl.KillService(ConfigValues.serviceName);
                                serviceControl.StartService(ConfigValues.serviceName);

                                //time after restarting process
                                Thread.Sleep(ConfigValues.testIntervalInMintues * 60000);
                            }
                            if (ipHostnameFailureCounter == ConfigValues.failuresCounterBeforeRestaringWindows)
                            {
                                restartCommandManager.Restart();
                            }
                        }
                        break;
                    case TestingLevel.OnlyRestartWindows:
                        if (!IpKeepAliveTest().Equals("200"))
                        {
                            ipHostnameFailureCounter++;
                            if (ipHostnameFailureCounter == ConfigValues.failuresCounterBeforeRestaringWindows)
                            {
                                restartCommandManager.Restart();
                            }
                        }
                        break;
                }

                log.Debug("Sleeping for " + ConfigValues.testIntervalInMintues + " min between intervals");
                Thread.Sleep(ConfigValues.testIntervalInMintues*60000);
            }
        }
    }
}
