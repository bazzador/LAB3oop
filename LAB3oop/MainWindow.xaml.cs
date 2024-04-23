using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Schema;


namespace LAB3oop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Horse> horses; // Список кіней
        public static int numberOfHorses = 6; // Кількість кіней за замовчуванням
        //public void RenderFrame() => image.Source = GetRender();
        static Barrier barrier = new Barrier(participantCount: numberOfHorses+1);

        public MainWindow()
        {
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
            InitializeComponent();
            InitializeHorses();
        }
        private void InitializeHorses()
        {
            string path = "C:\\Users\\Ivanchik\\Desktop\\Images\\Horses";
            string[] photoFiles = Directory.GetFiles(path, "*.png");

            Grid gridField = new Grid();
            gridField.Width = 1000;
            gridField.Height = 450;
            //for (int i = 0; i < numberOfHorses && i < photoFiles.Length; i++)
            //{
                Image image = new Image();

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new System.Uri(photoFiles[i]);
                bitmap.EndInit();
                image.Source = bitmap;

                image.Width = 100;
                image.Height = 100;

                image.Margin = new Thickness(280, 10, 10, 124);

                gridField.Children.Add(image);
                //}
                gridField.Margin = new Thickness(280, 10, 10, 124);
            
        }
        //private BitmapSource GetRender()
        //{
        //    var render_size = renderService.GetRenderSize();
        //    var bitmap = new RenderTargetBitmap(
        //        render_size.Item1, render_size.Item2,
        //        PixelsPerDip, PixelsPerDip, PixelFormats.Pbgra32);
        //    var drawingvisual = new DrawingVisual();
        //    using (DrawingContext dc = drawingvisual.RenderOpen())
        //    {
        //        Render(dc);
        //    }
        //    bitmap.Render(drawingvisual);
        //    return bitmap;
        //}

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
        //private async Task SimulateRaceAsync()
        //{
        //    foreach (var horse in horses)
        //    {
        //        await Task.Run(() => MoveHorse(horse)); // Асинхронно переміщуємо кожного кінь
        //    }
        //}
        //public List<ImageSource> GetHorseAnimation(Color color)
        //{
        //    const int count = 12;
        //    var bitmap_image_list = ReadImageList("Images/Horses", "WithOutBorder_", ".png", count);
        //    var mask_image_list = ReadImageList("Images/HorsesMask", "mask_", ".png", count);
        //    return bitmap_image_list.Select((item, index) => GetImageWithColor(item, mask_image_list[index], color)).ToList();
        //}
        //private List<BitmapImage> ReadImageList(string path, string name, string format, int count)
        //{
        //    path = $"pack://application:,,,/{path}/{name}";
        //    List<BitmapImage> list = new List<BitmapImage>();
        //    for (int i = 0; i < count; i++)
        //    {
        //        var uri = path + string.Format("{0:0000}", i) + format;
        //        var img = new BitmapImage(new Uri(uri));
        //        list.Add(img);
        //    }
        //    return list;
        //}
        //private ImageSource GetImageWithColor(BitmapImage image, BitmapImage mask, Color color)
        //{
        //    WriteableBitmap image_bmp = new WriteableBitmap(image);
        //    WriteableBitmap mask_bmp = new WriteableBitmap(mask);
        //    WriteableBitmap output_bmp = BitmapFactory.New(image.PixelWidth, image.PixelHeight);
        //    output_bmp.ForEach((x, y, c) =>
        //    {
        //        return MultiplyColors(image_bmp.GetPixel(x, y), color, mask_bmp.GetPixel(x, y).A);
        //    });
        //    return output_bmp;
        //}

        //private Color MultiplyColors(Color color_1, Color color_2, byte alpha) 
        //{
        //    var amount = alpha / 255.0;
        //    byte r = (byte)(color_2.R * amount + color_1.R * (1 - amount));
        //    byte g = (byte)(color_2.G * amount + color_1.G * (1 - amount));
        //    byte b = (byte)(color_2.B * amount + color_1.B * (1 -amount));
        //    return Color.FromArgb(color_1.A, r, g, b);
        //}
        //private void MoveHorse(Horse horse)
        //{
        //    Random random = new Random();
        //    double acceleration = random.NextDouble() * (1 - 0.7) + 0.7; // Випадкове прискорення
        //                                                                 // Реалізація руху конів з використанням прискорення
        //                                                                 // Тут потрібно здійснити анімацію руху кінів на трасі
        //}
    }
}
