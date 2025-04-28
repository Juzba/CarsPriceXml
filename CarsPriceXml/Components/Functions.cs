using XmlCarsPriceApp.Models;

namespace CarsPriceXml.Components
{
    public static class Functions
    {
        public static double DPHCalc(double price, double dph)
        {
            return price + (price / 100) * dph;
        }

        public static bool IsWeekend(DateTime? dateTime)
        {
            return dateTime?.DayOfWeek == DayOfWeek.Sunday || dateTime?.DayOfWeek == DayOfWeek.Saturday;
        }

        public static bool IsNotWeekend(DateTime? dateTime)
        {
            return dateTime != null && dateTime?.DayOfWeek != DayOfWeek.Sunday && dateTime?.DayOfWeek != DayOfWeek.Saturday;
        }
        public static bool SumCondition(MainWindow mainWin, Car car)
        {
            return (mainWin.comboBox.SelectedIndex == 0 && IsWeekend(car.DateDT))
            || (mainWin.comboBox.SelectedIndex == 1 && IsNotWeekend(car.DateDT))
            || mainWin.comboBox.SelectedIndex == 2;

        }

    }
}
