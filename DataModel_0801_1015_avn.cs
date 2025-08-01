// 代码生成时间: 2025-08-01 10:15:52
 * The models are designed to be simple and maintainable,
 * with clear structure and appropriate error handling.
 */

using System;
using System.Collections.Generic;

namespace YourNamespace.Models
{
    /// <summary>
    /// Represents a basic data model with common properties.
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// Unique identifier for the model.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Timestamp for when the model was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Timestamp for when the model was last updated.
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }

    /// <summary>
    /// Represents a user entity in the application.
    /// </summary>
    public class User : BaseModel
    {
        /// <summary>
        /// The user's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The user's email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Initializes a new instance of the User class.
        /// </summary>
        public User()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// Represents a product entity in the application.
    /// </summary>
    public class Product : BaseModel
    {
        /// <summary>
        /// The product's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The product's price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The product's description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Initializes a new instance of the Product class.
        /// </summary>
        public Product()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
