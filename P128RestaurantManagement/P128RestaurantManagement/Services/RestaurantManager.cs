using System;
using System.Collections.Generic;
using System.Text;
using P128RestaurantManagement.Enums;
using P128RestaurantManagement.Models;
using P128RestaurantManagement.Interfaces;
using System.Linq;

namespace P128RestaurantManagement.Services
{
    class RestaurantManager : IRestaurantManager
    {
        private List<MenuItem> _menuItems;
        public List<MenuItem> MenuItems => _menuItems;
        private List<Order> _orders;
        public List<Order> Orders => _orders;

        public RestaurantManager()
        {
            _menuItems = new List<MenuItem>();
            _orders = new List<Order>();
        }

        public void AddMenuItem(string name, double price, Category category)
        {
            MenuItem menuItem = new MenuItem(name, category, price);
            _menuItems.Add(menuItem);
        }

        public void AddOrder(List<OrderItem> orderItems)
        {
            Order order = new Order
            {
                Date = DateTime.UtcNow.AddHours(4),
                OrderItems = orderItems,
                TotalAmount = orderItems.Sum(o => o.MenuItem.Price * o.TotalCount)
            };

            _orders.Add(order);
        }

        public void EditMenuITem(string no, string name, double price, Category category)
        {
            MenuItem menuItem = _menuItems.Find(m => m.No == no.ToUpper());

            if (menuItem != null && !_menuItems.Any(m=>m.No != no.ToUpper() && m.Name == name.Trim().ToUpper()))
            {
                menuItem.Name = name;
                menuItem.Price = price;
                menuItem.No = menuItem.No.Replace(
                    oldValue: menuItem.Category == Category.Soup ? menuItem.Category.ToString().Substring(0, 1).ToUpper() : menuItem.Category.ToString().Substring(0, 2).ToUpper(),
                    newValue: category == Category.Soup ? category.ToString().Substring(0, 1).ToUpper() : category.ToString().Substring(0, 2).ToUpper());
                menuItem.Category = category;
                return;
            }
            Console.WriteLine("Edit Menu Item Duzgun Daxil Edit");
        }

        public List<MenuItem> GetMenuItemByCategroy(Category category)
        {
            return _menuItems.FindAll(m => m.Category == category);
        }

        public List<MenuItem> GetMenuItemByPriceInterval(double minPrice, double maxPrice)
        {
            return _menuItems.FindAll(m => m.Price >= minPrice && m.Price <= maxPrice);
        }

        public List<Order> GetOrderByDate(DateTime date)
        {
            return _orders.FindAll(o => o.Date.ToString("dd.MM.yyyy") == date.ToString("dd.MM.yyyy"));
        }

        public Order GetOrderByNo(int no)
        {
            Order order = _orders.Find(o => o.No == no);

            if (order == null)
                throw new NullReferenceException($"{no} -lu Order Tapilmadi");

            return order;
        }

        public List<Order> GetOrdersByDatesInterval(DateTime startDate, DateTime endDate)
        {
            return _orders.FindAll(o => o.Date >= startDate && o.Date <= endDate);
        }

        public List<Order> GetOrdersByPriceInterval(double minPrice, double maxPrice)
        {
            return _orders.FindAll(o => o.TotalAmount >= minPrice && o.TotalAmount <= minPrice);
        }

        public void RemoveOrder(int no)
        {
            Order order = _orders.Find(o => o.No == no);

            if (order == null)
                throw new NullReferenceException($"{no} -lu Order Tapilmadi");

            _orders.Remove(order);
        }

        public List<MenuItem> SearchMenuItem(string search)
        {
            return _menuItems.FindAll(m => m.No.Contains(search) || m.Name.Contains(search) || m.Category.ToString().ToUpper().Contains(search) || m.Price.ToString().Contains(search));
        }

        public void RemoveMenuItem(string no)
        {
            MenuItem menuItem = _menuItems.Find(m => m.No == no.Trim().ToUpper());

            if (menuItem == null)
                throw new NullReferenceException($"{no} - lu Menu Item Tapilmadi");

            _menuItems.Remove(menuItem);
        }
    }
}
