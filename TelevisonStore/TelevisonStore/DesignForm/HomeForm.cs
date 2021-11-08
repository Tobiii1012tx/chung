using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelevisonStore.DesignForm
{
    public partial class HomeForm : UserControl
    {
        public HomeForm()
        {
            InitializeComponent();
        }


        public void showHideFrom(UserControl form)
        {
            if (!this.Controls.Contains(form))
            {
                form.Width = this.Width;
                form.Height = this.Height;
                this.Controls.Remove(this);// xóa form cũ đi
                this.Controls.Add(form);
                form.Dock = DockStyle.Fill;
                form.BringToFront();
            }
            else
                form.BringToFront();
        }

        private void panelViewProduct_Click(object sender, EventArgs e)
        {
            showHideFrom(new FormReportByCustomer());

        }

        private void panelViewSomething_Click(object sender, EventArgs e)
        {
            showHideFrom(new FormReportBySupplier());

        }

        private void panelViewEmployee_Click(object sender, EventArgs e)
        {
            showHideFrom(new FormReportSupplierByMonth());
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            showHideFrom(new FormReportTopSupplierbyMonth());
        }

    }
}
