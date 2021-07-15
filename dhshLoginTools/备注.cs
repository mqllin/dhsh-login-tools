using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace dhshLoginTools
{
    public partial class 备注 : Form
    {
        public string callback = string.Empty;
        dashLoginTools All = new dashLoginTools();
        public 备注()
        {
            InitializeComponent();

            textBox1.Text = All.passback.ToString();
         
        }
        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_CLOSE = 0xF060;
            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_CLOSE)
            { return; }
            base.WndProc(ref m);
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if (string.IsNullOrEmpty((textBox1).Text))
            {


                callback = All.label3.Text;
            }
            else
            {
                callback = textBox1.Text;
            }
            this.Close();

        }

     
    }
}
