
namespace WPF_ModernChart_Client.ModelClasses
{
    /// <summary>
    /// The entity class for Sales information
    /// </summary>
    public partial class SalesTerritory
    {
        public int TerritoryID { get; set; }
        public string Name { get; set; }
        public string CountryRegionCode { get; set; }
        public string Group { get; set; }
        public decimal SalesYTD { get; set; }
        public decimal SalesLastYear { get; set; }
        public decimal CostYTD { get; set; }
        public decimal CostLastYear { get; set; }
        public System.Guid rowguid { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }

    /// <summary>
    /// The class which is used to provide the necessary information to draw chart
    /// on the chart axis.
    /// </summary>
    public partial class SalesInfo
    {
        public string Name { get; set; }
        public decimal Sales { get; set; }
    }

    /// <summary>
    /// The class used to represent information for the CountryRegioCode
    /// e.g. US, GB etc.
    /// </summary>
    public class CountryRegionCode
    {
        public string CountryRegion { get; set; }
    }
}
