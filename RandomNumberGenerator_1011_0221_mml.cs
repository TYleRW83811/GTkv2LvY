// 代码生成时间: 2025-10-11 02:21:21
 * It includes error handling and follows C# best practices for readability and maintainability.
 */

using System;

namespace MAUIApp
{
    public class RandomNumberGenerator
    {
        private readonly Random random;

        // Constructor initializes the Random instance
        public RandomNumberGenerator()
        {
            random = new Random();
        }

        // Method to generate a random integer between the specified minimum and maximum values
        public int GenerateInteger(int minValue, int maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentException("Minimum value cannot be greater than maximum value.");
            }

            return random.Next(minValue, maxValue + 1);
        }

        // Method to generate a random double between 0 and 1
        public double GenerateDouble()
        {
            return random.NextDouble();
        }

        // Additional methods for generating random numbers can be added here

        // Method to generate a random boolean value
        public bool GenerateBoolean()
        {
            return random.Next(2) == 1;
        }

        #region IDisposable Support
        // Implement IDisposable to properly dispose of the Random instance
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // Dispose managed state (managed objects)
                }

                // Free unmanaged resources (unmanaged objects) and override finalizer
                random = null;

                disposedValue = true;
            }
        }

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion IDisposable Support
    }
}
