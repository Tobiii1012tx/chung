using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelevisonStore.DesignForm.Messeage
{
    public partial class YesNo : Form
    {
        public bool check = false;
        Timer timer = new Timer();
        public YesNo()
        {
            InitializeComponent();

            // set thời gian hiện dần lên
            this.Opacity = .1;
            timer.Interval = 10;
            timer.Tick += ChangeOpacity;
            timer.Start();
        }

        void ChangeOpacity(object sender, EventArgs e)
        {
            this.Opacity += .10; //replace.10 with whatever you want
            if (this.Opacity == 1)
                timer.Stop();
        }

        private void YesNo_Load(object sender, EventArgs e)
        {
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
       (
           int nLeftRect,     // x-coordinate of upper-left corner
           int nTopRect,      // y-coordinate of upper-left corner
           int nRightRect,    // x-coordinate of lower-right corner
           int nBottomRect,   // y-coordinate of lower-right corner
           int nWidthEllipse, // width of ellipse
           int nHeightEllipse // height of ellipse
       );

        private void btnNo_Click(object sender, EventArgs e)
        {
            check = false;
            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += HideOpacity;
            timer.Start();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            check = true;
            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += HideOpacity;
            timer.Start();
        }

        void HideOpacity(object sender, EventArgs e)
        {
            this.Opacity -= .1;
            if (this.Opacity == 0)
            {
                timer.Stop();
                this.Dispose();
            }
        }

        public bool getCheck()
        {
            return check;
        }
        
    }
}
