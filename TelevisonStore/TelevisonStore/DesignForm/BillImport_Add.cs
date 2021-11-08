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
    public partial class BillImport_Add : UserControl
    {
        GeneralInForm generalInform;
        ConnectDatabase con;
        string id;
        public BillImport_Add()
        {
            InitializeComponent();
            con = new ConnectDatabase();
            generalInform = new GeneralInForm();
            comboBoxSupplier.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Supplier"), "Chọn nhà cung cấp").DataSource;
            comboBoxSupplier.ValueMember = "Id";
            comboBoxSupplier.DisplayMember = "Name";

            comboBoxEmployee.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Employee"), "Nhân viên nhập").DataSource;
            comboBoxEmployee.ValueMember = "Id";
            comboBoxEmployee.DisplayMember = "Name";

            comboBoxProduct.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Product"), "Chọn nhà sản phẩm").DataSource;
            comboBoxProduct.ValueMember = "Id";
            comboBoxProduct.DisplayMember = "Name";
        }
        public BillImport_Add(string id)
        {
            this.id = id;
            InitializeComponent();
            con = new ConnectDatabase();
            generalInform = new GeneralInForm();
            comboBoxSupplier.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Supplier"), "Chọn nhà cung cấp").DataSource;
            comboBoxSupplier.ValueMember = "Id";
            comboBoxSupplier.DisplayMember = "Name";

            comboBoxEmployee.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Employee"), "Nhân viên nhập").DataSource;
            comboBoxEmployee.ValueMember = "Id";
            comboBoxEmployee.DisplayMember = "Name";

            comboBoxProduct.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Product"), "Chọn nhà sản phẩm").DataSource;
            comboBoxProduct.ValueMember = "Id";
            comboBoxProduct.DisplayMember = "Name";
            textBox1.Text = id;
            DataTable dataTable = con.getData("select product_id, Stock,Unit_Price,Sale,Total from Bill_Import_detail where Bi_Id = " + id);
            int pos = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                dataGridViewDetail.Rows.Add();
                dataGridViewDetail.Rows[pos].Cells[0].Value = row["product_id"].ToString();
                dataGridViewDetail.Rows[pos].Cells[1].Value = row["Stock"].ToString();
                dataGridViewDetail.Rows[pos].Cells[2].Value = row["Unit_Price"].ToString();
                dataGridViewDetail.Rows[pos].Cells[3].Value = row["Sale"].ToString();
                dataGridViewDetail.Rows[pos].Cells[4].Value = row["Total"].ToString();
                pos++;
            }

            DataTable dt = con.getData("select * from Bill_Import where id = " + id);
            foreach (DataRow row in dt.Rows)
            {
                comboBoxEmployee.SelectedValue = row["employee_id"].ToString();
                comboBoxSupplier.SelectedValue = row["supplier_id"].ToString();
            }

            btnViewFormAdd.Text = "Cập nhật";
        }

        private void BillImport_Add_Load(object sender, EventArgs e)
        {
            
            
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (generalInform.checkTextBoxInForm(panel1))
            {
                string product = comboBoxProduct.SelectedValue.ToString();
                string count = textBoxCount.Text.ToString();
                string unitprice = textBoxUnitPrice.Text.ToString();
                string sale = textBoxSale.Text.ToString();
                string total = textBoxTotal.Text.ToString();
                int row = dataGridViewDetail.Rows.Count;
                for(int i = 0; i < row; i++)
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
                dataGridViewDetail.Rows[row].Cells[2].Value = unitprice;
                dataGridViewDetail.Rows[row].Cells[3].Value = sale;
                dataGridViewDetail.Rows[row].Cells[4].Value = total;
                
            }
        }

        private void comboBoxProduct_MouseClick(object sender, MouseEventArgs e)
        {
            comboBoxProduct.ForeColor = Color.Black;
        }

        private void comboBoxProduct_Leave(object sender, EventArgs e)
        {

        }

        private void textBoxCount_MouseClick(object sender, MouseEventArgs e)
        {
            generalInform.FocusTextBox(textBoxCount, null, "");
        }

        private void textBoxCount_Leave(object sender, EventArgs e)
        {
            generalInform.LeaveTextBox(textBoxCount, null, "");
        }

        private void textBoxUnitPrice_MouseClick(object sender, MouseEventArgs e)
        {
            generalInform.FocusTextBox(textBoxUnitPrice, null, "");
        }

        private void textBoxUnitPrice_Leave(object sender, EventArgs e)
        {
            generalInform.LeaveTextBox(textBoxUnitPrice, null, "");
        }

        private void textBoxSale_MouseClick(object sender, MouseEventArgs e)
        {
            generalInform.FocusTextBox(textBoxSale, null, "");

        }

        private void textBoxSale_Leave(object sender, EventArgs e)
        {
            generalInform.LeaveTextBox(textBoxSale, null, "");
        }

        private void textBoxCount_TextChanged(object sender, EventArgs e)
        {
            if(textBoxCount.Text.Length != 0 && textBoxUnitPrice.Text.Length != 0 && textBoxSale.Text.Length != 0 && textBoxCount.ForeColor == Color.Black && textBoxUnitPrice.ForeColor == Color.Black && textBoxSale.ForeColor == Color.Black)
            {
                double count = double.Parse(textBoxCount.Text.ToString());
                double unitprice = double.Parse(textBoxUnitPrice.Text.ToString());
                double sale = double.Parse(textBoxSale.Text.ToString());
                double total = count * (unitprice * ((100 - sale) / 100));
                textBoxTotal.Text = total.ToString();
            }    
        }

        private void textBoxUnitPrice_TextChanged(object sender, EventArgs e)
        {
            if (textBoxCount.Text.Length != 0 && textBoxUnitPrice.Text.Length != 0 && textBoxSale.Text.Length != 0 && textBoxCount.ForeColor == Color.Black && textBoxUnitPrice.ForeColor == Color.Black && textBoxSale.ForeColor == Color.Black)
            {
                double count = double.Parse(textBoxCount.Text.ToString());
                double unitprice = double.Parse(textBoxUnitPrice.Text.ToString());
                double sale = double.Parse(textBoxSale.Text.ToString());
                double total = count * (unitprice * ((100 - sale) / 100));
                textBoxTotal.Text = total.ToString();
            }
        }

        private void textBoxSale_TextChanged(object sender, EventArgs e)
        {
            if (textBoxCount.Text.Length != 0 && textBoxUnitPrice.Text.Length != 0 && textBoxSale.Text.Length != 0 && textBoxCount.ForeColor == Color.Black && textBoxUnitPrice.ForeColor == Color.Black && textBoxSale.ForeColor == Color.Black)
            {
                double count = double.Parse(textBoxCount.Text.ToString());
                double unitprice = double.Parse(textBoxUnitPrice.Text.ToString());
                double sale = double.Parse(textBoxSale.Text.ToString());
                double total = count * (unitprice * ((100 - sale) / 100));
                textBoxTotal.Text = total.ToString();
            }
        }

        private void textBoxCount_KeyPress_2(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            generalInform.resetFormUpdate(panel1);
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

        private void btnViewFormAdd_Click(object sender, EventArgs e)
        {
            if (generalInform.checkTextBoxInForm(panel2))
            {
                if(dataGridViewDetail.Rows.Count ==0)
                {
                    MessageBox.Show( "Bạn chưa thêm các mặt hàng", "Thông báo");
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
                        string product = dataGridViewDetail.Rows[i].Cells[0].Value.ToString();
                        string stock = dataGridViewDetail.Rows[i].Cells[1].Value.ToString();
                        string unitprice = dataGridViewDetail.Rows[i].Cells[2].Value.ToString() ;
                        string total = dataGridViewDetail.Rows[i].Cells[4].Value.ToString() ;
                        sumtotal += double.Parse(total);

                        string price = (double.Parse(unitprice) * 1.1).ToString();

                        string sql = String.Format("update Product set Import_price = {0} , Price = {1}, Stock = Stock+{3} where Id = {2}",unitprice,price,product,stock);
                       
                        con.UpdateData(sql,false);
                    }
                    string date = DateTime.Now.ToString();
                    string insert = String.Format("insert Bill_Import (Employee_ID,Supplier_Id,Created_Date,Total) values({0},{1},'{2}',{3})", employee, supplier, date, sumtotal);
                    if(textBox1.Text.ToString() != "")
                    {
                        insert = String.Format("Update Bill_Import set Employee_ID = {0},Supplier_Id={1},Created_Date='{2}',Total={3} where Id ={4} ", employee, supplier, date, sumtotal, id);
                    }
                    con.UpdateData(insert,false);


                    string idx;
                    if (textBox1.Text.ToString() != "")
                    {
                        idx = this.id;
                        con.UpdateData("delete Bill_Import_detail where Bi_id = " + id,false);
                    }
                    else
                    {
                        idx = con.GetTotal("select top 1 Id from Bill_Import order by Id desc").ToString();
                    }
                    for (int i = 0; i < row; i++)
                    {
                        string product = dataGridViewDetail.Rows[i].Cells[0].Value.ToString();
                        string count = dataGridViewDetail.Rows[i].Cells[1].Value.ToString();
                        string unitprice = dataGridViewDetail.Rows[i].Cells[2].Value.ToString();
                        string sale = dataGridViewDetail.Rows[i].Cells[3].Value.ToString();
                        string total = dataGridViewDetail.Rows[i].Cells[4].Value.ToString();
                        value += String.Format("({0},{1},{2},{3},{4},{5})", idx, product, count, unitprice, sale, total)+",";
                    }
                    value = value.Substring(0, value.Length - 1);

                    string insertdetail = "insert into Bill_Import_detail(BI_Id,Product_Id,Stock,Unit_price,sale,total) values" + value;
                    con.UpdateData(insertdetail);
                }    
            }
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
