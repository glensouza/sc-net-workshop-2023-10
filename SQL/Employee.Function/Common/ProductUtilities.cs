﻿using System;
using System.Collections.Generic;

namespace Employee.Function.Common;

public class ProductUtilities
{
    /// <summary>
    /// Returns a list of <paramref name="num"/> Products with sequential IDs, a cost of 100, and "test" as name.
    /// </summary>
    public static List<Product> GetNewProducts(int num)
    {
        List<Product> products = new List<Product>();
        for (int i = 0; i < num; i++)
        {
            Product product = new Product
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
        Random r = new Random();

        List<Product> products = new List<Product>(num);
        for (int i = 0; i < num; i++)
        {
            Product product = new Product
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
