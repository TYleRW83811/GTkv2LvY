// 代码生成时间: 2025-09-13 12:38:14
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace MAUIApp
{
    /// <summary>
    /// Provides functionality to validate URL links for validity.
    /// </summary>
    public class UrlValidatorService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlValidatorService"/> class.
        /// </summary>
        public UrlValidatorService()
        {
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Validates if the provided URL is valid by checking its status code.
        /// </summary>
        /// <param name="url">The URL to be validated.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A <see cref="bool"/> indicating whether the URL is valid.</returns>
        public async Task<bool> IsValidUrlAsync(string url, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("URL cannot be null or whitespace.", nameof(url));
            }

            try
            {
                var response = await _httpClient.GetAsync(url, cancellationToken);
                return response.StatusCode == HttpStatusCode.OK;
            }
            catch (HttpRequestException e)
            {
                // Log the exception or handle it according to your application's error handling policy.
                Console.WriteLine($"An error occurred while checking URL validity: {e.Message}");
                return false;
            }
        }
    }
}
