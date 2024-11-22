using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPO
{
    public static class Logger
    {
        private static readonly string LogPath = "log.txt";
        public static void Log(IEnumerable<SensorData> sensorData)
        {
            using (var writer = new StreamWriter(LogPath, true))
            {
                foreach (var data in sensorData)
                {
                    writer.WriteLine($"Room: {data.RoomName} Number of students: {data.Movement.Count(b => b)} Tempature: {data.Tempature}");
                }
            }
        }
    }
}
