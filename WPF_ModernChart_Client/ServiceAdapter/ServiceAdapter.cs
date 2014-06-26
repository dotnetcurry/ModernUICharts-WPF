
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WPF_ModernChart_Client.ModelClasses;

namespace WPF_ModernChart_Client.ServiceAdapter
{
    /// <summary>
    /// The class used to make call to WEB API and get the sales information
    /// </summary>
    public class ProxyAdapter
    {
        ObservableCollection<SalesTerritory> _SalesData;

        public async Task<ObservableCollection<SalesTerritory>> GetSalesInformation()
        {
            using (var webClient = new HttpClient())
            {
                webClient.BaseAddress = new Uri("http://localhost:6043/");
                webClient.DefaultRequestHeaders.Accept.Clear();
                webClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage resp = await webClient.GetAsync("api/SalesTerritories");

                if (resp.IsSuccessStatusCode)
                {
                    _SalesData = await resp.Content.ReadAsAsync<ObservableCollection<SalesTerritory>>();
                }
            }
            return _SalesData;
        }
    }
}
