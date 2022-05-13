using System;
using System.Collections.Generic;
using System.Text;
using P128RestaurantManagement.Enums;
using P128RestaurantManagement.Models;

namespace P128RestaurantManagement.Interfaces
{
    interface IRestaurantManager
    {
        List<MenuItem> MenuItems { get; }
        List<Order> Orders { get; }
        void AddOrder(List<OrderItem> orderItems);
        void RemoveOrder(int no);
        List<Order> GetOrdersByDatesInterval(DateTime startDate, DateTime endDate);
        List<Order> GetOrderByDate(DateTime date);
        List<Order> GetOrdersByPriceInterval(double minPrice, double maxPrice);
        Order GetOrderByNo(int no);
        void AddMenuItem(string name, double price, Category category);
        void EditMenuITem(string no, string name, double price, Category category);
        List<MenuItem> GetMenuItemByCategroy(Category category);
        List<MenuItem> GetMenuItemByPriceInterval(double minPrice, double maxPrice);
        List<MenuItem> SearchMenuItem(string search);
        void RemoveMenuItem(string no);
    }
}
