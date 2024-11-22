using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPO
{
    public class SensorData
    {
        public string RoomName { get; set; }
        public List<bool> Movement { get; set; }
        public double Tempature { get; set; }

        public bool IsOcupied => Movement.Any(m=> m) && Tempature >28.0;


    }
}
