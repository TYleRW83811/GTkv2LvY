// 代码生成时间: 2025-09-23 23:09:25
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;

namespace ShoppingCartMauiApp
{
    // Define a Product class to represent items in the shopping cart
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    // Define a ShoppingCart class to manage the shopping cart
    public class ShoppingCart
    {
        private List<Product> _products = new List<Product>();

        // Add a product to the shopping cart
        public void AddProduct(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                existingProduct.Quantity += product.Quantity;
            }
            else
            {
                _products.Add(product);
            }
        }

        // Remove a product from the shopping cart
        public void RemoveProduct(string productId)
        {
            if (string.IsNullOrEmpty(productId)) throw new ArgumentException("Product ID cannot be null or empty.", nameof(productId));

            var productToRemove = _products.FirstOrDefault(p => p.Id == productId);
            if (productToRemove != null)
            {
                _products.Remove(productToRemove);
            }
            else
            {
                throw new InvalidOperationException($"Product with ID {productId} not found in the shopping cart.");
            }
        }

        // Update the quantity of a product in the shopping cart
        public void UpdateQuantity(string productId, int newQuantity)
        {
            if (string.IsNullOrEmpty(productId)) throw new ArgumentException("Product ID cannot be null or empty.", nameof(productId));
            if (newQuantity < 0) throw new ArgumentOutOfRangeException(nameof(newQuantity), "Quantity cannot be negative.");

            var productToUpdate = _products.FirstOrDefault(p => p.Id == productId);
            if (productToUpdate != null)
            {
                productToUpdate.Quantity = newQuantity;
            }
            else
            {
                throw new InvalidOperationException($"Product with ID {productId} not found in the shopping cart.");
            }
        }

        // Get the total cost of the shopping cart
        public decimal GetTotalCost()
        {
            return _products.Sum(p => p.Price * p.Quantity);
        }

        // Get a list of products in the shopping cart
        public List<Product> GetProducts()
        {
            return _products.ToList();
        }
    }

    // Define a ViewModel for the shopping cart page
    public class ShoppingCartViewModel : BindableObject
    {
        public ShoppingCartViewModel(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
            Products = new List<Product>(_shoppingCart.GetProducts());
        }

        private ShoppingCart _shoppingCart;
        public List<Product> Products { get; private set; }
        public decimal TotalCost => _shoppingCart.GetTotalCost();

        // Method to add a product to the cart
        public void AddToCart(Product product)
        {
            _shoppingCart.AddProduct(product);
            Products.Add(product);
            OnPropertyChanged(nameof(Products));
            OnPropertyChanged(nameof(TotalCost));
        }

        // Method to remove a product from the cart
        public void RemoveFromCart(string productId)
        {
            try
            {
                _shoppingCart.RemoveProduct(productId);
                Products.RemoveAll(p => p.Id == productId);
                OnPropertyChanged(nameof(Products));
                OnPropertyChanged(nameof(TotalCost));
            }
            catch (Exception ex)
            {
                // Handle error, e.g., display a message to the user
                Console.WriteLine($"Error removing product: {ex.Message}");
            }
        }

        // Method to update the quantity of a product
        public void UpdateQuantity(string productId, int newQuantity)
        {
            try
            {
                _shoppingCart.UpdateQuantity(productId, newQuantity);
                var product = Products.FirstOrDefault(p => p.Id == productId);
                if (product != null)
                {
                    product.Quantity = newQuantity;
                    OnPropertyChanged(nameof(Products));
                    OnPropertyChanged(nameof(TotalCost));
                }
            }
            catch (Exception ex)
            {
                // Handle error, e.g., display a message to the user
                Console.WriteLine($"Error updating quantity: {ex.Message}");
            }
        }
    }
}
