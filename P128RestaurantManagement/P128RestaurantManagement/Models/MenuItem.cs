using System;
using System.Collections.Generic;
using System.Text;
using P128RestaurantManagement.Enums;

namespace P128RestaurantManagement.Models
{
    class MenuItem
    {
        private static int _count;
        public string No { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public double Price { get; set; }

        static MenuItem()
        {
            _count = 100;
        }

        public MenuItem(string name, Category category, double price)
        {
            _count++;
            No = (category == Category.Soup ? category.ToString().ToUpper().Substring(0,1) : category.ToString().ToUpper().Substring(0, 2)) + _count;
            Name = name.Trim().ToUpper(); ;
            Category = category;
            Price = price;

            //if (category == Category.Soup)
            //{
            //    No = $"{category.ToString()[0]}{_count}";
            //}
            //else
            //{
            //    No = $"{category.ToString().Substring(0,2)}{_count}";
            //}
        }

        public override string ToString()
        {
            return $"No: {No} Ad: {Name} Qiymet: {Price} Novu: {Category}";
        }
    }
}
