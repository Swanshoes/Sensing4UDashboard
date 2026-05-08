using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing4UDashboard.Models
{
    public class SensorData
    {
        public String Label { get; set; } = string.Empty;
        public double Value { get; set; } 
        public DateTime Timestamp { get; set; }
    }
}
