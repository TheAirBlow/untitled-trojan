using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uranus
{
    public static class CoolConsole
    {
        public static async Task WriteLine(string text)
        {
            Console.WriteLine(text);
            await Task.Delay(50);
        }

        public static async Task Loading(int count)
        {
            Console.WriteLine();
            for (int i = 0; i < count; i++)
            {
                Console.Write($"{Program.GetStr(6)}. |");
                Console.CursorLeft = 0;
                await Task.Delay(100);
                Console.Write($"{Program.GetStr(6)}. /");
                Console.CursorLeft = 0;
                await Task.Delay(100);
                Console.Write($"{Program.GetStr(6)}. -");
                Console.CursorLeft = 0;
                await Task.Delay(100);
                Console.Write($"{Program.GetStr(6)}. \\");
                Console.CursorLeft = 0;
                await Task.Delay(100);
            }
        }

        public static async Task ClearLines(int count)
        {
            int top = Console.CursorTop;
            for (int i = 0; i < count; i++) await WriteLine("                                     ");
            Console.CursorTop = top;
        }
    }
}
