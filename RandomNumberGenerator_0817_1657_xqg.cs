// 代码生成时间: 2025-08-17 16:57:45
using System;
using Microsoft.Maui.Controls;

namespace RandomNumberGeneratorApp
{
    public partial class MainPage : ContentPage
    {
        private const int MinValue = 1;
        private const int MaxValue = 100;
        private Random random = new Random();

        public MainPage()
        {
            InitializeComponent();
        }

        // 此方法生成一个随机数并显示在标签中
        private void GenerateRandomNumber()
        {
            try
            {
                // 生成一个介于MinValue和MaxValue之间的随机数
                int randomNumber = random.Next(MinValue, MaxValue + 1);
                // 显示随机数
                resultLabel.Text = $"""Random Number: {randomNumber}"";
            }
            catch (Exception ex)
            {
                // 错误处理：如果发生异常，显示错误信息
                resultLabel.Text = $"""Error: {ex.Message}"";
            }
        }
    }
}