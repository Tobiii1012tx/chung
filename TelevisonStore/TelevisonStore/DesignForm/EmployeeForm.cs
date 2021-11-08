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
    public partial class EmployeeForm : UserControl
    {
        GeneralInForm generalInform;
        ConnectDatabase con;

        // phân trang
        int pageSize = 10;   // số lượng bản ghi trên 1 trang
        int pageIndex = 1;   // trang hiện tại đang xem
        int pageCount = 1;  // tổng số trang
        public EmployeeForm()
        {
            InitializeComponent();
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            generalInform = new GeneralInForm();
            con = new ConnectDatabase();

            setData(pageIndex);

            comboBoxPageSize.SelectedIndex = 1;
            // combo add work
            comboBoxWork.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Work"),"Chọn công việc").DataSource;
            comboBoxWork.ValueMember = "Id";
            comboBoxWork.DisplayMember = "Name";

            // combo add shift
            comboBoxShift.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Shift"),"Chọn ca làm").DataSource;
            comboBoxShift.ValueMember = "Id";
            comboBoxShift.DisplayMember = "Name";

            // search by work
            comboBoxSearchWork.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Work"),"Chọn công việc").DataSource;
            comboBoxSearchWork.ValueMember = "Id";
            comboBoxSearchWork.DisplayMember = "Name";

            // search by shift
            comboBoxSearchShift.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Shift"),"Chọn ca làm").DataSource;
            comboBoxSearchShift.ValueMember = "Id";
            comboBoxSearchShift.DisplayMember = "Name";
        }

        public void setData(int pageNumber)
        {
            // phân trang
            string skip = (pageSize * (pageNumber - 1)).ToString();
            string take = ((pageSize * (pageNumber - 1)) + pageSize).ToString();

            // tìm kiếm
            string search = textBoxSearch.Text.ToString();
            string condition = "where ";
            string work = "-1";
            string shift = "-1";



            if (comboBoxSearchShift.SelectedIndex >= 0)
            {
                if(comboBoxSearchShift.SelectedValue.ToString()!="System.Data.DataRowView" && comboBoxSearchShift.SelectedValue.ToString() !="0")
                   shift  = comboBoxSearchShift.SelectedValue.ToString();
            }

            if (comboBoxSearchWork.SelectedIndex >= 0)
            {
                if(comboBoxSearchWork.SelectedValue.ToString()!="System.Data.DataRowView" && comboBoxSearchShift.SelectedValue.ToString() !="0")
                    work = comboBoxSearchWork.SelectedValue.ToString();
            }


            // khởi tạo câu lệnh truy vấn
            string sql = "select * from (select ROW_NUMBER() over (order by iD) as STT, Id ,Code,Name,iif(Gender=1,N'Nam',N'Nữ') as N'Gender',DOB,Phone,Email,Address,work_id,shift_id from Employee) as abc where abc.STT >" + skip + "and abc.STT <=" + take;
            string count = "select count(*) from Employee";
            // nếu có điều kiện thì thêm cả điều kiện
            if (search == "" && work == "-1" && shift == "-1")
            {
            }
            else
            {
                if (search != "")
                {
                    condition += "(code like N'%" + search + "%' or name like N'%" + search + "%' or phone like N'%" + search + "%' or email like N'%" + search + "%') and ";
                }
                if (work != "-1")
                {
                    condition += "work_id = N'" + work + "' and ";
                }
                if (shift != "-1")
                {
                    condition += "shift_id = N'" + shift + "' and ";
                }

                condition = condition.Substring(0, condition.Length - 4);
                sql = "select * from (select ROW_NUMBER() over (order by iD) as STT, Id ,Code,Name,iif(Gender=1,N'Nam',N'Nữ') as N'Gender',DOB,Phone,Email,Address,work_id,shift_id from Employee " + condition + ") as abc where abc.STT >" + skip + " and abc.STT <=" + take;
                count = "select count(*) from Employee " + condition;
            }


            // lấy tổng số lượng bản ghi
            int total = con.GetTotal(count);
            // chia lấy xem có bao nhiêu trang 
            pageCount = total % pageSize == 0 ? total / pageSize : total / pageSize + 1;
            // set data
            DataTable dt = con.getData(sql);
            dataGridView.DataSource = dt;
            // ẩn đi trường Id;
            dataGridView.Columns[1].Visible = false;
            dataGridView.Columns[0].Visible = false;
            dataGridView.Columns[9].Visible = false;
            dataGridView.Columns[10].Visible = false;
            // giải phóng bộ nhớ data table
            dt.Dispose();

            // set số lượng bản ghi 
            labelCountRecord.Text = "Số lượng bản ghi : " + total;
            btnCancel.Visible = false;
            btnEdit.Visible = false;
            btnDelete.Visible = false;
            textBoxId.Text = "0";
        }

        #region ChangeColorTextBox
        // sự kiện cho các textbox đổi màu , đổi trạng thái
        private void textBoxCode_MouseClick(object sender, MouseEventArgs e)
        {
            generalInform.FocusTextBox(textBoxCode, panelCode, "Nhập mã");
        }

        private void textBoxCode_Leave(object sender, EventArgs e)
        {
            generalInform.LeaveTextBox(textBoxCode, panelCode, "Nhập mã");
        }

        private void textBoxName_MouseClick(object sender, MouseEventArgs e)
        {
            generalInform.FocusTextBox(textBoxName, panelName, "Nhập tên");
        }

        private void textBoxName_Leave(object sender, EventArgs e)
        {
            generalInform.LeaveTextBox(textBoxName, panelName, "Nhập tên");
        }

        private void textBoxSearch_MouseClick(object sender, MouseEventArgs e)
        {
            generalInform.FocusTextBox(textBoxSearch, panelSearch, "");
        }

        private void textBoxSearch_Leave(object sender, EventArgs e)
        {
            generalInform.LeaveTextBox(textBoxSearch, panelSearch, "");
        }


        private void textBoxPhone_MouseClick(object sender, MouseEventArgs e)
        {
            generalInform.FocusTextBox(textBoxPhone, panelPhone, "Nhập số điện thoại");
        }

        private void textBoxEmail_MouseClick(object sender, MouseEventArgs e)
        {
            generalInform.FocusTextBox(textBoxEmail, panelEmail, "Nhập Email");
        }

        private void textBoxAddress_MouseClick(object sender, MouseEventArgs e)
        {
            generalInform.FocusTextBox(textBoxAddress, panelAddress, "Nhập địa chỉ");
        }

        private void textBoxAddress_Leave(object sender, EventArgs e)
        {
            generalInform.LeaveTextBox(textBoxAddress, panelAddress, "Nhập địa chỉ");
        }

        private void textBoxEmail_Leave(object sender, EventArgs e)
        {
            generalInform.LeaveTextBox(textBoxEmail, panelEmail, "Nhập Email");
        }

        private void textBoxPhone_Leave(object sender, EventArgs e)
        {
            generalInform.LeaveTextBox(textBoxPhone, panelPhone, "Nhập số điện thoại");

        }
        #endregion

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
            if (dataGridView.CurrentRow != null && dataGridView.CurrentRow.Index >= 0)
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
                string phone = textBoxPhone.Text.ToString();
                string email = textBoxEmail.Text.ToString();
                string address = textBoxAddress.Text.ToString();
                bool gender = active.Checked == true ? true : false;
                string date = StartTime.Value.ToString("yyyy-MM-dd");
                string workid = comboBoxWork.SelectedValue.ToString();
                string shiftid = comboBoxShift.SelectedValue.ToString();
                //insert && update table
                if (generalInform.checkMail(email))
                {
                    string sql = "insert into Employee(code,name,gender,phone,email,address,dob,work_id,shift_id) values (N'" + code + "',N'" + name + "','" + gender + "',N'" + phone + "',N'" + email + "',N'" + address + "','" + date + "','" + workid + "','" + shiftid + "')";
                    if (!id.Equals("0"))
                    {
                        sql = "update Employee set code =N'" + code + "', name = N'" + name + "',phone = N'" + phone + "',email = N'" + email + "',address = N'" + address + "', gender = '" + gender + "', dob = '" + date + "',work_id = '" + workid + "', shift_id = '" + shiftid + "' where id = " + id;
                    }
                    con.UpdateData(sql);
                    setData(pageIndex);
                }
                else
                {
                    MessageBox.Show("Email không đúng ??");
                }
               

            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // get data in datagridview
            int index = dataGridView.CurrentRow.Index;
            int id = (int)dataGridView.Rows[index].Cells[1].Value;
            string code = dataGridView.Rows[index].Cells[2].Value.ToString();
            string name = dataGridView.Rows[index].Cells[3].Value.ToString();
            string gender = dataGridView.Rows[index].Cells[4].Value.ToString();
            string dob = dataGridView.Rows[index].Cells[5].Value.ToString();
            string phone = dataGridView.Rows[index].Cells[6].Value.ToString();
            string email = dataGridView.Rows[index].Cells[7].Value.ToString();
            string address = dataGridView.Rows[index].Cells[8].Value.ToString();
            string work = dataGridView.Rows[index].Cells[9].Value.ToString();
            string shift = dataGridView.Rows[index].Cells[10].Value.ToString();

            textBoxId.Text = id.ToString();
            textBoxCode.Text = code;
            textBoxName.Text = name;
            textBoxPhone.Text = phone;
            textBoxEmail.Text = email;
            textBoxAddress.Text = address;
            StartTime.Value = DateTime.Parse(dob);
            if (gender == "Nam")
            {
                active.Checked = true;
            }
            else
            {
                notAcitve.Checked = true;
            }
            comboBoxWork.SelectedValue = work;
            comboBoxShift.SelectedValue = shift;

            foreach (var x in panelUpdateData.Controls)
            {
                if (x is TextBox)
                {
                    ((TextBox)x).ForeColor = Color.Black;
                }
                if (x is ComboBox)
                {
                    ((ComboBox)x).ForeColor = Color.Black;
                }
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
                con.UpdateData("delete Employee where Id = " + id);
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


        // thay đổi pageSize
        private void comboBoxPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string size = comboBoxPageSize.SelectedItem.ToString();
            pageSize = Int16.Parse(size);
            setData(1);
        }


        // kh
        private void dataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView.ClearSelection();
        }

        private void textBoxPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void comboBoxWork_MouseClick(object sender, MouseEventArgs e)
        {
            comboBoxWork.ForeColor = Color.Black;
        }

        private void comboBoxShift_MouseClick(object sender, MouseEventArgs e)
        {
            comboBoxShift.ForeColor = Color.Black;
        }

        private void comboBoxSearchShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            setData(1);
        }

        private void comboBoxSearchWork_SelectedIndexChanged(object sender, EventArgs e)
        {
           setData(1);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
           
            comboBoxSearchShift.SelectedIndex = -1;
            comboBoxSearchWork.SelectedIndex = -1;
            comboBoxSearchShift.Text = "Ca làm";
            comboBoxSearchWork.Text = "Công việc";
            textBoxSearch.Text = "";
            setData(1);
        }

        private void textBoxPhone_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
