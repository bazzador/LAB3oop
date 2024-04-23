using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Linq;

namespace LAB3oop
{
    internal class Horse
    {
        public string name {  get;  private set; }
        public Color playerColor { get; private set; }
        public TimeSpan time { get; private set; }
        public Horse(string name, Color color)
        {
            this.name = name;
            playerColor = color;
            time = TimeSpan.Zero; // Час початку забігу
        }
    }
}
