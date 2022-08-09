//Jose Velazquez
//Account with Excel

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
        //For all email that are in excel
        List<string> listEmail = new List<string>();
        //For all email that are in excel
        List<string> listPassword = new List<string>();
        //For Valid the email
        static Regex ValidEmailRegex = CreateValidEmailRegex();
        public CreateAccount()
        {
            InitializeComponent();

            //Set the list var in this form with the other form 
            Login F1 = new Login();
            listEmail = F1.listEmail;
            listPassword = F1.listPassword;
        }

        private void btn_Summit_Click(object sender, EventArgs e)
        {
            //Add Text to string
            string email = tbx_Email.Text;
            string password = tbx_password.Text;

            bool success = true;

            if (EmailIsValid(email))
            {
                //Verification that not exist another with same email
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
                        //Vefication that the password are the same passaword
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
            //For Change form to another form

            //Create other form var
            Login F1 = new Login();
            //Hide this form
            this.Hide();
            //Show the other form
            F1.Show();
        }

        public void addData(string email , string password)
        {
            //Get the exact location of Excel
            string path = Directory.GetCurrentDirectory() + "\\Accounts.xlsx";
            //The application Excel
            Excel.Application excel = new Excel.Application();
            //Book Excel
            Workbook wb;
            //Sheet Excel
            Worksheet ws;
            //Open Excel
            wb = excel.Workbooks.Open(path);
            //Which Sheets is to be opened
            ws = wb.Worksheets[1];
            //Search the cells what do you want put the value
            ws.Cells[9, 1] = email;
            ws.Cells[9, 2] = password;
            //Close Book
            wb.Close(true);
            //Close Excel
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
        }

    }
}
