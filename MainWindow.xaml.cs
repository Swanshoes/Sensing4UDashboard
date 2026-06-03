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

            LoadCurrentDataSet();

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
                    MessageBox.Show(
    $"Loaded: {loadedDataSet.Name}\n" +
    $"Rows: {loadedDataSet.RowCount}\n" +
    $"Columns: {loadedDataSet.ColumnCount}\n" +
    $"First value: {loadedDataSet.Data[0, 0]?.Value.ToString() ?? "NULL"}"
);
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

                    MessageBox.Show(
    $"About to save: {currentDataSet.Name}\n" +
    $"Rows: {currentDataSet.RowCount}\n" +
    $"Columns: {currentDataSet.ColumnCount}\n" +
    $"First value before save: {currentDataSet.Data[0, 0]?.Value.ToString() ?? "NULL"}"
);
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