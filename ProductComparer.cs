using System;
using System.Collections.Generic;

namespace Lab2;

public class ProductComparer : IComparer<Product>
{
    public int Compare(Product? x, Product? y)
    {
        if (ReferenceEquals(x, y))
        {
            return 0;
        }

        if (x is null)
        {
            return -1;
        }

        if (y is null)
        {
            return 1;
        }

        int byCode = x.Code.CompareTo(y.Code);
        return byCode != 0 ? byCode : string.Compare(x.Name, y.Name, StringComparison.Ordinal);
    }
}
