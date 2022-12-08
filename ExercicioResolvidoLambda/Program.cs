using ExercicioResolvidoLambda.Entities;
using System.Globalization;

Console.Write("Enter full file path: ");

string path = @Console.ReadLine()!;

List<Product> list = new List<Product>();

using (StreamReader sr = File.OpenText(path))
{
    try
    {
        while (!sr.EndOfStream)
        {
            string[] fields = sr.ReadLine()!.Split(',');
            string name = fields[0];
            double price = double.Parse(fields[1], CultureInfo.InvariantCulture );
            list.Add(new Product(name, price));
        }
        double average = list.Select(p => p.Price).DefaultIfEmpty(0.0).Average();
        Console.WriteLine("Average price: " + average.ToString("F2", CultureInfo.InvariantCulture));

        var names = list.Where(p => p.Price < average).OrderByDescending(p => p.Name).Select(p => p.Name);
        
        foreach (var name in names)
        {
            Console.WriteLine(name);
        }
            
    }catch(IOException e)
    {
        Console.WriteLine(e.Message);
    }
}
