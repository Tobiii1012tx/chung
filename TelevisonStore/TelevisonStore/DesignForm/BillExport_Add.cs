using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TelevisonStore.Common;
using TelevisonStore.DesignForm.Messeage;

namespace TelevisonStore.DesignForm
{
    public partial class BillExport_Add : UserControl
    {
        GeneralInForm generalInform;
        ConnectDatabase con;
        string id;
        public BillExport_Add()
        {
            InitializeComponent();
            con = new ConnectDatabase();
            generalInform = new GeneralInForm();
            comboBoxSupplier.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Customer"), "Chọn khách hàng").DataSource;
            comboBoxSupplier.ValueMember = "Id";
            comboBoxSupplier.DisplayMember = "Name";

            comboBoxEmployee.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Employee"), "Nhân viên nhập").DataSource;
            comboBoxEmployee.ValueMember = "Id";
            comboBoxEmployee.DisplayMember = "Name";

            comboBoxProduct.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Product"), "Chọn nhà sản phẩm").DataSource;
            comboBoxProduct.ValueMember = "Id";
            comboBoxProduct.DisplayMember = "Name";
        }

        public BillExport_Add(string id)
        {
            this.id = id;
            InitializeComponent();
            con = new ConnectDatabase();
            generalInform = new GeneralInForm();
            comboBoxSupplier.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Customer"), "Chọn nhà khách hàng").DataSource;
            comboBoxSupplier.ValueMember = "Id";
            comboBoxSupplier.DisplayMember = "Name";

            comboBoxEmployee.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Employee"), "Nhân viên nhập").DataSource;
            comboBoxEmployee.ValueMember = "Id";
            comboBoxEmployee.DisplayMember = "Name";

            comboBoxProduct.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Product"), "Chọn nhà sản phẩm").DataSource;
            comboBoxProduct.ValueMember = "Id";
            comboBoxProduct.DisplayMember = "Name";
            textBox1.Text = id;
            DataTable dataTable = con.getData("select product_id, Stock,Sale,Total from Bill_Export_detail where Be_Id = " + id);
            int pos = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                dataGridViewDetail.Rows.Add();
                dataGridViewDetail.Rows[pos].Cells[0].Value = row["product_id"].ToString();
                dataGridViewDetail.Rows[pos].Cells[1].Value = row["Stock"].ToString();
                dataGridViewDetail.Rows[pos].Cells[2].Value = row["Sale"].ToString();
                dataGridViewDetail.Rows[pos].Cells[3].Value = row["Total"].ToString();
                pos++;
            }

            DataTable dt = con.getData("select * from Bill_Export where id = " + id);
            foreach (DataRow row in dt.Rows)
            {
                comboBoxEmployee.SelectedValue = row["Employee_Id"].ToString();
                comboBoxSupplier.SelectedValue = row["Customer_id"].ToString();
                textBoxTax.Text = row["Tax"].ToString();
            }

            btnViewFormAdd.Text = "Cập nhật";
        }

        private void textBoxCount_KeyPress(object sender, KeyPressEventArgs e)
        {
             if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxUnitPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxSale_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void textBoxTax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxCount_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxProduct.SelectedIndex >0 && textBoxCount.Text.Length != 0 && textBoxSale.Text.Length != 0 && textBoxCount.ForeColor == Color.Black && textBoxSale.ForeColor == Color.Black)
            {
                var pro = comboBoxProduct.SelectedValue.ToString();
                double price = (double)(con.GetPrice("select top 1 price from Product where id =  " + pro));
                double count = double.Parse(textBoxCount.Text.ToString());
                double sale = double.Parse(textBoxSale.Text.ToString());
                double total = (price * (100 - sale) / 100) * count;
                textBoxTotal.Text = total.ToString();
            }
        }


        private void textBoxSale_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxProduct.SelectedIndex > 0 && textBoxCount.Text.Length != 0  && textBoxSale.Text.Length != 0 && textBoxCount.ForeColor == Color.Black && textBoxSale.ForeColor == Color.Black)
            { 
                var pro = comboBoxProduct.SelectedValue.ToString();
                double price = (double)(con.GetPrice("select top 1 price from Product where id =  " + pro));
                double count = double.Parse(textBoxCount.Text.ToString());
                double sale  = double.Parse(textBoxSale.Text.ToString());
                double total = (price*(100-sale)/100) * count;
                textBoxTotal.Text = total.ToString();
            }
        }

        private void comboBoxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxProduct.SelectedIndex > 0 && textBoxCount.Text.Length != 0 && textBoxSale.Text.Length != 0 && textBoxCount.ForeColor == Color.Black && textBoxSale.ForeColor == Color.Black)
            {
                var pro = comboBoxProduct.SelectedValue.ToString();
                double price = (double)(con.GetPrice("select top 1 price from Product where id =  " + pro));
                double count = double.Parse(textBoxCount.Text.ToString());
                double sale = double.Parse(textBoxSale.Text.ToString());
                double total = (price * (100 - sale) / 100) * count;
                textBoxTotal.Text = total.ToString();
            }
            comboBoxProduct.ForeColor = Color.Black;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (generalInform.checkTextBoxInForm(panel1))
            {
                string product = comboBoxProduct.SelectedValue.ToString();
                string count = textBoxCount.Text.ToString();
                string sale = textBoxSale.Text.ToString();
                string total = textBoxTotal.Text.ToString();
                int row = dataGridViewDetail.Rows.Count;
                for (int i = 0; i < row; i++)
                {
                    if (dataGridViewDetail.Rows[i].Cells[0].Value != null && dataGridViewDetail.Rows[i].Cells[0].Value.ToString() == product)
                    {
                        MessageBox.Show("Bạn đã chọn mặt hàng này rồi", "Thông báo");
                        return;
                    }
                }

                dataGridViewDetail.Rows.Add();
                dataGridViewDetail.Rows[row].Cells[0].Value = product;
                dataGridViewDetail.Rows[row].Cells[1].Value = count;
                dataGridViewDetail.Rows[row].Cells[2].Value = sale;
                dataGridViewDetail.Rows[row].Cells[3].Value = total;

            }
        }

        private void dataGridViewDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnCancel.Visible = true;
            btnDelete.Visible = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnCancel.Visible = false;
            btnDelete.Visible = false;
            dataGridViewDetail.ClearSelection();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            YesNo open = new YesNo();
            open.StartPosition = FormStartPosition.CenterParent;
            open.ShowDialog();
            if (open.getCheck())
            {
                int index = dataGridViewDetail.CurrentRow.Index;
                dataGridViewDetail.Rows.RemoveAt(index);
                open.Dispose();
            }
            else
            {
                open.Dispose();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            generalInform.resetFormUpdate(panel1);
        }

        private void btnViewFormAdd_Click(object sender, EventArgs e)
        {
            if (generalInform.checkTextBoxInForm(panel2))
            {
                if (dataGridViewDetail.Rows.Count == 0)
                {
                    MessageBox.Show("Bạn chưa thêm các mặt hàng", "Thông báo");
                }
                else
                {
                    // bill import
                    string supplier = comboBoxSupplier.SelectedValue.ToString();
                    string employee = comboBoxEmployee.SelectedValue.ToString();
                    // import detail
                    int row = dataGridViewDetail.Rows.Count;
                    string value = "";
                    double sumtotal = 0;
                    for (int i = 0; i < row; i++)
                    {
                        string idpro = dataGridViewDetail.Rows[i].Cells[0].Value.ToString();
                        string stock = dataGridViewDetail.Rows[i].Cells[1].Value.ToString();
                        string total = dataGridViewDetail.Rows[i].Cells[3].Value.ToString();
                        sumtotal += double.Parse(total);
                        string update = String.Format("Update Product set Stock = Stock - {0} where Id = {1}", stock, idpro);
                        con.UpdateData(update, false);
                    }
                    string tax = textBoxTax.Text.ToString();
                    sumtotal = sumtotal + sumtotal * double.Parse(tax) / 100;
                    string date = DateTime.Now.ToString();
                    string insert = String.Format("insert Bill_Export (Employee_ID,Customer_Id,Tax,Created_Date,Total) values({0},{1},{2},'{3}',{4})", employee, supplier,tax, date, sumtotal);
                    if (textBox1.Text.ToString() != "")
                    {
                        insert = String.Format("Update Bill_Export set Employee_ID = {0},Customer_Id={1},Tax = {5},Created_Date='{2}',Total={3} where Id ={4} ", employee, supplier, date, sumtotal, id,tax);
                    }
                    con.UpdateData(insert, false);


                    string idx;
                    if (textBox1.Text.ToString() != "")
                    {
                        idx = this.id;
                        con.UpdateData("delete Bill_Export_detail where Be_id = " + id, false);
                    }
                    else
                    {
                        idx = con.GetTotal("select top 1 Id from Bill_Export order by Id desc").ToString();
                    }
                    for (int i = 0; i < row; i++)
                    {
                        string product = dataGridViewDetail.Rows[i].Cells[0].Value.ToString();
                        string count = dataGridViewDetail.Rows[i].Cells[1].Value.ToString();
                        string sale = dataGridViewDetail.Rows[i].Cells[2].Value.ToString();
                        string total = dataGridViewDetail.Rows[i].Cells[3].Value.ToString();
                        value += String.Format("({0},{1},{2},{3},{4})", idx, product, count, sale, total)+",";
                    }
                    value = value.Substring(0, value.Length - 1);
                    string insertdetail = "insert into Bill_Export_detail(BE_Id,Product_Id,Stock,sale,total) values" + value;
                    con.UpdateData(insertdetail);
                }
            }
        }

        private void textBoxTax_MouseClick(object sender, MouseEventArgs e)
        {
            generalInform.FocusTextBox(textBoxTax, null, "");
        }

        private void textBoxTax_Leave(object sender, EventArgs e)
        {
            generalInform.LeaveTextBox(textBoxTax, null, "");
        }

        private void textBoxCount_MouseClick(object sender, MouseEventArgs e)
        {
            generalInform.FocusTextBox(textBoxCount, null, "");
        }

        private void textBoxCount_Leave(object sender, EventArgs e)
        {
            generalInform.LeaveTextBox(textBoxCount, null, "");

        }

        private void textBoxSale_MouseClick(object sender, MouseEventArgs e)
        {
            generalInform.FocusTextBox(textBoxSale, null, "");
        }

        private void textBoxSale_Leave(object sender, EventArgs e)
        {
            generalInform.LeaveTextBox(textBoxSale, null, "");
        }

        private void comboBoxSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxSupplier.ForeColor = Color.Black;

        }

        private void comboBoxEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxEmployee.ForeColor = Color.Black;

        }
    }
}
