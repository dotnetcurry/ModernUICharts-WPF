using System.Collections.ObjectModel;
using System.Linq;
using WPF_ModernChart_Client.HelperClasses;

using WPF_ModernChart_Client.ModelClasses;
using WPF_ModernChart_Client.ServiceAdapter;
using System.ComponentModel;
using WPF_ModernChart_Client.Commands;

namespace WPF_ModernChart_Client.ViewModelsRepository
{
    /// <summary>
    /// The below class contains:
    /// 1. The ChartInfo collection property to display the available charts in the combobox.
    /// 2. The SalesData collection property to use as a source for drawing charts in UI.
    /// 3. The CountryRegion collection property to use as a source for the CountryRegion code for UI.
    /// 4. The CountryRegionName property to use as a filter when the UI changes the CountryRegion to draw chart.
    /// 5. The IsRadioButtonEnabled property for enabling and disabling the RadioButton on UI
    /// 6. The SalesDetailsYTDCommand and SalesDetailsLastYearCommand command object to execute methods
    /// when action is taken on UI
    /// 7. The GetYTDSalesDataByCountryRegion() and GetLastYearSalesDataByCountryRegion() method are
    /// used get the YTD and LastYEar sales data respectively based upon the CountryRegionCode selected on UI.
    /// 8. The GetCountryRegionCodeGroup() method is used to provide information  about available CountryRegion code.
    /// </summary>
    public class ChartsViewModel : INotifyPropertyChanged
    {
        ProxyAdapter adapter;

        ObservableCollection<ChartNameStore> _ChartsInfo;

        public ObservableCollection<ChartNameStore> ChartsInfo
        {
            get { return _ChartsInfo; }
            set { _ChartsInfo = value; }
        }

        ObservableCollection<SalesInfo> _SalesData;

        public ObservableCollection<SalesInfo> SalesData
        {
            get { return _SalesData; }
            set { _SalesData = value; }
        }

 


        ObservableCollection<CountryRegionCode> _CountryRegion;

        public ObservableCollection<CountryRegionCode> CountryRegion
        {
            get { return _CountryRegion; }
            set { _CountryRegion = value; }
        }


        CountryRegionCode _CountryRegionName;

        public CountryRegionCode CountryRegionName
        {
            get { return _CountryRegionName; }
            set
            {
                _CountryRegionName = value;
                IsRadioButtonEnabled = true;
                onPropertyChanged("CountryRegionName");
            }
        }


        bool _IsRadioButtonEnabled = false;

        public bool IsRadioButtonEnabled
        {
            get { return _IsRadioButtonEnabled; }
            set 
            {
                _IsRadioButtonEnabled = value;
                onPropertyChanged("IsRadioButtonEnabled");
            }
        }

         
        ChartTypeHelper helper;

        public ChartsViewModel()
        {
            adapter = new ProxyAdapter(); 
            helper = new ChartTypeHelper();

            ChartsInfo = helper.GetChartNames();

            SalesData = new ObservableCollection<SalesInfo>();
            CountryRegion = new ObservableCollection<CountryRegionCode>(); 
            
            GetCountryRegionCodeGroup();


            SalesDetailsYTDCommand = new RelayCommand(GetYTDSalesDataByCountryRegion);
            SalesDetailsLastYearCommand = new RelayCommand(GetLastYearSalesDataByCountryRegion);
        }

        public RelayCommand SalesDetailsYTDCommand { get; set; }
        public RelayCommand SalesDetailsLastYearCommand { get; set; }

        /// <summary>
        /// Method to get Sales Data for YTD based upon the CountryRegion code
        /// </summary>
        public async void GetYTDSalesDataByCountryRegion()
        {
            if (CountryRegionName!=null)
            { 
            SalesData.Clear();
            var Res = (from sale in await adapter.GetSalesInformation()
                       where sale.CountryRegionCode == CountryRegionName.CountryRegion
                       select new { 
                          Name = sale.Name,
                          SaleYTD = sale.SalesYTD
                       }).ToList();

             foreach (var item in Res)
             {
                 SalesData.Add(new SalesInfo() { Name = item.Name, Sales = item.SaleYTD });
             }
            }
        }


        /// <summary>
        /// Method to get Sales Data for Last Year based upon the CountryRegion code
        /// </summary>
        public async void GetLastYearSalesDataByCountryRegion()
        {
            if (CountryRegionName != null)
            { 
            SalesData.Clear();
            var Res = (from sale in await adapter.GetSalesInformation()
                       where sale.CountryRegionCode == CountryRegionName.CountryRegion
                       select new
                       {
                           Name = sale.Name,
                           SalesLastYear = sale.SalesLastYear
                       }).ToList();

            foreach (var item in Res)
            {
                SalesData.Add(new SalesInfo() { Name = item.Name, Sales = item.SalesLastYear });
            }
            }
        }

        /// <summary>
        /// Method to Get the Country-Region Code
        /// </summary>
        public async void GetCountryRegionCodeGroup()
        {

            var Res = (from cr in await adapter.GetSalesInformation()
                      group cr by cr.CountryRegionCode into crg
                      select new CountryRegionCode()
                      {
                          CountryRegion = crg.Key
                      });
            foreach (var item in Res.ToList())
            {
                CountryRegion.Add(item);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void onPropertyChanged(string pName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,new PropertyChangedEventArgs(pName));
            }
        }
    }
}
