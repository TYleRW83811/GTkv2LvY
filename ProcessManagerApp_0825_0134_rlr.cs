// 代码生成时间: 2025-08-25 01:34:55
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

// 进程管理器应用程序的主类
public class ProcessManagerApp : Application
{
    // 程序的主页面
    public MainPage MainPage { get; set; }

    public ProcessManagerApp()
    {
        try
        {
            // 初始化主页面
            MainPage = new MainPage();
            // 设置主页面
            MainPage = new NavigationPage(new ProcessListPage());
        }
        catch (Exception ex)
        {
            // 异常处理
            Console.WriteLine($"Error initializing application: {ex.Message}");
        }
    }
}

// 显示进程列表的页面
public class ProcessListPage : ContentPage
{
    private ListView _processListView;
    private List<Process> _processList;

    public ProcessListPage()
    {
        // 标题
        Title = "Process Manager";

        // 创建ListView控件
        _processListView = new ListView()
        {
            RowHeight = 40
        };
        _processListView.ItemTapped += OnProcessSelected;

        // 设置ListView的ItemTemplate
        _processListView.ItemTemplate = new DataTemplate(() =>
        {
            var label = new Label();
            label.SetBinding(Label.TextProperty, "ProcessName");
            return label;
        });

        // 加载进程列表
        LoadProcessList();

        // 添加ListView到页面
        Content = _processListView;
    }

    private void LoadProcessList()
    {
        try
        {
            // 获取当前系统进程列表
            _processList = Process.GetProcesses().ToList();
            _processListView.ItemsSource = _processList;
        }
        catch (Exception ex)
        {
            // 异常处理
            Console.WriteLine($"Error loading process list: {ex.Message}");
        }
    }

    private void OnProcessSelected(object sender, ItemTappedEventArgs e)
    {
        // 当用户点击某个进程时，显示进程详细信息
        Process process = (Process)e.Item;
        // 导航到进程详情页面
        Navigation.PushAsync(new ProcessDetailPage(process));
    }
}

// 显示进程详情的页面
public class ProcessDetailPage : ContentPage
{
    private readonly Process _process;

    public ProcessDetailPage(Process process)
    {
        _process = process;
        // 标题
        Title = $"Process: {process.ProcessName}";

        // 创建一个StackLayout作为页面的布局容器
        StackLayout layout = new StackLayout
        {
            Padding = new Thickness(10),
            Spacing = 10
        };

        // 添加进程名称Label
        Label nameLabel = new Label
        {
            Text = $"Name: {process.ProcessName}"
        };
        layout.Children.Add(nameLabel);

        // 添加进程IDLabel
        Label idLabel = new Label
        {
            Text = $"ID: {process.Id}"
        };
        layout.Children.Add(idLabel);

        // 添加结束进程Button
        Button endButton = new Button
        {
            Text = "End Process",
            Command = new Command(async () => await EndProcess())
        };
        layout.Children.Add(endButton);

        // 设置页面内容
        Content = layout;
    }

    private async Task EndProcess()
    {
        try
        {
            // 尝试结束进程
            _process.Kill();
            await DisplayAlert("Process Manager", $"Process {_process.ProcessName} has been terminated.", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            // 异常处理
            await DisplayAlert("Error", $"Failed to terminate process: {ex.Message}", "OK");
        }
    }
}
