namespace XmlCarsPriceApp.Models;

public class Car
{
    public required string Name { get; set; }
    public required string Date { get; set; }
    public required string Price { get; set; }
    public double DPH { get; set; }



    public DateTime? DateDT
    {
        get { return DateTime.TryParse(Date, out DateTime result) ? result : null; }
    }


    public double PriceD
    {
        get
        {
            if (Price?.Length > 3)
            {
                string numString = Price.Remove(Price.Length - 2).Replace(".", "");
                if (double.TryParse(numString, out double result)) return result;
            }
            return 0;
        }
    }




}




