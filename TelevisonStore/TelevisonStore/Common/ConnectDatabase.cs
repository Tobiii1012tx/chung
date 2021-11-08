using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TelevisonStore.DesignForm.Messeage;
using Dapper;

namespace TelevisonStore.Common
{
    class ConnectDatabase
    {
        public string connectionString = @"Data Source=KHUATCHUNG\KHUATCHUNG;Initial Catalog=STORE_TELEVISION;Integrated Security=True";
        public SqlConnection con { get; set; }

        public void Connection()
        {
            con = new SqlConnection(connectionString);
            if(con.State != ConnectionState.Open)
            {
                con.Open();
            }
        }

        public void DisConnection()
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
            con.Dispose();
        }

        public DataTable getData(string sql)
        {

            DataTable dt = new DataTable();
            // mở chuỗi kết nối
            Connection();
            SqlDataAdapter sqlData = new SqlDataAdapter(sql, con);
            sqlData.Fill(dt);
            // đóng chuỗi kết nối
            DisConnection();
            return dt;
        }


        // lấy tổng số bản ghi
        public int GetTotal(string sql)
        {
            Connection();
            SqlCommand sqlCommand = new SqlCommand(sql, con);
            var res =  sqlCommand.ExecuteScalar();
            DisConnection();
            return Int16.Parse(res.ToString());
        }

        public double GetPrice(string sql)
        {
            Connection();
            SqlCommand sqlCommand = new SqlCommand(sql, con);
            var res = sqlCommand.ExecuteScalar();
            DisConnection();
            return double.Parse(res.ToString());
        }


        // thêm sửa xóa data
        public void UpdateData(string sql)
        {
            Connection();
            SqlCommand sqlCommand = new SqlCommand(sql,con);
            try
            {
                sqlCommand.ExecuteNonQuery();
                DisConnection();
                Success success = new Success();
                success.StartPosition = FormStartPosition.CenterParent;
                success.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Đã có lỗi xảy ra khi cập nhật dữ liệu !!", "Thông báo");
                DisConnection();
            }
        }

        public void UpdateData(string sql,bool check)
        {
            Connection();
            SqlCommand sqlCommand = new SqlCommand(sql, con);
            try
            {
                sqlCommand.ExecuteNonQuery();
                DisConnection();
            }
            catch
            {
                DisConnection();
            }
        }


        public ComboBox convertDataTableToComboBox(DataTable dt,string display)
        {
            DataRow dr = dt.NewRow();
            dr["Name"] = display;
            dr["ID"] = 0;
            dt.Rows.InsertAt(dr, 0);
            ComboBox cmb = new ComboBox();
            cmb.DataSource = dt;
            return cmb;
        }
    }
}
