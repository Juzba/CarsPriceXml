using CarsPriceXml.Components;
using CarsPriceXml.Models;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
using XmlCarsPriceApp.Models;

namespace CarsPriceXml;

public partial class MainWindow : Window
{
    Data _data = new();
    bool _isFileOpen = false;
    List<CarPrice> _carPricesList = new();


    public MainWindow()
    {
        InitializeComponent();
    }


    private void MainProg()
    {
        _carPricesList = new();

        if (_isFileOpen && _data.Cars.Count > 0)
        {

            foreach (var car in _data.Cars)
            {
                bool isCarInList = false;


                foreach (var item in _carPricesList)
                    if (item.Name.Contains(car.Name))
                    {
                        isCarInList = true;

                        // sell on weekend, week or evrything
                        if (Functions.SumCondition(this, car))
                        {
                            item.Price += car.PriceD;
                            item.PriceWithDPH += Functions.DPHCalc(car.PriceD, car.DPH);
                        }
                        break;
                    }


                if (isCarInList) continue;
                else
                {
                    // car not exist in carPricesList -> Creating new car
                    if (Functions.SumCondition(this, car))
                        _carPricesList.Add(new CarPrice(car.Name, car.PriceD, Functions.DPHCalc(car.PriceD, car.DPH)));
                    else
                        // car is created with zero price.
                        _carPricesList.Add(new CarPrice(car.Name, 0, 0));
                }
            }


            DataGridInput.ItemsSource = _data.Cars;
            DataGridResult.ItemsSource = _carPricesList;
            Functions.AddMessage(this, "Výpočet hotov.");
        }
        else if (_data.Cars.Count < 1 && _isFileOpen)
        {
            Functions.AddMessage(this, "Žádná položka.");
        }

    }

    private void ButtonOpenXmlFile_Click(object sender, RoutedEventArgs e)
    {
        _data = new();
        DataGridInput.ItemsSource = null;
        DataGridResult.ItemsSource = null;

        OpenFileDialog openFD = new();
        openFD.InitialDirectory = Directory.GetCurrentDirectory();
        openFD.Filter = "XML Files(*.xml)|*.xml";

        if (openFD.ShowDialog() == true)
        {
            textBlockPath.Text = openFD.FileName;

            XmlSerializer serializer = new XmlSerializer(typeof(Data));

            using (StreamReader reader = new(openFD.FileName))
            {
                try
                {
                    _data = (Data)serializer.Deserialize(reader);
                    _isFileOpen = true;
                    Functions.AddMessage(this, "Soubor XML spuštěn.", false);
                }
                catch (Exception)
                {
                    Functions.AddMessage(this, "Chyba čtení XML.");
                    _isFileOpen = false;
                    //throw;
                }
            }

            if (_isFileOpen) MainProg();
        }
    }


    // Combo box change, start sum
    private void ComboBoxClosed(object sender, EventArgs e)
    {
        MainProg();
    }
}
