using CatRenta.Application;
using CatRenta.EFData;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AddCatWPFCRUD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public EFDataContext _context { get; set; }
        private ObservableCollection<CatVM> _cats = new ObservableCollection<CatVM>();
        public MainWindow()
        {
            InitializeComponent();
            _context = new EFDataContext();
            //  Заповнення данних при умові що БД пуста
            DbSeeder.SeedAll(_context);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillList();
        }
        public void FillList() 
        {
            this.dgSimple.Items.Clear();
            //  Витягування данних з БД
            var list = this._context._cats.Select(x => new CatVM
            {
                Name = x.Name,
                Id = x.Id,
                ImgPath = x.ImagePath,
                Birthday = x.Birthday
            }).ToList();
            //  Формування колекції типу ObdervableCollection
            this._cats = new ObservableCollection<CatVM>(list);
            //  Передача данних дата гріду
            this.dgSimple.ItemsSource = this._cats;
        }
        private void dgSimple_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            //  При створенні нового обєкта в DataGrid
            //  Створюється новий обєкт CatVM, який присвоюється до DataGrid
            CatVM cat = new CatVM();
            cat.Birthday = DateTime.Now;
            cat.Name = "New Cat";
            cat.Id = this._cats.Last().Id+1;
            cat.IsNow = true;
            e.NewItem = cat;
        }

        /// <summary>
        ///     Метод, який викликається при нажатті на зображення
        /// </summary>
        /// <param name="sender">Кнопка(фотографія), яка згенерувала подію</param>
        /// <param name="e">Параметри</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //  Створюється обєкт провідника
            OpenFileDialog dlg = new OpenFileDialog();
            //  Задання фільтра пошуку
            dlg.Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png";
            if (dlg.ShowDialog().HasValue) 
            {
                //  Присвоєння до певного елемента колекції зображення,
                //  яке за допомогою MVVM відображатиметься у DataGrid
                int btnId = (int)((sender as Button).Tag);
                CatVM cat = this._cats.FirstOrDefault(x => x.Id == btnId);
                
                    if (File.Exists(dlg.FileName)) 
                    {
                        cat.ImgPath = dlg.FileName;
                    }
                
            }
        }

        /// <summary>
        ///     Подія, яка виникає при закриті програми
        ///     і зберігає усі додані елементи
        /// </summary>
        /// <param name="sender">Програма</param>
        /// <param name="e">Параметри</param>
        private void Window_Closed(object sender, EventArgs e)
        {
            //  Витягує усі елементи, які є новими
            var elements = this._cats.Where(x => x.IsNow).Select(x => x);
            if (elements.Count() > 0) 
            {
                foreach (var el in elements) 
                {
                    //  Додавання до таблиці вибраного зображення (зображення передається на пряму)
                    string imgName = System.IO.Path.GetRandomFileName();
                    using (FileStream fs = new FileStream(el.ImgPath, FileMode.Open, FileAccess.Read)) 
                    {
                        using (BinaryReader br = new BinaryReader(fs)) 
                        {
                            byte[] arr = br.ReadBytes((int)fs.Length);
                            this._context._images.Add(new CatRenta.Entities.ImageData
                            {
                                Name = imgName,
                                Data = arr
                            });
                        }
                    }

                    //  Створення елементів в таблиці AppCat, які були добавленні в DataGrid
                    this._context._cats.Add(new CatRenta.Entities.AppCat { 
                    Name = el.Name,
                    Birthday = el.Birthday,
                    ImagePath = imgName
                    });

                    this._context.SaveChanges();
                }
            }
        }

        /// <summary>
        ///     Метод, який використовується коли зображення потрібно вийняти з БД (Не по силці)
        /// </summary>
        /// <param name="sender">Зображення яке є активним у DataGrid</param>
        /// <param name="e">Параметри</param>
        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            //  Витягуєтсья обєкт який згенерував подію
            var el = sender as System.Windows.Controls.Image;
            //  Повертаються дані тега (а саме дані про шлях фотографії)
            var tag = (el.Tag as string);
            //  Перевірка чи назва зображення не силка
            
            if (tag != null && !tag.Contains("http") && !File.Exists(tag)) 
            {
                //  BitmapImage - являє собою клас, який загружає зображення у форматі XAML 
                BitmapImage bitmapimage = new BitmapImage();
                //  Відкривається блок
                bitmapimage.BeginInit();
                //  Задається ресурс, з якого зчитується зображення
                bitmapimage.StreamSource = new MemoryStream(this._context._images
                    .FirstOrDefault(x => x.Name == tag).Data);
                // Вказує як зображення використовує кешування памяті
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                //  Закриваєтсья блок
                bitmapimage.EndInit();

                //  Присвоєння зображенню в розмітці фотографії, яка була взята з БД
                el.Source = bitmapimage;
            }
        }
        /// <summary>
        ///     Подія яка викликає вікно в якому можна додати елемент (кота) до колекції (БД)
        /// </summary>
        /// <param name="sender">Кнопка що згенерувала подію</param>
        /// <param name="e">Параметри</param>
        private void btnAddEl_Click(object sender, RoutedEventArgs e)
        {
           AddElement dlg = new AddElement(this._context, this._cats);
            dlg.ShowDialog();
        }
    }
}
