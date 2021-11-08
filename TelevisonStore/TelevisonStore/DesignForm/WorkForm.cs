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
    public partial class WorkForm : UserControl
    {
        GeneralInForm generalInform;
        ConnectDatabase con;

        // phân trang
        int pageWork = 10;   // số lượng bản ghi trên 1 trang
        int pageIndex = 1;   // trang hiện tại đang xem
        int pageCount = 1;  // tổng số trang
        public WorkForm()
        {
            InitializeComponent();
        }

        private void WorkForm_Load(object sender, EventArgs e)
        {
            generalInform = new GeneralInForm();
            con = new ConnectDatabase();
            setData(pageIndex);
            comboBoxPageSize.SelectedIndex = 1;
        }

        public void setData(int pageNumber)
        {
            // phân trang
            string skip = (pageWork * (pageNumber - 1)).ToString();
            string take = ((pageWork * (pageNumber - 1)) + pageWork).ToString();
            string search = textBoxSearch.Text.ToString();

            // khởi tạo câu lệnh truy vấn
            string sql = "select * from (select ROW_NUMBER() over (order by iD) as STT,Id ,Code as N'Mã',Name as N'Tên',iif(Status=1,N'Hoạt động',N'Không hoạt động') as N'Trạng thái' from Work) as abc where abc.STT >" + skip + "and abc.STT <=" + take;
            string count = "select count(*) from Work";
            // nếu có điều kiện thì thêm cả điều kiện
            if (search != "")
            {
                sql = "select * from (select ROW_NUMBER() over (order by iD) as STT,Id ,Code as N'Mã',Name as N'Tên',iif(Status=1,N'Hoạt động',N'Không hoạt động') as N'Trạng thái' from Work where code like N'%" + search + "%' or name like N'%" + search + "%' ) as abc where abc.STT >" + skip + "and abc.STT <=" + take;
                count = "select count(*) from Work where code like N'%" + search + "%' or name like N'%" + search + "%'";
            }

            // lấy tổng số lượng bản ghi
            int total = con.GetTotal(count);
            // chia lấy xem có bao nhiêu trang 
            pageCount = total % pageWork == 0 ? total / pageWork : total / pageWork + 1;
            // set data
            DataTable dt = con.getData(sql);
            dataGridView.DataSource = dt;
            // ẩn đi trường Id;
            dataGridView.Columns[1].Visible = false;
            // giải phóng bộ nhớ data table
            dt.Dispose();

            // set số lượng bản ghi 
            labelCountRecord.Text = "Số lượng bản ghi : " + total;

            // mỗi lần refesh là ẩn các nút điều hướng data đi
            btnCancel.Visible = false;
            btnEdit.Visible = false;
            btnDelete.Visible = false;
            textBoxId.Text = "0";
        }



        // sự kiện cho các textbox đổi màu , đổi trạng thái
        private void textBoxCode_MouseClick(object sender, MouseEventArgs e)
        {
            generalInform.FocusTextBox(textBoxCode, panelCode, "Nhập mã chức vụ");
        }

        private void textBoxCode_Leave(object sender, EventArgs e)
        {
            generalInform.LeaveTextBox(textBoxCode, panelCode, "Nhập mã chức vụ");
        }

        private void textBoxName_MouseClick(object sender, MouseEventArgs e)
        {
            generalInform.FocusTextBox(textBoxName, panelName, "Nhập tên chức vụ");
        }

        private void textBoxName_Leave(object sender, EventArgs e)
        {
            generalInform.LeaveTextBox(textBoxName, panelName, "Nhập tên chức vụ");
        }

        private void textBoxSearch_MouseClick(object sender, MouseEventArgs e)
        {
            generalInform.FocusTextBox(textBoxSearch, panelSearch, "");
        }

        private void textBoxSearch_Leave(object sender, EventArgs e)
        {
            generalInform.LeaveTextBox(textBoxSearch, panelSearch, "");
        }



        // load lại form input
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            generalInform.resetFormUpdate(panelUpdateData);
        }


        // chuyển sang trang kế tiếp
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

        // chuyển về trang trước 
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

        // tìm kiếm
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            setData(1);
        }


        // chọn thành phần tên bảng
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.CurrentRow.Index >= 0)
            {
                btnEdit.Visible = true;
                btnDelete.Visible = true;
                btnCancel.Visible = true;
            }
        }



        // thay đổi nút thêm ==> sửa và ngược lại
        private void textBoxId_TextChanged(object sender, EventArgs e)
        {
            string val = textBoxId.Text.ToString();
            if (!val.Equals("0"))
            {
                buttonAdd.Text = "Cập nhật";
                buttonAdd.BackColor = Color.FromArgb(0, 83, 156);
            }
            else
            {
                buttonAdd.Text = "Thêm mới";
                buttonAdd.BackColor = Color.FromArgb(0, 155, 119);
            }

        }


        // sự kiện thêm/sửa/xóa data
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (generalInform.checkTextBoxInForm(panelUpdateData))
            {
                // get data in form
                string id = textBoxId.Text.ToString();
                string code = textBoxCode.Text.ToString();
                string name = textBoxName.Text.ToString();
                bool status = active.Checked == true ? true : false;
                // insert && update table
                string sql = "insert into Work(code,name,status) values ('" + code + "',N'" + name + "','" + status + "')";
                if (!id.Equals("0"))
                {
                    sql = "update Work set code =N'" + code + "', name = N'" + name + "', status = '" + status + "' where id = '" + id + "'";
                }
                con.UpdateData(sql);
                setData(pageIndex);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int index = dataGridView.CurrentRow.Index;
            int id = (int)dataGridView.Rows[index].Cells[1].Value;
            string code = dataGridView.Rows[index].Cells[2].Value.ToString();
            string name = dataGridView.Rows[index].Cells[3].Value.ToString();
            string status = dataGridView.Rows[index].Cells[4].Value.ToString();

            textBoxCode.Text = code;
            textBoxId.Text = id.ToString();
            textBoxName.Text = name;
            if (status == "Hoạt động")
            {
                active.Checked = true;
            }
            else
            {
                notAcitve.Checked = true;
            }

            textBoxCode.ForeColor = Color.Black;
            textBoxName.ForeColor = Color.Black;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            YesNo open = new YesNo();
            open.StartPosition = FormStartPosition.CenterParent;
            open.ShowDialog();
            if (open.getCheck())
            {
                int index = dataGridView.CurrentRow.Index;
                int id = (int)dataGridView.Rows[index].Cells[1].Value;
                con.UpdateData("delete Work where Id = " + id);
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
            buttonRefresh_Click(null, null);
            btnCancel.Visible = false;
            btnEdit.Visible = false;
            btnDelete.Visible = false;
            dataGridView.ClearSelection();
        }


        // thay đổi pageWork
        private void comboBoxPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string size = comboBoxPageSize.SelectedItem.ToString();
            pageWork = Int16.Parse(size);
            setData(1);
        }

        // khi thay đổi data trên datagridview
        private void dataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView.ClearSelection();
        }
    }
}
