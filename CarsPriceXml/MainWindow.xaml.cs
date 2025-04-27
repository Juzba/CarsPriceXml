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

    public MainWindow()
    {
        InitializeComponent();
    }

    private void WindowMain()
    {
        List<CarPrice> carPricesList = new();

        textBlockError.Text = "";
        textBlockError.Visibility = Visibility.Hidden;


        OpenFileDialog openFD = new();
        openFD.InitialDirectory = Directory.GetCurrentDirectory();
        openFD.Filter = "XML Files(*.xml)|*.xml";

        if (openFD.ShowDialog() == true)
        {
            textBlockPath.Text = openFD.FileName;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Data));

                using (StreamReader reader = new(openFD.FileName))
                {

                    _data = (Data)serializer.Deserialize(reader);

                    if (_data?.Cars != null)
                    {

                        foreach (var car in _data.Cars)
                        {

                            bool isCarInList = false;

                            foreach (var item in carPricesList)
                            {
                                if (item.Name.Contains(car.Name)) { isCarInList = true; break; }
                            }
                            if (isCarInList) continue;
                            else { carPricesList.Add(new CarPrice(car.Name, car.PriceD, 0)); }
                        }

                        DataGridInput.ItemsSource = _data.Cars;
                        DataGridResult.ItemsSource = carPricesList;
                    }

                    else
                    {
                        textBlockError.Visibility = Visibility.Visible;
                        textBlockError.Text = "Chyba: soubor otevřen, data nenalezeny.";
                    }
                }


            }
            catch (Exception)
            {
                textBlockError.Visibility = Visibility.Visible;
                textBlockError.Text = "Chyba při načítání dat Z XML nebo v průběhu.";
                throw;
            }






        }








    }


    private void ButtonOpen_Click(object sender, RoutedEventArgs e)
    {
        WindowMain();

    }
}
