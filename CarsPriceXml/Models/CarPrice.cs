namespace CarsPriceXml.Models
{
    public class CarPrice
    {

        public string Name { get; }
        public double Price { get; }
        public double PriceWithDPH { get; }


        public CarPrice(string name, double price, double priceWithDph)
        {
            Name = name;
            Price = price;
            PriceWithDPH = priceWithDph;

        }
    }
}
