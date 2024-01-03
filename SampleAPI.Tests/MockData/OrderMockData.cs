using SampleAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleAPI.Tests.MockData
{
    /// <summary>
    /// Mock data
    /// </summary>
    public class OrderMockData
    {
        public static List<Order> GetOrders()
        {
            return new List<Order>()
        {
            new Order()
            {
                Name = "Order1",
                Description = "This is the first order"
            },
            new Order()
            {
                Name = "Order2",
                Description = "This is the 2nd order"
            },
            new Order()
            {
                Name = "Order3",
                Description = "This is the 3rd order"
            }
        };
        }

        public static List<Order> EmptyOrders() 
        {  
            return new List<Order>();
        }

        public static Order GetOrder()
        {
            return new Order() { 
            Name = "Test",
            Description = "Test"
            };
        }

        public static List<Order> GetPastOrders()
        {
            return new List<Order>()
        {
            new Order()
            {
                Name = "Order1",
                Description = "This is the first order"
            },
            new Order()
            {
                Name = "Order2",
                Description = "This is the 2nd order",
                EntryDate = DateTime.Now.AddDays(-7)
            },
            new Order()
            {
                Name = "Order3",
                Description = "This is the 3rd order",
                EntryDate = DateTime.Now.AddDays(-8)
            }
        };
        }
    }
}
