﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Principal;

namespace Wilkin_Router
{
    public partial class MainScreen : Form
    {
        Process newProcess = new Process();

        public MainScreen()
        {
            newProcess.StartInfo.UseShellExecute = false;
            newProcess.StartInfo.CreateNoWindow = true;
            newProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Hide();
        }
        
        public bool isUserAdmin()
        {
            bool isAdmin;
            try
            {
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal princi = new WindowsPrincipal(user);
                isAdmin = princi.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException)
            {
                isAdmin = false;
            }
            catch(Exception)
            {
                isAdmin = false;
            }
            return isAdmin;
        }

        public void processStart_1()
        {
            progBar.Increment(25);
            newProcess.StartInfo.FileName = "netsh";
            newProcess.StartInfo.Arguments = "wlan stop hostednetwork";
            try
            {
                using (Process execute = Process.Start(newProcess.StartInfo)) {
                    execute.WaitForExit();
                    progBar.Increment(25);
                    processStart_2();
                }
            }
            catch
            {
                //nothing
            }
        }

        public void processStart_2()
        {
            newProcess.StartInfo.FileName = "netsh";
            newProcess.StartInfo.Arguments = "wlan set hostednetwork mode=allow ssid="+SSID.Text+" key="+password.Text;
            try
            {
                using (Process execute = Process.Start(newProcess.StartInfo))
                {
                    execute.WaitForExit();
                    progBar.Increment(25);
                    processStart_3();
                }
            }
            catch
            {
                //nothing
            }
        }

        public void processStart_3()
        {
            newProcess.StartInfo.FileName = "netsh";
            newProcess.StartInfo.Arguments = "wlan start hostednetwork";
            try
            {
                using (Process execute = Process.Start(newProcess.StartInfo))
                {
                    execute.WaitForExit();
                    mainPanel.Visible = true;
                    progBar.Increment(25);
                    startRouter.Text = "Stop Wilkin Router";
                    SSID.Enabled = false;
                    password.Enabled = false;
                    pictureBox1.Show();
                    pictureBox2.Hide();
                    label6.Text = Class1.GetIPAddress().MapToIPv4().ToString();
                }
            }
            catch
            {
                //nothing
            }
        }

        private void startRouter_Click(object sender, EventArgs e)
        {
            if (startRouter.Text.Equals("Start Wilkin Router"))
            {
                if (SSID.TextLength < 1)
                {
                    MessageBox.Show("SSID should be at least 1 character", "SSID Error");
                }
                else if (password.TextLength < 8)
                {
                    MessageBox.Show("Password should be atleast 8 characters", "Password Error");
                }
                else
                {
                    mainPanel.Visible = false;
                    processStart_1();
                }
            }
            else
            {
                mainPanel.Visible = false;
                processStop();
            }
        }

        public void processStop()
        {
            newProcess.StartInfo.FileName = "netsh";
            newProcess.StartInfo.Arguments = "wlan stop hostednetwork";
            try
            {
                progBar.Increment(50);
                using (Process execute = Process.Start(newProcess.StartInfo))
                {
                    execute.WaitForExit();
                    progBar.Increment(50);
                    mainPanel.Visible = true;
                    startRouter.Text = "Start Wilkin Router";
                    pictureBox1.Hide();
                    pictureBox2.Show();
                    SSID.Enabled = true;
                    password.Enabled = true;
                }
            }
            catch
            {
                //nothing
            }
        }
        
    }
}
