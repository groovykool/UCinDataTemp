using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Rectangle = Windows.UI.Xaml.Shapes.Rectangle;
using Point = Windows.Foundation.Point;
using Windows.Devices.WiFiDirect.Services;
using Windows.UI.Xaml.Controls.Primitives;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace UCinDataTemp.Controls
{
    public partial class Part : UserControl
    {
        #region Properties
       
        public Transform Trans { get; set; }
        //string PData { get; set; }
     
        string PathKey;
      
        public static TextBox Log;
       
        public static double ConSize = 6.0;
        public static double HalfConSize = ConSize / 2.0;
        

        #endregion

        public static readonly DependencyProperty
          PtypeProperty = DependencyProperty.Register(
            "Ptype",
            typeof(string),
            typeof(Part),
            new PropertyMetadata("d", PtypeChanged));
        
        public string Ptype
        {
            get { return (string)GetValue(PtypeProperty); }
            set { SetValue(PtypeProperty, value); }
        }

        public static readonly DependencyProperty
          PDataProperty = DependencyProperty.Register(
            "PData",
            typeof(string),
            typeof(Part),
            new PropertyMetadata("M0,50 l2,0"));

        public string PData
        {
            get { return (string)GetValue(PDataProperty); }
            set { SetValue(PDataProperty, value); }
        }


        private static void PtypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
            var p = d as Part;
            p.PathKey = p.Ptype + "path";
            var shape = p.VB;
            if (shape == null) return;
            //Get resource dictionary of Part Types
            var resd = p.Resources.MergedDictionaries.ElementAt(0);
            p.PData = (string)resd.SingleOrDefault(x => (string)x.Key == p.PathKey).Value;
        }
      
        public Part()
        {
            this.InitializeComponent();         
        }
    }
    
   
}

