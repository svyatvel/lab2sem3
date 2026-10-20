using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("Лабораторна робота 3.2. Дослідження колекцій та узагальнених колекцій C#");
        Console.WriteLine(new string('=', 90));

        Console.WriteLine("\n=== 1. Створення списку List<T> ===");
        List<Product> products = new()
        {
            new Product(101, "Laptop", "Dell", 22000m, 5, "A-10"),
            new Product(104, "Mouse", "Logitech", 1200m, 20, "M-5"),
            new Product(103, "Monitor", "Samsung", 8500m, 8, "P-21"),
            new Product(102, "Keyboard", "Acer", 3000m, 12, "K-7")
        };

        Console.WriteLine("Початковий список:");
        PrintCollection(products);

        Console.WriteLine("\nДодавання нового товару:");
        products.Add(new Product(105, "Printer", "HP", 7600m, 6, "PR-2"));
        PrintCollection(products);

        Console.WriteLine("\nОновлення ціни товару з кодом 102:");
        Product? toUpdate = products.FirstOrDefault(p => p.Code == 102);
        if (toUpdate is not null)
        {
            toUpdate.IncreasePrice(10m);
        }
        PrintCollection(products);

        Console.WriteLine("\nВидалення товару з кодом 104:");
        products.RemoveAll(p => p.Code == 104);
        PrintCollection(products);

        Console.WriteLine("\nПошук товару з кодом 103:");
        Product? found = products.FirstOrDefault(p => p.Code == 103);
        Console.WriteLine(found is null ? "Товар не знайдено." : found.ToString());

        Console.WriteLine("\nПеребір списку за допомогою foreach:");
        foreach (Product product in products)
        {
            product.PrintInfo();
        }

        Console.WriteLine("\n=== 2. Масив Product[] ===");
        Product[] productArray = products.ToArray();
        Console.WriteLine($"Початкова довжина масиву: {productArray.Length}");

        Array.Resize(ref productArray, productArray.Length + 1);
        productArray[^1] = new Product(110, "Tablet", "Lenovo", 18500m, 4, "T-12");
        Console.WriteLine("Після додавання нового елемента:");
        foreach (Product item in productArray)
        {
            Console.WriteLine(item);
        }

        int index = Array.FindIndex(productArray, p => p.Code == 110);
        if (index >= 0)
        {
            productArray[index].IncreasePrice(5m);
        }

        Console.WriteLine("\nПісля зміни ціни елемента у масиві:");
        foreach (Product item in productArray)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("\nПорівняння List<T> і масиву:");
        Console.WriteLine("- List<T> має динамічний розмір, зручний для додавання/видалення елементів.");
        Console.WriteLine("- Масив має фіксований розмір, тому для вставки/видалення потрібно використовувати копію або Array.Resize.");
        Console.WriteLine("- У масиві доступ до елемента виконується за індексом, а в List<T> - через індекс або пошук по умові.");
        Console.WriteLine("- Обидва типи підтримують перебір даних, але List<T> зручніший для динамічної колекції.");

        Console.WriteLine("\n=== 3. Бінарне дерево для об'єктів Product ===");
        BinaryTree<Product> tree = new(new ProductComparer());
        foreach (Product product in products)
        {
            tree.Add(product);
        }

        Console.WriteLine("Вміст дерева в порядку InOrder:");
        tree.InOrder(item => Console.WriteLine(item));

        Console.WriteLine("\nПошук у дереві: товар з кодом 103");
        Console.WriteLine(tree.Contains(products.First(p => p.Code == 103)) ? "Знайдено" : "Не знайдено");

        Console.WriteLine("\nВидалення товару з кодом 101 з дерева:");
        bool removed = tree.Remove(products.First(p => p.Code == 101));
        Console.WriteLine(removed ? "Товар видалено" : "Товар не було видалено");
        tree.InOrder(item => Console.WriteLine(item));

        Console.WriteLine("\n=== 4. Сортування через IComparer<T> ===");
        List<Product> sortedProducts = products
            .OrderBy(p => p, new ProductComparer())
            .ToList();

        Console.WriteLine("Відсортований список товарів (за кодом):");
        foreach (Product product in sortedProducts)
        {
            Console.WriteLine(product);
        }
    }

    private static void PrintCollection(IEnumerable<Product> source)
    {
        foreach (Product item in source)
        {
            Console.WriteLine(item);
        }
    }
}
