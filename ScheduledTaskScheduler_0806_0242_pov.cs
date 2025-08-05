// 代码生成时间: 2025-08-06 02:42:49
using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Maui;

namespace MauiApp
{
    /// <summary>
# 添加错误处理
    /// A simple scheduled task scheduler using C# and MAUI framework.
    /// </summary>
    public class ScheduledTaskScheduler
# 添加错误处理
    {
        private Timer _timer;
# 改进用户体验
        private TimeSpan _interval;
        private Func<Task> _task;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduledTaskScheduler"/> class.
        /// </summary>
        /// <param name="interval">The interval between tasks in milliseconds.</param>
        /// <param name="task