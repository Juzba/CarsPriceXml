namespace CarsPriceXml.Models
{
    public class CarPrice
    {

        public string Name { get; }
        public double Price { get; set; }
        public double PriceWithDPH { get; set; }


        public CarPrice(string name, double price, double priceWithDph)
        {
            Name = name;
            Price = price;
            PriceWithDPH = priceWithDph;
        }
    }
}
