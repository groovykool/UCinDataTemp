
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UCinDataTemp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {

        ObservableCollection<Stupid> Source = new ObservableCollection<Stupid>();
        private string _BLeader;

        public string BLeader
        {
            get { return _BLeader; }
            set { Set(ref _BLeader, value, "BLeader"); }
        }
        public MainPage()
        {
            this.InitializeComponent();
            BLeader = "Leader from x:Bind";
            Source.Add(new Stupid("Leader one"));
            Source.Add(new Stupid("Leader two"));
            Source.Add(new Stupid("Leader three"));
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
    public class Stupid
    {
        public string Strg { get; set; }
        public Stupid(string s)
        {
            Strg = s;
        }
    }
}
