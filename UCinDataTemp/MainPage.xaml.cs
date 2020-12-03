
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Controls;



namespace UCinDataTemp
{
    
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {

        ObservableCollection<Stupid> Source = new ObservableCollection<Stupid>();
        ObservableCollection<Stupid> Source2 = new ObservableCollection<Stupid>();
        private string _BLeader;

        public string BLeader
        {
            get { return _BLeader; }
            set { Set(ref _BLeader, value, "BLeader"); }
        }

        private string _PText;

        public string PText
        {
            get { return _PText; }
            set { Set(ref _PText, value, "PText"); }
        }
        public MainPage()
        {
            this.InitializeComponent();
            BLeader = "Leader from x:Bind";
            PText = "inductor";
            Source.Add(new Stupid("Leader one"));
            Source.Add(new Stupid("Leader two"));
            Source.Add(new Stupid("Leader three"));
            Source2.Add(new Stupid("inductor"));
            Source2.Add(new Stupid("gnd"));
            //Source2.Add(new SchLVItem {Parttype="inductor",TextLab="Inductor" });
            //Source2.Add(new SchLVItem { Parttype = "gnd", TextLab = "Ground" });
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public class Stupid : INotifyPropertyChanged
    {
        //public string Strg { get; set; }
        private string _Strg;

        public string Strg
        {
            get { return _Strg; }
            set { Set(ref _Strg, value, "Strg"); }
        }
        public Stupid(string s)
        {
            Strg = s;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            //if (Equals(storage, value))
            //{
            //    return;
            //}

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
