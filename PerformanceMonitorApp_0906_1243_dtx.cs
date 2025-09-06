// 代码生成时间: 2025-09-06 12:43:05
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;

namespace SystemMonitorApp
{
    // MainPage是MAUI应用的主页面
    public class MainPage : ContentPage
    {
        private Timer timer;
        private Label cpuUsageLabel;
        private Label memoryUsageLabel;
        private Label diskUsageLabel;

        // 构造函数
        public MainPage()
        {
            // 初始化页面布局
            InitializeComponent();
        }

        // 初始化页面内容
        private void InitializeComponent()
        {
            // 设置页面标题
            Title = "System Performance Monitor";

            // 创建CPU使用率标签
            cpuUsageLabel = new Label
            {
                Text = "CPU Usage: 0%",
                FontSize = FontSize.Medium
            };

            // 创建内存使用率标签
            memoryUsageLabel = new Label
            {
                Text = "Memory Usage: 0%",
                FontSize = FontSize.Medium
            };

            // 创建磁盘使用率标签
            diskUsageLabel = new Label
            {
                Text = "Disk Usage: 0%",
                FontSize = FontSize.Medium
            };

            // 设置页面内容
            Content = new StackLayout
            {
                Children =
                {
                    cpuUsageLabel,
                    memoryUsageLabel,
                    diskUsageLabel
                }
            };

            // 启动性能监控
            StartMonitoring();
        }

        // 启动性能监控
        private void StartMonitoring()
        {
            // 创建一个定时器，每1秒刷新一次数据
            timer = new Timer(1000);
            timer.Elapsed += OnTimedEvent;
            timer.Start();
        }

        // 定时器事件处理函数
        private async void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            try
            {
                // 更新CPU使用率
                cpuUsageLabel.Text = $"CPU Usage: {GetCpuUsage()}%";

                // 更新内存使用率
                memoryUsageLabel.Text = $"Memory Usage: {GetMemoryUsage()}%";

                // 更新磁盘使用率
                diskUsageLabel.Text = $"Disk Usage: {GetDiskUsage()}%";
            }
            catch (Exception ex)
            {
                // 处理异常情况
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        // 获取CPU使用率
        private double GetCpuUsage()
        {
            // 使用System.Diagnostics命名空间下的Process类获取CPU使用率
            return ((double)Process.GetCurrentProcess().TotalProcessorTime.Ticks /
                TimerFrequency) * 100 / (Environment.ProcessorCount *
                StopWatchFrequency);
        }

        // 获取内存使用率
        private double GetMemoryUsage()
        {
            // 使用System.Diagnostics命名空间下的Process类获取内存使用率
            return ((double)Process.GetCurrentProcess().WorkingSet64 /
                1024) / 1024;
        }

        // 获取磁盘使用率
        private double GetDiskUsage()
        {
            // 使用System.IO命名空间下的DriveInfo类获取磁盘使用率
            DriveInfo driveInfo = new DriveInfo("C:");
            return (driveInfo.TotalFreeSpace / driveInfo.TotalSize) * 100;
        }
    }
}
