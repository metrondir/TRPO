using System;
using System.Collections.Generic;
using System.Linq;
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
        private Label label1;
        private Label ClassRoom1;
        private Label label2;
        private Label ClassRoom2;
        private Label label4;
        private Label ClassRoom3;
        private Label busyRooms;
        private Label busyRoomsLabel;
        public MainForm()
        {
            InitializeSensors();
            InitializeComponent();
            StartMonitoring();

            Classroom1 classroom1 = new Classroom1(sensors[0]);
            classroom1.Show();
            Classroom3 classroom3 = new Classroom3(sensors[1]);
            classroom3.Show();
            Classroom2 classroom2 = new Classroom2(sensors[2]);
            classroom2.Show();
        }
        private void InitializeSensors()
        {
            sensors = new List<Sensor>
            {
                new Sensor("ClassRoom1"),
                new Sensor("ClassRoom2"),
                new Sensor("ClassRoom3"),
            };
        }
        private async void StartMonitoring()
        {
            while (true)
            {
                var SensorTasks = sensors.Select(sensor => sensor.GetSensorData()).ToList();
                var SensorData = await Task.WhenAll(SensorTasks);
                var busyRooms = SensorData.Where(data => data.IsOcupied).ToList();
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
                    sensorLabel.Text = $"Room: {sensor.RoomName} Number of students: {sensor.Movement.Count(b => b)} Tempature: {sensor.Tempature}";
                }
            }
            var busyRoomsLabel = Controls.Find("busyRooms", true).FirstOrDefault() as Label;
            if (busyRoomsLabel != null)
            {
                busyRoomsLabel.Text = $" {string.Join(", ", busyRooms.Select(b => b.RoomName))}";
            }

        }
        private void InitializeComponent()
        {
           

            this.busyRoomsLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ClassRoom1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ClassRoom2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ClassRoom3 = new System.Windows.Forms.Label();
            this.busyRooms = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // busyRoomsLabel
            // 
            this.busyRoomsLabel.AutoSize = true;
            this.busyRoomsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.busyRoomsLabel.Location = new System.Drawing.Point(16, 270);
            this.busyRoomsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.busyRoomsLabel.Name = "busyRoomsLabel";
            this.busyRoomsLabel.Size = new System.Drawing.Size(126, 25);
            this.busyRoomsLabel.TabIndex = 1;
            this.busyRoomsLabel.Text = "Busy rooms: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Classroom 1:";
            // 
            // ClassRoom1
            // 
            this.ClassRoom1.AutoSize = true;
            this.ClassRoom1.Location = new System.Drawing.Point(140, 37);
            this.ClassRoom1.Name = "ClassRoom1";
            this.ClassRoom1.Size = new System.Drawing.Size(59, 16);
            this.ClassRoom1.TabIndex = 3;
            this.ClassRoom1.Text = "unknown";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Classroom 2";
            // 
            // ClassRoom2
            // 
            this.ClassRoom2.AutoSize = true;
            this.ClassRoom2.Location = new System.Drawing.Point(140, 82);
            this.ClassRoom2.Name = "ClassRoom2";
            this.ClassRoom2.Size = new System.Drawing.Size(59, 16);
            this.ClassRoom2.TabIndex = 5;
            this.ClassRoom2.Text = "unknown";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Classroom 3";
            // 
            // ClassRoom3
            // 
            this.ClassRoom3.AutoSize = true;
            this.ClassRoom3.Location = new System.Drawing.Point(140, 127);
            this.ClassRoom3.Name = "ClassRoom3";
            this.ClassRoom3.Size = new System.Drawing.Size(59, 16);
            this.ClassRoom3.TabIndex = 7;
            this.ClassRoom3.Text = "unknown";
            // 
            // busyRooms
            // 
            this.busyRooms.AutoSize = true;
            this.busyRooms.Location = new System.Drawing.Point(139, 277);
            this.busyRooms.Name = "busyRooms";
            this.busyRooms.Size = new System.Drawing.Size(59, 16);
            this.busyRooms.TabIndex = 8;
            this.busyRooms.Text = "unknown";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.busyRooms);
            this.Controls.Add(this.ClassRoom3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ClassRoom2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ClassRoom1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.busyRoomsLabel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Room Monitoring";
            this.ResumeLayout(false);
            this.PerformLayout();

        }


    }
  
}

