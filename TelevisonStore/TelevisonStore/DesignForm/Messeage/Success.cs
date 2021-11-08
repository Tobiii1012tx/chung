using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelevisonStore.DesignForm.Messeage
{
    public partial class Success : Form
    {

        Timer timer = new Timer();
        public Success()
        {
            InitializeComponent();

            this.Opacity = .1;
            timer.Interval = 10;
            timer.Tick += ChangeOpacity;
            timer.Start();
        }

        void ChangeOpacity(object sender, EventArgs e)
        {
            this.Opacity += .10; //replace.10 with whatever you want
            if (this.Opacity == 1)
            {
                timer.Stop();
                //this.Dispose();
                timer = new Timer();
                timer.Interval = 10;
                timer.Tick += HideOpacity;
                timer.Start();
            }
        }

        void HideOpacity(object sender, EventArgs e)
        {
            this.Opacity -= .05;
            if(this.Opacity == 0)
            {
                timer.Stop();
                this.Dispose();
            }
        }



    }
}
