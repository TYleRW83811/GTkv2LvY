// 代码生成时间: 2025-08-12 17:23:48
using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace MauiSortingApp
{
    // 主页面
    public class MainPage : ContentPage
    {
        private Entry numberInput;
        private Button sortButton;
        private Label resultLabel;
        private List<int> numbers;

        public MainPage()
        {
            // 初始化页面布局和控件
            Title = "Sorting Algorithm MAUI App";
            
            // 创建输入框
            numberInput = new Entry
            {
                Placeholder = "Enter numbers separated by space"
            };

            // 创建按钮
            sortButton = new Button
            {
                Text = "Sort"
            };
            sortButton.Clicked += OnSortClicked;

            // 创建结果显示标签
            resultLabel = new Label
            {
                Text = "Sorting result will be shown here"
            };

            // 设置页面内容
            Content = new StackLayout
            {
                Children =
                {
                    numberInput,
                    sortButton,
                    resultLabel
                }
            };
        }

        private void OnSortClicked(object sender, EventArgs e)
        {
            try
            {
                // 从输入框获取字符串并分割成数字列表
                string input = numberInput.Text;
                numbers = new List<int>();
                foreach (var num in input.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                {
                    if (int.TryParse(num, out int n))
                    {
                        numbers.Add(n);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid input. Please enter only numbers separated by space.");
                    }
                }

                // 对数字列表进行排序
                SortNumbers();

                // 显示排序结果
                resultLabel.Text = $"Sorted numbers: {string.Join(", ", numbers)}";
            }
            catch (Exception ex)
            {
                // 错误处理
                resultLabel.Text = $"Error: {ex.Message}";
            }
        }

        private void SortNumbers()
        {
            // 使用插入排序算法进行排序
            for (int i = 1; i < numbers.Count; i++)
            {
                int current = numbers[i];
                int j = i - 1;
                
                while (j >= 0 && numbers[j] > current)
                {
                    numbers[j + 1] = numbers[j];
                    j--;
                }
                numbers[j + 1] = current;
            }
        }
    }
}
