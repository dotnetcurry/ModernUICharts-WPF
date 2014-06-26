using De.TorstenMandelkow.MetroChart;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WPF_ModernChart_Client.HelperClasses;
using WPF_ModernChart_Client.ModelClasses;
using WPF_ModernChart_Client.ViewModelsRepository;
using WPF_ModernChart_Client.ChartControls;

namespace WPF_ModernChart_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ChartsViewModel vm;

         
        /// <summary>
        /// The Constructor set the DataContext of the Window to an object of 
        /// the ViewModel class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            vm = new ChartsViewModel();
            this.DataContext = vm;
        }

 
        /// <summary>
        /// The method gets executed when the chart is selected from the ComboBox.
        /// The method add the UserControl in the Grid of name grdChartContainer
        /// based upon the chart name selected from the ComboBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstcharttype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            grdChartContainer.Children.Clear();


            var chartType = lstcharttype.SelectedItem as ChartNameStore;

            switch (chartType.Name)
            {
                case "PieChart":
                    PieChartUserControl pie = new PieChartUserControl();
                    pie.DataContext = grdChartContainer.DataContext;
                    grdChartContainer.Children.Add(pie);
                    break;
                case "ClusteredColumnChart":
                    ClusteredColumnChartUserControl ccchart = new ClusteredColumnChartUserControl();
                    ccchart.DataContext = grdChartContainer.DataContext;
                    grdChartContainer.Children.Add(ccchart);
                    break;
                case "ClusteredBarChart":
                    ClusteredBarChartUserControl cbchart = new ClusteredBarChartUserControl();
                    cbchart.DataContext = grdChartContainer.DataContext;
                    grdChartContainer.Children.Add(cbchart);
                    break;
                case "DoughnutChart":
                    DoughnutChartUserControl dnchart = new DoughnutChartUserControl();
                    dnchart.DataContext = grdChartContainer.DataContext;
                    grdChartContainer.Children.Add(dnchart);
                    break;
                case "StackedColumnChart":
                    StackedColumnChartUserControl stcchart = new StackedColumnChartUserControl();
                    stcchart.DataContext = grdChartContainer.DataContext;
                    grdChartContainer.Children.Add(stcchart);
                    break;
                case "StackedBarChart":
                    StackedBarChartUserControl stbchart = new StackedBarChartUserControl();
                    stbchart.DataContext = grdChartContainer.DataContext;
                    grdChartContainer.Children.Add(stbchart);
                    break;
                case "RadialGaugeChart":
                    RadialGaugeChartUserControl rgchart = new RadialGaugeChartUserControl();
                    rgchart.DataContext = grdChartContainer.DataContext;
                    grdChartContainer.Children.Add(rgchart);
                    break;
            }

        }
    }
}
