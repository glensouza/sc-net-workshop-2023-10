﻿namespace Employee.Function.Common;

public class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; }

    public int Cost { get; set; }

    public override bool Equals(object obj)
    {
        if (obj is Product)
        {
            Product that = obj as Product;
            return this.ProductId == that.ProductId && this.Name == that.Name && this.Cost == that.Cost;
        }
        return false;
    }
}

public class ProductWithOptionalId
{
    public int? ProductId { get; set; }

    public string Name { get; set; }

    public int Cost { get; set; }
}

public class ProductName
{
    public string Name { get; set; }

    public override bool Equals(object obj)
    {
        if (obj is Product)
        {
            Product that = obj as Product;
            return this.Name == that.Name;
        }
        return false;
    }
}

public class ProductWithDefaultPK
{
    public string Name { get; set; }

    public int Cost { get; set; }
}

public class ProductWithoutId
{
    public string Name { get; set; }

    public int Cost { get; set; }
}

public class MultiplePrimaryKeyProductWithoutId
{
    public int ExternalId { get; set; }

    public string Name { get; set; }

    public int Cost { get; set; }
}