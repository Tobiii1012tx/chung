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
    public partial class BillImportForm : UserControl
    {
        GeneralInForm generalInform;
        ConnectDatabase con;

        // phân trang
        int pageSize = 10;   // số lượng bản ghi trên 1 trang
        int pageIndex = 1;   // trang hiện tại đang xem
        int pageCount = 1;  // tổng số trang
        public BillImportForm()
        {
            generalInform = new GeneralInForm();
            con = new ConnectDatabase();
            InitializeComponent();
            setData(1);
            comboBoxTivi.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Product"), "Chọn sản phẩm").DataSource;
            comboBoxTivi.ValueMember = "Id";
            comboBoxTivi.DisplayMember = "Name";
        }


        public void setData(int pageNumber)
        {
            // phân trang
            string skip = (pageSize * (pageNumber - 1)).ToString();
            string take = ((pageSize * (pageNumber - 1)) + pageSize).ToString();

            // tìm kiếm
            string search = textBoxSearch.Text.ToString();
            string condition = "where ";
            string tivi = "-1";


            if (comboBoxTivi.SelectedIndex > 0)
            {
                if (comboBoxTivi.SelectedValue.ToString() != "System.Data.DataRowView")
                    tivi = comboBoxTivi.SelectedValue.ToString();
            }


            // khởi tạo câu lệnh truy vấn
            string sql = "select * from (select ROW_NUMBER() over (order by b.Id) as STT,b.Id ,e.Name as N'Nhân viên',s.Name as N'NCC',b.Created_Date as N'Ngày tạo', b.TOTAL as N'Tổng tiền' from Bill_Import b " +
                            " join Employee e on e.Id = b.Employee_Id"+
                            " join Supplier s on s.Id = b.Supplier_Id"+
                            " ) as abc where abc.STT >" + skip + "and abc.STT <=" + take;
            string count = "select count(*) from Bill_Import";
            // nếu có điều kiện thì thêm cả điều kiện
            if (search == "" && tivi == "-1")
            {
            }
            else
            {
                if (search != "")
                {
                    condition += "( dt.stock = " + search + ") and ";
                }
                if (tivi != "-1")
                {
                    condition += "dt.product_id = N'" + tivi + "' and ";
                }

                condition = condition.Substring(0, condition.Length - 4);
                sql = "select * from (select ROW_NUMBER() over (order by b.Id) as STT,b.Id ,e.Name as N'Nhân viên',s.Name as N'NCC',b.Created_Date as N'Ngày tạo', b.TOTAL as N'Tổng tiền' from Bill_Import b " +
                           " join Employee e on e.Id = b.Employee_Id" +
                           " join Supplier s on s.Id = b.Supplier_Id" +
                           " join Bill_Import_detail dt on dt.Bi_id = b.Id "+
                           " "+condition+" " +
                           "group by b.Id ,e.Name ,s.Name ,b.Created_Date, b.TOTAL ) as abc where abc.STT >" + skip + "and abc.STT <=" + take;
                count = "select count(*) from Bill_Import b" +
                        " join Bill_Import_detail dt on dt.Bi_id = b.Id " + condition;
            }


            // lấy tổng số lượng bản ghi
            int total = con.GetTotal(count);
            // chia lấy xem có bao nhiêu trang 
            pageCount = total % pageSize == 0 ? total / pageSize : total / pageSize + 1;
            // set data
            DataTable dt = con.getData(sql);
            dataGridView.DataSource = dt;
            // ẩn đi trường Id;
            dataGridView.Columns[0].Visible = false;
            // giải phóng bộ nhớ data table
            dt.Dispose();

            // set số lượng bản ghi 
            labelCountRecord.Text = "Số lượng bản ghi : " + total;
            btnCancel.Visible = false;
            btnEdit.Visible = false;
            btnDelete.Visible = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int row = dataGridView.CurrentRow.Index;
            string id = dataGridView.Rows[row].Cells[1].Value.ToString();
            showHideFrom(new BillImport_Add(id));
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            YesNo open = new YesNo();
            open.StartPosition = FormStartPosition.CenterParent;
            open.ShowDialog();
            if (open.getCheck())
            {
                int row = dataGridView.CurrentRow.Index;
                int id = Int16.Parse(dataGridView.Rows[row].Cells[1].Value.ToString());
                string sql = "delete Bill_Import where id = " + id;
                string sql2 = "delete Bill_Import_Detail where Bi_id = " + id;
                con.UpdateData(sql2, false);
                con.UpdateData(sql);
                setData(pageIndex);
                open.Dispose();
            }
            else
            {
                open.Dispose();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnDelete.Visible = false;
            btnCancel.Visible = false;
            btnEdit.Visible = false;
            dataGridView.ClearSelection();
        }

        private void btnViewFormAdd_Click(object sender, EventArgs e)
        {
            showHideFrom(new BillImport_Add());   
        }

        private void pagePrev_Click(object sender, EventArgs e)
        {
            int a = Int16.Parse(textBoxPageNumber.Text.ToString());
            if (a > 1 && a <= pageCount)
            {
                a--;
                pageIndex = a;
                setData(pageIndex);
                textBoxPageNumber.Text = a.ToString();
            }
        }

        private void pageNext_Click(object sender, EventArgs e)
        {
            int a = Int16.Parse(textBoxPageNumber.Text.ToString());
            if (a > 0 && a < pageCount)
            {
                a++;
                pageIndex = a;
                setData(pageIndex);
                textBoxPageNumber.Text = a.ToString();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            comboBoxTivi.SelectedIndex = 0;
            textBoxSearch.Text = "";
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            setData(1);
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnDelete.Visible = true;
            btnCancel.Visible = true;
            btnEdit.Visible = true;
        }

        private void dataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void comboBoxPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void comboBoxTivi_SelectedIndexChanged(object sender, EventArgs e)
        {
            setData(1);
        }

        private void textBoxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
