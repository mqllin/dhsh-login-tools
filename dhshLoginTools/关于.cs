using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace dhshLoginTools
{
    public partial class 关于 : Form
    {
        public 关于()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 163 && this.ClientRectangle.Contains(this.PointToClient(new Point(m.LParam.ToInt32()))) && m.WParam.ToInt32() == 2)
                m.WParam = (IntPtr)1;
            base.WndProc(ref m);
            if (m.Msg == 132 && m.Result.ToInt32() == 1)
                m.Result = (IntPtr)2;
        }

        private void 关于_Load(object sender, EventArgs e)
        {

        }
    }
}
