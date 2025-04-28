namespace XmlCarsPriceApp.Models;

public class Car
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public double Price { get; set; }
    public double DPH { get; set; }

    public Car(string name, DateTime date, double price, double dPH)
    {
        Name = name;
        Date = date;
        Price = price;
        DPH = dPH;
    }
}




