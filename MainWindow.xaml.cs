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
        }

        public SensorDataSet CreateSampleDataSet()
        {
            SensorDataSet dataSet = new SensorDataSet(2, 3);
            dataSet.Name = "Test DataSet";

            DateTime timeOne = DateTime.Now;
            DateTime timeTwo = DateTime.Now.AddMinutes(5);

            dataSet.Data[0, 0] = new SensorData { Label = "Temperature", Value = 22.5, Timestamp = timeOne };
            dataSet.Data[0, 1] = new SensorData { Label = "Humidity", Value = 45.0, Timestamp = timeOne };
            dataSet.Data[0, 2] = new SensorData { Label = "Pressure", Value = 101.2, Timestamp = timeOne };

            dataSet.Data[1, 0] = new SensorData { Label = "Temperature", Value = 23.5, Timestamp = timeTwo };
            dataSet.Data[1, 1] = new SensorData { Label = "Humidity", Value = 47.0, Timestamp = timeTwo };
            dataSet.Data[1, 2] = new SensorData { Label = "Pressure", Value = 100.8, Timestamp = timeTwo };

            return dataSet;
        }
    }
}