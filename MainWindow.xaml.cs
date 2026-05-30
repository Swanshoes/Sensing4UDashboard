using Sensing4UDashboard.Models;
using Sensing4UDashboard.Services;
using System;
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
        private double _lowerBound = 0;
        private double _upperBound = 100;

        public MainWindow()
        {
            InitializeComponent();

            SensorDataSet dataSet = CreateSampleDataSet();

            double average = DataProcessor.Instance.CalculateAverage(dataSet);
            AverageTextBlock.Text = $"Average Value: {average:F2}";

            DataTable dataTable = DataProcessor.Instance.ConvertToDataTable(dataSet);
            SensorDataGrid.ItemsSource = dataTable.DefaultView;
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

        private void LoadMenuItem_Click(object sender, RoutedEventArgs e)
        {
            StatusMessageTextBlock.Text = "Load selected.";
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            StatusMessageTextBlock.Text = "Save selected.";
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PreviousDataSetButton_Click(object sender, RoutedEventArgs e)
        {
            StatusMessageTextBlock.Text = "Previous dataset selected.";
        }

        private void NextDataSetButton_Click(object sender, RoutedEventArgs e)
        {
            StatusMessageTextBlock.Text = "Next dataset selected.";
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
            SensorDataGrid.Items.Refresh();
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

                    if (double.TryParse(cellValue.ToString(), out double value) {
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
    }
}