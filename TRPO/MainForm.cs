using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TRPO
{
    public class MainForm : Form
    {
        private const double TemperatureThreshold = 28.0;
        private const int SensorUpdateInterval = 3000;
        public List<Sensor> sensors;
        //private TextBox sensorTextBox;
        private Button buttonRoom1;
        private Button buttonRoom2;
        private Button buttonRoom3;

        private Label busyRoomsLabel;
        public MainForm()
        {
            InitializeSensors();
            InitializeComponent();
            StartMonitoring();
        }
        private void InitializeSensors()
        {
            sensors = new List<Sensor>
            {
                new Sensor("Classroom 1"),
                new Sensor("Classroom 2"),
                new Sensor("Classroom 3"),
            };
        }
        private async void StartMonitoring()
        {
            while (true)
            {
                var SensorTasks = sensors.Select(sensor => sensor.GetSensorData()).ToList();
                var SensorData = await Task.WhenAll(SensorTasks);
                var busyRooms = SensorData.Where(data => data.IsOcupied &&).ToList();
                UpdateUI(SensorData, busyRooms);

                Logger.Log(SensorData);

                Task.Delay(SensorUpdateInterval).Wait();

            }
        }

        private void UpdateUI(IEnumerable<SensorData> sensors, List<SensorData> busyRooms)
        {
            foreach (var sensor in sensors)
            {
                var sensorLabel = Controls.Find(sensor.RoomName, true).FirstOrDefault() as Label;
                if (sensorLabel != null)
                {
                    sensorLabel.Text = $"Room: {sensor.RoomName} Movement: {sensor.Movement} Tempature: {sensor.Tempature}";
                }
            }
            var busyRoomsLabel = Controls.Find("BusyRooms", true).FirstOrDefault() as Label;
            if (busyRoomsLabel != null)
            {
                busyRoomsLabel.Text = $"Busy rooms: {string.Join(", ", busyRooms)}";
            }

        }
        private void InitializeComponent()
        {
            Classroom1 classroom1 = new Classroom1(sensors[0]);
            classroom1.Show();
            Classroom3 classroom3 = new Classroom3(sensors[1]);
            classroom3.Show();
            Classroom2 classroom2 = new Classroom2(sensors[2]);
            classroom2.Show();
            this.busyRoomsLabel = new System.Windows.Forms.Label();

            this.SuspendLayout();
            // 
            // busyRoomsLabel
            // 
            this.busyRoomsLabel.AutoSize = true;
            this.busyRoomsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.busyRoomsLabel.Location = new System.Drawing.Point(18, 338);
            this.busyRoomsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.busyRoomsLabel.Name = "busyRoomsLabel";
            this.busyRoomsLabel.Size = new System.Drawing.Size(151, 29);
            this.busyRoomsLabel.TabIndex = 1;
            this.busyRoomsLabel.Text = "Busy rooms: ";
            // 
     
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.busyRoomsLabel);
            this.Controls.Add(this.buttonRoom1);
            this.Controls.Add(this.buttonRoom2);
            this.Controls.Add(this.buttonRoom3);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "Room Monitoring";
            this.ResumeLayout(false);
            this.PerformLayout();

        }


    }
  
}

