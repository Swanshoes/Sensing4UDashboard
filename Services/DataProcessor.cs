using Sensing4UDashboard.Models;
using System;
using System.Collections.Generic;
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

        public double CalculateAverage(SensorDataSet dataSet)
        {
            double total = 0;
            int count = 0;

            for (int row = 0; row < dataSet.columnCount; row++)
            {
                for (int col = 0; col < dataSet.columnCount; col++)
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

            return total/count;
        }
    }
}
