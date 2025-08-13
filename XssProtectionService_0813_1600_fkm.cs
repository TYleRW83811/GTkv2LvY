// 代码生成时间: 2025-08-13 16:00:22
// <copyright file="XssProtectionService.cs" company="YourCompany">
//     Copyright (c) YourCompany. All rights reserved.
// </copyright>

using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace YourApp
{
    /// <summary>
    /// Provides services for preventing XSS (Cross-Site Scripting) attacks.
    /// </summary>
    public class XssProtectionService
    {
        /// <summary>
        /// Sanitizes the input string to prevent XSS attacks.
        /// </summary>
        /// <param name="input">The input string that needs to be sanitized.</param>
        /// <returns>The sanitized string.</returns>
        public string SanitizeInput(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            // Replace HTML tags with their corresponding text representation
            input = Regex.Replace(input, "<[^>]+>