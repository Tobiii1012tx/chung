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
    public partial class BillExportForm : UserControl
    {
        GeneralInForm generalInform;
        ConnectDatabase con;

        // phân trang
        int pageSize = 10;   // số lượng bản ghi trên 1 trang
        int pageIndex = 1;   // trang hiện tại đang xem
        int pageCount = 1;  // tổng số trang
        public BillExportForm()
        {
            con = new ConnectDatabase();
            generalInform = new GeneralInForm();
            InitializeComponent();
        }
        private void BillExportForm_Load(object sender, EventArgs e)
        {
            setData(1);
        }
        public void setData(int pageNumber)
        {
            // phân trang
            string skip = (pageSize * (pageNumber - 1)).ToString();
            string take = ((pageSize * (pageNumber - 1)) + pageSize).ToString();



            // khởi tạo câu lệnh truy vấn
            string sql = "select * from (select ROW_NUMBER() over (order by b.Id) as STT,b.Id ,e.Name as N'Nhân viên',s.Name as N'Khách hàng',b.Created_Date as N'Ngày tạo', b.TOTAL as N'Tổng tiền' from Bill_Export b " +
                            " join Employee e on e.Id = b.Employee_Id" +
                            " join Customer s on s.Id = b.Customer_Id" +
                            " ) as abc where abc.STT >" + skip + "and abc.STT <=" + take;
            string count = "select count(*) from Bill_export";
           
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
            int idx = dataGridView.CurrentRow.Index;
            string id = dataGridView.Rows[idx].Cells[1].Value.ToString();
            showHideFrom(new BillExport_Add(id));
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
           YesNo open = new YesNo();
            open.StartPosition = FormStartPosition.CenterParent;
            open.ShowDialog();
            if (open.getCheck())
            {
                int idx = dataGridView.CurrentRow.Index;
                string id = dataGridView.Rows[idx].Cells[1].Value.ToString();
                con.UpdateData("delete Bill_export where id = " + id, false);
                con.UpdateData("delete Bill_export_detail where be_id = " + id);
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
            btnCancel.Visible = true;
            btnDelete.Visible = true;
            btnEdit.Visible = true;
            dataGridView.ClearSelection();
        }

        private void btnViewFormAdd_Click(object sender, EventArgs e)
        {
            showHideFrom(new BillExport_Add());   
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

        private void comboBoxPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string size = comboBoxPageSize.SelectedItem.ToString();
            pageSize = Int16.Parse(size);
            setData(1);
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnCancel.Visible = true;
            btnDelete.Visible = true;
            btnEdit.Visible = true;

        }

        private void dataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
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
    }
}
