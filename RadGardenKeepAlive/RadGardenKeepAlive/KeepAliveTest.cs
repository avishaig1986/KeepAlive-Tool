using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace RadGardenKeepAlive
{
    class KeepAliveTest
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public string getResponseCodeFromLocalhost(string hostname, int port, string path)
        {
            String address = string.Format("http://{0}" + ":" + "{1}/{2}", hostname, port, path);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(address);
            try
            {
                webRequest.AllowAutoRedirect = false;
                log.Debug("Attempting to get response from " + address);
                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                response.Close();
                log.Info("Returned " + (int)response.StatusCode + " " + response.StatusCode + " from " + address);
                return string.Format("{0}", (int)response.StatusCode);
            }
            catch (WebException ex)
            {
                log.Error("Failed to send request to " + address + " Reason: " + ex.Message);
                return ex.Message;

            }
        }

            public string getResponseCodeFromIp(string hostname, int port, string path)
        {
            String address = string.Format("http://{0}" + ":" + "{1}/{2}", hostname, port, path);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(address);
            try
            {
                webRequest.AllowAutoRedirect = false;
                log.Debug("Attempting to get response from " + address);
                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                response.Close();
                log.Info("Returned "+ (int)response.StatusCode + " " + response.StatusCode +" from "+  address);
                return string.Format("{0}", (int)response.StatusCode);
            }
            catch (WebException ex)
            {
                log.Error("Failed to send request to " + address + " Reason: " + ex.Message);
                return ex.Message;

            }
        }
    }
}
