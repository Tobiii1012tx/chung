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
    public partial class FormReportSupplierByMonth : UserControl
    {
        ConnectDatabase con;
        GeneralInForm GeneralInForm;
        public FormReportSupplierByMonth()
        {
            InitializeComponent(); 
            con = new ConnectDatabase();
            GeneralInForm = new GeneralInForm();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (comboBoxMonth.SelectedIndex == -1)
            {
                MessageBox.Show("chưa chọn quý");
            }
            else
            {
                int value = comboBoxMonth.SelectedIndex;
                string sql = "select bi.ID,e.NAME as name, bi.CREATED_DATE,bi.TOTAL from BILL_IMPORT bi join EMPLOYEE e on bi.EMPLOYEE_ID = e.ID ";
                string condition = "";
                if(value == 0)
                {
                    condition = "where MONTH(bi.CREATED_DATE) >=1 and MONTH(bi.CREATED_DATE) <=3 and YEAR(bi.CREATED_DATE) = YEAR(GETDATE()) ";
                }                                                
                if (value == 1)                                  
                {                                                
                    condition = "where MONTH(bi.CREATED_DATE) >=4 and MONTH(bi.CREATED_DATE) <=6 and YEAR(bi.CREATED_DATE)  = YEAR(GETDATE()) ";
                }                                                 
                if (value == 2)                                   
                {                                                 
                    condition = "where MONTH(bi.CREATED_DATE) >=7 and MONTH(bi.CREATED_DATE) <=9 and YEAR(bi.CREATED_DATE)  = YEAR(GETDATE()) ";
                }                                                 
                if (value == 3)                                   
                {                                                 
                    condition = "where MONTH(bi.CREATED_DATE) >=10 and  MONTH(bi.CREATED_DATE) <=12 and YEAR(bi.CREATED_DATE)  = YEAR(GETDATE()) ";
                }
                sql += condition;
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
                worKsheeT.Cells[2, 2] = comboBoxMonth.Text;

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
                worKsheeT.Cells[i, 1] = "Tổng trị giá : " + total.ToString();

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
