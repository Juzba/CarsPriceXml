using System.Xml.Serialization;



namespace XmlCarsPriceApp.Models
{
    [XmlRoot("Data")]
    public class Data
    {
        [XmlElement("Car")]
        public List<Car> Cars { get; set; }
    }
}
