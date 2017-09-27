using System;
using System.Runtime.InteropServices;

namespace RadGardenKeepAlive
{
    class Program
    {
        #region Initilize and configure console window
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_MINIMIZE = 2;
        const int SW_SHOW = 5;
        #endregion
        static void Main(string[] args)
        {
            #region Start and minimize console window
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_MINIMIZE);
            #endregion

            MainFlow flow = new MainFlow();
            flow.FlowLogic();
        }
    }
}
