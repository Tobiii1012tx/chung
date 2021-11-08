using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using TelevisonStore.Common;
using TelevisonStore.Models;
using TelevisonStore.DesignForm.Messeage;

namespace TelevisonStore.DesignForm
{
    public partial class AddProductForm : UserControl
    {
        ConnectDatabase con;
        GeneralInForm generalInform;
        ProductModel productModel;
        public AddProductForm(object product)
        {
            con = new ConnectDatabase();
            generalInform = new GeneralInForm();
            InitializeComponent();
            inputImage.SizeMode = PictureBoxSizeMode.StretchImage;

            // add datacombobox

            comboBoxBrand.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Brand"), "Chọn nhãn hàng").DataSource;
            comboBoxBrand.ValueMember = "Id";
            comboBoxBrand.DisplayMember = "Name";

            comboBoxScreen.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Screen"), "Chọn màn hình").DataSource;
            comboBoxScreen.ValueMember = "Id";
            comboBoxScreen.DisplayMember = "Name";

            comboBoxColor.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Color"), "Chọn mã màu").DataSource;
            comboBoxColor.ValueMember = "Id";
            comboBoxColor.DisplayMember = "Name";

            comboBoxDesign.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Design"), "Chọn thiết kế").DataSource;
            comboBoxDesign.ValueMember = "Id";
            comboBoxDesign.DisplayMember = "Name";

            comboBoxSize.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Size"), "Chọn kích cỡ").DataSource;
            comboBoxSize.ValueMember = "Id";
            comboBoxSize.DisplayMember = "Name";

            comboBoxCountry.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Country"), "Chọn quốc gia").DataSource;
            comboBoxCountry.ValueMember = "Id";
            comboBoxCountry.DisplayMember = "Name";

            productModel = (ProductModel)product;
            if (productModel != null)
            {
                btnAdd.BackColor = Color.Orange;
                btnAdd.Text = "Cập nhật";
                textBoxCode.ForeColor = Color.Black;
                textBoxName.ForeColor = Color.Black;
                textBoxPrice.ForeColor = Color.Black;
                textBoxWarranty.ForeColor = Color.Black;
            }
            textBoxCode.ReadOnly = true;
            textBoxId.Text = productModel.Id;
            textBoxCode.Text = productModel.Code;
            textBoxName.Text = productModel.Name;
            comboBoxBrand.SelectedValue = productModel.Brand_Id;
            comboBoxDesign.SelectedValue = productModel.Design_Id;
            comboBoxColor.SelectedValue = productModel.Color_Id;
            comboBoxScreen.SelectedValue = productModel.Screen_Id;
            comboBoxSize.SelectedValue = productModel.Size_Id;
            comboBoxCountry.SelectedValue = productModel.Country_Id;
            textBoxPrice.Text = productModel.PriceIn;
            textBoxWarranty.Text = productModel.Warranty_Time;
            var executingFolder = Directory.GetCurrentDirectory();
            string[] fileRoot = executingFolder.Split(new string[] { "\\" }, StringSplitOptions.None);
            string destination = executingFolder.Substring(0, executingFolder.Length - fileRoot[fileRoot.Length - 1].Length - fileRoot[fileRoot.Length - 2].Length - 2);
            inputImage.ImageLocation = destination+@"\"+productModel.Image;
        }

        public AddProductForm()
        {
            con = new ConnectDatabase();
            generalInform = new GeneralInForm();
            InitializeComponent();
            inputImage.SizeMode = PictureBoxSizeMode.StretchImage;
            var executingFolder = Directory.GetCurrentDirectory();
            string[] fileRoot = executingFolder.Split(new string[] { "\\" }, StringSplitOptions.None);
            string destination = executingFolder.Substring(0, executingFolder.Length - fileRoot[fileRoot.Length - 1].Length - fileRoot[fileRoot.Length - 2].Length - 2);
            string targetPath = destination + @"\Content\Image\ms-icon-310x310.png";

            inputImage.ImageLocation = targetPath;


            // add datacombobox

            comboBoxBrand.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Brand"), "Chọn nhãn hàng").DataSource;
            comboBoxBrand.ValueMember = "Id";
            comboBoxBrand.DisplayMember = "Name";

            comboBoxScreen.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Screen"), "Chọn màn hình").DataSource;
            comboBoxScreen.ValueMember = "Id";
            comboBoxScreen.DisplayMember = "Name";

            comboBoxColor.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Color"), "Chọn mã màu").DataSource;
            comboBoxColor.ValueMember = "Id";
            comboBoxColor.DisplayMember = "Name";

            comboBoxDesign.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Design"), "Chọn thiết kế").DataSource;
            comboBoxDesign.ValueMember = "Id";
            comboBoxDesign.DisplayMember = "Name";

            comboBoxSize.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Size"), "Chọn kích cỡ").DataSource;
            comboBoxSize.ValueMember = "Id";
            comboBoxSize.DisplayMember = "Name";

            comboBoxCountry.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Country"), "Chọn quốc gia").DataSource;
            comboBoxCountry.ValueMember = "Id";
            comboBoxCountry.DisplayMember = "Name";


            // set id
            textBoxId.Text = "0";
        }

        private void inputImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog res = new OpenFileDialog();

            //Filter
            res.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            //res.InitialDirectory = Directory.GetCurrentDirectory();
            //When the user select the file
            if (res.ShowDialog() == DialogResult.OK)
            {
                //Get the file's path
                var filePath = res.FileName;
                inputImage.Image =Image.FromFile(copyImage(filePath));
                res.Dispose();
            }
        }


        private string copyImage(string filepath)
        {
            // lấy ra file root của solution
            var executingFolder = Directory.GetCurrentDirectory();
            // chia ra lấy đường dẫn
            string []fileRoot = executingFolder.Split(new string[] { "\\" }, StringSplitOptions.None);
            // đường dẫn của file vừa chọn
            string[] file = filepath.Split(new string[] { "\\" }, StringSplitOptions.None);
            // tên file cần copy
            string fileName = file.LastOrDefault().ToString();
            // nguồn file được chọn
            string sourcePath = filepath.Substring(0,filepath.Length - fileName.Length - 1) ;
            // forder cần copy đến
            string destination = executingFolder.Substring(0, executingFolder.Length - fileRoot[fileRoot.Length - 1].Length - fileRoot[fileRoot.Length - 2].Length - 2);

            string targetPath = destination+@"\Content\Image";
          
            string sourceFile = Path.Combine(sourcePath, fileName);
            string destFile = Path.Combine(targetPath, fileName);

            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
                //already to exits file
                File.Copy(sourceFile, destFile, true);
            }
            inputImage.ImageLocation = destFile;
            return destFile;
        }

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

        private void textBoxPrice_MouseClick(object sender, MouseEventArgs e)
        {
            generalInform.FocusTextBox(textBoxPrice, panelPrice, "Nhập giá nhập");

        }

        private void textBoxPrice_Leave(object sender, EventArgs e)
        {
            generalInform.LeaveTextBox(textBoxPrice, panelPrice, "Nhập giá nhập");

        }

        private void textBoxWarranty_MouseClick(object sender, MouseEventArgs e)
        {
            generalInform.FocusTextBox(textBoxWarranty, panelWarranty, "Nhập thời gian bảo hành");
        }

        private void textBoxWarranty_Leave(object sender, EventArgs e)
        {
            generalInform.LeaveTextBox(textBoxWarranty, panelWarranty, "Nhập thời gian bảo hành");
        }


        private void comboBoxBrand_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxBrand_MouseClick(object sender, MouseEventArgs e)
        {
            comboBoxBrand.ForeColor = Color.Black;
        }

        private void comboBoxDesign_MouseClick(object sender, MouseEventArgs e)
        {
            comboBoxDesign.ForeColor = Color.Black;
        }

        private void comboBoxColor_MouseClick(object sender, MouseEventArgs e)
        {
            comboBoxColor.ForeColor = Color.Black;

        }

        private void comboBoxScreen_MouseClick(object sender, MouseEventArgs e)
        {
            comboBoxScreen.ForeColor = Color.Black;

        }

        private void comboBoxSize_MouseClick(object sender, MouseEventArgs e)
        {
            comboBoxSize.ForeColor = Color.Black;
        }

        private void comboBoxCountry_MouseClick(object sender, MouseEventArgs e)
        {
            comboBoxCountry.ForeColor = Color.Black;
        }



        // them mới sản phẩm
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (generalInform.checkTextBoxInForm(panelformAdd))
            {
                string id = textBoxId.Text.ToString();
                string code = textBoxCode.Text.ToString();
                string name = textBoxName.Text.ToString();
                string brand = comboBoxBrand.SelectedValue.ToString();
                string design = comboBoxDesign.SelectedValue.ToString();
                string color = comboBoxColor.SelectedValue.ToString();
                string screen = comboBoxScreen.SelectedValue.ToString();
                string size = comboBoxSize.SelectedValue.ToString();
                string country = comboBoxCountry.SelectedValue.ToString();
                string pricein = textBoxPrice.Text.ToString();
                string warranty = textBoxWarranty.Text.ToString();
                string image = inputImage.ImageLocation.ToString();
                image =image.Substring(image.LastIndexOf('\\'));
                image = "Content\\Image" + image;
                double pri = double.Parse(pricein);
                string priceout = ((pri * 110) / 100).ToString();
                if(id == "0")
                {
                    string sql = "insert into Product(code,name,brand_id,design_id,color_id,screen_id,size_id,country_id,stock,import_price,price,warranty_time,image) values (N'{0}',N'{1}','{2}','{3}','{4}','{5}','{6}','{7}','0','{8}','{9}',N'{10}',N'{11}')";
                    sql = String.Format(sql, code, name, brand, design, color, screen, size, country, pricein, priceout, warranty, image);
                    con.UpdateData(sql);
                    
                }
                else
                {
                    // update ở đây
                    string sql = "update Product set code = N'{0}' , name = '{1}',brand_id = '{2}',design_id = '{3}',color_id = '{4}',screen_id='{5}',size_id = '{6}',country_id = '{7}',import_price = '{8}',price = N'{9}',warranty_time = N'{10}',image=N'{11}' where id = {12}";
                    sql = String.Format(sql, code, name, brand, design, color, screen, size, country, pricein, priceout, warranty, image,id);
                    con.UpdateData(sql);
                }
                
            }
        }

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            generalInform.resetFormUpdate(panelformAdd);
        }

        private void textBoxPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void iconButtonBack_Click(object sender, EventArgs e)
        {
            showHideFrom(new ProductForm());
        }

        public void showHideFrom(UserControl form)
        {
            if (!this.Controls.Contains(form))
            {
                form.Width = this.Width;
                form.Height = this.Height;
                this.Controls.Add(form);
                this.Dispose();
                form.Dock = DockStyle.Fill;
                form.BringToFront();
            }
            else
                form.BringToFront();
        }

        private void panelformAdd_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
