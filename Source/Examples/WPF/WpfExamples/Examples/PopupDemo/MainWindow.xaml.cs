// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// <summary>
//   Interaction logic for MainWindow.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PopupDemo
{
    using System.ComponentModel;
    using System.Windows;

    using OxyPlot;
    using OxyPlot.Series;

    using WpfExamples;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Example("A plot in a popup.")]
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            var tmp = new PlotModel { Title = "Popup Plot" };
            tmp.Series.Add(new FunctionSeries(System.Math.Sin, -10, +10, 0.01, "Sin"));
            this.PopupModel = tmp;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public PlotModel PopupModel { get; set; }
        private bool _showPopup = true;

        public bool ShowPopup
        {
            get
            {
                return _showPopup;
            }
            set
            {
                if (_showPopup != value)
                {
                    _showPopup = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowPopup)));
                }
            }
        }
    }
}
