using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TRPO
{
    public class Sensor
    {
        private Random Random = new Random();
        public string RoomName { get; set; }   

        public Sensor (string roomName)
        {
            RoomName = roomName;
        }
        public Task<SensorData> GetSensorData()
        {
            return Task.Run(() =>
            {
                Thread.Sleep(1000);
                var random = new Random();
                return new SensorData
                {
                    RoomName = RoomName,
                    Movement = GetMovement(random),
                    Tempature = random.NextDouble() * 10 + 15
                };
            });
        }
        private List<bool> GetMovement(Random random)
        {
            var result = new List<bool>();
            for (int i = 0; i < 21; i++)
            {
                result.Add(random.Next(0, 2) == 1);
            }
            return result;
        }
    }
}
