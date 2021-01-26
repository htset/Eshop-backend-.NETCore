﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop_API.Models
{
    public static class DbInitializer
    {
        public static void Initialize(ItemContext context)
        {
            //context.Database.EnsureCreated();

            // Look for any students.
            if (context.Items.Any())
            {
                return;   // DB has been seeded
            }

            var items= new Item[]
            {
            new Item{Name="aa", Price=2.2, Description="3434", Category="Clothes"},
            new Item{Name="bb", Price=12.2, Description="ff", Category="Clothes"},
            new Item{Name="cc", Price=4.6, Description="fdfd", Category="Shoes"},
            new Item{Name="dd", Price=5.1, Description="xz", Category="Shoes"},
            };
            foreach (Item i in items)
            {
                context.Items.Add(i);
            }
            context.SaveChanges();

        }
    }
}