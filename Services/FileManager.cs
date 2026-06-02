using Sensing4UDashboard.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing4UDashboard.Services
{
    public class FileManager
    {
        public void SaveBinaryFile(string filePath, SensorDataSet sensorDataSet)
        {
            using BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create));

            writer.Write(sensorDataSet.Name);
            writer.Write(sensorDataSet.RowCount);
            writer.Write(sensorDataSet.ColumnCount);

            for (int row = 0; row < sensorDataSet.RowCount; row++)
            {
                for (int column = 0; column < sensorDataSet.ColumnCount; column++)
                {
                    SensorData? reading = sensorDataSet.Data[row, column];

                    if (reading == null)
                    {
                        writer.Write(false);
                    }
                    else
                    {
                        writer.Write(true);
                        writer.Write(reading.Label);
                        writer.Write(reading.Value);
                        writer.Write(reading.Timestamp.ToBinary());
                    }
                }
            }
        }

        public SensorDataSet LoadBinaryFile(string filePath) 
        {
            using BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open));

            string name = reader.ReadString();
            int rowCount = reader.ReadInt32();
            int columnCount = reader.ReadInt32();

            SensorDataSet sensorDataSet = new SensorDataSet(rowCount, columnCount);

            sensorDataSet.Name = name;

            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    bool hasReading = reader.ReadBoolean();
                    if (hasReading)
                    {
                        string label = reader.ReadString();
                        double value = reader.ReadDouble();
                        DateTime timestamp = DateTime.FromBinary(reader.ReadInt64());
                        sensorDataSet.Data[row, column] = new SensorData
                        {
                            Label = label,
                            Value = value,
                            Timestamp = timestamp
                        };
                    }
                }
            }
            return sensorDataSet;
        }
    }
}
