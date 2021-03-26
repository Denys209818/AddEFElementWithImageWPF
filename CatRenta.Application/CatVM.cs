using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatRenta.Application
{
    public class CatVM : INotifyPropertyChanged
    {

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value;
                OnPropertyChanged("Id");
            }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value;
                OnPropertyChanged("Name");
            }
        }

        private string _imgPath;

        public string ImgPath
        {
            get { return _imgPath; }
            set { _imgPath = value;
                OnPropertyChanged("ImgPath");
            }
        }

        private DateTime _birthday;

        public DateTime Birthday
        {
            get { return _birthday; }
            set { _birthday = value;
                OnPropertyChanged("Birthday");
            }
        }

        public bool IsNow { get; set; } = false;



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
