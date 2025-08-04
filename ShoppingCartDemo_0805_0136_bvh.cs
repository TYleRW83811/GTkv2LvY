// 代码生成时间: 2025-08-05 01:36:12
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace ShoppingCartDemo
{
    // 定义一个商品类
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    // 定义购物车类，包含添加商品、删除商品和计算总价等方法
    public class ShoppingCart
    {
        private Dictionary<string, Product> _products = new Dictionary<string, Product>();

        // 添加商品到购物车
        public void AddProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product cannot be null");
            }

            if (_products.ContainsKey(product.Id))
            {
                _products[product.Id].Quantity += product.Quantity;
            }
            else
            {
                _products.Add(product.Id, product);
            }
        }

        // 从购物车删除商品
        public void RemoveProduct(string productId)
        {
            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentException("Product ID cannot be null or empty");
            }

            if (_products.ContainsKey(productId))
            {
                _products.Remove(productId);
            }
            else
            {
                throw new KeyNotFoundException("Product not found in the shopping cart");
            }
        }

        // 计算购物车总价
        public decimal CalculateTotal()
        {
            return _products.Values.Sum(p => p.Price * p.Quantity);
        }
    }

    // MAUI页面类，包含购物车界面
    public class ShoppingCartPage : ContentPage
    {
        private ShoppingCart _cart = new ShoppingCart();
        private ListView _productList;
        private Button _addButton;
        private Button _removeButton;
        private Label _totalLabel;

        public ShoppingCartPage()
        {
            // 初始化页面布局和控件
            InitLayout();
        }

        private void InitLayout()
        {
            // 添加商品列表
            _productList = new ListView();
            _productList.ItemsSource = _cart.Products;

            // 添加添加按钮
            _addButton = new Button
            {
                Text = "Add Product"
            };
            _addButton.Clicked += (sender, e) =>
            {
                // 处理添加商品逻辑
            };

            // 添加移除按钮
            _removeButton = new Button
            {
                Text = "Remove Product"
            };
            _removeButton.Clicked += (sender, e) =>
            {
                // 处理移除商品逻辑
            };

            // 添加总价标签
            _totalLabel = new Label
            {
                Text = "Total: 0"
            };

            // 布局控件
            StackLayout layout = new StackLayout
            {
                Children =
                {
                    _productList,
                    _addButton,
                    _removeButton,
                    _totalLabel
                }
            };

            Content = layout;
        }

        // 更新购物车总价显示
        private void UpdateTotal()
        {
            _totalLabel.Text = $"Total: {_cart.CalculateTotal()}";
        }
    }
}
