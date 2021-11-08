using System;
using System.Data;
using System.Windows.Forms;
using TelevisonStore.Common;
using Excel = Microsoft.Office.Interop.Excel;

namespace TelevisonStore.DesignForm
{
    public partial class FormReportByCustomer : UserControl
    {
        ConnectDatabase con;
        GeneralInForm GeneralInForm;
        public FormReportByCustomer()
        {
            con = new ConnectDatabase();
            GeneralInForm = new GeneralInForm();
            InitializeComponent();
            comboBoxCustomer.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Customer"), "Chọn khách hàng").DataSource;
            comboBoxCustomer.ValueMember = "Id";
            comboBoxCustomer.DisplayMember = "Name";
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (comboBoxCustomer.SelectedIndex == 0)
            {
                MessageBox.Show("chưa chọn khách hàng");
            }
            else
            {
                string value = comboBoxCustomer.SelectedValue.ToString();
                string sql = "select top 3 p.NAME as Name,sum(bx.STOCK) as Sum from BILL_EXPORT be"
                             + " join BILL_EXPORT_DETAIL bx on be.ID = bx.BE_ID"
                             + " join PRODUCT p on p.ID = bx.PRODUCT_ID"
                             + " where be.Customer_Id = " + value
                             + " group by p.NAME"
                             + " order by sum(bx.STOCK) desc";
                var dt = con.getData(sql);

                var excel = new Excel.Application();
                var worKbooK = excel.Workbooks.Add(Type.Missing);
                var worKsheeT = (Excel.Worksheet)worKbooK.ActiveSheet;
                worKsheeT.Name = "Top sản phẩm theo khách hàng";
                worKsheeT.Range[worKsheeT.Cells[1, 1], worKsheeT.Cells[1, 8]].Merge();
                // Họ tên khách hàng
                worKsheeT.Range[worKsheeT.Cells[2, 1], worKsheeT.Cells[2, 2]].Merge();
                worKsheeT.Range[worKsheeT.Cells[2, 3], worKsheeT.Cells[2, 4]].Merge();

                worKsheeT.Cells[1,1] = "Báo cáo theo sản phẩm top theo khách hàng";
                worKsheeT.Cells[2,1] = "Họ Tên : ";
                worKsheeT.Cells[2, 2] = comboBoxCustomer.Text;

                worKsheeT.Cells[4, 1] = "Tên sản phẩm";
                worKsheeT.Cells[4, 2] = "Số lượng";

                int i = 5;
                foreach(DataRow row in dt.Rows)
                {
                    worKsheeT.Cells[i, 1] = row["Name"].ToString();
                    worKsheeT.Cells[i, 2] = row["Sum"].ToString();
                    i++;
                }


                worKbooK.Activate(); //Kích hoạt file Excel//Thiết lập các thuộc tính của 
                SaveFileDialog dlgSave = new SaveFileDialog();
                    dlgSave.Filter = "Excel Document(*.xls)|*.xls  |Word Document(*.doc) |*.doc|All files(*.*)|*.*";
                dlgSave.FilterIndex = 1;
                dlgSave.AddExtension = true;
                dlgSave.DefaultExt = ".xlsx";
                if(dlgSave.ShowDialog() == DialogResult.OK)
                    worKbooK.SaveAs(dlgSave.FileName.ToString());//Lưu file
                worKbooK.Close(true);
                excel.Quit();
            }
        }

        private void FormReportByCustomer_Load(object sender, EventArgs e)
        {

        }
    }
}
