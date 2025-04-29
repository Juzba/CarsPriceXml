using CarsPriceXml.Components;
using CarsPriceXml.Models;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Xml;
using XmlCarsPriceApp.Models;

namespace CarsPriceXml;

public partial class MainWindow : Window
{
    bool _isFileOpen = false;
    List<CarPrice> _carPricesList = new();
    List<Car> _carList = new();


    public MainWindow()
    {
        InitializeComponent();
    }


    private void MainProg()
    {
        _carPricesList = new();


        if (_isFileOpen && _carList.Count > 0)
        {
            foreach (var car in _carList)
            {
                bool isCarInList = false;


                foreach (var item in _carPricesList)
                    // if item is already in seznam _carPriceList
                    if (item.Name.Contains(car.Name))
                    {
                        isCarInList = true;

                        // sell on weekend, week or evrything
                        if (Functions.SumCondition(this, car))
                        {
                            item.Price += car.Price;
                            item.PriceWithDPH += Functions.DPHCalc(car.Price, car.DPH);
                        }
                        break;
                    }


                if (isCarInList) continue;
                else
                {
                    // car not exist in carPricesList -> Creating new car in list
                    if (Functions.SumCondition(this, car))
                        _carPricesList.Add(new CarPrice(car.Name, car.Price, Functions.DPHCalc(car.Price, car.DPH)));
                    else
                        // car name is created with zero price.
                        _carPricesList.Add(new CarPrice(car.Name, 0, 0));
                }
            }

            DataGridInput.ItemsSource = _carList;
            DataGridResult.ItemsSource = _carPricesList;
            Functions.AddMessage(this, "Výpočet hotov.");
        }
    }


    private void ButtonOpenXmlFile_Click(object sender, RoutedEventArgs e)
    {
        _isFileOpen = false;
        int count = 0;
        int countError = 0;
        _carList = new();
        DataGridInput.ItemsSource = null;
        DataGridResult.ItemsSource = null;

        OpenFileDialog openFD = new();
        openFD.InitialDirectory = Directory.GetCurrentDirectory();
        openFD.Filter = "XML Files(*.xml)|*.xml";

        if (openFD.ShowDialog() == true)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNodeList? carNodes = null;
            textBlockPath.Text = openFD.FileName;

            try
            {
                xmlDoc.Load(openFD.FileName);
                carNodes = xmlDoc.DocumentElement?.SelectNodes("/Data/Car");
            }
            catch (Exception)
            {
                Functions.AddMessage(this, $"XML obsahuje chyby.");
                //throw;
            }

            if (carNodes != null)
            {

                foreach (XmlNode node in carNodes)
                {
                    count++;
                    string? name = node?.SelectSingleNode("Name")?.InnerText;
                    string? date = node?.SelectSingleNode("Date")?.InnerText;
                    string? price = node?.SelectSingleNode("Price")?.InnerText;
                    string? dph = node?.SelectSingleNode("DPH")?.InnerText;

                    if (
                        name?.Length > 2
                        && DateTime.TryParse(date, out DateTime parsedDate)
                        && double.TryParse(dph, out double parsedDph)
                        && price?.Length > 3
                        && double.TryParse(price.Remove(price.Length - 2).Replace(".", ""), out double parsedPrice)
                        )
                    {
                        _carList.Add(new Car(name, parsedDate, parsedPrice, parsedDph));
                    }
                    else
                    {
                        Functions.AddMessage(this, $"Chyba zadání v Car číslo{count}!", false);
                        countError++;
                    }
                }


                if (_carList.Count - countError > 0)
                {
                    _isFileOpen = true;
                    Functions.AddMessage(this, $"Načteno {count - countError} položek z XML.", false);
                    MainProg();
                }

            }
        }
    }


    // Combo box change, start sum
    private void ComboBoxClosed(object sender, EventArgs e)
    {
        MainProg();
    }
}
