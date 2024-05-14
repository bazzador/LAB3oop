using Syncfusion.Drawing;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace LAB3oop 
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RenderService renderService;
        private List<Horse> horses; 
        public static int numberOfHorses = 5; 
        public void RenderFrame1() => horse_1.Source = GetRender();
        public void RenderFrame2() => horse_2.Source = GetRender();
        public void RenderFrame3() => horse_3.Source = GetRender();
        public void RenderFrame4() => horse_4.Source = GetRender();
        public void RenderFrame5() => horse_5.Source = GetRender();

        //static Barrier barrier = new Barrier(participantCount: numberOfHorses+1);
        Stopwatch timer;
        //public double PixelsPerDip { get; set; }

        public MainWindow()
        {
            renderService = new RenderService();

            horses = new List<Horse>
            {
                new Horse("1", Colors.Red),
                new Horse("2", Colors.Blue),
                new Horse("3", Colors.Orange),
                new Horse("4", Colors.Green),
                new Horse("5", Colors.White)
            };
            timer = new Stopwatch();
            List<List<ImageSource>> horseImages = new List<List<ImageSource>>(numberOfHorses);
            for (int i = 0; i < numberOfHorses; i++)
            {
                horseImages.Add(GetHorseAnimation(horses[i].playerColor));
            }
            InitializeComponent();
        }
        private BitmapSource GetRender()
        {
            var render_size = renderService.GetRenderSize();
            var bitmap = new RenderTargetBitmap(
                (int)render_size.Item1, (int)render_size.Item2,
                100, 100, PixelFormats.Pbgra32);
            var drawingvisual = new DrawingVisual();
            using (DrawingContext dc = drawingvisual.RenderOpen())
            {
                Render(dc);
            }
            bitmap.Render(drawingvisual);
            return bitmap;
        }
        private void Render(DrawingContext dc)
        {
            foreach (var horse in horses)
            {
                var image = GetImageForHorse(horse);

                double left = Canvas.GetLeft(image);
                double top = Canvas.GetTop(image);

                dc.DrawImage(image.Source, new Rect(left+horse.speed, top, image.Width, image.Height));
            }
        }
        private async Task SimulateRaceAsync()
        {
            List<Task> tasks = new List<Task>();
            timer.Start();
            foreach (var horse in horses)
            {
                tasks.Add(horse.ChangeAcceleration());
                //MoveHorse(GetImageForHorse(horse), horse);
                Thread.Sleep(70);
                GetRender();
            }
            await Task.WhenAll(tasks);
        }
        public List<ImageSource> GetHorseAnimation(System.Windows.Media.Color color)
        {
            const int count = 12;
            var bitmap_image_list = ReadImageList("Images/Horses", "WithOutBorder_", ".png", count);
            var mask_image_list = ReadImageList("Images/HorsesMask", "mask_", ".png", count);
            return bitmap_image_list.Select((item, index) => GetImageWithColor(item, mask_image_list[index], color)).ToList();
        }
        private List<BitmapImage> ReadImageList(string path, string name, string format, int count)
        {
            path = $"C:\\Users\\Ivanchik\\Desktop\\{path}\\{name}";
            List<BitmapImage> list = new List<BitmapImage>();
            for (int i = 0; i < count; i++)
            {
                var uri = path + string.Format("{0:0000}", i) + format;
                var img = new BitmapImage(new Uri(uri));
                list.Add(img);
            }
            return list;
        }
        private ImageSource GetImageWithColor(BitmapImage image, BitmapImage mask, System.Windows.Media.Color color)
        {
            WriteableBitmap image_bmp = new WriteableBitmap(image);
            WriteableBitmap mask_bmp = new WriteableBitmap(mask);
            WriteableBitmap output_bmp = BitmapFactory.New(image.PixelWidth, image.PixelHeight);
            output_bmp.ForEach((x, y, c) =>
            {
                return MultiplyColors(image_bmp.GetPixel(x, y), color, mask_bmp.GetPixel(x, y).A);
            });
            return output_bmp;
        }
        private System.Windows.Media.Color MultiplyColors(System.Windows.Media.Color color_1, System.Windows.Media.Color color_2, byte alpha)
        {
            var amount = alpha / 255.0;
            byte r = (byte)(color_2.R * amount + color_1.R * (1 - amount));
            byte g = (byte)(color_2.G * amount + color_1.G * (1 - amount));
            byte b = (byte)(color_2.B * amount + color_1.B * (1 - amount));
            return System.Windows.Media.Color.FromArgb(color_1.A, r, g, b);
        }

        private System.Windows.Controls.Image GetImageForHorse(Horse horse)
        {
            switch (horse.name)
            {
                case "1":
                    return horse_1;
                case "2":
                    return horse_2;
                case "3":
                    return horse_3;
                case "4":
                    return horse_4;
                case "5":
                    return horse_5;
                default:
                    throw new ArgumentException("Invalid horse name");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SimulateRaceAsync();
        }
    }
}

//private void InitializeHorses()
//{
//    horses = new List<Horse>();
//    Random random = new Random();

//    for (int i = 0; i < numberOfHorses; i++)
//    {
//        Color color = Color.FromRgb((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256));
//        horses.Add(new Horse("Horse " + (i + 1), color));
//    }
//}
//string gifPath = @"C:\Users\Ivanchik\Desktop\Images\horseGif.gif"; // Шлях до вашого гіф зображення

//if (File.Exists(gifPath))
//{
//    Console.WriteLine("Файл знайдено: " + gifPath);

//    // Тут ви можете робити будь-які додаткові операції з файлом

//    // Наприклад, якщо ви хочете скопіювати файл в інше місце:
//    string destinationFolder = @"C:\Users\Ivanchik\Desktop\Images"; // Шлях до папки, куди ви хочете скопіювати файл
//    string newFilePath = System.IO.Path.Combine(destinationFolder, System.IO.Path.GetFileName(gifPath));

//    File.Copy(gifPath, newFilePath, true); // Копіюємо файл

//    Console.WriteLine("Файл скопійовано в: " + newFilePath);
//}
//else
//{
//    Console.WriteLine("Файл не знайдено: " + gifPath);
//}
//InitializeComponent();
//MoveHorse();
//InitializeHorses();        //private void InitializeHorses()
//{
//    string path = "C:\\Users\\Ivanchik\\Desktop\\Images\\Horses";
//    string[] photoFiles = Directory.GetFiles(path, "*.png");

//}
// https://github.com/reneschulte/WriteableBitmapEx
//private <int,int> MoveHorse(System.Windows.Controls.Image image, Horse horse)
//{
//    double initialLeft = Canvas.GetLeft(image);
//        if (initialLeft < 1000)
//        {
//            Canvas.SetLeft(image, initialLeft + horse.speed);
//            return 
//            Thread.Sleep(50);
//        }
//}