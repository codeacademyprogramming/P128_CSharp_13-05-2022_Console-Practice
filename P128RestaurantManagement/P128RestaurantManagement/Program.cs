using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using P128RestaurantManagement.Enums;
using P128RestaurantManagement.Models;
using P128RestaurantManagement.Services;

namespace P128RestaurantManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            RestaurantManager restaurantManager = new RestaurantManager();

            do
            {
                Console.WriteLine("1. Menu Uzerinde Emeliyyatlar");
                Console.WriteLine("2. Sifaris Uzerinde Emeliyyatlar");
                Console.WriteLine("3. Sistemden Cixis");

                string choose = Console.ReadLine();
                int chooseNum;

                while (!int.TryParse(choose, out chooseNum) || chooseNum < 1 || chooseNum > 3)
                {
                    Console.WriteLine("Duzgun Secimm Edin");
                    choose = Console.ReadLine();
                }

                switch (chooseNum)
                {
                    case 1:
                        MenuOperations(ref restaurantManager);
                        break;
                    case 2:
                        OrderOperations(ref restaurantManager);
                        break;
                    case 3:
                        return;
                }
            } while (true);
        }
        #region Menu Item Operations
        static void MenuOperations(ref RestaurantManager restaurantManager)
        {
            do
            {
                Console.WriteLine("1. Yeni Menu Item Elave Et");
                Console.WriteLine("2. Menu Item Uzerinde Duzelis Et");
                Console.WriteLine("3. Menu Item Sil");
                Console.WriteLine("4. Butun Menu Itemlari Gosder");
                Console.WriteLine("5. Novune Gore Butun Menu Itemlari Gosder");
                Console.WriteLine("6. Qiymet Araligina Gore Butun Menu Itemlari Gosder");
                Console.WriteLine("7. Axdaris");
                Console.WriteLine("8. Ana Menyuya Qayit");

                string choose = Console.ReadLine();
                int chooseNum;

                while (!int.TryParse(choose, out chooseNum) || chooseNum < 1 || chooseNum > 8)
                {
                    Console.WriteLine("Duzgun Secimm Edin");
                    choose = Console.ReadLine();
                }

                switch (chooseNum)
                {
                    case 1:
                        AddMenuItem(ref restaurantManager);
                        break;
                    case 2:
                        EditMenuITem(ref restaurantManager);
                        break;
                    case 3:
                        RemoveMenuItem(ref restaurantManager);
                        break;
                    case 4:
                        ShowAllMenuItem(ref restaurantManager);
                        break;
                    case 5:
                        GetMenuItemByCategroy(ref restaurantManager);
                        break;
                    case 6:
                        GetMenuItemByPriceInterval(ref restaurantManager);
                        break;
                    case 7:
                        SearchMenuItem(ref restaurantManager);
                        break;
                    case 8:
                        return;
                }
            } while (true);
        }

        static void AddMenuItem(ref RestaurantManager restaurantManager)
        {
            Console.WriteLine("Menu Item Adini Daxil Et");
            string name = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(name) || restaurantManager.MenuItems.Any(m=>m.Name == name.Trim().ToUpper()))
            {
                Console.WriteLine("Dzugun Ad Daxil Et");
                name = Console.ReadLine();
            }

            Console.WriteLine("Menu Item Qiymetini Daxil Et");
            string priceStr = Console.ReadLine();
            double price;

            while (!double.TryParse(priceStr, out price) || price < 0)
            {
                Console.WriteLine("Duzgun Qiymet Daxil Et");
                priceStr = Console.ReadLine();
            }

            Console.WriteLine("Menu Item Novunu Sec");
            foreach (var item in Enum.GetValues(typeof(Category)))
            {
                Console.WriteLine($"{(int)item} {item}");
            }
            string categoryStr = Console.ReadLine();
            int categoryNum;

            while (!int.TryParse(categoryStr, out categoryNum) || categoryNum < 1 || categoryNum > 4)
            {
                Console.WriteLine("Duzgun Nov Sec");
                categoryStr = Console.ReadLine();
            }

            restaurantManager.AddMenuItem(name.Trim().ToUpper(), price, (Category)categoryNum);
        }

        static void EditMenuITem(ref RestaurantManager restaurantManager)
        {
            Console.WriteLine("Duzelis Etmek Isdediyniz Menu Item-i Siyahidan Secin Nomresini Daxil Edin");
            foreach (MenuItem menuItem in restaurantManager.MenuItems)
            {
                Console.WriteLine(menuItem);
            }
            string no = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(no) || !restaurantManager.MenuItems.Any(m=>m.No == no.Trim().ToUpper()) )
            {
                Console.WriteLine("Duzgun Nomre Sec");
                no = Console.ReadLine();
            }

            Console.WriteLine("Menu Item Adini Daxil Et");
            string name = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(name) || restaurantManager.MenuItems.Any(m =>m.No != no && m.Name == name.Trim().ToUpper()))
            {
                Console.WriteLine("Dzugun Ad Daxil Et");
                name = Console.ReadLine();
            }

            Console.WriteLine("Menu Item Qiymetini Daxil Et");
            string priceStr = Console.ReadLine();
            double price;

            while (!double.TryParse(priceStr, out price) || price < 0)
            {
                Console.WriteLine("Duzgun Qiymet Daxil Et");
                priceStr = Console.ReadLine();
            }

            Console.WriteLine("Menu Item Novunu Sec");
            foreach (var item in Enum.GetValues(typeof(Category)))
            {
                Console.WriteLine($"{(int)item} {item}");
            }
            string categoryStr = Console.ReadLine();
            int categoryNum;

            while (!int.TryParse(categoryStr, out categoryNum) || categoryNum < 1 || categoryNum > 4)
            {
                Console.WriteLine("Duzgun Nov Sec");
                categoryStr = Console.ReadLine();
            }

            restaurantManager.EditMenuITem(no, name, price, (Category)categoryNum);
        }

        static void RemoveMenuItem(ref RestaurantManager restaurantManager)
        {
            Console.WriteLine("Duzelis Etmek Isdediyniz Menu Item-i Siyahidan Secin Nomresini Daxil Edin");
            foreach (MenuItem menuItem in restaurantManager.MenuItems)
            {
                Console.WriteLine(menuItem);
            }
            string no = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(no) || !restaurantManager.MenuItems.Any(m => m.No == no.Trim().ToUpper()))
            {
                Console.WriteLine("Duzgun Nomre Sec");
                no = Console.ReadLine();
            }

            restaurantManager.RemoveMenuItem(no);
        }

        static void ShowAllMenuItem(ref RestaurantManager restaurantManager)
        {
            foreach (MenuItem menuItem in restaurantManager.MenuItems)
            {
                Console.WriteLine(menuItem);
            }
        }

        static void GetMenuItemByCategroy(ref RestaurantManager restaurantManager)
        {
            Console.WriteLine("Menu Item Novunu Sec");
            foreach (var item in Enum.GetValues(typeof(Category)))
            {
                Console.WriteLine($"{(int)item} {item}");
            }
            string categoryStr = Console.ReadLine();
            int categoryNum;

            while (!int.TryParse(categoryStr, out categoryNum) || categoryNum < 1 || categoryNum > 4)
            {
                Console.WriteLine("Duzgun Nov Sec");
                categoryStr = Console.ReadLine();
            }

            foreach (MenuItem item in restaurantManager.GetMenuItemByCategroy((Category)categoryNum))
            {
                Console.WriteLine(item);
            }
        }

        static void GetMenuItemByPriceInterval(ref RestaurantManager restaurantManager)
        {
            step1:
            Console.WriteLine("Baslangic Qiymeti Daxil Et:");
            string minStr = Console.ReadLine();
            double min;

            while (!double.TryParse(minStr, out min) || min < 0)
            {
                Console.WriteLine("Duzgun Balangic Qiymet Daxil Et");
                minStr = Console.ReadLine();
            }

            Console.WriteLine("Son Qiymeti Daxil Et:");
            string maxStr = Console.ReadLine();
            double max;

            while (!double.TryParse(maxStr, out max) || max < 0)
            {
                Console.WriteLine("Duzgun Son Qiymet Daxil Et");
                maxStr = Console.ReadLine();
            }

            if (min > max)
            {
                goto step1;
            }

            foreach (MenuItem item in restaurantManager.GetMenuItemByPriceInterval(min,max))
            {
                Console.WriteLine(item);
            }
        }

        static void SearchMenuItem(ref RestaurantManager restaurantManager)
        {
            Console.WriteLine("Axdaris Deyerini Daxil Et");
            string search = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(search))
            {
                Console.WriteLine("Duzgun Axtaris Deyeri Daxil Et");
                search = Console.ReadLine();
            }

            foreach (MenuItem item in restaurantManager.SearchMenuItem(search))
            {
                Console.WriteLine(item);
            }
        }
        #endregion

        #region Order Operations
        static void OrderOperations(ref RestaurantManager restaurantManager)
        {
            do
            {
                Console.WriteLine("1. Yeni sifaris elave etmek");
                Console.WriteLine("2. Sifarisin legvi");
                Console.WriteLine("3. Butun sifarisler");
                Console.WriteLine("4. Tarix Araligina Gore Butun sifarisler");
                Console.WriteLine("5. Mebleg Araligina Gore Butun sifarisler");
                Console.WriteLine("6. Tarixe Gore Butun sifarisler");
                Console.WriteLine("7. Nomresine Gore Sifarisin Tapilmasi");
                Console.WriteLine("8. Ana Menuya Qayit");

                string choose = Console.ReadLine();
                int chooseNum;

                while (!int.TryParse(choose,out chooseNum) || chooseNum < 1 || chooseNum > 8)
                {
                    Console.WriteLine("Duzgun Secim Edin");
                    choose = Console.ReadLine();
                }

                switch (chooseNum)
                {
                    case 1:
                        AddOrder(ref restaurantManager);
                        break;
                    case 2:
                        RemoveOrder(ref restaurantManager);
                        break;
                    case 3:
                        ShowAllOrder(ref restaurantManager);
                        break;
                    case 4:
                        GetOrdersByDatesInterval(ref restaurantManager);
                        break;
                    case 5:
                        GetOrdersByPriceInterval(ref restaurantManager);
                        break;
                    case 6:
                        GetOrderByDate(ref restaurantManager);
                        break;
                    case 7:
                        GetOrderByNo(ref restaurantManager);
                        break;
                    case 8:
                        return;
                }

            } while (true);
        }

        static void AddOrder(ref RestaurantManager restaurantManager)
        {
            List<OrderItem> orderItems = new List<OrderItem>();

            Console.WriteLine("Menyu:");
            foreach (MenuItem item in restaurantManager.MenuItems)
            {
                Console.WriteLine(item);
            }
            step1:
            Console.WriteLine("Menyudan Secin Nomresini Daxil Edin");
            string noStr = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(noStr) || !restaurantManager.MenuItems.Any(m=>m.No == noStr.Trim().ToUpper()))
            {
                Console.WriteLine("Duzgun Secim Edin");
                noStr = Console.ReadLine();
            }

            Console.WriteLine("Sayini daxil Edin");
            string countStr = Console.ReadLine();
            int count;
            while (!int.TryParse(countStr,out count) || count < 1)
            {
                Console.WriteLine("Duzgun Say Daxil Edin");
                countStr = Console.ReadLine();
            }

            OrderItem orderItem = new OrderItem
            {
                TotalCount = count,
                MenuItem = restaurantManager.MenuItems.Find(m => m.No == noStr.Trim().ToUpper())
            };

            orderItems.Add(orderItem);

            Console.WriteLine("Elave Nese Isdiyirsinizmi  y/n");
            string choose = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(choose) || (choose.Trim().ToLower() != "y" && choose.Trim().ToLower() != "n"))
            {
                Console.WriteLine("Duzgun Secim Edin");
                choose = Console.ReadLine();
            }

            if (choose.Trim().ToLower() == "y")
            {
                goto step1;
            }

            restaurantManager.AddOrder(orderItems);
        }

        static void RemoveOrder(ref RestaurantManager restaurantManager)
        {
            Console.WriteLine("Siferisler");
            foreach (Order item in restaurantManager.Orders)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Legv Etmenk Isdediyniz Sifarisin Nomresini Daxil Et");
            string noStr = Console.ReadLine();
            int no;

            while (!int.TryParse(noStr,out no) || no < 1)
            {
                Console.WriteLine("Duzgun Nomre Daxil Et");
                noStr = Console.ReadLine();
            }

            restaurantManager.RemoveOrder(no);
        }

        static void ShowAllOrder(ref RestaurantManager restaurantManager)
        {
            Console.WriteLine("Siferisler");
            foreach (Order item in restaurantManager.Orders)
            {
                Console.WriteLine(item);
            }
        }

        static void GetOrdersByDatesInterval(ref RestaurantManager restaurantManager)
        {
            Console.WriteLine("Siferisler");
            foreach (Order item in restaurantManager.Orders)
            {
                Console.WriteLine(item);
            }

            Regex regex = new Regex(@"^((((([0]{1}[1-9])|([1-2]{1}[0-9]))\.([0]{1}[2]{1}))|(([0]{1}[1-9]{1})|([1-2]{1}[0-9]{1})|([3]{1}[0]{1})\.(([0]{1}(([4]{1})|([6]{1})|([9]{1})))|([1]{2})))|((([0]{1}[1-9]{1})|([1-2]{1}[1-9]{1})|([3]{1}[0-1]{1}))\.(([0]{1}(([1]{1})|([3]{1})|([5]{1})|([7]{1})|([8]{1})))|([1]{1}[0,2]{1}))))\.((([0-9]{2})((([0]{1})|([2]{1})|([4]{1})|([6]{1})|([8]{1}))(([0]{1})|([4]{1})|([8]{1}))))|(([0-9]{2})((([1]{1})|([3]{1})|([5]{1})|([7]{1})|([9]{1}))(([2]{1})|([6]{1}))))))|((((([0]{1}[1-9])|([1-2]{1}[0-8]))\.([0]{1}[2]{1}))|(([0]{1}[1-9]{1})|([1-2]{1}[0-9]{1})|([3]{1}[0]{1})\.(([0]{1}(([4]{1})|([6]{1})|([9]{1})))|([1]{2})))|((([0]{1}[1-9]{1})|([1-2]{1}[1-9]{1})|([3]{1}[0-1]{1}))\.(([0]{1}(([1]{1})|([3]{1})|([5]{1})|([7]{1})|([8]{1})))|([1]{1}[0,2]{1}))))\.([0-9]{4}))$");

            Console.WriteLine("Baslangic Tarixi Daxil Edin (ex: 23.11.2022)");
            step1:
            string startDateStr = Console.ReadLine();
            while (!regex.IsMatch(startDateStr))
            {
                Console.WriteLine("Duzgun Tarix Daxil Et (ex: 23.11.2022)");
                startDateStr = Console.ReadLine();
            }

            string[] date = startDateStr.Split('.');

            DateTime starDate = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));

            Console.WriteLine("Son Tarixi Daxil Edin (ex: 23.11.2022)");
            string endDateStr = Console.ReadLine();
            while (!regex.IsMatch(endDateStr))
            {
                Console.WriteLine("Duzgun Tarix Daxil Et (ex: 23.11.2022)");
                endDateStr = Console.ReadLine();
            }

            date = endDateStr.Split('.');

            DateTime endDate = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));

            if (starDate > endDate)
            {
                Console.WriteLine("Duzgun Tarix Araligi Daxil Et");
                goto step1;
            }

            foreach (var item in restaurantManager.GetOrdersByDatesInterval(starDate, endDate))
            {
                Console.WriteLine(item);
            }
        }

        static void GetOrdersByPriceInterval(ref RestaurantManager restaurantManager)
        {
            Console.WriteLine("Siferisler");
            foreach (Order item in restaurantManager.Orders)
            {
                Console.WriteLine(item);
            }
            step1:
            Console.WriteLine("Baslangic Meblegi Daxil Et");
            string minPriceStr = Console.ReadLine();
            double minPrice;
            while (!double.TryParse(minPriceStr,out minPrice) || minPrice < 0)
            {
                Console.WriteLine("Duzgun Daxil Et");
                minPriceStr = Console.ReadLine();
            }

            Console.WriteLine("Son Meblegi Daxil Et");
            string maxPriceStr = Console.ReadLine();
            double maxPrice;
            while (!double.TryParse(maxPriceStr, out maxPrice) || maxPrice < 0)
            {
                Console.WriteLine("Duzgun Daxil Et");
                maxPriceStr = Console.ReadLine();
            }

            if (minPrice > maxPrice)
            {
                Console.WriteLine("Duzgun Mebleg Araligi Daxil Et");
                goto step1;
            }

            foreach (var item in restaurantManager.GetMenuItemByPriceInterval(minPrice, maxPrice))
            {
                Console.WriteLine(item);
            }
        }

        static void GetOrderByDate(ref RestaurantManager restaurantManager)
        {
            Console.WriteLine("Siferisler");
            foreach (Order item in restaurantManager.Orders)
            {
                Console.WriteLine(item);
            }

            Regex regex = new Regex(@"^((((([0]{1}[1-9])|([1-2]{1}[0-9]))\.([0]{1}[2]{1}))|(([0]{1}[1-9]{1})|([1-2]{1}[0-9]{1})|([3]{1}[0]{1})\.(([0]{1}(([4]{1})|([6]{1})|([9]{1})))|([1]{2})))|((([0]{1}[1-9]{1})|([1-2]{1}[1-9]{1})|([3]{1}[0-1]{1}))\.(([0]{1}(([1]{1})|([3]{1})|([5]{1})|([7]{1})|([8]{1})))|([1]{1}[0,2]{1}))))\.((([0-9]{2})((([0]{1})|([2]{1})|([4]{1})|([6]{1})|([8]{1}))(([0]{1})|([4]{1})|([8]{1}))))|(([0-9]{2})((([1]{1})|([3]{1})|([5]{1})|([7]{1})|([9]{1}))(([2]{1})|([6]{1}))))))|((((([0]{1}[1-9])|([1-2]{1}[0-8]))\.([0]{1}[2]{1}))|(([0]{1}[1-9]{1})|([1-2]{1}[0-9]{1})|([3]{1}[0]{1})\.(([0]{1}(([4]{1})|([6]{1})|([9]{1})))|([1]{2})))|((([0]{1}[1-9]{1})|([1-2]{1}[1-9]{1})|([3]{1}[0-1]{1}))\.(([0]{1}(([1]{1})|([3]{1})|([5]{1})|([7]{1})|([8]{1})))|([1]{1}[0,2]{1}))))\.([0-9]{4}))$");

            Console.WriteLine("Tarixi Daxil Edin (ex: 23.11.2022)");

            string dateStr = Console.ReadLine();
            while (!regex.IsMatch(dateStr))
            {
                Console.WriteLine("Duzgun Tarix Daxil Et (ex: 23.11.2022)");
                dateStr = Console.ReadLine();
            }

            string[] date = dateStr.Split('.');

            DateTime Date = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));

            foreach (var item in restaurantManager.GetOrderByDate(Date))
            {
                Console.WriteLine(item);
            }
        }

        static void GetOrderByNo(ref RestaurantManager restaurantManager)
        {
            Console.WriteLine("Siferisler");
            foreach (Order item in restaurantManager.Orders)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Isdediyniz Sifarisin Nomresini Daxil Et");
            string noStr = Console.ReadLine();
            int no;

            while (!int.TryParse(noStr, out no) || no < 1)
            {
                Console.WriteLine("Duzgun Nomre Daxil Et");
                noStr = Console.ReadLine();
            }

            Console.WriteLine(restaurantManager.GetOrderByNo(no));
        }
        #endregion
    }
}