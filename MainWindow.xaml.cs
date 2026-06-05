using Microsoft.Win32;
using Sensing4UDashboard.Models;
using Sensing4UDashboard.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        private List<SensorDataSet> _dataSets = new List<SensorDataSet>();
        private int _currentDataSetIndex = 0;

        private double _lowerBound = 0;
        private double _upperBound = 100;

        public MainWindow()
        {
            InitializeComponent();

            _dataSets.Add(CreateSampleDataSet());
            _dataSets.Add(CreateSampleDataSet2());
            _dataSets.Add(CreateGreenhouseTemperatureDataSet());
            _dataSets.Add(CreateColdStorageHumidityDataSet());
            _dataSets.Add(CreateWaterTankPressureDataSet());

            LoadCurrentDataSet();

        }

        public SensorDataSet CreateSampleDataSet()
        {
            SensorDataSet dataSet = new SensorDataSet(2, 3);
            dataSet.Name = "Initial Data";

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
        public SensorDataSet CreateSampleDataSet2()
        {
            SensorDataSet dataSet = new SensorDataSet(2, 3);
            dataSet.Name = "Storage Room Sensors";

            DateTime timeOne = DateTime.Now;
            DateTime timeTwo = DateTime.Now.AddMinutes(5);

            dataSet.Data[0, 0] = new SensorData { Label = "Temperature", Value = 18.2, Timestamp = timeOne };
            dataSet.Data[0, 1] = new SensorData { Label = "Humidity", Value = 52.0, Timestamp = timeOne };
            dataSet.Data[0, 2] = new SensorData { Label = "Pressure", Value = 99.5, Timestamp = timeOne };

            dataSet.Data[1, 0] = new SensorData { Label = "Temperature", Value = 19.1, Timestamp = timeTwo };
            dataSet.Data[1, 1] = new SensorData { Label = "Humidity", Value = 53.5, Timestamp = timeTwo };
            dataSet.Data[1, 2] = new SensorData { Label = "Pressure", Value = 100.1, Timestamp = timeTwo };

            return dataSet;
        }

        private SensorDataSet CreateGreenhouseTemperatureDataSet()
        {
            SensorDataSet dataSet = new SensorDataSet(10, 3);
            dataSet.Name = "Greenhouse Temperature Readings";

            DateTime baseDate = new DateTime(2026, 6, 4);

            dataSet.Data[0, 0] = new SensorData { Label = "Temp Sensor A", Value = 21.4, Timestamp = baseDate.AddHours(8) };
            dataSet.Data[0, 1] = new SensorData { Label = "Temp Sensor B", Value = 21.7, Timestamp = baseDate.AddHours(8) };
            dataSet.Data[0, 2] = new SensorData { Label = "Temp Sensor C", Value = 22.1, Timestamp = baseDate.AddHours(8) };

            dataSet.Data[1, 0] = new SensorData { Label = "Temp Sensor A", Value = 22.0, Timestamp = baseDate.AddHours(9) };
            dataSet.Data[1, 1] = new SensorData { Label = "Temp Sensor B", Value = 22.3, Timestamp = baseDate.AddHours(9) };
            dataSet.Data[1, 2] = new SensorData { Label = "Temp Sensor C", Value = 22.8, Timestamp = baseDate.AddHours(9) };

            dataSet.Data[2, 0] = new SensorData { Label = "Temp Sensor A", Value = 23.1, Timestamp = baseDate.AddHours(10) };
            dataSet.Data[2, 1] = new SensorData { Label = "Temp Sensor B", Value = 23.5, Timestamp = baseDate.AddHours(10) };
            dataSet.Data[2, 2] = new SensorData { Label = "Temp Sensor C", Value = 24.0, Timestamp = baseDate.AddHours(10) };

            dataSet.Data[3, 0] = new SensorData { Label = "Temp Sensor A", Value = 24.2, Timestamp = baseDate.AddHours(11) };
            dataSet.Data[3, 1] = new SensorData { Label = "Temp Sensor B", Value = 24.6, Timestamp = baseDate.AddHours(11) };
            dataSet.Data[3, 2] = new SensorData { Label = "Temp Sensor C", Value = 25.1, Timestamp = baseDate.AddHours(11) };

            dataSet.Data[4, 0] = new SensorData { Label = "Temp Sensor A", Value = 25.3, Timestamp = baseDate.AddHours(12) };
            dataSet.Data[4, 1] = new SensorData { Label = "Temp Sensor B", Value = 25.9, Timestamp = baseDate.AddHours(12) };
            dataSet.Data[4, 2] = new SensorData { Label = "Temp Sensor C", Value = 26.4, Timestamp = baseDate.AddHours(12) };

            dataSet.Data[5, 0] = new SensorData { Label = "Temp Sensor A", Value = 26.2, Timestamp = baseDate.AddHours(13) };
            dataSet.Data[5, 1] = new SensorData { Label = "Temp Sensor B", Value = 26.7, Timestamp = baseDate.AddHours(13) };
            dataSet.Data[5, 2] = new SensorData { Label = "Temp Sensor C", Value = 27.1, Timestamp = baseDate.AddHours(13) };

            dataSet.Data[6, 0] = new SensorData { Label = "Temp Sensor A", Value = 26.8, Timestamp = baseDate.AddHours(14) };
            dataSet.Data[6, 1] = new SensorData { Label = "Temp Sensor B", Value = 27.4, Timestamp = baseDate.AddHours(14) };
            dataSet.Data[6, 2] = new SensorData { Label = "Temp Sensor C", Value = 28.0, Timestamp = baseDate.AddHours(14) };

            dataSet.Data[7, 0] = new SensorData { Label = "Temp Sensor A", Value = 26.1, Timestamp = baseDate.AddHours(15) };
            dataSet.Data[7, 1] = new SensorData { Label = "Temp Sensor B", Value = 26.5, Timestamp = baseDate.AddHours(15) };
            dataSet.Data[7, 2] = new SensorData { Label = "Temp Sensor C", Value = 27.0, Timestamp = baseDate.AddHours(15) };

            dataSet.Data[8, 0] = new SensorData { Label = "Temp Sensor A", Value = 24.9, Timestamp = baseDate.AddHours(16) };
            dataSet.Data[8, 1] = new SensorData { Label = "Temp Sensor B", Value = 25.2, Timestamp = baseDate.AddHours(16) };
            dataSet.Data[8, 2] = new SensorData { Label = "Temp Sensor C", Value = 25.7, Timestamp = baseDate.AddHours(16) };

            dataSet.Data[9, 0] = new SensorData { Label = "Temp Sensor A", Value = 23.6, Timestamp = baseDate.AddHours(17) };
            dataSet.Data[9, 1] = new SensorData { Label = "Temp Sensor B", Value = 24.0, Timestamp = baseDate.AddHours(17) };
            dataSet.Data[9, 2] = new SensorData { Label = "Temp Sensor C", Value = 24.3, Timestamp = baseDate.AddHours(17) };

            return dataSet;
        }

        private SensorDataSet CreateColdStorageHumidityDataSet()
        {
            SensorDataSet dataSet = new SensorDataSet(10, 3);
            dataSet.Name = "Cold Storage Humidity Readings";

            DateTime baseDate = new DateTime(2026, 6, 4);

            dataSet.Data[0, 0] = new SensorData { Label = "Humidity Sensor A", Value = 63.2, Timestamp = baseDate.AddHours(6) };
            dataSet.Data[0, 1] = new SensorData { Label = "Humidity Sensor B", Value = 64.1, Timestamp = baseDate.AddHours(6) };
            dataSet.Data[0, 2] = new SensorData { Label = "Humidity Sensor C", Value = 62.8, Timestamp = baseDate.AddHours(6) };

            dataSet.Data[1, 0] = new SensorData { Label = "Humidity Sensor A", Value = 63.8, Timestamp = baseDate.AddHours(7) };
            dataSet.Data[1, 1] = new SensorData { Label = "Humidity Sensor B", Value = 64.6, Timestamp = baseDate.AddHours(7) };
            dataSet.Data[1, 2] = new SensorData { Label = "Humidity Sensor C", Value = 63.3, Timestamp = baseDate.AddHours(7) };

            dataSet.Data[2, 0] = new SensorData { Label = "Humidity Sensor A", Value = 65.0, Timestamp = baseDate.AddHours(8) };
            dataSet.Data[2, 1] = new SensorData { Label = "Humidity Sensor B", Value = 65.7, Timestamp = baseDate.AddHours(8) };
            dataSet.Data[2, 2] = new SensorData { Label = "Humidity Sensor C", Value = 64.4, Timestamp = baseDate.AddHours(8) };

            dataSet.Data[3, 0] = new SensorData { Label = "Humidity Sensor A", Value = 66.2, Timestamp = baseDate.AddHours(9) };
            dataSet.Data[3, 1] = new SensorData { Label = "Humidity Sensor B", Value = 67.1, Timestamp = baseDate.AddHours(9) };
            dataSet.Data[3, 2] = new SensorData { Label = "Humidity Sensor C", Value = 65.9, Timestamp = baseDate.AddHours(9) };

            dataSet.Data[4, 0] = new SensorData { Label = "Humidity Sensor A", Value = 67.5, Timestamp = baseDate.AddHours(10) };
            dataSet.Data[4, 1] = new SensorData { Label = "Humidity Sensor B", Value = 68.0, Timestamp = baseDate.AddHours(10) };
            dataSet.Data[4, 2] = new SensorData { Label = "Humidity Sensor C", Value = 66.8, Timestamp = baseDate.AddHours(10) };

            dataSet.Data[5, 0] = new SensorData { Label = "Humidity Sensor A", Value = 68.1, Timestamp = baseDate.AddHours(11) };
            dataSet.Data[5, 1] = new SensorData { Label = "Humidity Sensor B", Value = 69.2, Timestamp = baseDate.AddHours(11) };
            dataSet.Data[5, 2] = new SensorData { Label = "Humidity Sensor C", Value = 67.9, Timestamp = baseDate.AddHours(11) };

            dataSet.Data[6, 0] = new SensorData { Label = "Humidity Sensor A", Value = 68.7, Timestamp = baseDate.AddHours(12) };
            dataSet.Data[6, 1] = new SensorData { Label = "Humidity Sensor B", Value = 69.5, Timestamp = baseDate.AddHours(12) };
            dataSet.Data[6, 2] = new SensorData { Label = "Humidity Sensor C", Value = 68.3, Timestamp = baseDate.AddHours(12) };

            dataSet.Data[7, 0] = new SensorData { Label = "Humidity Sensor A", Value = 67.9, Timestamp = baseDate.AddHours(13) };
            dataSet.Data[7, 1] = new SensorData { Label = "Humidity Sensor B", Value = 68.8, Timestamp = baseDate.AddHours(13) };
            dataSet.Data[7, 2] = new SensorData { Label = "Humidity Sensor C", Value = 67.2, Timestamp = baseDate.AddHours(13) };

            dataSet.Data[8, 0] = new SensorData { Label = "Humidity Sensor A", Value = 66.4, Timestamp = baseDate.AddHours(14) };
            dataSet.Data[8, 1] = new SensorData { Label = "Humidity Sensor B", Value = 67.0, Timestamp = baseDate.AddHours(14) };
            dataSet.Data[8, 2] = new SensorData { Label = "Humidity Sensor C", Value = 65.8, Timestamp = baseDate.AddHours(14) };

            dataSet.Data[9, 0] = new SensorData { Label = "Humidity Sensor A", Value = 65.5, Timestamp = baseDate.AddHours(15) };
            dataSet.Data[9, 1] = new SensorData { Label = "Humidity Sensor B", Value = 66.1, Timestamp = baseDate.AddHours(15) };
            dataSet.Data[9, 2] = new SensorData { Label = "Humidity Sensor C", Value = 64.9, Timestamp = baseDate.AddHours(15) };

            return dataSet;
        }

        private SensorDataSet CreateWaterTankPressureDataSet()
        {
            SensorDataSet dataSet = new SensorDataSet(10, 3);
            dataSet.Name = "Water Tank Pressure Readings";

            DateTime baseDate = new DateTime(2026, 6, 4);

            dataSet.Data[0, 0] = new SensorData { Label = "Pressure Sensor A", Value = 118.4, Timestamp = baseDate.AddHours(5) };
            dataSet.Data[0, 1] = new SensorData { Label = "Pressure Sensor B", Value = 119.1, Timestamp = baseDate.AddHours(5) };
            dataSet.Data[0, 2] = new SensorData { Label = "Pressure Sensor C", Value = 117.8, Timestamp = baseDate.AddHours(5) };

            dataSet.Data[1, 0] = new SensorData { Label = "Pressure Sensor A", Value = 120.2, Timestamp = baseDate.AddHours(6) };
            dataSet.Data[1, 1] = new SensorData { Label = "Pressure Sensor B", Value = 121.0, Timestamp = baseDate.AddHours(6) };
            dataSet.Data[1, 2] = new SensorData { Label = "Pressure Sensor C", Value = 119.4, Timestamp = baseDate.AddHours(6) };

            dataSet.Data[2, 0] = new SensorData { Label = "Pressure Sensor A", Value = 124.5, Timestamp = baseDate.AddHours(7) };
            dataSet.Data[2, 1] = new SensorData { Label = "Pressure Sensor B", Value = 125.2, Timestamp = baseDate.AddHours(7) };
            dataSet.Data[2, 2] = new SensorData { Label = "Pressure Sensor C", Value = 123.8, Timestamp = baseDate.AddHours(7) };

            dataSet.Data[3, 0] = new SensorData { Label = "Pressure Sensor A", Value = 128.3, Timestamp = baseDate.AddHours(8) };
            dataSet.Data[3, 1] = new SensorData { Label = "Pressure Sensor B", Value = 129.1, Timestamp = baseDate.AddHours(8) };
            dataSet.Data[3, 2] = new SensorData { Label = "Pressure Sensor C", Value = 127.6, Timestamp = baseDate.AddHours(8) };

            dataSet.Data[4, 0] = new SensorData { Label = "Pressure Sensor A", Value = 131.0, Timestamp = baseDate.AddHours(9) };
            dataSet.Data[4, 1] = new SensorData { Label = "Pressure Sensor B", Value = 132.4, Timestamp = baseDate.AddHours(9) };
            dataSet.Data[4, 2] = new SensorData { Label = "Pressure Sensor C", Value = 130.2, Timestamp = baseDate.AddHours(9) };

            dataSet.Data[5, 0] = new SensorData { Label = "Pressure Sensor A", Value = 134.7, Timestamp = baseDate.AddHours(10) };
            dataSet.Data[5, 1] = new SensorData { Label = "Pressure Sensor B", Value = 135.5, Timestamp = baseDate.AddHours(10) };
            dataSet.Data[5, 2] = new SensorData { Label = "Pressure Sensor C", Value = 133.8, Timestamp = baseDate.AddHours(10) };

            dataSet.Data[6, 0] = new SensorData { Label = "Pressure Sensor A", Value = 136.2, Timestamp = baseDate.AddHours(11) };
            dataSet.Data[6, 1] = new SensorData { Label = "Pressure Sensor B", Value = 137.0, Timestamp = baseDate.AddHours(11) };
            dataSet.Data[6, 2] = new SensorData { Label = "Pressure Sensor C", Value = 135.6, Timestamp = baseDate.AddHours(11) };

            dataSet.Data[7, 0] = new SensorData { Label = "Pressure Sensor A", Value = 133.9, Timestamp = baseDate.AddHours(12) };
            dataSet.Data[7, 1] = new SensorData { Label = "Pressure Sensor B", Value = 134.6, Timestamp = baseDate.AddHours(12) };
            dataSet.Data[7, 2] = new SensorData { Label = "Pressure Sensor C", Value = 133.0, Timestamp = baseDate.AddHours(12) };

            dataSet.Data[8, 0] = new SensorData { Label = "Pressure Sensor A", Value = 129.8, Timestamp = baseDate.AddHours(13) };
            dataSet.Data[8, 1] = new SensorData { Label = "Pressure Sensor B", Value = 130.4, Timestamp = baseDate.AddHours(13) };
            dataSet.Data[8, 2] = new SensorData { Label = "Pressure Sensor C", Value = 129.1, Timestamp = baseDate.AddHours(13) };

            dataSet.Data[9, 0] = new SensorData { Label = "Pressure Sensor A", Value = 125.6, Timestamp = baseDate.AddHours(14) };
            dataSet.Data[9, 1] = new SensorData { Label = "Pressure Sensor B", Value = 126.2, Timestamp = baseDate.AddHours(14) };
            dataSet.Data[9, 2] = new SensorData { Label = "Pressure Sensor C", Value = 124.9, Timestamp = baseDate.AddHours(14) };

            return dataSet;
        }

        private void LoadMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Binary sensor files (*.bin)|*.bin|All files (*.*)|*.*";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                try
                {
                    FileManager fileManager = new FileManager();
                    SensorDataSet loadedDataSet = fileManager.LoadBinaryFile(dialog.FileName);

                    _dataSets.Add(loadedDataSet);

                    _currentDataSetIndex = _dataSets.Count - 1;
                    LoadCurrentDataSet();
                    StatusMessageTextBlock.Text = $"Loaded {loadedDataSet.Name}.";
                   
                }
                catch (Exception ex)
                {
                    StatusMessageTextBlock.Text = $"Error loading file: {ex.Message}";
                }

            }
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (_dataSets.Count == 0)
            {
                StatusMessageTextBlock.Text = "No datasets to save.";
                return;
            }

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Binary sensor files (*.bin)|*.bin|All files (*.*)|*.*";
            dialog.FileName = _dataSets[_currentDataSetIndex].Name + ".bin";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                try
                {
                    FileManager fileManager = new FileManager();
                    SensorDataSet currentDataSet = _dataSets[_currentDataSetIndex];

                    
                    fileManager.SaveBinaryFile(dialog.FileName, currentDataSet);

                    StatusMessageTextBlock.Text = $"Saved {currentDataSet.Name}.";
                }
                catch (Exception ex)
                {
                    StatusMessageTextBlock.Text = $"Error saving file: {ex.Message}";
                }
            }
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PreviousDataSetButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentDataSetIndex > 0)
            {
                _currentDataSetIndex--;
                LoadCurrentDataSet();
            }
        }

        private void NextDataSetButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentDataSetIndex < _dataSets.Count - 1)
            {
                _currentDataSetIndex++;
                LoadCurrentDataSet();
            }
        }

        private void ApplyBoundsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(LowerBoundTextBox.Text, out var lowerBound))
            {
                StatusMessageTextBlock.Text = "Please enter a valid numerical lower bound.";
                return;
            }

            if (!double.TryParse(UpperBoundTextBox.Text, out var upperBound))
            {
                StatusMessageTextBlock.Text = "Please enter a valid numerical upper bound.";
                return;
            }

            if (lowerBound >= upperBound)
            {
                StatusMessageTextBlock.Text = "Lower bound must be less than upper bound.";
                return;
            }
            _lowerBound = lowerBound;
            _upperBound = upperBound;
            LoadCurrentDataSet();
            StatusMessageTextBlock.Text = $"Bounds applied: Lower = {_lowerBound}, Upper = {_upperBound}";
        }

        private void SensorDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (e.Row.Item is not DataRowView rowView)
                return;

            e.Row.Loaded += (s, args) =>
            {
                for (int columnIndex = 1; columnIndex < SensorDataGrid.Columns.Count; columnIndex++)
                {
                    DataGridCell? cell = GetDataGridCell(e.Row, columnIndex);

                    if (cell == null) continue;

                    object? cellValue = rowView.Row[columnIndex];

                    if (double.TryParse(cellValue.ToString(), out double value)) {
                        string status = DataProcessor.Instance.GetValueStatus(value, _lowerBound, _upperBound);
                        switch (status)
                        {
                            case "Low":
                                cell.Background = Brushes.LightBlue;
                                break;
                            case "Acceptable":
                                cell.Background = Brushes.LightGreen;
                                break;
                            case "High":
                                cell.Background = Brushes.LightCoral;
                                break;
                            default:
                                cell.Background = Brushes.White;
                                break;
                        }
                    }
                }
            };
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (_dataSets.Count == 0)
            {
                StatusMessageTextBlock.Text = "No datasets loaded.";
                return;
            }

            if (!double.TryParse(SearchValueTextBox.Text, out double value)) 
            {
                StatusMessageTextBlock.Text = "Please enter a valid numerical value.";
                return;
            }

            SensorDataSet currentDataSet = _dataSets[_currentDataSetIndex];

            SensorData? foundData = DataProcessor.Instance.FindSensorValue(currentDataSet, value);

            if (foundData != null)
            {
                StatusMessageTextBlock.Text = $"Found value: {foundData.Value} at {foundData.Timestamp}";
            }
            else
            {
                StatusMessageTextBlock.Text = "Value not found in current dataset.";
            }
        }

        //below two functions are used to find the specific cell in the DataGrid to apply the background color based on the value status
        private DataGridCell? GetDataGridCell(DataGridRow row, int columnIndex)
        {
            DataGridCellsPresenter? presenter = FindVisualChild<DataGridCellsPresenter>(row);
            if (presenter == null) return null;

            DataGridCell? cell = presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex) as DataGridCell;
            return cell;
        }

        private static T? FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null) return null;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T typedChild)
                    return typedChild;
                T? result = FindVisualChild<T>(child);
                if (result != null)
                    return result;
            }
            return null;
        }

        //resuable function to allow the dataSet to be loaded and displayed depending on what user choice is
        private void LoadCurrentDataSet()
        {
            if (_dataSets.Count == 0)
            {
                StatusMessageTextBlock.Text = "No datasets loaded.";
                return;
            }
            SensorDataSet currentDataSet = _dataSets[_currentDataSetIndex];
            DataTable dataTable = DataProcessor.Instance.ConvertToDataTable(currentDataSet);
            SensorDataGrid.ItemsSource = dataTable.DefaultView;
            double average = DataProcessor.Instance.CalculateAverage(currentDataSet);
            AverageTextBlock.Text = average.ToString("F2");

            CurrentDataSetTextBlock.Text = $"Dataset: {currentDataSet.Name}";

            PreviousDataSetButton.IsEnabled = _currentDataSetIndex > 0;
            NextDataSetButton.IsEnabled = _currentDataSetIndex < _dataSets.Count - 1;

            StatusMessageTextBlock.Text = $"Loaded dataset: {_currentDataSetIndex + 1} of {_dataSets.Count}";
        }
    }
}