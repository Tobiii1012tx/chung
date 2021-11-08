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
using Excel = Microsoft.Office.Interop.Excel;

namespace TelevisonStore.DesignForm
{
    public partial class FormReportBySupplier : UserControl
    {
        ConnectDatabase con;
        GeneralInForm GeneralInForm;
        public FormReportBySupplier()
        {
            con = new ConnectDatabase();
            GeneralInForm = new GeneralInForm();
            InitializeComponent();
            comboBoxSupplier.DataSource = con.convertDataTableToComboBox(con.getData("select Id,Name from Supplier"), "Chọn nhà cung cấp").DataSource;
            comboBoxSupplier.ValueMember = "Id";
            comboBoxSupplier.DisplayMember = "Name";
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (comboBoxSupplier.SelectedIndex == 0)
            {
                MessageBox.Show("chưa chọn khách hàng");
            }
            else
            {
                string value = comboBoxSupplier.SelectedValue.ToString();
                string sql = "select bi.ID,e.NAME as name, bi.CREATED_DATE,bi.TOTAL from BILL_IMPORT bi join EMPLOYEE e on bi.EMPLOYEE_ID = e.ID where bi.SUPPLIER_ID = " + value;
                var dt = con.getData(sql);

                var excel = new Excel.Application();
                var worKbooK = excel.Workbooks.Add(Type.Missing);
                var worKsheeT = (Excel.Worksheet)worKbooK.ActiveSheet;
                worKsheeT.Name = "Danh sách hóa đơn";
                worKsheeT.Range[worKsheeT.Cells[1, 1], worKsheeT.Cells[1, 8]].Merge();
                // Họ tên khách hàng
                worKsheeT.Range[worKsheeT.Cells[2, 1], worKsheeT.Cells[2, 2]].Merge();
                worKsheeT.Range[worKsheeT.Cells[2, 3], worKsheeT.Cells[2, 4]].Merge();

                worKsheeT.Cells[1, 1] = "Danh sách hóa đơn của nhà cung cấp";
                worKsheeT.Cells[2, 1] = "Đơn vị : ";
                worKsheeT.Cells[2, 2] = comboBoxSupplier.Text;

                worKsheeT.Cells[4, 1] = "Mã hóa đơn";
                worKsheeT.Cells[4, 2] = "Tên Nhân viên";
                worKsheeT.Cells[4, 3] = "Ngày tạo";
                worKsheeT.Cells[4, 4] = "Tổng tiền";

                int i = 5;
                double total = 0;
                foreach (DataRow row in dt.Rows)
                {
                    worKsheeT.Cells[i, 1] = row["Id"].ToString();
                    worKsheeT.Cells[i, 2] = row["Name"].ToString();
                    worKsheeT.Cells[i, 3] = row["Created_date"].ToString();
                    worKsheeT.Cells[i, 4] = row["Total"].ToString();
                    total += double.Parse(row["Total"].ToString());
                    i++;
                }
                i++;
                worKsheeT.Range[worKsheeT.Cells[i, 1], worKsheeT.Cells[i, 4]].Merge();
                worKsheeT.Cells[i, 1] ="Tổng trị giá : "+ total.ToString();

                worKbooK.Activate(); //Kích hoạt file Excel//Thiết lập các thuộc tính của 
                SaveFileDialog dlgSave = new SaveFileDialog();
                dlgSave.Filter = "Excel Document(*.xls)|*.xls  |Word Document(*.doc) |*.doc|All files(*.*)|*.*";
                dlgSave.FilterIndex = 1;
                dlgSave.AddExtension = true;
                dlgSave.DefaultExt = ".xlsx";
                if (dlgSave.ShowDialog() == DialogResult.OK)
                    worKbooK.SaveAs(dlgSave.FileName.ToString());//Lưu file
                worKbooK.Close(true);
                excel.Quit();
            }
        }
    }
}
