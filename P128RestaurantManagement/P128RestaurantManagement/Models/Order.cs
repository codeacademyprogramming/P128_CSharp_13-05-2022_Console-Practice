using System;
using System.Collections.Generic;
using System.Text;

namespace P128RestaurantManagement.Models
{
    class Order
    {
        private static int _count;
        public int No { get;  }
        public List<OrderItem> OrderItems { get; set; }
        public double TotalAmount { get; set; }
        public DateTime Date { get; set; }

        static Order()
        {
            _count = 0;
        }

        public Order()
        {
            _count++;
            No = _count;
        }

        public override string ToString()
        {
            foreach (var item in OrderItems)
            {
                Console.WriteLine(item);
            }

            return $"Sifareisin Nomrs {No} Umumi Mebleg: {TotalAmount.ToString("0.00")} Tarix: {Date.ToString("dd.MM.yyyy")}";
        }
    }
}
