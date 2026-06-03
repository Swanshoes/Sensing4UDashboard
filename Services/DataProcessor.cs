using Microsoft.Windows.Themes;
using Sensing4UDashboard.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing4UDashboard.Services
{
    public class DataProcessor
    {
        private static DataProcessor? _instance;

        public static DataProcessor Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DataProcessor();
                }
                return _instance;
            }
        }

        private DataProcessor()
        {
            // Private constructor to prevent instantiation
        }

        //method to calcluate the average held in the 2d array
        public double CalculateAverage(SensorDataSet dataSet)
        {
            if (dataSet == null || dataSet.Data == null)
            {
                return 0;
            }

            double total = 0;
            int count = 0;

            // Iterate through the 2D array and sum up the values while counting the number of valid entries
            for (int row = 0; row < dataSet.RowCount; row++)
            {
                for (int col = 0; col < dataSet.ColumnCount; col++)
                {
                    if (dataSet.Data[row, col] != null)
                    {
                        total += dataSet.Data[row, col].Value;
                        count++;
                    }
                }
            }

            if (count == 0)
            {
                return 0; // Avoid division by zero
            }

            return total / count;
        }

        // Method to set bounds for the 2d array values in identifying an acceptable range
        public string GetValueStatus(double value, double min, double max)
        {
            if (value < min)
            {
                return "Low";
            }
            else if (value > max)
            {
                return "High";
            }
            else
            {
                return "Acceptable";
            }
        }

        // Method to convert the 2d array into a DataTable for easier display in the UI
        public DataTable ConvertToDataTable(SensorDataSet dataSet)
        {
            DataTable table = new DataTable();

            table.Columns.Add("Timestamp", typeof(DateTime));

            // Add columns to the DataTable
            for (int col = 0; col < dataSet.ColumnCount; col++)
            {
                string columnName = dataSet.Data[0, col]?.Label ?? $"Sensor {col + 1}";

                table.Columns.Add(columnName, typeof(double));
            }
            // Add rows to the DataTable
            for (int row = 0; row < dataSet.RowCount; row++)
            {
                DataRow dataRow = table.NewRow();

                dataRow["Timestamp"] = dataSet.Data[row, 0]?.Timestamp ?? DateTime.MinValue;

                for (int col = 0; col < dataSet.ColumnCount; col++)
                {
                    SensorData? sensorReading = dataSet.Data[row, col];

                    if (sensorReading != null)
                    {
                        dataRow[col + 1] = sensorReading.Value;
                    }
                    else
                    {
                        dataRow[col + 1] = DBNull.Value;
                    }
                }
                table.Rows.Add(dataRow);
            }
            return table;
        }

        public List<SensorData> SortData(SensorDataSet data)
        {
            List<SensorData> sortedData = new List<SensorData>();

            for (int row = 0; row < data.RowCount; row++)
            {
                for (int column = 0; column < data.ColumnCount; column++)
                {
                    SensorData? reading = data.Data[row, column];

                    if (reading != null)
                    {
                        sortedData.Add(reading);
                    }

                    
                }
            }
            sortedData = sortedData.OrderBy(reading => reading.Value).ToList();

            return sortedData;
        }

        public int BinarySearch(List<SensorData> sortedData, double target)
        {
            int low = 0;
            int high = sortedData.Count - 1;

            while (low <= high)
            {
                int mid = (low + high) / 2;
                double midValue = sortedData[mid].Value;

                if (midValue == target)
                {
                    return mid;
                }

                if (midValue < target)
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid - 1;
                }
            }
            return -1;
        }

        //this method uses the above two (Binary Search and SortData) to find the data point to return info to user
        public SensorData? FindSensorValue(SensorDataSet sensorData, double target)
        {
            List<SensorData> sortedData = SortData(sensorData);

            int index = BinarySearch(sortedData, target);

            if (index == -1)
            {
                return null;
            }

            return sortedData[index];
        }
    }
}
