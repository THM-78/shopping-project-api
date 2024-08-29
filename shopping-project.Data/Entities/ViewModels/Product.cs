using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace shopping_project.Data.Entities.ViewModels
{
    public class Product
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int Stock {  get; set; }
        public string? Color { get; set; }
        public int CategoryId { get; set; }


        public Product()
        {
            
        }
        [JsonConstructor]
        public Product(int id, string name, decimal price, string description, int stock, string? color, int categoryId)
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
            Stock = stock;
            Color = color;
            CategoryId = categoryId;
        }
        public Product(TblProduct product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            Description = product.Description ?? null;
            Stock = product.Stock ?? 0;
            Color = product.Color;
            CategoryId = Convert.ToInt32(product.CategoryId); 
        }
    }
}
