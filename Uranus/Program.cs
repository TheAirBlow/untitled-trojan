using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Uranus
{
    public class Program
    {
        public static int IntVer = 1;
        public static bool isRussian = false;
        public static string Files = "";

        public static string Fetch(string path)
        {
            using (WebClient client = new WebClient())
                return client.DownloadString(Files + path);
        }

        public static string GetStr(int i) => isRussian ? Localization.russian[i] : Localization.english[i];

        public static async Task Main(string[] args)
        {
            Console.Title = $"Project Uranus | Modification {IntVer}";
            await CoolConsole.WriteLine("Project Uranus - TheAirBlow");
            await CoolConsole.Loading(5);
            Console.Clear();

            await CoolConsole.WriteLine("=================================");
            await CoolConsole.WriteLine("Project Uranus - TheAirBlow");
            await CoolConsole.WriteLine(GetStr(0));
            await CoolConsole.WriteLine("=================================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            await CoolConsole.WriteLine("Напиши '1'  - Русский язык");
            await CoolConsole.WriteLine("Press Enter - English language");
            Console.ForegroundColor = ConsoleColor.Gray;
            await CoolConsole.WriteLine("=================================");
            char option1 = Console.ReadKey().KeyChar;
            Console.CursorLeft = 0;
            Console.CursorTop -= 4;
            await CoolConsole.ClearLines(6);
            if (option1 == '1') isRussian = true;

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
            Console.CursorTop = 4;
            await CoolConsole.ClearLines(6);
            Console.ForegroundColor = ConsoleColor.Cyan;
            await CoolConsole.WriteLine("Welcome! Select an option:");
            await CoolConsole.WriteLine("1) Destructive version");
            await CoolConsole.WriteLine("2) Safe version");
            await CoolConsole.WriteLine("3) Exit");
            Console.ForegroundColor = ConsoleColor.Gray;
            await CoolConsole.WriteLine("=================================");
            char option = Console.ReadKey().KeyChar;
            Console.CursorLeft = 0;
            switch (option)
            {
                case '1':
                    Console.CursorTop -= 5;
                    await CoolConsole.ClearLines(6);
                    Console.ForegroundColor = ConsoleColor.Red;
                    await CoolConsole.WriteLine("Are you sure about that?");
                    await CoolConsole.WriteLine("This will erase MBR");
                    await CoolConsole.WriteLine("And destroy the OS.");
                    await CoolConsole.WriteLine("Type 'destroy' if you agree.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    await CoolConsole.WriteLine("=================================");
                    string input = Console.ReadLine().ToLower();
                    if (input != "destroy")
                    {
                        await Menu();
                        return;
                    }
                    // Main payload
                    Console.CursorTop = 4;
                    await CoolConsole.ClearLines(6);
                    await CoolConsole.Loading(5);
                    return;
                case '2':
                    Console.CursorTop -= 5;
                    await CoolConsole.ClearLines(7);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    await CoolConsole.WriteLine("Are you sure about that?");
                    await CoolConsole.WriteLine("This payload is safe, but");
                    await CoolConsole.WriteLine("it is still harmful for your");
                    await CoolConsole.WriteLine("eyes and your OS.");
                    await CoolConsole.WriteLine("Type 'harm' if you agree.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    await CoolConsole.WriteLine("=================================");
                    string input1 = Console.ReadLine().ToLower();
                    if (input1 != "harm")
                    {
                        await Menu();
                        return;
                    }
                    // Safe payload
                    Console.CursorTop = 4;
                    await CoolConsole.ClearLines(6);
                    await CoolConsole.Loading(5);
                    return;
                case '3':
                    Console.CursorTop -= 5;
                    await CoolConsole.ClearLines(6);
                    await CoolConsole.WriteLine("Goodbye! See you next time.");
                    await CoolConsole.WriteLine("=================================");
                    await Task.Delay(1000);
                    return;
                default:
                    Console.CursorTop -= 5;
                    await CoolConsole.ClearLines(5);
                    Console.ForegroundColor = ConsoleColor.Red;
                    await CoolConsole.WriteLine("Unknown option!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    await CoolConsole.WriteLine("=================================");
                    await Task.Delay(1000
                        );
                    await Menu();
                    return;
            }
        }
    }
}
