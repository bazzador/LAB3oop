using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;

namespace LAB3oop
{
    internal class Horse
    {
        public string name {  get;  private set; }
        public Color playerColor { get; private set; }
        public TimeSpan time { get; private set; }
        public double speed {  get; private set; }
        public Horse(string name, Color color)
        {
            Random rnd = new Random();
            this.name = name;
            playerColor = color;
            time = TimeSpan.Zero; 
            speed = rnd.Next(5, 10);
        }
        public Task ChangeAcceleration()
        {
            Random random = new Random();
            double acceleration = random.NextDouble() * (1 - 0.7) + 0.7;
            speed *= acceleration;
            return Task.CompletedTask;
        }
    }
}
