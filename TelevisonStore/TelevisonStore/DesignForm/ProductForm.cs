using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TelevisonStore.Models;
using TelevisonStore.Common;
using TelevisonStore.DesignForm.Messeage;

namespace TelevisonStore.DesignForm
{
    public partial class ProductForm : UserControl
    {
        GeneralInForm generalInform;
        ConnectDatabase con;

        // phân trang
        int pageSize = 10;   // số lượng bản ghi trên 1 trang
        int pageIndex = 1;   // trang hiện tại đang xem
        int pageCount = 1;  // tổng số trang
        public ProductForm()
        {
            InitializeComponent();
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            generalInform = new GeneralInForm();
            con = new ConnectDatabase();
            setData(pageIndex);
            comboBoxPageSize.SelectedIndex = 1;

            comboBoxBrand.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Brand"),"Chọn nhãn hàng").DataSource;
            comboBoxBrand.ValueMember = "Id";
            comboBoxBrand.DisplayMember = "Name";

            comboBoxScreen.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Screen"), "Chọn màn hình").DataSource;
            comboBoxScreen.ValueMember = "Id";
            comboBoxScreen.DisplayMember = "Name";

            comboBoxSize.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Size"), "Chọn kích thước").DataSource;
            comboBoxSize.ValueMember = "Id";
            comboBoxSize.DisplayMember = "Name";
        }

        public void setData(int pageNumber)
        {
            // phân trang
            string skip = (pageSize * (pageNumber - 1)).ToString();
            string take = ((pageSize * (pageNumber - 1)) + pageSize).ToString();

            // tìm kiếm
            string search = textBoxSearch.Text.ToString();
            string condition = "where ";
            string brand = "-1";
            string screen = "-1";
            string size = "-1";



            if (comboBoxBrand.SelectedIndex > 0)
            {
                if (comboBoxBrand.SelectedValue.ToString() != "System.Data.DataRowView")
                    brand = comboBoxBrand.SelectedValue.ToString();
            }

            if (comboBoxSize.SelectedIndex > 0)
            {
                if (comboBoxSize.SelectedValue.ToString() != "System.Data.DataRowView")
                    size = comboBoxSize.SelectedValue.ToString();
            }
            if (comboBoxScreen.SelectedIndex > 0)
            {
                if (comboBoxScreen.SelectedValue.ToString() != "System.Data.DataRowView")
                    screen = comboBoxScreen.SelectedValue.ToString();
            }


            // khởi tạo câu lệnh truy vấn
            string sql = "select * from (select ROW_NUMBER() over (order by p.Id) as STT, p.Id, p.Code , p.Name, b.Name as N'Brand' , d.Name as N'Design' , c.Name as N'Color' , s.Name as N'Screen' , si.Name as N'Size' , co.Name as N'Country' , p.Stock as N'Stock' , p.Import_Price as N'Price In' , p.Price as N'Price Out', p.Warranty_Time as N'BH' from Product p " +
                            " join brand b on b.Id = p.Brand_id "+
                            " join design d on d.Id = p.Design_id "+
                            " join Color c on c.Id = p.Color_Id "+
                            " join Screen s on s.Id = p.Screen_Id "+
                            " join Size si on si.Id = p.Size_Id "+
                            " join Country co on co.Id = p.Country_Id) as abc where abc.STT >" + skip + "and abc.STT <=" + take;
            string count = "select count(*) from Product";
            // nếu có điều kiện thì thêm cả điều kiện
            if (search == "" && brand == "-1" && screen == "-1" && size == "-1")
            {
            }
            else
            {
                if (search != "")
                {
                    condition += " ( p.code like N'%" + search + "%' or p.name like N'%" + search + "%' or p.price like N'%" + search + "%' or p.warranty_time like N'%" + search + "%') and ";
                }
                if (brand != "-1")
                {
                    condition += "p.brand_id = N'" + brand + "' and ";
                }
                if (screen != "-1")
                {
                    condition += "p.screen_id = N'" + screen + "' and ";
                }
                if (size != "-1")
                {
                    condition += "p.size_id = N'" + size + "' and ";
                }

                condition = condition.Substring(0, condition.Length - 4);
                sql = "select * from (select  ROW_NUMBER() over (order by p.Id) as STT, p.Id, p.Code , p.Name , b.Name as N'Brand' , d.Name as N'Design' , c.Name as N'Color' , s.Name as N'Screen' , si.Name as N'Size' , co.Name as N'Country' , p.Stock as N'Stock'  , p.Import_Price as N'PriceIn' , p.Price as N'Price Out' , p.Warranty_Time as N'BH' from Product p " +
                             " join brand b on b.Id = p.Brand_id " +
                             " join design d on d.Id = p.Design_id " +
                             " join Color c on c.Id = p.Color_Id " +
                             " join Screen s on s.Id = p.Screen_Id " +
                             " join Size si on si.Id = p.Size_Id " +
                             " join Country co on co.Id = p.Country_Id " + condition + ") as abc where abc.STT >" + skip + "and abc.STT <=" + take;
                count = "select count(*) from Product p " + condition;
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
            // giải phóng bộ nhớ data table
            dt.Dispose();

            // set số lượng bản ghi 
            labelCountRecord.Text = "Số lượng bản ghi : " + total;
            btnCancel.Visible = false;
            btnEdit.Visible = false;
            btnDelete.Visible = false;
            //textBoxId.Text = "0";
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            setData(1);
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

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.CurrentRow != null && dataGridView.CurrentRow.Index >= 0)
            {
                btnEdit.Visible = true;
                btnDelete.Visible = true;
                btnCancel.Visible = true;
            }
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
                con.UpdateData("delete Product where Id = " + id);
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


        private void dataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        // mở form mới để thêm hoặc sửa sản phẩm
        private void btnViewFormAdd_Click(object sender, EventArgs e)
        {
            showHideFrom(new AddProductForm());
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int index = dataGridView.CurrentRow.Index;
            string id = dataGridView.Rows[index].Cells[1].Value.ToString();

            string sql = "select * from Product where id = " + id;
            DataTable dt = con.getData(sql);
            if(dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                string code = dr["code"].ToString();
                string name = dr["name"].ToString();
                string brand = dr["brand_id"].ToString();
                string design = dr["design_id"].ToString();
                string color = dr["color_id"].ToString();
                string screen = dr["screen_id"].ToString();
                string size = dr["size_id"].ToString();
                string country = dr["country_id"].ToString();
                string priceIn = dr["Import_Price"].ToString();
                string warranty = dr["warranty_time"].ToString();
                string iamge = dr["image"].ToString();

                ProductModel product = new ProductModel()
                {
                    Id = id,
                    Code = code,
                    Name = name,
                    Brand_Id = brand,
                    Design_Id = design,
                    Color_Id = color,
                    Country_Id = country,
                    Screen_Id = screen,
                    Size_Id = size,
                    PriceIn = priceIn,
                    Warranty_Time = warranty,
                    Image = iamge
                };

                showHideFrom(new AddProductForm(product));
        }
            
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            comboBoxSize.SelectedIndex = 0;
            comboBoxScreen.SelectedIndex = 0;
            comboBoxBrand.SelectedIndex = 0;
            textBoxSearch.Text = "";
            setData(1);
        }

        private void comboBoxSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            setData(1);
        }

        private void comboBoxScreen_SelectedIndexChanged(object sender, EventArgs e)
        {
            setData(1);
        }

        private void comboBoxBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            setData(1);
        }

        // chuyển form
        public void showHideFrom(UserControl form)
        {
            if (!this.Controls.Contains(form))
            {
                form.Width = this.Width;
                form.Height = this.Height;
                this.Controls.Add(form);
                form.Dock = DockStyle.Fill;
                form.BringToFront();
            }
            else
                form.BringToFront();
        }
    }
}
