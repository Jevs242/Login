using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;

namespace Login
{
    public partial class Login : Form
    {
        string path = Directory.GetCurrentDirectory() + "\\Accounts.xlsx";
        Excel.Application excel = new Excel.Application();
        Workbook wb;
        Worksheet ws;
        public List<string> listEmail = new List<string>();
        public List<string> listPassword = new List<string>();
        
        private void readExcel()
        {
            

            wb = excel.Workbooks.Open(path);

            ws = wb.Worksheets[1];

            Range columns = ws.Columns["A:A"];   //Range["A2:A6"]; //[Colums , Row]

            foreach (string Result in columns.Value)
            {
                listEmail.Add(Result);
            }

            columns = ws.Columns["B:B"];
            foreach (string Result in columns.Value)
            {
                listPassword.Add(Result);
            }

            wb.Close(true);
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
        }


        public Login()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            readExcel();
        }

        private void btn_Summit_Click(object sender, EventArgs e)
        {
            string email = tbx_Email.Text;
            string password = tbx_password.Text;
            bool success = false;
            int numEmail = 0;

            for (int i = 0; i < listEmail.Count; i++)
            {
                if (listEmail[i] == "Email")
                {
                    continue;
                }

                if (listEmail[i] == email.ToLower())
                {
                    success = true;
                    numEmail = i;
                    break;
                }
            }
            if (success)
            {
                if (password == listPassword[numEmail])
                {
                    success = true;
                }
                else
                {
                    success = false;
                }
            }
            if (success)
            {
                lbl_Error.Text = "Success to sign in";
                lbl_Error.ForeColor = Color.DeepSkyBlue;
            }
            else
            {
                lbl_Error.Text = "Failure to sign in";
                lbl_Error.ForeColor = Color.Firebrick;
            }

        }

        private void btn_signup_Click(object sender, EventArgs e)
        {
            CreateAccount Form2 = new CreateAccount();
            this.Hide();
            Form2.Show();
        }

        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

    }


}
