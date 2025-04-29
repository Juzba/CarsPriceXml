using XmlCarsPriceApp.Models;

namespace CarsPriceXml.Components
{
    public static class Functions
    {

        public static double DPHCalc(double price, double dph) => price + (price / 100 * dph);


        public static bool SumCondition(MainWindow mainWin, Car car)
        {
            // 0 - isSellOnWeekend?, 1 - isSellOnWeek?, 2 - isSellSumAll?
            return (mainWin.comboBox.SelectedIndex == 0 && IsWeekend(car.Date))
            || (mainWin.comboBox.SelectedIndex == 1 && !IsWeekend(car.Date))
            || mainWin.comboBox.SelectedIndex == 2;
        }


        static bool IsWeekend(DateTime dateTime)
        {
            return dateTime.DayOfWeek == DayOfWeek.Sunday || dateTime.DayOfWeek == DayOfWeek.Saturday;
        }


        public static void AddMessage(MainWindow mainWindow, string text, bool withTime = true)
        {
            if (withTime)
            {
                var time = DateTime.Now.ToLongTimeString();
                mainWindow.textBox.AppendText($"\n{text} - {time}");
            }
            else
            {
                mainWindow.textBox.AppendText($"\n{text}");
            }

            mainWindow.textBox.ScrollToEnd();
        }
    }
}
