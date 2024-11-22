using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TRPO
{
    public partial class Classroom3 : Form
    {
        public Classroom3(Sensor sensor)
        {
            InitializeComponent();

            sensor.GetSensorData().ContinueWith(task =>
            {
                var data = task.Result;

                this.Invoke((MethodInvoker)delegate
                {
                    var labels = this.Controls.OfType<Label>().ToArray();

                    for (int i = 0; i < labels.Length && i < data.Movement.Count; i++)
                    {
                        var movement = data.Movement[i];
                        var label = labels[i];

                        if (movement)
                        {
                            label.Text = "Occupied";
                            label.ForeColor = Color.Red;
                        }
                        else
                        {
                            label.Text = "Free";
                            label.ForeColor = Color.Green;
                        }
                    }
                });
            });
        }
    }
}
