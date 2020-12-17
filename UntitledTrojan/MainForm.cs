using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            switch (ticks)
            {
                case 10:
                    DisplayFuck.StartWarn();
                    break;
                case 20:
                    DisplayFuck.StartGlitch();
                    break;

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
        }
    }
}
