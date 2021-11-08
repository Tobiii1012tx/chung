using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using TelevisonStore.Common;

namespace TelevisonStore.DesignForm
{
    public partial class FormReportTopSupplierbyMonth : UserControl
    {
        ConnectDatabase con;
        GeneralInForm GeneralInForm;
        public FormReportTopSupplierbyMonth()
        {
            con = new ConnectDatabase();
            GeneralInForm = new GeneralInForm();
            InitializeComponent();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (comboBoxMonth.SelectedIndex == -1)
            {
                MessageBox.Show("chưa chọn quý");
            }
            else
            {
                int value = comboBoxMonth.SelectedIndex+1;
                string sql = "select top 5 s.NAME , SUM(bd.STOCK) as sum from BILL_IMPORT bi join BILL_IMPORT_DETAIL bd on bi.ID = bd.BI_ID join SUPPLIER s on s.ID = bi.SUPPLIER_ID "
                             + "where MONTH(bi.CREATED_DATE) = "+value 
                             + " and YEAR(bi.CREATED_DATE) = YEAR(GETDATE()) group by s.NAME order by SUM(bd.STOCK) desc";


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

                worKsheeT.Cells[4, 1] = "Tên Nhà cung cấp";
                worKsheeT.Cells[4, 2] = "Số lượng";
                int i = 5;
                foreach (DataRow row in dt.Rows)
                {
                    worKsheeT.Cells[i, 1] = row["name"].ToString();
                    worKsheeT.Cells[i, 2] = row["sum"].ToString();
                    i++;
                }
                
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
