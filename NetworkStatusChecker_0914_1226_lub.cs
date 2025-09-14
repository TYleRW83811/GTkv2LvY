// 代码生成时间: 2025-09-14 12:26:13
using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;

namespace NetworkStatusChecker
{
    public class NetworkStatusChecker : INotifyPropertyChanged
    {
        private bool isNetworkAvailable;
        public bool IsNetworkAvailable
        {
            get => isNetworkAvailable;
            set
            {
                isNetworkAvailable = value;
                OnPropertyChanged();
            }
        }

        private string networkStatusMessage;
        public string NetworkStatusMessage
        {
            get => networkStatusMessage;
            set
            {
                networkStatusMessage = value;
                OnPropertyChanged();
            }
        }

        public NetworkStatusChecker()
        {
            // Initial check for network connectivity
            IsNetworkAvailable = CheckNetworkAvailability();
        }

        // Method to check network availability
        private bool CheckNetworkAvailability()
        {
            try
            {
                return NetworkInterface.GetIsNetworkAvailable();
            }
            catch (Exception ex)
            {
                NetworkStatusMessage = $"Error checking network status: {ex.Message}";
                return false;
            }
        }

        // Method to periodically check network status
        public async Task CheckNetworkStatusPeriodicallyAsync(TimeSpan interval)
        {
            while (true)
            {
                await Task.Delay(interval);
                IsNetworkAvailable = CheckNetworkAvailability();
            }
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
