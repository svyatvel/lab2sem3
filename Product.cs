using System;

namespace Lab2;

public class Product : IComparable<Product>
{
    public int Code { get; set; }
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Batch { get; set; }

    public Product(int code, string name, string manufacturer, decimal price, int quantity, string batch)
    {
        Code = code;
        Name = name;
        Manufacturer = manufacturer;
        Price = price;
        Quantity = quantity;
        Batch = batch;
    }

    public decimal TotalCost() => Price * Quantity;

    public void IncreasePrice(decimal percent)
    {
        Price += Price * (percent / 100m);
    }

    public void PrintInfo()
    {
        Console.WriteLine($"Код: {Code}, Товар: {Name}, Виробник: {Manufacturer}, Ціна: {Price:C}, Кількість: {Quantity}, Партія: {Batch}");
    }

    public int CompareTo(Product? other)
    {
        if (other is null)
        {
            return 1;
        }

        return Code.CompareTo(other.Code);
    }

    public override string ToString() => $"{Code} | {Name} | {Manufacturer} | {Price:C} | {Quantity} | {Batch}";
}
