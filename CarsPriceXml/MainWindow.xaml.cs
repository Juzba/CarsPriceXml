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
    int _count = 0;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void MainProg()
    {
        List<CarPrice> carPricesList = new();

        if (_isFileOpen)
        {

            _count++;
            text.Text = _count.ToString();

            foreach (var car in _data.Cars)
            {

                bool isCarInList = false;

                foreach (var item in carPricesList)
                {
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
                }
                if (isCarInList) continue;
                else
                {
                    if (Functions.SumCondition(this, car))
                        carPricesList.Add(new CarPrice(car.Name, car.PriceD, Functions.DPHCalc(car.PriceD, car.DPH)));
                    else
                        carPricesList.Add(new CarPrice(car.Name, 0, 0));
                }
            }

            DataGridInput.ItemsSource = _data.Cars;
            DataGridResult.ItemsSource = carPricesList;
        }

    }












    private void ButtonOpenXmlFile_Click(object sender, RoutedEventArgs e)
    {
        textBlockError.Text = "";
        textBlockError.Visibility = Visibility.Hidden;


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
                }

                catch (Exception)
                {
                    textBlockError.Visibility = Visibility.Visible;
                    textBlockError.Text = $"Chyba čtení XML.";
                    //throw;
                }
            }

            _isFileOpen = true;
            MainProg();
        }
    }


    private void ComboBoxClosed(object sender, EventArgs e)
    {
        MainProg();
    }
}
