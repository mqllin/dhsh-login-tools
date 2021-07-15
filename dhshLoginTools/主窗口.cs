using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;//引用命名空间
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace dhshLoginTools
{
    public partial class dashLoginTools : Form
    {
        public bool GameOpen;
        public bool RunLogin;

        public string passback = string.Empty;


        public dashLoginTools()
        {
            InitializeComponent();
            start();
            
            getChooseFromIni();
        }

        [DllImport("USER32.DLL")]//引入user32.dll
        public static extern IntPtr PostMessage(IntPtr hwnd, int msg, int wparam, int lparam);

        [DllImport("user32.dll")]
        public static extern int EnumChildWindows(IntPtr hWndParent, CallBack lpfn, int lParam);
        [DllImport("user32.dll", EntryPoint = "FindWindowEx", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, uint hwndChildAfter, string lpszClass, string lpszWindow);


        //  释放按键的常量
        public delegate bool CallBack(IntPtr hwnd, int lParam);
        //找到窗口（进程名称 可空，窗口名称）
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpCkassName, string lpWindowName);
        //把窗口放到最前面
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        //模拟键盘
        [DllImport("User32.dll")]
        public static extern void keybd_event(Byte bVk, Byte bScan, Int32 dwFlags, Int32 dwExtraInfo);
        [DllImport("user32", EntryPoint = "GetWindow")]
        public static extern IntPtr GetWindow(IntPtr hwnd, int wFlag);

        //  释放按键的常量
        private const int KEYEVENTF_KEYUP = 2;
        //发送消息
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        //获取窗口大小
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindoRect(IntPtr hWnd, ref Rectangle lpRect);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern int ShowWindow(int hwnd, int nCmdShow);
        private const int SW_HIDE = 0;
        private const int SW_NORMAL = 1;
        private const int SW_MINIMIZED = 2;
        private const int SW_MAXIMIZE = 3;
        private const int SW_SHOWNOACTIVATE = 4;
        private const int SW_SHOW = 5;
        private const int SW_MINIMIZE = 6;
        private const int SW_RESTORE = 9;
        private const int SW_SHOWDEFAULT = 10;

        [StructLayout(LayoutKind.Sequential)]//获取窗口坐标
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

        }

        //显示窗口

        //


        #region SendMessage 参数
        private const int WM_KEYDOWN = 0X100;
        private const int WM_KEYUP = 0X101;
        private const int WM_SYSCHAR = 0X106;
        private const int WM_SYSKEYUP = 0X105;
        private const int WM_SYSKEYDOWN = 0X104;
        private const int WM_CHAR = 0X102;
        #endregion

        //鼠标实践
        private readonly int MOUSEEVENTF_LEFTDOWN = 0x2;
        private readonly int MOUSEEVENTF_LEFTUP = 0x4;
        [DllImport("user32")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int dwdata, int dwExtraInfo);
        String path, path2; /*= "D:\\tools\\dhsh\\launch.exe"*/

        #region SendMessage 参数

        private const int WM_LBUTTONDOWN = 0X201;
        private const int WM_LBUTTONUP = 0X202;

        #endregion




        public void InputStr(IntPtr k, string input)
        {
            //不能发送汉字，只能发送键盘上有的内容也可以模拟shift+!
            byte[] ch = (ASCIIEncoding.ASCII.GetBytes(input));
            for (int i = 0; i < ch.Length; i++)
            {
                SendMessage(k, WM_CHAR, ch[i], 0);
            }
        }
        //----------------新版本登陆-----------------------------------
        void LoginGmae(string name,string pass)
        {
           
                if (Rbtn1.Checked)
                {
                   
                    Utils nu = new Utils();
                    if (nu.LoginGameMode1(name, pass))
                    {
                        label_tips.Text = name + "登陆成功！";
                    }
                    else
                     {
                        label_tips.Text = "没有找到游戏，登陆失败！";
                     }
        }


            if (Rbtn2.Checked)
            {
               
                Utils nu = new Utils();
                if (nu.LoginGameMode2(name, pass))
                {
                    label_tips.Text = name + "登陆成功！";
                }
                else
                {
                    label_tips.Text = "没有找到游戏，登陆失败！";
                }
            }

            //if (Rbtn3.Checked)
            //{
               
            //    Function nu = new Function();
            //    if (nu.LoginGameMode3(name, pass))
            //    {
            //        label17.Text = name + "登陆成功！";
            //    }
            //    else
            //    {
            //        label17.Text = "没有找到游戏，登陆失败！";
            //    }
            //}


        }
        //----------------新版本登陆-----------------------------------
       
      

        private void button1_Click(object sender, EventArgs e)
        {
            if ((tbname1.Text.Length > 0) && (tbpaw1.Text.Length > 0))
            {
                string name = tbname1.Text;
                string pass = tbpaw1.Text;
                Utils nu = new Utils();
                nu.OpenGame();
             
                var thread = new System.Threading.Thread(new System.Threading.ThreadStart(()
      =>
                {
                    Thread.Sleep(5000);
                    for (int i = 0; i < 15; i++)
                    {
                       
                        IntPtr k = FindWindow("fsgamehero0150", null);
                        if (k.ToString() != "0")
                        {
                           
                            LoginGmae(name, pass);
                            break;
                        }
                        Thread.Sleep(1000);
                        if (i == 15)
                        {
                            label_tips.Text = "等了15秒都没有找到游戏呢！";
                            break;
                        }

                    }
                    //这里面写你的代码，一般会吧比较耗时的方法放在后台里执行。
                }))
                { IsBackground = true };
                thread.Start();
            }
            else
            {
                label_tips.Text = "请输入账号密码！";
            }
        }

       public void start()
        {
            string Current;

            Current = Directory.GetCurrentDirectory();//获取当前根目录  
            Ini ini = new Ini(Current + "/" + comboBox1.Text + ".ini");
            if (File.Exists(@"" + Current + "/" + comboBox1.Text + ".ini"))
            {
                tbname1.Text = ini.ReadValue("Setting", "key1");
                tbpaw1.Text = ini.ReadValue("Setting", "paw1");
                label3.Text = ini.ReadValue("Name", "username1");

                tbname2.Text = ini.ReadValue("Setting", "key2");
                tbpaw2.Text = ini.ReadValue("Setting", "paw2");
                label4.Text = ini.ReadValue("Name", "username2");

                tbname3.Text = ini.ReadValue("Setting", "key3");
                tbpaw3.Text = ini.ReadValue("Setting", "paw3");
                label5.Text = ini.ReadValue("Name", "username3");

                tbname4.Text = ini.ReadValue("Setting", "key4");
                tbpaw4.Text = ini.ReadValue("Setting", "paw4");
                label6.Text = ini.ReadValue("Name", "username4");


                tbname5.Text = ini.ReadValue("Setting", "key5");
                tbpaw5.Text = ini.ReadValue("Setting", "paw5");
                label7.Text = ini.ReadValue("Name", "username5");

                tbname6.Text = ini.ReadValue("Setting", "key6");
                tbpaw6.Text = ini.ReadValue("Setting", "paw6");
                label8.Text = ini.ReadValue("Name", "username6");

                tbname7.Text = ini.ReadValue("Setting", "key7");
                tbpaw7.Text = ini.ReadValue("Setting", "paw7");
                label9.Text = ini.ReadValue("Name", "username7");


                tbname8.Text = ini.ReadValue("Setting", "key8");
                tbpaw8.Text = ini.ReadValue("Setting", "paw8");
                label10.Text = ini.ReadValue("Name", "username8");


                tbname9.Text = ini.ReadValue("Setting", "key9");
                tbpaw9.Text = ini.ReadValue("Setting", "paw9");
                label11.Text = ini.ReadValue("Name", "username9");


                tbname10.Text = ini.ReadValue("Setting", "key10");
                tbpaw10.Text = ini.ReadValue("Setting", "paw10");
                label12.Text = ini.ReadValue("Name", "username10");

                tbbz1_1.Text = ini.ReadValue("Notes1", "notes1");
                tbbz1_2.Text = ini.ReadValue("Notes1", "notes2");
                tbbz1_3.Text = ini.ReadValue("Notes1", "notes3");
                tbbz1_4.Text = ini.ReadValue("Notes1", "notes4");
                tbbz1_5.Text = ini.ReadValue("Notes1", "notes5");
                tbbz1_6.Text = ini.ReadValue("Notes1", "notes6");
                tbbz1_7.Text = ini.ReadValue("Notes1", "notes7");
                tbbz1_8.Text = ini.ReadValue("Notes1", "notes8");
                tbbz1_9.Text = ini.ReadValue("Notes1", "notes9");
                tbbz1_10.Text = ini.ReadValue("Notes1", "notes10");

                tbbz2_1.Text = ini.ReadValue("Notes1", "notes1");
                tbbz2_2.Text = ini.ReadValue("Notes1", "notes2");
                tbbz2_3.Text = ini.ReadValue("Notes1", "notes3");
                tbbz2_4.Text = ini.ReadValue("Notes1", "notes4");
                tbbz2_5.Text = ini.ReadValue("Notes1", "notes5");
                tbbz2_6.Text = ini.ReadValue("Notes1", "notes6");
                tbbz2_7.Text = ini.ReadValue("Notes1", "notes7");
                tbbz2_8.Text = ini.ReadValue("Notes1", "notes8");
                tbbz2_9.Text = ini.ReadValue("Notes1", "notes9");
                tbbz2_10.Text = ini.ReadValue("Notes1", "notes10");


                label_tips.Text = "读取成功！";
            }
            else
            {

                // 写入ini  
                                              
                ini.Writue("Setting", "key1", tbname1.Text);
                ini.Writue("Setting", "paw1", tbpaw1.Text);
                ini.Writue("Name", "username1", label3.Text);
                
                ini.Writue("Setting", "key2", tbname2.Text);
                ini.Writue("Setting", "paw2", tbpaw2.Text);
                ini.Writue("Name", "username2", label4.Text);

                ini.Writue("Setting", "key3", tbname3.Text);
                ini.Writue("Setting", "paw3", tbpaw3.Text);
                ini.Writue("Name", "username3", label5.Text);

                ini.Writue("Setting", "key4", tbname4.Text);
                ini.Writue("Setting", "paw4", tbpaw4.Text);
                ini.Writue("Name", "username4", label6.Text);

                ini.Writue("Setting", "key5", tbname5.Text);
                ini.Writue("Setting", "paw5", tbpaw5.Text);
                ini.Writue("Name", "username5", label7.Text);

                ini.Writue("Setting", "key6", tbname6.Text);
                ini.Writue("Setting", "paw6", tbpaw6.Text);
                ini.Writue("Name", "username6", label8.Text);

                ini.Writue("Setting", "key7", tbname7.Text);
                ini.Writue("Setting", "paw7", tbpaw7.Text);
                ini.Writue("Name", "username7", label9.Text);

                ini.Writue("Setting", "key8", tbname8.Text);
                ini.Writue("Setting", "paw8", tbpaw8.Text);
                ini.Writue("Name", "username8", label10.Text);

                ini.Writue("Setting", "key9", tbname9.Text);
                ini.Writue("Setting", "paw9", tbpaw9.Text);
                ini.Writue("Name", "username9", label11.Text);

                ini.Writue("Setting", "key10", tbname10.Text);
                ini.Writue("Setting", "paw10", tbpaw10.Text);
                ini.Writue("Name", "username10", label12.Text);

                ini.Writue("Notes1", "notes1", tbbz1_1.Text);
                ini.Writue("Notes1", "notes2", tbbz1_2.Text);
                ini.Writue("Notes1", "notes3", tbbz1_3.Text);
                ini.Writue("Notes1", "notes4", tbbz1_4.Text);
                ini.Writue("Notes1", "notes5", tbbz1_5.Text);
                ini.Writue("Notes1", "notes6", tbbz1_6.Text);
                ini.Writue("Notes1", "notes7", tbbz1_7.Text);
                ini.Writue("Notes1", "notes8", tbbz1_8.Text);
                ini.Writue("Notes1", "notes9", tbbz1_9.Text);
                ini.Writue("Notes1", "notes10", tbbz1_10.Text);

                ini.Writue("Notes2", "notes1", tbbz2_1.Text);
                ini.Writue("Notes2", "notes2", tbbz2_2.Text);
                ini.Writue("Notes2", "notes3", tbbz2_3.Text);
                ini.Writue("Notes2", "notes4", tbbz2_4.Text);
                ini.Writue("Notes2", "notes5", tbbz2_5.Text);
                ini.Writue("Notes2", "notes6", tbbz2_6.Text);
                ini.Writue("Notes2", "notes7", tbbz2_7.Text);
                ini.Writue("Notes2", "notes8", tbbz2_8.Text);
                ini.Writue("Notes2", "notes9", tbbz2_9.Text);
                ini.Writue("Notes2", "notes10", tbbz2_10.Text);
                if (checkBox1.CheckState == CheckState.Checked)
                {
                    ini.Writue("Choose", "choose1", "1");
                }
                if (checkBox2.CheckState == CheckState.Checked)
                {
                    ini.Writue("Choose", "choose2", "1");
                }
                if (checkBox3.CheckState == CheckState.Checked)
                {
                    ini.Writue("Choose", "choose3", "1");
                }
                if (checkBox4.CheckState == CheckState.Checked)
                {
                    ini.Writue("Choose", "choose4", "1");
                }
                if (checkBox5.CheckState == CheckState.Checked)
                {
                    ini.Writue("Choose", "choose5", "1");
                }
                if (checkBox6.CheckState == CheckState.Checked)
                {
                    ini.Writue("Choose", "choose6", "1");
                }
                if (checkBox7.CheckState == CheckState.Checked)
                {
                    ini.Writue("Choose", "choose7", "1");
                }
                if (checkBox8.CheckState == CheckState.Checked)
                {
                    ini.Writue("Choose", "choose8", "1");
                }
                if (checkBox9.CheckState == CheckState.Checked)
                {
                    ini.Writue("Choose", "choose9", "1");
                }
                if (checkBox10.CheckState == CheckState.Checked)
                {
                    ini.Writue("Choose", "choose10", "1");
                }

                label_tips.Text = "初始化完成！";
            }

        }



        private void button2_Click(object sender, EventArgs e)
        {

            string Current;
            Current = Directory.GetCurrentDirectory();//获取当前根目录  

            // 写入ini  
            DialogResult result = MessageBox.Show("是否要保存，如果保存空白文件将覆盖原有数据。", "提示：", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Ini ini = new Ini(Current + "/" + comboBox1.Text + ".ini");
                ini.Writue("Setting", "key1", tbname1.Text);
                ini.Writue("Setting", "paw1", tbpaw1.Text);
                ini.Writue("Name", "username1", label3.Text);


                ini.Writue("Setting", "key2", tbname2.Text);
                ini.Writue("Setting", "paw2", tbpaw2.Text);
                ini.Writue("Name", "username2", label4.Text);

                ini.Writue("Setting", "key3", tbname3.Text);
                ini.Writue("Setting", "paw3", tbpaw3.Text);
                ini.Writue("Name", "username3", label5.Text);

                ini.Writue("Setting", "key4", tbname4.Text);
                ini.Writue("Setting", "paw4", tbpaw4.Text);
                ini.Writue("Name", "username4", label6.Text);

                ini.Writue("Setting", "key5", tbname5.Text);
                ini.Writue("Setting", "paw5", tbpaw5.Text);
                ini.Writue("Name", "username5", label7.Text);

                ini.Writue("Setting", "key6", tbname6.Text);
                ini.Writue("Setting", "paw6", tbpaw6.Text);
                ini.Writue("Name", "username6", label8.Text);

                ini.Writue("Setting", "key7", tbname7.Text);
                ini.Writue("Setting", "paw7", tbpaw7.Text);
                ini.Writue("Name", "username7", label9.Text);

                ini.Writue("Setting", "key8", tbname8.Text);
                ini.Writue("Setting", "paw8", tbpaw8.Text);
                ini.Writue("Name", "username8", label10.Text);

                ini.Writue("Setting", "key9", tbname9.Text);
                ini.Writue("Setting", "paw9", tbpaw9.Text);
                ini.Writue("Name", "username9", label11.Text);

                ini.Writue("Setting", "key10", tbname10.Text);
                ini.Writue("Setting", "paw10", tbpaw10.Text);
                ini.Writue("Name", "username10", label12.Text);

                ini.Writue("Notes1", "notes1", tbbz1_1.Text);
                ini.Writue("Notes1", "notes2", tbbz1_2.Text);
                ini.Writue("Notes1", "notes3", tbbz1_3.Text);
                ini.Writue("Notes1", "notes4", tbbz1_4.Text);
                ini.Writue("Notes1", "notes5", tbbz1_5.Text);
                ini.Writue("Notes1", "notes6", tbbz1_6.Text);
                ini.Writue("Notes1", "notes7", tbbz1_7.Text);
                ini.Writue("Notes1", "notes8", tbbz1_8.Text);
                ini.Writue("Notes1", "notes9", tbbz1_9.Text);
                ini.Writue("Notes1", "notes10", tbbz1_10.Text);

                ini.Writue("Notes2", "notes1", tbbz2_1.Text);
                ini.Writue("Notes2", "notes2", tbbz2_2.Text);
                ini.Writue("Notes2", "notes3", tbbz2_3.Text);
                ini.Writue("Notes2", "notes4", tbbz2_4.Text);
                ini.Writue("Notes2", "notes5", tbbz2_5.Text);
                ini.Writue("Notes2", "notes6", tbbz2_6.Text);
                ini.Writue("Notes2", "notes7", tbbz2_7.Text);
                ini.Writue("Notes2", "notes8", tbbz2_8.Text);
                ini.Writue("Notes2", "notes9", tbbz2_9.Text);
                ini.Writue("Notes2", "notes10", tbbz2_10.Text);
                if( checkBox1.CheckState == CheckState.Checked)
                {
                    ini.Writue("Choose", "choose1", "1");
                }
                if(checkBox2.CheckState == CheckState.Checked)
                {
                    ini.Writue("Choose", "choose2", "1");
                }
                 if(checkBox3.CheckState == CheckState.Checked)
                {
                    ini.Writue("Choose", "choose3", "1");
                }
                 if(checkBox4.CheckState == CheckState.Checked)
                {
                    ini.Writue("Choose", "choose4", "1");
                }
                 if (checkBox5.CheckState == CheckState.Checked)
                {
                    ini.Writue("Choose", "choose5", "1");
                }
                 if (checkBox6.CheckState == CheckState.Checked)
                {
                    ini.Writue("Choose", "choose6", "1");
                }
                 if (checkBox7.CheckState == CheckState.Checked)
                {
                    ini.Writue("Choose", "choose7", "1");
                }
               if (checkBox8.CheckState == CheckState.Checked)
                {
                    ini.Writue("Choose", "choose8", "1");
                }
               if (checkBox9.CheckState == CheckState.Checked)
                {
                    ini.Writue("Choose", "choose9", "1");
                }
                if (checkBox10.CheckState == CheckState.Checked)
                {
                    ini.Writue("Choose", "choose10", "1");
                }

                label_tips.Text = "保存成功！";

            }
            else
            {
                label_tips.Text = "取消保存";
            }

        }

      


        private void getChooseFromIni()
        {
            string Current;

            Current = Directory.GetCurrentDirectory();//获取当前根目录  
            Ini ini = new Ini(Current + "/" + comboBox1.Text + ".ini");
            if (File.Exists(@"" + Current + "/" + comboBox1.Text + ".ini"))
            {
                CheckBox[] boxs = { checkBox1, checkBox2, checkBox3, checkBox4, checkBox5, checkBox6, checkBox7, checkBox8, checkBox9, checkBox10 };
                for (int i = 1; i <= 10; i++)
                {
                    if (ini.ReadValue("Choose", "choose" + i) == "1")
                    {
                        boxs[i - 1].CheckState = CheckState.Checked;
                    }
                }

            }
        }

        //private void getChooseFromIni()
        //{
        //    string Current;

        //    Current = Directory.GetCurrentDirectory();//获取当前根目录  
        //    Ini ini = new Ini(Current + "/" + comboBox1.Text + ".ini");
        //    if (File.Exists(@"" + Current + "/" + comboBox1.Text + ".ini"))
        //    {
        //        if (Convert.ToBoolean(ini.ReadValue("Chooes1", "1")))
        //        {
        //            checkBox1.CheckState = CheckState.Checked;
        //        }

        //    }
        //}

        private void getInfoFromIni()
        {
            // 读取ini  
            string Current;

            Current = Directory.GetCurrentDirectory();//获取当前根目录  
            Ini ini = new Ini(Current + "/" + comboBox1.Text + ".ini");
            if (File.Exists(@"" + Current + "/" + comboBox1.Text + ".ini"))
            {
                tbname1.Text = ini.ReadValue("Setting", "key1");
                tbpaw1.Text = ini.ReadValue("Setting", "paw1");
                label3.Text = ini.ReadValue("Name", "username1");

                tbname2.Text = ini.ReadValue("Setting", "key2");
                tbpaw2.Text = ini.ReadValue("Setting", "paw2");
                label4.Text = ini.ReadValue("Name", "username2");

                tbname3.Text = ini.ReadValue("Setting", "key3");
                tbpaw3.Text = ini.ReadValue("Setting", "paw3");
                label5.Text = ini.ReadValue("Name", "username3");

                tbname4.Text = ini.ReadValue("Setting", "key4");
                tbpaw4.Text = ini.ReadValue("Setting", "paw4");
                label6.Text = ini.ReadValue("Name", "username4");


                tbname5.Text = ini.ReadValue("Setting", "key5");
                tbpaw5.Text = ini.ReadValue("Setting", "paw5");
                label7.Text = ini.ReadValue("Name", "username5");

                tbname6.Text = ini.ReadValue("Setting", "key6");
                tbpaw6.Text = ini.ReadValue("Setting", "paw6");
                label8.Text = ini.ReadValue("Name", "username6");

                tbname7.Text = ini.ReadValue("Setting", "key7");
                tbpaw7.Text = ini.ReadValue("Setting", "paw7");
                label9.Text = ini.ReadValue("Name", "username7");


                tbname8.Text = ini.ReadValue("Setting", "key8");
                tbpaw8.Text = ini.ReadValue("Setting", "paw8");
                label10.Text = ini.ReadValue("Name", "username8");


                tbname9.Text = ini.ReadValue("Setting", "key9");
                tbpaw9.Text = ini.ReadValue("Setting", "paw9");
                label11.Text = ini.ReadValue("Name", "username9");


                tbname10.Text = ini.ReadValue("Setting", "key10");
                tbpaw10.Text = ini.ReadValue("Setting", "paw10");
                label12.Text = ini.ReadValue("Name", "username10");

                tbbz1_1.Text = ini.ReadValue("Notes1", "notes1");
                tbbz1_2.Text = ini.ReadValue("Notes1", "notes2");
                tbbz1_3.Text = ini.ReadValue("Notes1", "notes3");
                tbbz1_4.Text = ini.ReadValue("Notes1", "notes4");
                tbbz1_5.Text = ini.ReadValue("Notes1", "notes5");
                tbbz1_6.Text = ini.ReadValue("Notes1", "notes6");
                tbbz1_7.Text = ini.ReadValue("Notes1", "notes7");
                tbbz1_8.Text = ini.ReadValue("Notes1", "notes8");
                tbbz1_9.Text = ini.ReadValue("Notes1", "notes9");
                tbbz1_10.Text = ini.ReadValue("Notes1", "notes10");

                tbbz2_1.Text = ini.ReadValue("Notes1", "notes1");
                tbbz2_2.Text = ini.ReadValue("Notes1", "notes2");
                tbbz2_3.Text = ini.ReadValue("Notes1", "notes3");
                tbbz2_4.Text = ini.ReadValue("Notes1", "notes4");
                tbbz2_5.Text = ini.ReadValue("Notes1", "notes5");
                tbbz2_6.Text = ini.ReadValue("Notes1", "notes6");
                tbbz2_7.Text = ini.ReadValue("Notes1", "notes7");
                tbbz2_8.Text = ini.ReadValue("Notes1", "notes8");
                tbbz2_9.Text = ini.ReadValue("Notes1", "notes9");
                tbbz2_10.Text = ini.ReadValue("Notes1", "notes10");


                label_tips.Text = "读取成功！";
            }
            else
            {
                MessageBox.Show("该文件不存在，请先用保存按钮创建文件。");
            }


        }

    

        private void label8_Click(object sender, EventArgs e)
        {
            备注 dll = new 备注();
            dll.ShowDialog();
            label8.Text = dll.callback;
        }

        

        private void button10_Click(object sender, EventArgs e)
        {
            if ((tbname7.Text.Length > 0) && (tbpaw7.Text.Length > 0))
            {
                string name = tbname7.Text;
                string pass = tbpaw7.Text;
                Utils nu = new Utils();
                nu.OpenGame();
                var thread = new System.Threading.Thread(new System.Threading.ThreadStart(()
      =>
                {
                    Thread.Sleep(5000);
                    for (int i = 0; i < 15; i++)
                    {

                        IntPtr k = FindWindow("fsgamehero0150", null);
                        if (k.ToString() != "0")
                        {
                            
                            LoginGmae(name, pass);
                            break;
                        }
                        Thread.Sleep(1000);
                        if (i == 15)
                        {
                            label_tips.Text = "等了15秒都没有找到游戏呢！";
                            break;
                        }

                    }



                    //这里面写你的代码，一般会吧比较耗时的方法放在后台里执行。
                }))
                { IsBackground = true };
                thread.Start();




            }
            else
            {
                label_tips.Text = "请输入账号密码！";
            }
        }

       

        private void button8_Click(object sender, EventArgs e)
        {
            if ((tbname6.Text.Length > 0) && (tbpaw6.Text.Length > 0))
            {
                string name = tbname6.Text;
                string pass = tbpaw6.Text;
                Utils nu = new Utils();
                nu.OpenGame();
                var thread = new System.Threading.Thread(new System.Threading.ThreadStart(()
      =>
                {
                    Thread.Sleep(5000);
                    for (int i = 0; i < 15; i++)
                    {
                       
                        IntPtr k = FindWindow("fsgamehero0150", null);
                        if (k.ToString() != "0")
                        {
                            
                            LoginGmae(name, pass);
                            break;
                        }
                        Thread.Sleep(1000);
                        if (i == 15)
                        {
                            label_tips.Text = "等了15秒都没有找到游戏呢！";
                            break;
                        }

                    }



                    //这里面写你的代码，一般会吧比较耗时的方法放在后台里执行。
                }))
                { IsBackground = true };
                thread.Start();




            }
            else
            {
                label_tips.Text = "请输入账号密码！";
            }
        }

       

        private void button6_Click(object sender, EventArgs e)
        {
            if ((tbname5.Text.Length > 0) && (tbpaw5.Text.Length > 0))
            {
                string name = tbname5.Text;
                string pass = tbpaw5.Text;
                Utils nu = new Utils();
                nu.OpenGame();
                var thread = new System.Threading.Thread(new System.Threading.ThreadStart(()
      =>
                {
                    for (int i = 0; i < 15; i++)
                    {
                        Thread.Sleep(5000);
                        IntPtr k = FindWindow("fsgamehero0150", null);
                        if (k.ToString() != "0")
                        {
                           
                            LoginGmae(name, pass);
                            break;
                        }
                        Thread.Sleep(1000);
                        if (i == 15)
                        {
                            label_tips.Text = "等了15秒都没有找到游戏呢！";
                            break;
                        }

                    }



                    //这里面写你的代码，一般会吧比较耗时的方法放在后台里执行。
                }))
                { IsBackground = true };
                thread.Start();




            }
            else
            {
                label_tips.Text = "请输入账号密码！";
            }
        }

     

        private void button4_Click(object sender, EventArgs e)
        {
            if ((tbname4.Text.Length > 0) && (tbpaw4.Text.Length > 0))
            {
                string name = tbname4.Text;
                string pass = tbpaw4.Text;
                Utils nu = new Utils();
                nu.OpenGame();
                var thread = new System.Threading.Thread(new System.Threading.ThreadStart(()
      =>
                {
                    Thread.Sleep(5000);
                    for (int i = 0; i < 15; i++)
                    {

                        IntPtr k = FindWindow("fsgamehero0150", null);
                        if (k.ToString() != "0")
                        {
                           
                            LoginGmae(name, pass);
                            break;
                        }
                        Thread.Sleep(1000);
                        if (i == 15)
                        {
                            label_tips.Text = "等了15秒都没有找到游戏呢！";
                            break;
                        }

                    }



                    //这里面写你的代码，一般会吧比较耗时的方法放在后台里执行。
                }))
                { IsBackground = true };
                thread.Start();




            }
            else
            {
                label_tips.Text = "请输入账号密码！";
            }
        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            if ((tbname3.Text.Length > 0) && (tbpaw3.Text.Length > 0))
            {
                string name = tbname3.Text;
                string pass = tbpaw3.Text;
                Utils nu = new Utils();
                nu.OpenGame();
                var thread = new System.Threading.Thread(new System.Threading.ThreadStart(()
      =>
                {
                    Thread.Sleep(5000);
                    for (int i = 0; i < 15; i++)
                    {

                        IntPtr k = FindWindow("fsgamehero0150", null);
                        if (k.ToString() != "0")
                        {
                           
                            LoginGmae(name, pass);
                            break;
                        }
                        Thread.Sleep(1000);
                        if (i == 15)
                        {
                            label_tips.Text = "等了15秒都没有找到游戏呢！";
                            break;
                        }

                    }



                    //这里面写你的代码，一般会吧比较耗时的方法放在后台里执行。
                }))
                { IsBackground = true };
                thread.Start();




            }
            else
            {
                label_tips.Text = "请输入账号密码！";
            }
        }

   

        private void btnname2_Click(object sender, EventArgs e)
        {
            if ((tbname2.Text.Length > 0) && (tbpaw2.Text.Length > 0))
            {
                string name = tbname2.Text;
                string pass = tbpaw2.Text;
                Utils nu = new Utils();
                nu.OpenGame();
                var thread = new System.Threading.Thread(new System.Threading.ThreadStart(()
      =>
                {
                    Thread.Sleep(5000);
                    for (int i = 0; i < 15; i++)
                    {

                        IntPtr k = FindWindow("fsgamehero0150", null);
                        if (k.ToString() != "0")
                        {
                            
                            LoginGmae(name, pass);
                            break;
                        }
                        Thread.Sleep(1000);
                        if (i == 15)
                        {
                            label_tips.Text = "等了15秒都没有找到游戏呢！";
                            break;
                        }

                    }



                    //这里面写你的代码，一般会吧比较耗时的方法放在后台里执行。
                }))
                { IsBackground = true };
                thread.Start();




            }
            else
            {
                label_tips.Text = "请输入账号密码！";
            }


            

        }

        private void btnname10_Click(object sender, EventArgs e)
        {
            if ((tbname10.Text.Length > 0) && (tbpaw10.Text.Length > 0))
            {
                string name = tbname10.Text;
                string pass = tbpaw10.Text;
                Utils nu = new Utils();
                nu.OpenGame();
                var thread = new System.Threading.Thread(new System.Threading.ThreadStart(()
      =>
                {
                    Thread.Sleep(5000);
                    for (int i = 0; i < 15; i++)
                    {

                        IntPtr k = FindWindow("fsgamehero0150", null);
                        if (k.ToString() != "0")
                        {
                           
                            LoginGmae(name, pass);
                            break;
                        }
                        Thread.Sleep(1000);
                        if (i == 15)
                        {
                            label_tips.Text = "等了15秒都没有找到游戏呢！";
                            break;
                        }

                    }



                    //这里面写你的代码，一般会吧比较耗时的方法放在后台里执行。
                }))
                { IsBackground = true };
                thread.Start();




            }
            else
            {
                label_tips.Text = "请输入账号密码！";
            }


        }

       
       

        private void button14_Click(object sender, EventArgs e)
        {
            if ((tbname9.Text.Length > 0) && (tbpaw9.Text.Length > 0))
            {
                string name = tbname9.Text;
                string pass = tbpaw9.Text;
                Utils nu = new Utils();
                nu.OpenGame();
                var thread = new System.Threading.Thread(new System.Threading.ThreadStart(()
      =>
                {
                    Thread.Sleep(5000);
                    for (int i = 0; i < 15; i++)
                    {

                        IntPtr k = FindWindow("fsgamehero0150", null);
                        if (k.ToString() != "0")
                        {
                           
                            LoginGmae(name, pass);
                            break;
                        }
                        Thread.Sleep(1000);
                        if (i == 15)
                        {
                            label_tips.Text = "等了15秒都没有找到游戏呢！";
                            break;
                        }

                    }



                    //这里面写你的代码，一般会吧比较耗时的方法放在后台里执行。
                }))
                { IsBackground = true };
                thread.Start();




            }
            else
            {
                label_tips.Text = "请输入账号密码！";
            }
        }

      

        private void button12_Click(object sender, EventArgs e)
        {
            if ((tbname8.Text.Length > 0) && (tbpaw8.Text.Length > 0))
            {
                string name = tbname8.Text;
                string pass = tbpaw8.Text;
                Utils nu = new Utils();
                nu.OpenGame();
                var thread = new System.Threading.Thread(new System.Threading.ThreadStart(()
      =>
                {
                    Thread.Sleep(4000);
                    for (int i = 0; i < 15; i++)
                    {

                        IntPtr k = FindWindow("fsgamehero0150", null);
                        if (k.ToString() != "0")
                        {
                            LoginGmae(name, pass);
                            break;
                        }
                        Thread.Sleep(1000);
                        if (i == 15)
                        {
                            label_tips.Text = "等了15秒都没有找到游戏呢！";
                            break;
                        }

                    }



                    //这里面写你的代码，一般会吧比较耗时的方法放在后台里执行。
                }))
                { IsBackground = true };
                thread.Start();




            }
            else
            {
                label_tips.Text = "请输入账号密码！";
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {
            关于 info = new 关于();
            info.Show();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            更新内容 dll = new 更新内容();
            dll.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            label3.Text = label3.Text;

            备注 dll = new 备注();
            dll.ShowDialog();
            label3.Text = dll.callback;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            备注 dll = new 备注();
            dll.ShowDialog();
            label4.Text = dll.callback;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            备注 dll = new 备注();
            dll.ShowDialog();
            label5.Text = dll.callback;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            备注 dll = new 备注();
            dll.ShowDialog();
            label6.Text = dll.callback;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            备注 dll = new 备注();
            dll.ShowDialog();
            label7.Text = dll.callback;
        }

        private void label9_Click(object sender, EventArgs e)
        {
            备注 dll = new 备注();
            dll.ShowDialog();
            label9.Text = dll.callback;
        }

        private void label10_Click(object sender, EventArgs e)
        {
            备注 dll = new 备注();
            dll.ShowDialog();
            label10.Text = dll.callback;
        }

        private void label11_Click(object sender, EventArgs e)
        {
            备注 dll = new 备注();
            dll.ShowDialog();
            label11.Text = dll.callback;
        }

        private void label12_Click(object sender, EventArgs e)
        {
            备注 dll = new 备注();
            dll.ShowDialog();
            label12.Text = dll.callback;
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否要将该页面恢复默认？", "提示：", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                tbname1.Text = "";
                tbpaw1.Text = "";
                label3.Text = "账号1";

                tbname2.Text = "";
                tbpaw2.Text = "";
                label4.Text = "账号2";

                tbname3.Text = "";
                tbpaw3.Text = "";
                label5.Text = "账号3";

                tbname4.Text = "";
                tbpaw4.Text = "";
                label6.Text = "账号4";


                tbname5.Text = "";
                tbpaw5.Text = "";
                label7.Text = "账号5";

                tbname6.Text = "";
                tbpaw6.Text = "";
                label8.Text = "账号6";

                tbname7.Text = "";
                tbpaw7.Text = "";
                label9.Text = "账号7";


                tbname8.Text = "";
                tbpaw8.Text = "";
                label10.Text = "账号8";


                tbname9.Text = "";
                tbpaw9.Text = "";
                label11.Text = "账号9";


                tbname10.Text = "";
                tbpaw10.Text = "";
                label12.Text = "账号10";

                tbbz1_1.Text = "";
                tbbz1_2.Text = "";
                tbbz1_3.Text = "";
                tbbz1_4.Text = "";
                tbbz1_5.Text = "";
                tbbz1_6.Text = "";
                tbbz1_7.Text = "";
                tbbz1_8.Text = "";
                tbbz1_9.Text = "";
                tbbz1_10.Text = "";

                tbbz2_1.Text = "";
                tbbz2_2.Text = "";
                tbbz2_3.Text = "";
                tbbz2_4.Text = "";
                tbbz2_5.Text = "";
                tbbz2_6.Text = "";
                tbbz2_7.Text = "";
                tbbz2_8.Text = "";
                tbbz2_9.Text = "";
                tbbz2_10.Text = "";

                label_tips.Text = "重置成功！";
            }
            else
            {
                label_tips.Text = "取消重置！";
            }

        }

        private void 账号记录本_Load(object sender, EventArgs e)
        {
            string str = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string k = str.Substring(str.Length - 7, 7);
            if (k != "shdata\\")
            {
                MessageBox.Show("请将本软件放到水浒游戏文件的“shdata”文件夹内运行，为了方便可以放过去后在桌面创建快捷方式。","警告");
            }
            
        }

        private void button2_Click_2(object sender, EventArgs e)
        {

            if (this.btn_get.Text.ToString() == "<<<")
            {
                this.Width = this.Width - 426;
                this.btn_get.Text = ">>>";
                this.label_tips.Text = "扩展模式";
            }
            else if (this.btn_get.Text.ToString() == ">>>")
            {
                this.Width = this.Width + 426;
                this.btn_get.Text = "<<<";
                this.label_tips.Text = "精简模式";
            }


        }

        private void button2_Click_3(object sender, EventArgs e)
        {
            if (this.btn_mode.Text.ToString() == "隐藏密码")
            {


                tbpaw1.PasswordChar = '*';
                tbpaw2.PasswordChar = '*';
                tbpaw3.PasswordChar = '*';
                tbpaw4.PasswordChar = '*';
                tbpaw5.PasswordChar = '*';
                tbpaw6.PasswordChar = '*';
                tbpaw7.PasswordChar = '*';
                tbpaw8.PasswordChar = '*';
                tbpaw9.PasswordChar = '*';
                tbpaw10.PasswordChar = '*';

                this.btn_mode.Text = "显示密码";
                this.label_tips.Text = "密码已隐藏";
            }
            else if (this.btn_mode.Text.ToString() == "显示密码")
            {
                tbpaw1.PasswordChar = new char();
                tbpaw2.PasswordChar = new char();
                tbpaw3.PasswordChar = new char();
                tbpaw4.PasswordChar = new char();
                tbpaw5.PasswordChar = new char();
                tbpaw6.PasswordChar = new char();
                tbpaw7.PasswordChar = new char();
                tbpaw8.PasswordChar = new char();
                tbpaw9.PasswordChar = new char();
                tbpaw10.PasswordChar = new char();
                this.btn_mode.Text = "隐藏密码";
                this.label_tips.Text = "密码已显示";
            }

        }

       
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            getInfoFromIni();
            getChooseFromIni();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Current;
            Current = Directory.GetCurrentDirectory();//获取当前根目录  
            Ini ini = new Ini(Current + "/" + comboBox1.Text + ".ini");

            string BatchLoginName ="" ;
            string BatchLoginPass="" ;
            if (Rbtn1.Checked)
            {
                if (checkBox1.Checked)
                {
                    BatchLoginName += tbname1.Text + ",";
                    BatchLoginPass += tbpaw1.Text + ",";
                    ini.Writue("Choose", "choose1", "1");
                }
                else
                {
                    ini.Writue("Choose", "choose1", "0");
                }
                if (checkBox2.Checked)
                {
                    BatchLoginName += tbname2.Text + ",";
                    BatchLoginPass += tbpaw2.Text + ",";
                    ini.Writue("Choose", "choose2", "1");
                }
                else
                {
                    ini.Writue("Choose", "choose2", "0");
                }
                if (checkBox3.Checked)
                {
                    BatchLoginName += tbname3.Text + ",";
                    BatchLoginPass += tbpaw3.Text + ",";
                    ini.Writue("Choose", "choose3", "1");
                }
                else
                {
                    ini.Writue("Choose", "choose3", "0");
                }
                if (checkBox4.Checked)
                {
                    BatchLoginName += tbname4.Text + ",";
                    BatchLoginPass += tbpaw4.Text + ",";
                    ini.Writue("Choose", "choose4", "1");
                }
                else
                {
                    ini.Writue("Choose", "choose4", "0");
                }
                if (checkBox5.Checked)
                {
                    BatchLoginName += tbname5.Text + ",";
                    BatchLoginPass += tbpaw5.Text + ",";
                    ini.Writue("Choose", "choose5", "1");
                }
                else
                {
                    ini.Writue("Choose", "choose5", "0");
                }
                if (checkBox6.Checked)
                {
                    BatchLoginName += tbname6.Text + ",";
                    BatchLoginPass += tbpaw6.Text + ",";
                    ini.Writue("Choose", "choose6", "1");
                }
                else
                {
                    ini.Writue("Choose", "choose6", "0");
                }
                if (checkBox7.Checked)
                {
                    BatchLoginName += tbname7.Text + ",";
                    BatchLoginPass += tbpaw7.Text + ",";
                    ini.Writue("Choose", "choose7", "1");
                }
                else
                {
                    ini.Writue("Choose", "choose7", "0");
                }
                if (checkBox8.Checked)
                {
                    BatchLoginName += tbname8.Text + ",";
                    BatchLoginPass += tbpaw8.Text + ",";
                    ini.Writue("Choose", "choose8", "1");
                }
                else
                {
                    ini.Writue("Choose", "choose8", "0");
                }
                if (checkBox9.Checked)
                {
                    BatchLoginName += tbname9.Text + ",";
                    BatchLoginPass += tbpaw9.Text + ",";
                    ini.Writue("Choose", "choose9", "1");
                }
                else
                {
                    ini.Writue("Choose", "choose9", "0");
                }
                if (checkBox10.Checked)
                {
                    BatchLoginName += tbname10.Text + ",";
                    BatchLoginPass += tbpaw10.Text + ",";
                    ini.Writue("Choose", "choose10", "1");
                }
                else
                {
                    ini.Writue("Choose", "choose10", "0");
                }

                string[] ArrayName = BatchLoginName.Split(Convert.ToChar(","));
                string[] ArrayPass = BatchLoginPass.Split(Convert.ToChar(","));

                var thread = new System.Threading.Thread(new System.Threading.ThreadStart(()
        =>
                {

                    for (int i = 0; i < ArrayName.Length; i++)
                    {
                        string Uname = ArrayName[i];
                        string Upass = ArrayPass[i];


                        if (Uname != "" && Uname != null && Upass != "" && Upass != null)
                        {
                            MessageBox.Show(i.ToString());

                            Utils nu = new Utils();
                            nu.OpenGame();
                            IntPtr k = FindWindow("fsgamehero0150", null);
                            ShowWindowAsync(k, SW_SHOW);//把窗口最大化
                                                        //-----------
                            for (int j = 0; j < 15; j++)
                            {

                                if (k.ToString() != "0")
                                {
                                   
                                    LoginGmae(Uname, Upass);
                                    ShowWindowAsync(k, SW_MINIMIZE);//把窗口最小化
                                    RunLogin = true;
                                    break;
                                }
                                Thread.Sleep(1000);
                                if (i == 15)
                                {
                                    label_tips.Text = "等了15秒都没有找到游戏呢！";
                                    
                                    break;
                                }
                            }
                            //--------------

                        }

                        MessageBox.Show("登陆结束 开始等待");
                       Thread.Sleep(5000);

                    }


                    //这里面写你的代码，一般会吧比较耗时的方法放在后台里执行。
                }))
                { IsBackground = true };
                thread.Start();


                

            }
           


            //--------------------重写一键登录---结束---------------------
//            {
//                string Current;
//                Current = Directory.GetCurrentDirectory();//获取当前根目录  
//                Ini ini = new Ini(Current + "/" + comboBox1.Text + ".ini");
              
//                    if (Rbtn1.Checked)
//                    {
//                        if (checkBox1.CheckState == CheckState.Checked)
//                        {
//                        RunLogin = true;
//                          var thread = new System.Threading.Thread(new System.Threading.ThreadStart(()
//=>
//                          {
//                            for (int i = 0; i < 99; i++)
//                            {

//                                  if (RunLogin)
//                                  {
//                                      button1_Click(null, null);
//                                      ini.Writue("Choose", "choose1", "1");
//                                      IntPtr k = FindWindow("fsgamehero0150", null);
//                                      ShowWindowAsync(k, SW_MINIMIZE);//把窗口最小化
//                                      RunLogin = false;
//                                  }
//                                  Thread.Sleep(2000);

//                            }                            

//                            //这里面写你的代码，一般会吧比较耗时的方法放在后台里执行。
//                        }))
//                        { IsBackground = true };
//                        thread.Start();                        
//                    }
//                        else
//                        {
//                            ini.Writue("Choose", "choose1", "0");
//                        }
//                        if (checkBox2.CheckState == CheckState.Checked)
//                        {

//                        btnname2_Click(null, null);
//                        ini.Writue("Choose", "choose2", "1");
//                        }
//                        else
//                        {
//                            ini.Writue("Choose", "choose2", "0");
//                        }
//                        if (checkBox3.CheckState == CheckState.Checked)
//                        {
//                              button2_Click_1(null, null);
//                            ini.Writue("Choose", "choose3", "1");
//                        }
//                        else
//                        {
//                            ini.Writue("Choose", "choose3", "0");
//                        }
//                        if (checkBox4.CheckState == CheckState.Checked)
//                        {

//                        button4_Click(null, null);
//                            ini.Writue("Choose", "choose4", "1");
//                        }
//                        else
//                        {
//                            ini.Writue("Choose", "choose4", "0");
//                        }
//                        if (checkBox5.CheckState == CheckState.Checked)
//                        {

//                            button6_Click(null,null);
//                            ini.Writue("Choose", "choose5", "1");
//                        }
//                        else
//                        {
//                            ini.Writue("Choose", "choose5", "0");
//                        }
//                        if (checkBox6.CheckState == CheckState.Checked)
//                        {

//                        button8_Click(null, null);
//                            ini.Writue("Choose", "choose6", "1");
//                        }
//                        else
//                        {
//                            ini.Writue("Choose", "choose6", "0");
//                        }
//                        if (checkBox7.CheckState == CheckState.Checked)
//                        {

//                        button10_Click(null, null);
//                            ini.Writue("Choose", "choose7", "1");
//                        }
//                        else
//                        {
//                            ini.Writue("Choose", "choose7", "0");
//                        }
//                        if (checkBox8.CheckState == CheckState.Checked)
//                        {

//                        button12_Click(null, null);
//                            ini.Writue("Choose", "choose8", "1");
//                        }
//                        else
//                        {
//                            ini.Writue("Choose", "choose8", "0");
//                        }
//                        if (checkBox9.CheckState == CheckState.Checked)
//                        {
//                        button14_Click(null,null);
//                            ini.Writue("Choose", "choose9", "1");
//                        }
//                        else
//                        {
//                            ini.Writue("Choose", "choose9", "0");
//                        }
//                        if (checkBox10.CheckState == CheckState.Checked)
//                        {

//                        btnname10_Click(null,null);
//                            ini.Writue("Choose", "choose10", "1");
//                        }
//                        else
//                        {
//                            ini.Writue("Choose", "choose10", "0");
//                        }

//                    }
                 
//                    //一键登录结束

//             }

        }

        private void Rbtn1_CheckedChanged(object sender, EventArgs e)
        {
            label_tips.Text = "选用模式1";
        }

        private void Rbtn2_CheckedChanged(object sender, EventArgs e)
        {
            label_tips.Text = "选用模式2";
        }

        private void buttonOpenGame_Click(object sender, EventArgs e)
        {
            
            
                string str5 = Application.StartupPath; //取本地路径

                string str = "\\CProtect1.exe Game.exe A89DD7AD dhsh1 CYOU Space zy@fs";//水浒的命令行

                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
                p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;//不显示程序窗口
                p.Start();//启动程序

                //向cmd窗口发送输入信息
                p.StandardInput.WriteLine(str5 + str);

                p.StandardInput.AutoFlush = true;
                p.StandardInput.WriteLine("exit");
                //向标准输入写入要执行的命令。这里使用&是批处理命令的符号，表示前面一个命令不管是否执行成功都执行后面(exit)命令，如果不执行exit命令，后面调用ReadToEnd()方法会假死
                //同类的符号还有&&和||前者表示必须前一个命令执行成功才会执行后面的命令，后者表示必须前一个命令执行失败才会执行后面的命令

                p.WaitForExit();//等待程序执行完退出进程
                p.Close();//关闭CMD                 




            
        }

        private void Rbtn3_CheckedChanged(object sender, EventArgs e)
        {
            label_tips.Text = "红尘模式";
        }
    }
}
