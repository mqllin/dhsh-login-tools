using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace dhshLoginTools
{
    class Utils
    {

        public void OpenGame()
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
   //-------------------------------------------获取句柄发送账号密码核心事件--开始-----------------------------------------------------

        //找到窗口（进程名称 可空，窗口名称）
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpCkassName, string lpWindowName);

        //把窗口放到最前面
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        //发送消息
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        #region SendMessage 参数
        private const int WM_KEYDOWN = 0X100;
        private const int WM_KEYUP = 0X101;
        private const int WM_SYSCHAR = 0X106;
        private const int WM_SYSKEYUP = 0X105;
        private const int WM_SYSKEYDOWN = 0X104;
        private const int WM_CHAR = 0X102;
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
   //-------------------------------------------获取句柄发送账号密码核心事件--结束-----------------------------------------------------

        public bool LoginGameMode1(string name,string pass)//登陆模式1 登录到验证码
        {
                           
                IntPtr k = FindWindow("fsgamehero0150", null);
                if (k.ToString() != "0")
               {

                    SetForegroundWindow(k);
                   

                    SendMessage(k, WM_KEYDOWN, 0X0D, 0);//发送回车 按下
                    SendMessage(k, WM_KEYUP, 0X0D, 0);//发送回车 弹起
                    SendMessage(k, WM_CHAR, 0X0D, 0);

                    SendMessage(k, WM_KEYDOWN, 0X0D, 0);//发送回车 按下
                    SendMessage(k, WM_KEYUP, 0X0D, 0);//发送回车 弹起
                    SendMessage(k, WM_CHAR, 0X0D, 0);

                    SendMessage(k, WM_KEYDOWN, 0X0D, 0);//发送回车 按下
                    SendMessage(k, WM_KEYUP, 0X0D, 0);//发送回车 弹起
                    SendMessage(k, WM_CHAR, 0X0D, 0);


                    InputStr(k, name);

                    SendMessage(k, WM_KEYDOWN, 0X09, 0);//发送TAB 按下
                    SendMessage(k, WM_KEYUP, 0X09, 0);//发送TAB 弹起
                    SendMessage(k, WM_CHAR, 0X09, 0);

                    InputStr(k, pass);

                    SendMessage(k, WM_KEYDOWN, 0X0D, 0);//发送回车 按下
                    SendMessage(k, WM_KEYUP, 0X0D, 0);//发送回车 弹起
                    SendMessage(k, WM_CHAR, 0X0D, 0);
                    Thread.Sleep(500);
                    SendMessage(k, WM_KEYDOWN, 0X0D, 0);//发送回车 按下
                    SendMessage(k, WM_KEYUP, 0X0D, 0);//发送回车 弹起
                    SendMessage(k, WM_CHAR, 0X0D, 0);
                    SendMessage(k, WM_KEYDOWN, 0X0D, 0);//发送回车 按下
                    SendMessage(k, WM_KEYUP, 0X0D, 0);//发送回车 弹起
                    SendMessage(k, WM_CHAR, 0X0D, 0);
                    return true;
                   
                }
                else
                {
                    
                    return false;
                }

            }

        public bool LoginGameMode2(string name, string pass) //登陆模式2 登录到角色
        {


           
            IntPtr k = FindWindow("fsgamehero0150", null);
            if (k.ToString() != "0")
            {

                SetForegroundWindow(k);


                SendMessage(k, WM_KEYDOWN, 0X0D, 0);//发送回车 按下
                SendMessage(k, WM_KEYUP, 0X0D, 0);//发送回车 弹起
                SendMessage(k, WM_CHAR, 0X0D, 0);

                SendMessage(k, WM_KEYDOWN, 0X0D, 0);//发送回车 按下
                SendMessage(k, WM_KEYUP, 0X0D, 0);//发送回车 弹起
                SendMessage(k, WM_CHAR, 0X0D, 0);

                SendMessage(k, WM_KEYDOWN, 0X0D, 0);//发送回车 按下
                SendMessage(k, WM_KEYUP, 0X0D, 0);//发送回车 弹起
                SendMessage(k, WM_CHAR, 0X0D, 0);


                InputStr(k, name);

                SendMessage(k, WM_KEYDOWN, 0X09, 0);//发送TAB 按下
                SendMessage(k, WM_KEYUP, 0X09, 0);//发送TAB 弹起
                SendMessage(k, WM_CHAR, 0X09, 0);

                InputStr(k, pass);

                SendMessage(k, WM_KEYDOWN, 0X0D, 0);//发送回车 按下
                SendMessage(k, WM_KEYUP, 0X0D, 0);//发送回车 弹起
                SendMessage(k, WM_CHAR, 0X0D, 0);
                Thread.Sleep(500);
                SendMessage(k, WM_KEYDOWN, 0X0D, 0);//发送回车 按下
                SendMessage(k, WM_KEYUP, 0X0D, 0);//发送回车 弹起
                SendMessage(k, WM_CHAR, 0X0D, 0);
             
                return true;

            }
            else
            {
                return false;
            }

        }

        public bool LoginGameMode3(string name, string pass) //登陆模式3 登录到角色
        {


            OpenGame();
            IntPtr k = FindWindow("fsgamehero0150", null);
            if (k.ToString() != "0")
            {

                SetForegroundWindow(k);


                SendMessage(k, WM_KEYDOWN, 0X0D, 0);//发送回车 按下
                SendMessage(k, WM_KEYUP, 0X0D, 0);//发送回车 弹起
                SendMessage(k, WM_CHAR, 0X0D, 0);

                SendMessage(k, WM_KEYDOWN, 0X0D, 0);//发送回车 按下
                SendMessage(k, WM_KEYUP, 0X0D, 0);//发送回车 弹起
                SendMessage(k, WM_CHAR, 0X0D, 0);

                SendMessage(k, WM_KEYDOWN, 0X0D, 0);//发送回车 按下
                SendMessage(k, WM_KEYUP, 0X0D, 0);//发送回车 弹起
                SendMessage(k, WM_CHAR, 0X0D, 0);


                InputStr(k, name);

                SendMessage(k, WM_KEYDOWN, 0X09, 0);//发送TAB 按下
                SendMessage(k, WM_KEYUP, 0X09, 0);//发送TAB 弹起
                SendMessage(k, WM_CHAR, 0X09, 0);

                InputStr(k, pass);

                SendMessage(k, WM_KEYDOWN, 0X0D, 0);//发送回车 按下
                SendMessage(k, WM_KEYUP, 0X0D, 0);//发送回车 弹起
                SendMessage(k, WM_CHAR, 0X0D, 0);
                Thread.Sleep(500);
                SendMessage(k, WM_KEYDOWN, 0X0D, 0);//发送回车 按下
                SendMessage(k, WM_KEYUP, 0X0D, 0);//发送回车 弹起
                SendMessage(k, WM_CHAR, 0X0D, 0);
                SendMessage(k, WM_KEYDOWN, 0X0D, 0);//发送回车 按下
                SendMessage(k, WM_KEYUP, 0X0D, 0);//发送回车 弹起
                SendMessage(k, WM_CHAR, 0X0D, 0);
                return true;

            }
            else
            {
                return false;
            }

        }

        


    }//class结束

    }//namepase结束
