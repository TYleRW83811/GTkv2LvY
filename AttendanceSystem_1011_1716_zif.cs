// 代码生成时间: 2025-10-11 17:16:54
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;

namespace AttendanceApp
{
    // 主界面类
    public class AttendancePage : ContentPage
    {
        private readonly AttendanceService _attendanceService;
        private Button _clockInButton;
        private Button _clockOutButton;
        private Label _attendanceStatusLabel;

        public AttendancePage()
        {
            _attendanceService = new AttendanceService();

            // 初始化按钮和标签
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // 考勤打卡按钮
            _clockInButton = new Button
            {
                Text = "Clock In"
            };
            _clockInButton.Clicked += OnClockInButtonClicked;

            // 考勤打卡结束按钮
            _clockOutButton = new Button
            {
                Text = "Clock Out"
            };
            _clockOutButton.Clicked += OnClockOutButtonClicked;

            // 考勤状态显示标签
            _attendanceStatusLabel = new Label
            {
                Text = "Attendance status: Not Clocked In"
            };

            // 页面布局
            Content = new StackLayout
            {
                Children =
                {
                    _clockInButton,
                    _clockOutButton,
                    _attendanceStatusLabel
                }
            };
        }

        private void OnClockInButtonClicked(object sender, EventArgs e)
        {
            try
            {
                // 打卡操作，并更新状态
                var clockInResult = _attendanceService.ClockIn();
                _attendanceStatusLabel.Text = $"Attendance status: Clocked In at {clockInResult.Time}";
            }
            catch (Exception ex)
            {
                // 错误处理
                _attendanceStatusLabel.Text = $"Error: {ex.Message}";
            }
        }

        private void OnClockOutButtonClicked(object sender, EventArgs e)
        {
            try
            {
                // 结束打卡操作，并更新状态
                var clockOutResult = _attendanceService.ClockOut();
                _attendanceStatusLabel.Text = $"Attendance status: Clocked Out at {clockOutResult.Time}";
            }
            catch (Exception ex)
            {
                // 错误处理
                _attendanceStatusLabel.Text = $"Error: {ex.Message}";
            }
        }
    }

    // 考勤服务类
    public class AttendanceService
    {
        private List<DateTime> _clockRecords = new List<DateTime>();

        public AttendanceResult ClockIn()
        {
            if (_clockRecords.Any(record => record > DateTime.Now.AddMinutes(-15)))
            {
                throw new Exception("Clock in attempt too close to previous clock in");
            }
            _clockRecords.Add(DateTime.Now);
            return new AttendanceResult { Time = DateTime.Now };
        }

        public AttendanceResult ClockOut()
        {
            if (!_clockRecords.Any())
            {
                throw new Exception("No clock in record found for clock out");
            }
            var lastClockInTime = _clockRecords.Last();
            _clockRecords.Remove(lastClockInTime);
            return new AttendanceResult { Time = DateTime.Now };
        }
    }

    // 考勤结果类
    public class AttendanceResult
    {
        public DateTime Time { get; set; }
    }
}
