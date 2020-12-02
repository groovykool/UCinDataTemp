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
        //bool IsMenuVisible = false;
        public Transform Trans { get; set; }
        string PData { get; set; }
        //string PText;
        string PSettings;
        //string TextKey;
        //string PinKey;
        string PathKey;
        //string[] TextItems;
        //string[] PinItems;
        string[] SettingsItems;
        public List<TextLabel> Labels { get; set; } = new List<TextLabel>();
        //public List<Pin> Pins { get; set; } = new List<Pin>();
        //public List<HideNode> HideNodes { get; set; } = new List<HideNode>();
        //Grid PGrid;
        int PRows, PCols, Row, Col, basex, basey, colspan, rowspan, hitwidth, hitheight;
        double GWidth, GHeight, hitleft, hittop;
        //static bool IsDragging = false;

        //static Matrix rot90 = new Matrix(0, 1, -1, 0, 0, 0);
        //Matrix MLast = Matrix.Identity;
        //static Matrix mirx = new Matrix(-1, 0, 0, 1, 0, 0);
        //public static Dictionary<string, (string prefix, int next)>
                                //NextRefDes = new Dictionary<string, (string prefix, int next)>();
        //eight possible position and transforms
        //not transformed
        //static Matrix one = Matrix.Identity;
        //rotate 90 CW
        //static Matrix two = new Matrix(0, 1, -1, 0, 0, 0);
        //rotate 180 CW
        //static Matrix three = new Matrix(-1, 0, 0, -1, 0, 0);
        //rotate 270 CW
        //static Matrix four = new Matrix(0, -1, 1, 0, 0, 0);
        // mirror x at 0CW
        //static Matrix five = new Matrix(-1, 0, 0, 1, 0, 0);
        // mirror x at 90CW or (mirror y at 0CW and then 90CW)
        //tatic Matrix six = new Matrix(0, 1, 1, 0, 0, 0);
        //mirror x at 180CW
        //static Matrix seven = new Matrix(1, 0, 0, -1, 0, 0);
        //mirror x at 270CW or (mirror y at 0CW and then 270CW)
        static Matrix eight = new Matrix(0, -1, -1, 0, 0, 0);
        public static TextBox Log;
        //public static Control UnFocus;
        public static double ConSize = 6.0;
        public static double HalfConSize = ConSize / 2.0;
        //static List<Part> AllParts = new List<Part>();

        //private int partPosition = 1;
        //public int PartPosition
        //{
        //    get { return partPosition; }
        //    set { partPosition = Math.Clamp(value, 1, 8); }
        //}

        //private static bool alldisabled = false;
        //public static bool AllDisabled
        //{
        //    get { return alldisabled; }
        //    set
        //    {
        //        alldisabled = value;
        //        //Enabler(!value);
        //    }
        //}


        #endregion

        #region Dependency Properties
        //public static readonly DependencyProperty
        // StrokeProperty = DependencyProperty.Register(
        //   "Stroke",
        //   typeof(Brush),
        //   typeof(Part),
        //   new PropertyMetadata(new SolidColorBrush(Colors.OrangeRed)));

        public static readonly DependencyProperty
          PtypeProperty = DependencyProperty.Register(
            "Ptype",
            typeof(string),
            typeof(Part),
            new PropertyMetadata("d", PtypeChanged));

        //public static readonly DependencyProperty
        //  TnessProperty = DependencyProperty.Register(
        //    "Tness",
        //    typeof(double),
        //    typeof(Part),
        //    new PropertyMetadata(2.0));

        //public static readonly DependencyProperty
        //  RefDesProperty = DependencyProperty.Register(
        //    "RefDes",
        //    typeof(string),
        //    typeof(Part),
        //    new PropertyMetadata("d", RefDesChanged));

        //public static readonly DependencyProperty
        //  ValueProperty = DependencyProperty.Register(
        //    "Value",
        //    typeof(string),
        //    typeof(Part),
        //    new PropertyMetadata("d", ValueChanged));


        //public Brush Stroke
        //{
        //    get { return (Brush)GetValue(StrokeProperty); }
        //    set { SetValue(StrokeProperty, value); }
        //}
        public string Ptype
        {
            get { return (string)GetValue(PtypeProperty); }
            set { SetValue(PtypeProperty, value); }
        }
        //public string RefDes
        //{
        //    get { return (string)GetValue(RefDesProperty); }
        //    set { SetValue(RefDesProperty, value); }
        //}
        //public string Value
        //{
        //    get { return (string)GetValue(ValueProperty); }
        //    set { SetValue(ValueProperty, value); }
        //}

        //public double Tness
        //{
        //    get { return (double)GetValue(TnessProperty); }
        //    set { SetValue(TnessProperty, value); }
        //}

        private static void ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //var p = d as Part;
            //var valueTB = p.Labels?.SingleOrDefault(x => x.type == "value")?.TB;
            //if (valueTB != null)
            //{
            //    valueTB.Text = p.Value;
            //}

        }

        private static void RefDesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var p = d as Part;
            //var refdesTB = p.Labels.SingleOrDefault(x => x.type == "refdes")?.TB;
            //if (refdesTB != null)
            //{
            //    refdesTB.Text = p.RefDes;
            //    foreach (var pin in p.Pins)
            //    {
            //        pin.RefDes = p.RefDes;
            //    }
            //}

        }
        private static void PtypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PtypeChanged(d);
        }

        private static void PtypeChanged(DependencyObject d)
        {
            var p = d as Part;
            p.PathKey = p.Ptype + "path";
            //p.TextKey = p.Ptype + "labels";
            //p.PinKey = p.Ptype + "pins";
            //if (p == null) return;
            var shape = p.VB; // Straightforward version.
            //var shape = p.FindChild2<Path>("Shape");
            if (shape == null) return;
            //Get resource dictionary of Part Types
            var resd = p.Resources.MergedDictionaries.ElementAt(0);
            //p.PSettings = (string)resd.SingleOrDefault(x => (string)x.Key == p.Ptype).Value;
            p.BuildPart(p.PSettings);
            //Get Path data for Part Type
            p.PData = (string)resd.SingleOrDefault(x => (string)x.Key == p.PathKey).Value;

            //p.PText = (string)resd.SingleOrDefault(x => (string)x.Key == p.TextKey).Value;
            //p.DrawLabels(p.PText);
            //var pindata = (string)resd.SingleOrDefault(x => (string)x.Key == p.PinKey).Value;
            //if (pindata != null)
            //{
            //    p.DrawPins(pindata);
            //}



        }
        #endregion

        //constructor
        public Part()
        {
            this.InitializeComponent();
            //(this.Content as FrameworkElement).DataContext = this;
            //Loaded += Part_Loaded;
            //Unloaded += Part_Unloaded;
        }

        private void Part_Unloaded(object sender, RoutedEventArgs e)
        {
            //AllParts.Remove(sender as Part);
        }

        private void Part_Loaded(object sender, RoutedEventArgs e)
        {
            //this.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
            //var part = sender as Part;
            //AllParts.Add(sender as Part);
            //var grid = part.Parent as Grid;

            //grid.Loaded += (s, ex) =>
            //    {
            //        //Log.Text += "Parent Grid Loaded Event\n";
            //        GetPGridProps();
            //        GetPinLocations();
            //    };

            //if (grid.IsLoaded)
            //{
            //    GetPGridProps();
            //    GetPinLocations();
            //}
            ////RefDes is really set here.
            //if (RefDes == "")
            //{
            //    IncRefDes();
            //}
            //Trans = Sub.RenderTransform;
        }

        //Event to notify application when the Part has been Manipulated
        //public event EventHandler<ManipulationCompletedRoutedEventArgs> PartManipulated;

        //public void RaisePartManipulatedEvent(ManipulationCompletedRoutedEventArgs ex)
        //{
        //    this.PartManipulated?.Invoke(this, ex);
        //    RaisePinsChangedEvent(new RoutedEventArgs());
        //}

        //Event to notify application when the pins have changed
        //public static event EventHandler<RoutedEventArgs> PinsChanged;

        //public void RaisePinsChangedEvent(RoutedEventArgs ex)
        //{
        //    PinsChanged?.Invoke(this, ex);
        //}

        #region Methods

        //public Part Clone(int row = 0, int col = 0)
        //{
        //    Part clone = new Part()
        //    {
        //        Tag = "",
        //        Ptype = this.Ptype,
        //        RefDes = this.RefDes,
        //        Value = this.Value,
        //        Trans = this.Trans,
        //        MLast = this.MLast,
        //        Name = this.Name,        
        //    };
        //    var i = 0;
        //    foreach (var label in this.Labels)
        //    {
        //        clone.Labels.ElementAt(i).Position = label.Position;
        //        i++;
        //    }
        //    i = 0;
        //    foreach (var pin in Pins)
        //    {
        //        clone.Pins.ElementAt(i).RefDes = pin.RefDes;
        //        clone.Pins.ElementAt(i).Row = pin.Row;
        //        clone.Pins.ElementAt(i).Col = pin.Col;
        //        clone.Pins.ElementAt(i).NodeCol = pin.NodeCol;
        //        clone.Pins.ElementAt(i).NodeRow = pin.NodeRow;
        //        clone.Pins.ElementAt(i).Rect.Visibility = pin.Rect.Visibility;
        //        i++;
        //    }
        //    clone.SetValue(Grid.RowProperty, row);
        //    clone.SetValue(Grid.ColumnProperty, col);
        //    clone.PartManipulated = this.PartManipulated;
        //    //VisualStateManager.GoToState(clone, "Grouped", true);
        //    return clone;
        //}
        //public void SetState(string state)
        //{        
        //    VisualStateManager.GoToState(this, state, true);               
        //}
       
        //private void GetPinLocations()
        //{
        //    foreach (var pin in Pins)
        //    {
        //        var trans = pin.Rect.TransformToVisual(PGrid);
        //        var tpos = trans.TransformPoint(new Point(HalfConSize, HalfConSize));
        //        pin.Ppoint = tpos;
        //        pin.NodeCol = (int)Math.Round(tpos.X / GWidth);
        //        pin.NodeRow = (int)Math.Round(tpos.Y / GHeight);
        //    }
        //    foreach (var node in HideNodes)
        //    {
        //        var trans = Sub.TransformToVisual(PGrid);
        //        var tpos = trans.TransformPoint(new Point(node.X, node.Y));
        //        node.NodeCol = (int)Math.Round(tpos.X / GWidth);
        //        node.NodeRow = (int)Math.Round(tpos.Y / GHeight);
        //    }
        //}
        //public static void Enabler(bool state)
        //{
        //    foreach (var part in AllParts)
        //    {
        //        part.IsEnabled = state;
        //    }
        //}

        //public void PinsOn()
        //{
        //    foreach (var pin in Pins)
        //    {
        //        pin.Rect.Visibility = Visibility.Visible;
        //    }
        //}

        //public void IncRefDes()
        //{
            //var tup = (prefix: "", next: 0);
            //var result = NextRefDes.TryGetValue(this.Ptype, out tup);
            //if (result)
            //{
            //    RefDes = tup.prefix + tup.next.ToString();
            //    NextRefDes[this.Ptype] = (tup.prefix, ++tup.next);
            //    foreach (var pin in Pins)
            //    {
            //        pin.RefDes = RefDes;
            //    }
            //}
            //else
            //{
            //    RefDes = "P No RefDes Set";
            //}
        //}

        //private void GetPGridProps()
        //{
        //    PGrid = this.Parent as Grid;
        //    PRows = PGrid.RowDefinitions.Count;
        //    PCols = PGrid.ColumnDefinitions.Count;
        //    Row = (int)this.GetValue(Grid.RowProperty);
        //    Col = (int)this.GetValue(Grid.ColumnProperty);
        //    GHeight = PGrid.RowDefinitions.ElementAt(Row).ActualHeight;
        //    GWidth = PGrid.ColumnDefinitions.ElementAt(Col).ActualWidth;
        //}

        //private void SetRowCol(Point posInGrid)
        //{
        //    //subtrcat 1 for index and 1 more for span=2 at gris edges
        //    //subtract 1 from row/col because span=2.  put mouse in center when dragging part
        //    var row = Math.Clamp((int)Math.Round(posInGrid.Y / GHeight) - 1, 0, PRows - 2);
        //    var col = Math.Clamp((int)Math.Round(posInGrid.X / GWidth) - 1, 0, PCols - 2);
        //    this.SetValue(Grid.RowProperty, row);
        //    this.SetValue(Grid.ColumnProperty, col);
        //}

        //public void Rot()
        //{
        //    MLast = MMult(MLast, rot90);
        //    Trans = new MatrixTransform()
        //    { Matrix = MLast };
        //    Sub.RenderTransform = Trans;
        //    //Clip.Transform = Trans;
        //    RotPinsLabels();
        //    SetPartPosition();
        //    //GetPinLocations();
        //}
        //public void Mir()
        //{
        //    MLast = MMult(MLast, mirx);
        //    Trans = new MatrixTransform()
        //    { Matrix = MLast };
        //    Sub.RenderTransform = Trans;

        //    MirPinsLabels();
        //    SetPartPosition();
        //    //GetPinLocations();
        //}

        //private void RotPinsLabels()
        //{
        //    foreach (var lab in Labels)
        //    {
        //        lab.Position = lab.Position % 4 + 1;
        //    }
        //}

        //private void MirPinsLabels()
        //{
        //    foreach (var lab in Labels)
        //    {
        //        switch (lab.Position)
        //        {
        //            case 1:
        //                lab.Position = 3;
        //                break;
        //            case 3:
        //                lab.Position = 1;
        //                break;
        //        }
        //    }
        //}

        //private void SetPartPosition()
        //{
        //    switch (MLast)
        //    {
        //        case Matrix mat when mat == one:
        //            PartPosition = 1;
        //            break;
        //        case Matrix mat when mat == two:
        //            PartPosition = 2;
        //            break;
        //        case Matrix mat when mat == three:
        //            PartPosition = 3;
        //            break;
        //        case Matrix mat when mat == four:
        //            PartPosition = 4;
        //            break;
        //        case Matrix mat when mat == five:
        //            PartPosition = 5;
        //            break;
        //        case Matrix mat when mat == six:
        //            PartPosition = 6;
        //            break;
        //        case Matrix mat when mat == seven:
        //            PartPosition = 7;
        //            break;
        //        case Matrix mat when mat == eight:
        //            PartPosition = 8;
        //            break;
        //    }
        //}

        private void BuildPart(string settings)
        {
            //SettingsItems = settings.Split(",");
            basex = 100;
            basey = 100;
            colspan = 2;
            rowspan = 2;
            hitwidth = 80;
            hitheight = 50;
            hitleft = 10;
            hittop = 25;
        }


        //private void DrawLabels(string full)
        //{
        //    TextItems = full.Split("*");

        //    foreach (var item in TextItems)
        //    {

        //        var labels = item.Split(":");
        //        var labelheads = labels[0].Split(",");
        //        var name = labelheads[0];
        //        var poslabel = labelheads[1];
        //        var type = labelheads[2];
        //        var p1 = labels[1].Split(",");
        //        double[] pos1 = { double.Parse(p1[0]), double.Parse(p1[1]), double.Parse(p1[2]), 0, 0 };
        //        if (p1.Length == 5)
        //        {
        //            pos1[3] = double.Parse(p1[3]);
        //            pos1[4] = double.Parse(p1[4]);
        //        }
        //        var p2 = labels[2].Split(",");
        //        double[] pos2 = { double.Parse(p2[0]), double.Parse(p2[1]), double.Parse(p2[2]), 0, 0 };
        //        if (p2.Length == 5)
        //        {
        //            pos2[3] = double.Parse(p2[3]);
        //            pos2[4] = double.Parse(p2[4]);
        //        }
        //        var p3 = labels[3].Split(",");
        //        double[] pos3 = { double.Parse(p3[0]), double.Parse(p3[1]), double.Parse(p3[2]), 0, 0 };
        //        if (p3.Length == 5)
        //        {
        //            pos3[3] = double.Parse(p3[3]);
        //            pos3[4] = double.Parse(p3[4]);
        //        }
        //        var p4 = labels[4].Split(",");
        //        double[] pos4 = { double.Parse(p4[0]), double.Parse(p4[1]), double.Parse(p4[2]), 0, 0 };
        //        if (p4.Length == 5)
        //        {
        //            pos4[3] = double.Parse(p4[3]);
        //            pos4[4] = double.Parse(p4[4]);
        //        }
        //        uint startpos = uint.Parse(poslabel);

        //        var newlabel = new TextLabel()
        //        {
        //            TB = new TextBlock()
        //            {
        //                Padding = new Thickness(2.0),
        //                FontSize = 8,
        //                Name = name,
        //                Text = name,
        //                IsHitTestVisible = false
        //            },
        //            pos1 = pos1,
        //            pos2 = pos2,
        //            pos3 = pos3,
        //            pos4 = pos4,
        //            Position = startpos,
        //            type = type
        //        };
        //        Labels.Add(newlabel);
        //    }
        //    foreach (var tl in Labels)
        //    {
        //        Base.Children.Add(tl.TB);
        //    }
        //}

        //private void DrawPins(string full)
        //{
        //    var allpins = full.Split("#");
        //    PinItems = allpins[0].Split("*");
        //    double x, y;

        //    foreach (var item in PinItems)
        //    {
        //        var pin = item.Split(",");
        //        var pincol = int.Parse(pin[1]);
        //        var pinrow = int.Parse(pin[2]);
        //        //subtract rectangle offset to center pin


        //        x = basex / colspan * pincol - HalfConSize;
        //        y = basey / rowspan * pinrow - HalfConSize;

        //        var newrect = new Rectangle()
        //        {
        //            Name = pin[0],
        //            Stroke = new SolidColorBrush(Colors.Gold),
        //            Width = Part.ConSize,
        //            Height = Part.ConSize,
        //            IsHitTestVisible = false
        //        };
        //        newrect.SetValue(Canvas.LeftProperty, x);
        //        newrect.SetValue(Canvas.TopProperty, y);
        //        var newpin = new Pin()
        //        {
        //            Rect = newrect,
        //            PinName = pin[0],
        //            Row = pinrow,
        //            Col = pincol,
        //            RefDes = this.RefDes
        //        };
        //        Pins.Add(newpin);
        //    }
        //    foreach (var pin in Pins)
        //    {
        //        Sub.Children.Add(pin.Rect);
        //    }

        //    if (allpins.Length==2)
        //    {
        //        var hidenodes = allpins[1].Split("*");
        //        foreach (var item in hidenodes)
        //        {
        //            var node = item.Split(",");
        //            var col = int.Parse(node[0]);
        //            var row = int.Parse(node[1]);
        //            x = basex / colspan * col;
        //            y = basey / rowspan * row;
        //            var newnode = new HideNode()
        //            {
        //                Row = row,
        //                Col = col,
        //                X = x,
        //                Y = y
        //            };

        //            HideNodes.Add(newnode);
        //        }
        //    }
        //}
       

        //private Matrix MMult(Matrix a, Matrix b)
        //{
        //    Matrix c = new Matrix(a.M11 * b.M11 + a.M12 * b.M21,
        //                          a.M11 * b.M12 + a.M12 * b.M22,
        //                          a.M21 * b.M11 + a.M22 * b.M21,
        //                          a.M21 * b.M12 + a.M22 * b.M22,
        //                          0,
        //                          0);
        //    return c;
        //}
        #endregion

        #region UserControl Events
        
        

        

       

        
        

       
        #endregion

        #region Manipulation
       
        #endregion
    }
    //Namespace scope
    #region Namespace Items
    #region TextLabel Class
    public class TextLabel
    {
        public double[] pos1;
        public double[] pos2;
        public double[] pos3;
        public double[] pos4;
        public string type;
        private TextBlock tb;
        public TextBlock TB
        {
            get { return tb; }
            set
            {
                tb = value;
                tb.SizeChanged += TB2_SizeChanged;
            }
        }

        private uint position = 1;
        public uint Position
        {
            get { return position; }
            set
            {
                position = value;
                switch (position)
                {
                    case 1:
                        SetPosition(pos1);
                        break;
                    case 2:
                        SetPosition(pos2);
                        break;
                    case 3:
                        SetPosition(pos3);
                        break;
                    case 4:
                        SetPosition(pos4);
                        break;
                }
            }
        }
        public TextLabel()
        {

        }
        public TextLabel Clone(Canvas can)
        {
            TextLabel clone = (TextLabel)this.MemberwiseClone();
            clone.TB = new TextBlock()
            {
                Padding = this.TB.Padding,
                FontSize = 8,
                Name = this.TB.Name,
                Text = this.TB.Text,
                IsHitTestVisible = false
            };
            clone.TB.SetValue(Canvas.LeftProperty, this.TB.GetValue(Canvas.LeftProperty));
            clone.TB.SetValue(Canvas.TopProperty, this.TB.GetValue(Canvas.TopProperty));
            //can.Children.Remove(this.TB);
            can.Children.Add(clone.TB);
            return clone;
        }
        private void TB2_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            switch (position)
            {
                case 1:
                    SetPosition(pos1);
                    break;
                case 2:
                    SetPosition(pos2);
                    break;
                case 3:
                    SetPosition(pos3);
                    break;
                case 4:
                    SetPosition(pos4);
                    break;
            }
            if (type == "pin")
            {
                TB.Opacity = .8;
            }
            else
            {
                TB.Opacity = 1;
            }
        }
        private void SetPosition(double[] pos)
        {
            double xshift = 0.0;
            double yshift = 0.0;

            if (pos[2] >= 89)
            {
                var trans = new RotateTransform() { Angle = 90 };
                TB.RenderTransform = trans;
                xshift = -TB.ActualHeight * pos[3];
                yshift = TB.ActualWidth * pos[4];
            }
            else
            {
                TB.RenderTransform = null;
                xshift = TB.ActualWidth * pos[3];
                yshift = TB.ActualHeight * pos[4];
            }
            TB.SetValue(Canvas.LeftProperty, pos[0] - xshift);
            TB.SetValue(Canvas.TopProperty, pos[1] - yshift);
        }
    }
    #endregion

    //public class Pin
    //{

    //    public Rectangle Rect { get; set; }
    //    public string PinName { get; set; } = "";
    //    public string RefDes { get; set; } = "";
    //    public int Row { get; set; }
    //    public int Col { get; set; }
    //    public int NodeRow { get; set; }
    //    public int NodeCol { get; set; }

    //    private Point ppointrnd;
    //    public Point Ppoint
    //    {
    //        get
    //        {
    //            //Point round = new Point(Math.Round(ppointrnd.X,3), Math.Round(ppointrnd.Y,3));
    //            return ppointrnd;
    //        }
    //        set
    //        {
    //            ppointrnd = new Point(Math.Round(value.X, 3), Math.Round(value.Y, 3));
    //        }
    //    }

    //    public Pin Clone()
    //    {
    //        Pin clone = (Pin)this.MemberwiseClone();
    //        clone.Rect = new Rectangle()
    //        {
    //            Name = this.Rect.Name,
    //            Fill = new SolidColorBrush(Colors.Gold),
    //            Width = Part.ConSize,
    //            Height = Part.ConSize,
    //            IsHitTestVisible = false
    //        };
    //        clone.Rect.SetValue(Canvas.LeftProperty, this.Rect.GetValue(Canvas.LeftProperty));
    //        clone.Rect.SetValue(Canvas.TopProperty, this.Rect.GetValue(Canvas.TopProperty));
    //        return clone;
    //    }
    //    public Pin Clone(Canvas can)
    //    {
    //        Pin clone = (Pin)this.MemberwiseClone();
    //        clone.Rect = new Rectangle()
    //        {
    //            Name = this.Rect.Name,
    //            Fill = new SolidColorBrush(Colors.Gold),
    //            Width = Part.ConSize,
    //            Height = Part.ConSize,
    //            IsHitTestVisible = false
    //        };
    //        clone.Rect.SetValue(Canvas.LeftProperty, this.Rect.GetValue(Canvas.LeftProperty));
    //        clone.Rect.SetValue(Canvas.TopProperty, this.Rect.GetValue(Canvas.TopProperty));
    //        can.Children.Add(clone.Rect);
    //        return clone;
    //    }
    //}
    //public class HideNode
    //{
    //    public int Row { get; set; }
    //    public int Col { get; set; }
    //    public int NodeRow { get; set; }
    //    public int NodeCol { get; set; }
    //    public double X { get; set; }
    //    public double Y { get; set; }

    //}
    #region Extensions
    //static class DependencyObjectExtensions
    //{
    //    /// <summary>
    //    /// Finds a Child of a given item in the visual tree. 
    //    /// </summary>
    //    /// <param name="parent">A direct parent of the queried item.</param>
    //    /// <typeparam name="T">The type of the queried item.</typeparam>
    //    /// <param name="childName">x:Name or Name of child. </param>
    //    /// <returns>The first parent item that matches the submitted type parameter. 
    //    /// If not matching item can be found, 
    //    /// a null parent is being returned.</returns>
    //    /// <remarks>
    //    /// http://stackoverflow.com/a/1759923/1188513
    //    /// </remarks>
    //    public static T FindChild2<T>(this DependencyObject parent, string childName)
    //       where T : DependencyObject
    //    {
    //        if (parent == null) return null;

    //        T foundChild = null;

    //        var childrenCount = VisualTreeHelper.GetChildrenCount(parent);
    //        for (var i = 0; i < childrenCount; i++)
    //        {
    //            var child = VisualTreeHelper.GetChild(parent, i);
    //            var childType = child as T;
    //            if (childType == null)
    //            {
    //                foundChild = FindChild2<T>(child, childName);
    //                if (foundChild != null) break;
    //            }
    //            else if (!string.IsNullOrEmpty(childName))
    //            {
    //                var frameworkElement = child as FrameworkElement;
    //                if (frameworkElement != null && frameworkElement.Name == childName)
    //                {
    //                    foundChild = (T)child;
    //                    break;
    //                }
    //            }
    //            else
    //            {
    //                foundChild = (T)child;
    //                break;
    //            }
    //        }
    //        return foundChild;
    //    }

    //}
    #endregion
    #endregion
}

