using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Ping_Checker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public delegate void LeagueDelegate();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void PingInspectorWndow_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Run(() => { GetLeaguePing(); });
            Task.Run(() => { GetDota2Ping(); });
            Task.Run(() => { GetValorantPing(); });
            
        }

        //PH Server
        private void GetLeaguePing() {
            try
            {
                using (Process LeaguePingProcess = new Process())
                {
                    LeaguePingProcess.StartInfo = new ProcessStartInfo("cmd.exe")
                    {
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        Verb = "runas",
                        Arguments = @"/K ping 125.5.6.151 -t"
                    };

                    LeaguePingProcess.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
                    {
                        LeaguePingLabel.Dispatcher.Invoke(() =>
                        {
                            if (e.Data == null)
                            {
                                LeaguePingLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
                                LeaguePingLabel.Text = "Request Timed Out";
                                return;
                            }
                            if (e.Data != string.Empty && e.Data.Contains("Reply from") && !e.Data.Contains("unreachable."))
                            {
                                int ping = 0;
                                string output = e.Data;
                                string pingString = output.Split(' ')[4];
                                if (pingString.Length == 9)
                                {
                                    ping = int.Parse(pingString.Substring(5, 2));
                                   
                                }
                                else if(pingString.Length == 10)
                                {
                                    ping = int.Parse(pingString.Substring(5, 3));
                                }
                                else if (pingString.Length == 11)
                                {
                                    ping = int.Parse(pingString.Substring(5, 4));
                                }


                                if (ping >= 0 && ping <= 110)
                                {
                                    LeaguePingLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#008000"));
                                }
                                else if (ping >= 111 && ping <= 700)
                                {
                                    LeaguePingLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CCCC00"));
                                }
                                else if (ping > 701)
                                {
                                    LeaguePingLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
                                }
                                LeaguePingLabel.Text = ping.ToString() + "ms";
                            }
                            else 
                            {
                                LeaguePingLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
                                LeaguePingLabel.Text = "Request Timed Out";
                            }
                        });

                    });

                    LeaguePingProcess.Start();
                    LeaguePingProcess.BeginOutputReadLine();
                    LeaguePingProcess.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        //Singapore Server
        private void GetDota2Ping()
        {
            try
            {
                using (Process Dota2PingProcess = new Process())
                {
                    Dota2PingProcess.StartInfo = new ProcessStartInfo("cmd.exe")
                    {
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        Verb = "runas",
                        Arguments = @"/K ping sgp-2.valve.net -t"
                    };

                    Dota2PingProcess.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
                    {
                        Dota2PingLabel.Dispatcher.Invoke(() =>
                        {
                            if (e.Data == null)
                            {
                                Dota2PingLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
                                Dota2PingLabel.Text = "Request Timed Out";
                                return;
                            }
                            if (e.Data != string.Empty && e.Data.Contains("Reply from") && !e.Data.Contains("unreachable."))
                            {
                                int ping = 0;
                                string output = e.Data;
                                string pingString = output.Split(' ')[4];
                                if (pingString.Length == 9)
                                {
                                    ping = int.Parse(pingString.Substring(5, 2));

                                }
                                else if (pingString.Length == 10)
                                {
                                    ping = int.Parse(pingString.Substring(5, 3));
                                }
                                else if (pingString.Length == 11)
                                {
                                    ping = int.Parse(pingString.Substring(5, 4));
                                }


                                if (ping >= 0 && ping <= 110)
                                {
                                    Dota2PingLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#008000"));
                                }
                                else if (ping >= 111 && ping <= 700)
                                {
                                    Dota2PingLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CCCC00"));
                                }
                                else if (ping > 701)
                                {
                                    Dota2PingLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
                                }
                                Dota2PingLabel.Text = ping.ToString() + "ms";
                            }
                            else
                            {
                                Dota2PingLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
                                Dota2PingLabel.Text = "Request Timed Out";
                            }
                        });

                    });

                    Dota2PingProcess.Start();
                    Dota2PingProcess.BeginOutputReadLine();
                    Dota2PingProcess.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        //Asia Pasific Server
        private void GetValorantPing()
        {
            try
            {
                using (Process ValorantPingProcess = new Process())
                {
                    ValorantPingProcess.StartInfo = new ProcessStartInfo("cmd.exe")
                    {
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        Verb = "runas",
                        Arguments = @"/K ping 52.221.183.157 -t"
                    };

                    ValorantPingProcess.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
                    {
                        ValorantPingLabel.Dispatcher.Invoke(() =>
                        {
                            if (e.Data == null)
                            {
                                ValorantPingLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
                                ValorantPingLabel.Text = "Request Timed Out";
                                return;
                            }
                            if (e.Data != string.Empty && e.Data.Contains("Reply from") && !e.Data.Contains("unreachable."))
                            {
                                int ping = 0;
                                string output = e.Data;
                                string pingString = output.Split(' ')[4];
                                if (pingString.Length == 9)
                                {
                                    ping = int.Parse(pingString.Substring(5, 2));

                                }
                                else if (pingString.Length == 10)
                                {
                                    ping = int.Parse(pingString.Substring(5, 3));
                                }
                                else if (pingString.Length == 11)
                                {
                                    ping = int.Parse(pingString.Substring(5, 4));
                                }


                                if (ping >= 0 && ping <= 110)
                                {
                                    ValorantPingLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#008000"));
                                }
                                else if (ping >= 111 && ping <= 700)
                                {
                                    ValorantPingLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CCCC00"));
                                }
                                else if (ping > 701)
                                {
                                    ValorantPingLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
                                }
                                ValorantPingLabel.Text = ping.ToString() + "ms";
                            }
                            else
                            {
                                ValorantPingLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
                                ValorantPingLabel.Text = "Request Timed Out";
                            }
                        });

                    });

                    ValorantPingProcess.Start();
                    ValorantPingProcess.BeginOutputReadLine();
                    ValorantPingProcess.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void ExitCmdExe()
        {
            Process[] ps = Process.GetProcessesByName("CMD");

            foreach (Process p in ps)
            {
                try
                {
                    if (!p.HasExited)
                    {
                        p.Kill();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(String.Format("Unable to kill process {0}, exception: {1}", p.ToString(), ex.ToString()));
                }
            }
        }

        private void ExitPingExe()
        {
            Process[] ps = Process.GetProcessesByName("PING");

            foreach (Process p in ps)
            {
                try
                {
                    if (!p.HasExited)
                    {
                        p.Kill();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(String.Format("Unable to kill process {0}, exception: {1}", p.ToString(), ex.ToString()));
                }
            }
        }

        private void PingInspectorWndow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ExitPingExe();
            ExitCmdExe();
        }
    }
}
