using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UCinDataTemp.Models
{
    class SchLVItem : INotifyPropertyChanged
    {

        private string textLab = "";
        public string TextLab
        {
            get { return textLab; }
            set { Set(ref textLab, value, "TextLab"); }
        }

        private string parttype = "gnd";
        public string Parttype
        {
            get { return parttype; }
            set { Set(ref parttype, value, "Parttype"); }
        }

        public SchLVItem()
        {

        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }
    }

   
}
