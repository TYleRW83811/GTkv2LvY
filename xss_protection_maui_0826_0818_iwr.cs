// 代码生成时间: 2025-08-26 08:18:38
using System;
using System.Text.RegularExpressions;
using Microsoft.Maui.Controls;

namespace XssProtectionMaui
{
    // 定义一个用于XSS防护的类
    public static class XssProtection
    {
        // 定义一个方法来清理HTML，防止XSS攻击
        public static string SanitizeHtml(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            // 使用正则表达式移除任何可能的XSS代码
            string output = Regex.Replace(input, "<script>.*?</script>|<.*?javascript:.*?>", "", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            // 将HTML实体进行编码，避免HTML标签被浏览器解析
            output = WebUtility.HtmlEncode(output);

            return output;
        }
    }

    // 主页面类
    public class MainPage : ContentPage
    {
        public MainPage()
        {
            // 设置页面标题
            Title = "XSS Protection Demo";

            // 创建输入框和显示结果的标签
            Entry inputEntry = new Entry
            {
                Placeholder = "Enter HTML content here..."
            };
            Label resultLabel = new Label
            {
                Text = ""
            };

            // 添加按钮，点击后将清理输入框的内容
            Button clearButton = new Button
            {
                Text = "Clear HTML"
            };
            clearButton.Clicked += async (sender, e) =>
            {
                try
                {
                    // 清理HTML内容，并显示结果
                    resultLabel.Text = XssProtection.SanitizeHtml(inputEntry.Text);
                }
                catch (Exception ex)
                {
                    // 错误处理，显示错误信息
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            };

            // 将控件添加到页面
            Content = new StackLayout
            {
                Children =
                {
                    inputEntry,
                    clearButton,
                    resultLabel
                }
            };
        }
    }
}
