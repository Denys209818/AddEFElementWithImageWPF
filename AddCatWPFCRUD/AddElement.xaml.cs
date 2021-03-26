using CatRenta.Application;
using CatRenta.EFData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AddCatWPFCRUD
{
    /// <summary>
    /// Interaction logic for AddElement.xaml
    /// </summary>
    public partial class AddElement : Window
    {
        private EFDataContext _context;
        private ObservableCollection<CatVM> _cats;
        public CatVM Cat { get; set; }
        public AddElement(EFDataContext context, ObservableCollection<CatVM> cats)
        {
            InitializeComponent();
            this._context = context;
            this._cats = cats;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string name = this.txtName.Text;
            if (string.IsNullOrEmpty(name)) 
            {
                MessageBox.Show("Заповніть поле: Назва");
                return;
            }
            string strUrl = this.txtImg.Text;
            if (string.IsNullOrEmpty(strUrl) || !strUrl.StartsWith("http"))
            {
                MessageBox.Show("Заповніть поле: Силка");
                return;
            }
            DateTime date;
            if (this.Birthday.SelectedDate.HasValue)
            {
                date = this.Birthday.SelectedDate.Value;
            }
            else 
            {
                date = DateTime.Now;
            }

            this._cats.Add(new CatVM
            {
                Name = name,
                ImgPath = strUrl,
                Birthday = date
            });
            _context._cats.Add(new CatRenta.Entities.AppCat { 
                Name = name,
                ImagePath = strUrl,
                Birthday = date
            });



            this.Close();
            _context.SaveChanges();
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            var el = sender as TextBox;
            if (el.Name == "txtName")
            {

                if (!string.IsNullOrEmpty(this.txtName.Text))
                {
                    this.brdName.BorderBrush = Brushes.LimeGreen;
                }
                else
                {
                    this.brdName.BorderBrush = Brushes.Red;
                }
            } 
            else if(el.Name == "txtImg")
            {
                if (!string.IsNullOrEmpty(this.txtImg.Text) && this.txtImg.Text.StartsWith("http"))
                {
                    this.brdImg.BorderBrush = Brushes.LimeGreen;
                }
                else
                {
                    this.brdImg.BorderBrush = Brushes.Red;
                }
            }
        }
    }
}
