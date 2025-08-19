// 代码生成时间: 2025-08-20 05:24:43
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Hosting;
using System;
using System.Threading.Tasks;

namespace MauiApp
{
    // 主程序类
    public static class MauiProgram
    {
        // 配置MAUI应用
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            return builder.Build();
        }
    }

    // MAUI应用的主类
    public class App : Application
    {
        private const string DarkTheme = "Dark";
        private const string LightTheme = "Light";
        private static bool isDarkTheme;

        // 构造函数
        public App()
        {
            // 设置默认主题
            InitializeTheme();

            // 设置主页面
            MainPage = new MainPage();
        }

        // 初始化主题
        private void InitializeTheme()
        {
            isDarkTheme = Preferences.Get("IsDarkTheme", false);
            if (isDarkTheme)
            {
                ApplyTheme(DarkTheme);
            }
            else
            {
                ApplyTheme(LightTheme);
            }
        }

        // 应用主题
        public async void ApplyTheme(string theme)
        {
            try
            {
                if (theme == DarkTheme)
                {
                    this.UserAppTheme = Microsoft.Maui.Controls.OSAppTheme.Dark;
                    await Preferences.SetAsync("IsDarkTheme", true);
                    await Preferences.SaveAsync();
                }
                else if (theme == LightTheme)
                {
                    this.UserAppTheme = Microsoft.Maui.Controls.OSAppTheme.Light;
                    await Preferences.SetAsync("IsDarkTheme", false);
                    await Preferences.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                // 错误处理
                Console.WriteLine($"Failed to apply theme: {ex.Message}");
            }
        }
    }

    // 主页面
    public class MainPage : ContentPage
    {
        private Button toggleThemeButton;

        public MainPage()
        {
            // 设置页面标题
            Title = "Theme Switcher";

            // 创建切换主题按钮
            toggleThemeButton = new Button
            {
                Text = App.isDarkTheme ? "Switch to Light Theme" : "Switch to Dark Theme",
                BackgroundColor = Colors.White,
                TextColor = Colors.Black
            };

            // 按钮点击事件处理
            toggleThemeButton.Clicked += ToggleThemeButton_Clicked;

            // 构建页面内容
            Content = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        Text = "Welcome to the Maui App Theme Switcher!",
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                    },
                    toggleThemeButton
                },
                Padding = new Thickness(20)
            };
        }

        // 切换主题按钮事件处理
        private async void ToggleThemeButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var app = Application.Current as App;
                if (app != null)
                {
                    // 切换主题
                    app.ApplyTheme(app.isDarkTheme ? App.LightTheme : App.DarkTheme);

                    // 更新按钮文本
                    toggleThemeButton.Text = app.isDarkTheme ? "Switch to Light Theme" : "Switch to Dark Theme";
                }
            }
            catch (Exception ex)
            {
                // 错误处理
                Console.WriteLine($"Failed to toggle theme: {ex.Message}");
            }
        }
    }
}
