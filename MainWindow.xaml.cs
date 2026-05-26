using Sensing4UDashboard.Models;
using Sensing4UDashboard.Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sensing4UDashboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SensorDataSet dataSet = new SensorDataSet(2, 3);
            dataSet.Name = "Sample Sensor Data";

            dataSet.Data[0, 0] = new SensorData
            {
                Label = "Temperature",
                Value = 22.5,
                Timestamp = DateTime.Now
            };

            dataSet.Data[0, 1] = new SensorData
            {
                Label = "Humidity",
                Value = 45.0,
                Timestamp = DateTime.Now
            };

            dataSet.Data[0, 2] = new SensorData
            {
                Label = "Pressure",
                Value = 101.2,
                Timestamp = DateTime.Now
            };

            dataSet.Data[1, 0] = new SensorData
            {
                Label = "Temperature",
                Value = 23.5,
                Timestamp = DateTime.Now.AddMinutes(5)
            };

            dataSet.Data[1, 1] = new SensorData
            {
                Label = "Humidity",
                Value = 47.0,
                Timestamp = DateTime.Now.AddMinutes(5)
            };

            dataSet.Data[1, 2] = new SensorData
            {
                Label = "Pressure",
                Value = 100.8,
                Timestamp = DateTime.Now.AddMinutes(5)
            };

            double average = DataProcessor.Instance.CalculateAverage(dataSet);

            string status = DataProcessor.Instance.GetValueStatus(average, 20.0, 25.0);
            MessageBox.Show($"Average Value: {average}\nStatus: {status}", "Sensor Data Analysis", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}