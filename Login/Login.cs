//Jose Velazquez
//Account with Excel

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;

namespace Login
{
    public partial class Login : Form
    {
        //For all email that are in excel
        public List<string> listEmail = new List<string>();
        //For all email that are in excel
        public List<string> listPassword = new List<string>(); 
        
        private void readExcel()
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
            //Search the range what do you want
            Range columns = ws.Columns["A:A"]; //Range["A2:A6"]; //[Colums , Row]

            //Add to list all content in the colums (Email and Password)
            foreach (string Result in columns.Value)
            {
                listEmail.Add(Result);
            }
            columns = ws.Columns["B:B"];
            foreach (string Result in columns.Value)
            {
                listPassword.Add(Result);
            }

            //Close Book
            wb.Close(true);
            //Close Excel 
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
            //Add Text to string
            string email = tbx_Email.Text;
            string password = tbx_password.Text;


            bool success = false;
            int numEmail = 0;

            //Verification of the existence of e-mail
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
                //Verification of the existence of password
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
            //For Change form to another form

            //Create other form var
            CreateAccount Form2 = new CreateAccount();
            //Hide this form
            this.Hide();
            //Show the other form
            Form2.Show();
        }
    }
}
