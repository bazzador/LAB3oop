using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB3oop
{
    public class RenderService
    {
        // Метод, який повертає розмір для рендерингу
        public Tuple<double, double> GetRenderSize()
        {
            // Тут реалізація отримання розміру для рендерингу
            double width = 1000; // Приклад значення ширини
            double height = 450; // Приклад значення висоти
            return Tuple.Create(width, height);
        }
    }
}
