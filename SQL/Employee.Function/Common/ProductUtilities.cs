﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Function.Common;

public class ProductUtilities
{
    /// <summary>
    /// Returns a list of <paramref name="num"/> Products with sequential IDs, a cost of 100, and "test" as name.
    /// </summary>
    public static List<Product> GetNewProducts(int num)
    {
        var products = new List<Product>();
        for (int i = 0; i < num; i++)
        {
            var product = new Product
            {
                ProductId = i,
                Cost = 100 * i,
                Name = "test"
            };
            products.Add(product);
        }
        return products;
    }

    /// <summary>
    /// Returns a list of <paramref name="num"/> Products with a random cost between 1 and <paramref name="cost"/>.
    /// Note that ProductId is randomized too so list may not be unique.
    /// </summary>
    public static List<Product> GetNewProductsRandomized(int num, int cost)
    {
        var r = new Random();

        var products = new List<Product>(num);
        for (int i = 0; i < num; i++)
        {
            var product = new Product
            {
                ProductId = r.Next(1, num),
                Cost = (int)Math.Round(r.NextDouble() * cost),
                Name = "test"
            };
            products.Add(product);
        }
        return products;
    }
}