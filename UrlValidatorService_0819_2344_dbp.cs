// 代码生成时间: 2025-08-19 23:44:15
// UrlValidatorService.cs
// 这个类提供了URL链接有效性验证的功能。

using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Maui;

namespace UrlValidatorApp
{
    /// <summary>
    /// URL链接有效性验证服务
    /// </summary>
    public class UrlValidatorService
    {
        private HttpClient httpClient;

        public UrlValidatorService()
        {
            httpClient = new HttpClient();
        }

        /// <summary>
        /// 验证URL链接是否有效
        /// </summary>
        /// <param name="url">待验证的URL链接</param>
        /// <returns>返回一个布尔值，表示URL是否有效</returns>
        public async Task<bool> ValidateUrlAsync(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("URL cannot be null or empty.", nameof(url));
            }

            try
            {
                var response = await httpClient.GetAsync(url);
                return response.StatusCode == HttpStatusCode.OK;
            }
            catch (HttpRequestException ex)
            {
                // 记录或处理异常
                Console.WriteLine($"Error occurred: {ex.Message}");
                return false;
            }
        }
    }
}
