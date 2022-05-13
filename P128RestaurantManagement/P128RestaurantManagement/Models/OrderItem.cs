using System;
using System.Collections.Generic;
using System.Text;

namespace P128RestaurantManagement.Models
{
    class OrderItem
    {
        public MenuItem MenuItem { get; set; }
        public int TotalCount { get; set; }

        public override string ToString()
        {
            return $"{MenuItem} {TotalCount}";
        }
    }
}
