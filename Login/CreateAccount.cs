using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;
using System.IO;

namespace Login
{
    public partial class CreateAccount : Form
    {
        string path = Directory.GetCurrentDirectory() + "\\Accounts.xlsx";
        Excel.Application excel = new Excel.Application();
        Workbook wb;
        Worksheet ws;
        List<string> listEmail = new List<string>();
        List<string> listPassword = new List<string>();
        static Regex ValidEmailRegex = CreateValidEmailRegex();
        public CreateAccount()
        {
            InitializeComponent();
            Login F1 = new Login();
            listEmail = F1.listEmail;
            listPassword = F1.listPassword;
        }

        private void btn_Summit_Click(object sender, EventArgs e)
        {
            string email = tbx_Email.Text;
            string password = tbx_password.Text;

            bool success = true;

            if (EmailIsValid(email))
            {
                for (int i = 0; i < listEmail.Count; i++)
                {
                    if (listEmail[i] == "Email")
                    {
                        continue;
                    }

                    if (listEmail[i] == email.ToLower())
                    {
                        lbl_Error.Text = "This email address is already used";
                        success = false;
                        break;
                    }
                }
                if (success)
                {
                    if (ValidatePassword(password))
                    {
                        if (tbx_password.Text == tbx_ConfirmPassword.Text)
                        {
                            
                            listEmail.Add(email);
                            listEmail.Add(password);
                            addData(email , password);
                            lbl_Error.Text = "Account Created";
                            lbl_Error.ForeColor = Color.DeepSkyBlue;
                        }
                        else
                        {
                            lbl_Error.Text = "The Password is not same";
                        }
                    }
                    else
                    {
                        lbl_Error.Text = "Password Invalid";
                    }

                    
                }
            }
            else
            {
                lbl_Error.Text = "Email Invalid";
            }



        }

        private static Regex CreateValidEmailRegex()
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
        }

        internal static bool EmailIsValid(string emailAddress)
        {
            return ValidEmailRegex.IsMatch(emailAddress);
        }

        static bool ValidatePassword(string passWord)
        {
            int validConditions = 0;
            foreach (char c in passWord)
            {
                if (c >= 'a' && c <= 'z')
                {
                    validConditions++;
                    break;
                }
            }
            foreach (char c in passWord)
            {
                if (c >= 'A' && c <= 'Z')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 0) return false;
            foreach (char c in passWord)
            {
                if (c >= '0' && c <= '9')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 1) return false;
            if (validConditions == 2)
            {
                char[] special = { '@', '#', '$', '%', '^', '&', '+', '=' };
                if (passWord.IndexOfAny(special) == -1) return false;
            }
            return true;
        }

        private void btn_signup_Click(object sender, EventArgs e)
        {
            Login F1 = new Login();
            this.Hide();
            F1.Show();
        }

        public void addData(string email , string password)
        {
            wb = excel.Workbooks.Open(path);

            ws = wb.Worksheets[1];

            ws.Cells[9, 1] = email;
            ws.Cells[9, 2] = password;

            wb.Close(true);
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
        }

    }
}
