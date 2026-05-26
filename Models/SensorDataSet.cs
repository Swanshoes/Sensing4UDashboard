using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing4UDashboard.Models
{
    public class SensorDataSet
    {
        public string Name { get; set; } = string.Empty;
        public SensorData[,] Data { get; set; }
        public int RowCount { get { return Data.GetLength(0); } }
        public int ColumnCount { get { return Data.GetLength(1); } }
        public SensorDataSet(int rows, int columns) 
        {
            Data = new SensorData[rows, columns];
        }
    }
}
