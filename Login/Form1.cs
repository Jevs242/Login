using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;

namespace Login
{
    public partial class Form1 : Form
    {
        List<string> listEmail = new List<string>();
        List<string> listPassword = new List<string>();

        private void readExcel()
        {
            string path = Directory.GetCurrentDirectory() + "\\Accounts.xlsx";
            Excel.Application excel = new Excel.Application();
            Workbook wb;
            Worksheet ws;

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

            //MessageBox.Show(cellValue);
        }


        public Form1()
        {
            InitializeComponent();
            readExcel();
        }
        

        public void OpenFile()
        {
           //MessageBox.Show(cell.)
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_Summit_Click(object sender, EventArgs e)
        {
            string email = tbx_Email.Text;
            string password = tbx_password.Text;
            bool success = false; 

            for(int i = 0; i < listEmail.Count; i++)
            {
                if(listEmail[i] == "Email")
                {
                    continue;
                }

                if(listEmail[i] == email.ToLower())
                {
                    //lbl_Error.Text = "This email address is already used";
                    success = true;
                    break;
                }
            }
            if(success)
            {
                for (int i = 0; i < listPassword.Count; i++)
                {
                    if (listEmail[i] == "Password")
                    {
                        continue;
                    }

                    if (listPassword[i] == password)
                    {
                        success = true;
                        break;
                    }
                    else
                    {
                        success = false;
                    }
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
    }

    
}
