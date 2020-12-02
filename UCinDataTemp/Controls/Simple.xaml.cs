using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace UCinDataTemp.Controls
{
    public sealed partial class Simple : UserControl
    {
        static List<Simple> AllSimple = new List<Simple>();
        public static readonly DependencyProperty LeaderProperty =
            DependencyProperty.Register("Leader",
                typeof(string),
                typeof(Simple), new PropertyMetadata("Leader Text", LeaderChanged));

        private static void LeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            LeaderChanged(d);
        }

        private static void LeaderChanged(DependencyObject d)
        {
            var p = d as Simple;
        }




        int index = 1;

        public Simple()
        {
            this.InitializeComponent();
            Loaded += Simple_Loaded;
            Unloaded += Simple_Unloaded; ;
        }

        private void Simple_Unloaded(object sender, RoutedEventArgs e)
        {
            AllSimple.Remove(sender as Simple);
        }

        private void Simple_Loaded(object sender, RoutedEventArgs e)
        {
            AllSimple.Add(sender as Simple);
        }

        public string Leader
        {
            get { return (string)GetValue(LeaderProperty); }
            set { SetValue(LeaderProperty, value); }
        }

        private void tbutton_Click(object sender, RoutedEventArgs e)
        {
            follower.Text = $"Pressed {index} times.";
            //Leader = $"Set From Code {index} times. ";
            index++;
        }
    }
}
