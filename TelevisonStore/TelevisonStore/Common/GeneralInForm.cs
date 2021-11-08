using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelevisonStore.Common
{
    class GeneralInForm
    {
        // fous textView
        public void FocusTextBox(TextBox txtBox, Panel panel, String strtemp)
        {
            if (txtBox.Text.Equals(strtemp) || txtBox.Text.Equals("Bạn không được để trống !"))
            {
                txtBox.Text = "";
            }
            txtBox.ForeColor = Color.Black;
            if (panel != null)
            panel.BackColor = Color.FromArgb(0, 165, 145);
        }

        public void LeaveTextBox(TextBox txtBox, Panel panel, String strtemp)
        {
            if (txtBox.Text.Equals(""))
            {
                txtBox.ForeColor = Color.LightGray;
                txtBox.Text = strtemp;
            }
            if(panel != null)
                panel.BackColor = Color.FromArgb(0, 155, 119);
        }


        // kiểm tra input text trong panel có rỗng hay không
        // chưa xong
        public bool checkTextBoxInForm(Panel panel)
        {
            int check = 0;
            foreach (var x in panel.Controls)
            {
                if (x is TextBox)
                {
                    if ((((TextBox)x).ForeColor != Color.Black || ((TextBox)x).Text == "") && ((TextBox)x).Enabled == true)
                    {
                        check++;
                        ((TextBox)x).Text = "Bạn không được để trống !";
                        ((TextBox)x).ForeColor = Color.Red;
                    }
                }
                if (x is ComboBox)
                {
                    if (((ComboBox)x).SelectedIndex == 0)
                    {
                        check++;
                        ((ComboBox)x).ForeColor = Color.Red;
                    }
                    else
                    {
                        // đã chọn rồi 
                    }
                }
            }
            return check == 0 ? true : false;
        }

        public void resetFormUpdate(Panel panel)
        {
            foreach (var x in panel.Controls)
            {
                if (x is TextBox)
                {
                    if (((TextBox)x).Enabled == true)
                    {
                        ((TextBox)x).Text = "";
                    }
                    else
                    {
                        ((TextBox)x).Text = "0";
                    }
                }
                else if (x is ComboBox)
                {
                    ((ComboBox)x).SelectedIndex = 0;
                }
                else if (x is RadioButton)
                {
                    ((RadioButton)x).Checked = true;
                }
                else if (x is DateTimePicker)
                {
                    ((DateTimePicker)x).Value = DateTime.Now;
                }
            }
        }


        public bool checkMail(string mail)
        {
            return  Regex.IsMatch(mail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }
    }
}
