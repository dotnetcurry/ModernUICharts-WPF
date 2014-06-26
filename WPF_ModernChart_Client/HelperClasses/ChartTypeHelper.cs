using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;

namespace WPF_ModernChart_Client.HelperClasses
{
    /// <summary>
    /// The class used to store the Chart Name and its number
    /// from the xml file. 
    /// </summary>
    public class ChartNameStore
    {
        public int Number { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// The class used to load Chart types form the XML file.
    /// </summary>
    public class ChartTypeHelper
    {
        ObservableCollection<ChartNameStore> _ChartsNames;

        /// <summary>
        /// The Method Load the Xml file and return chart names.
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<ChartNameStore> GetChartNames()
        {
            _ChartsNames = new ObservableCollection<ChartNameStore>(); 

            XDocument xDoc = XDocument.Load("ChartNames.xml");

            var Charts = from c in xDoc.Descendants("ChartTypes").Elements("Chart")
                         select c;

            foreach (var item in Charts)
            {
                _ChartsNames.Add(new ChartNameStore()
                { 
                 Name =  item.Attribute("Name").Value,
                 Number =Convert.ToInt32(item.Attribute("Number").Value)
                });
            }

            return _ChartsNames;

        }
    }
}
