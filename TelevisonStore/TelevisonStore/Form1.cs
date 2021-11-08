using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TelevisonStore.DesignForm;

namespace TelevisonStore
{
    public partial class AppTelevision : Form
    {
        public AppTelevision()
        {
            InitializeComponent();
            showHideFrom(new HomeForm());
            
        }
        // common function
        #region Funtion Show/Hide Menu && Form

        public void showHideSubMenu(Panel panel)
        {
            if (panel.Visible == true)
            {
                panel.Visible = false;
            }
            else
            {
                panelViewBill.Visible = false;
                panelViewConfig.Visible = false;
                panelViewHuman.Visible = false;
                panel.Visible = true;
            }
        }

        public void showHideFrom(UserControl form)
        {
            if (!panelMain.Controls.Contains(form))
            {
                form.Width = panelMain.Width;
                form.Height = panelMain.Height;
                panelMain.Controls.Remove(this);// xóa form cũ đi
                panelMain.Controls.Add(form);
                form.Dock = DockStyle.Fill;
                form.BringToFront();
            }
            else
                form.BringToFront();
        }
        #endregion

        //open Sub Menu
        #region showSubMenu
        // Show/Hide Sub Menu
        private void btnConfig_Click(object sender, EventArgs e)
        {
            showHideSubMenu(panelViewConfig);
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            showHideSubMenu(panelViewBill);
        }

        private void btnHuman_Click(object sender, EventArgs e)
        {
            showHideSubMenu(panelViewHuman);
        }


        #endregion

        //open new form
        #region openNewForm

        private void btnHome_Click(object sender, EventArgs e)
        {
            showHideFrom(new HomeForm());
        }
        private void btnBrand_Click(object sender, EventArgs e)
        {
            showHideFrom(new BrandForm());
        }
        private void btnSupplier_Click(object sender, EventArgs e)
        {
            showHideFrom(new SupplierForm());
        }

        private void btnDesign_Click(object sender, EventArgs e)
        {
            showHideFrom(new DesignsForm());
        }

        private void btnScreen_Click(object sender, EventArgs e)
        {
            showHideFrom(new ScreenForm());
        }

        private void btnSize_Click(object sender, EventArgs e)
        {
            showHideFrom(new SizeForm());
        }

        #endregion

        private void btnColor_Click(object sender, EventArgs e)
        {
            showHideFrom(new ColorForm());
        }

        private void btnCountry_Click(object sender, EventArgs e)
        {
            showHideFrom(new CountryForm());
        }

        private void btnShift_Click(object sender, EventArgs e)
        {
            showHideFrom(new ShiftForm());
        }

        private void btnWork_Click(object sender, EventArgs e)
        {
            showHideFrom(new WorkForm());
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            showHideFrom(new ProductForm());
        }

        private void btnBillExport_Click(object sender, EventArgs e)
        {
            showHideFrom(new BillExportForm());
        }

        private void btnBillImport_Click(object sender, EventArgs e)
        {
            showHideFrom(new BillImportForm());
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            showHideFrom(new CustomerForm());
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            showHideFrom(new EmployeeForm());
        }
    }
}
