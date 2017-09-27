using System;
using System.ComponentModel;
using System.ServiceProcess;
namespace RadGardenKeepAlive
{
    class ServiceControl
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void KillService(string processName)
        {
            log.Info("Attempting to kill service " + ConfigValues.serviceName);
            try
            {
                ServiceController service = new ServiceController(ConfigValues.serviceName);
                service.Stop();

                log.Info("The Process " + ConfigValues.serviceName + " started successfully");
            }
            catch (Exception ex)
            {
                log.Error("Killing process "+ConfigValues.serviceName+"  failed! Reason:" + ex.Message);
                throw ex;
            }
        }

        public void StartService(string processName)

        {
            log.Info("Attempting to start proccess " + ConfigValues.serviceName);
            try
            {
                ServiceController service = new ServiceController(ConfigValues.serviceName);
                service.Start();

                log.Info("The Process " + ConfigValues.serviceName + " started successfully");
            }
            catch (Exception ex)
            {
                log.Error("Starting process " + ConfigValues.serviceName + " failed! Reason:" + ex.Message);
                throw ex;
            }

        }

    }
}
