namespace XmlCarsPriceApp.Models;

public class Car
{
    public required string Name { get; set; }
    public required string Date { get; set; }

    public DateTime? DateDT
    {
        get
        {
            return DateTime.TryParse(Date, out DateTime result) ? result : null;
        }
    }
    public required string Price { get; set; }
    public double PriceD
    {
        get
        {
            string numString = Price.Remove(Price.Length - 2).Replace(".", "");
            return double.TryParse(numString, out double result) ? result : 0;
        }
    }
    public double DPH { get; set; }


}




