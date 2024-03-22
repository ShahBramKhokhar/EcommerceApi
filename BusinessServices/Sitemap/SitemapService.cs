using WebRexErpAPI.Business.Sitemap.Dto;
using WebRexErpAPI.BusinessServices.AzureStorage.Dto;
using WebRexErpAPI.Data.UnitOfWork;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Linq;

namespace WebRexErpAPI.Business.Sitemap
{
    public interface ISitemapService
    {
        Task<bool> GenerateAndUploadSitemap();
    }
    public class SitemapService : ISitemapService, IDisposable
    {

        private readonly IAzureStorage _azureStorage;
        private readonly IUnitOfWork _unitOfWork;
        public SitemapService(
            IAzureStorage azureStorage,
            IUnitOfWork unitOfWork
            )
        {
            _azureStorage = azureStorage;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> GenerateAndUploadSitemap()
        {

            try
            {
                
                //string siteName = "KingSurplusApp";
                //string userName = "$KingSurplusApp";
                //string password = "xZQQCo9ux22fNupXf1Fhw3bWjqPmDj9rciPmLgDrTmeZoHdRuclQpeRks2Ml";
                //string filePath = "/site/wwwroot/public/sitemap.xml";
                


                var items = await _unitOfWork.item.GetAllAsync();
              //  var baseUrl = "https://www.kingsurplus.com/";
                var baseUrl = "";
                var sitemapUrls = new List<SitemapUrlDto>
            {
                new SitemapUrlDto { Loc = baseUrl + "appraisals", Changefreq = "weekly", Priority = 0.8M },
                new SitemapUrlDto { Loc = baseUrl + "inventory", Changefreq = "daily", Priority = 1.0M },
                new SitemapUrlDto { Loc = baseUrl + "inventorydetail", Changefreq = "daily", Priority = 1.0M },
                new SitemapUrlDto { Loc = baseUrl + "clientassetmanagement", Changefreq = "weekly", Priority = 0.8M },
                new SitemapUrlDto { Loc = baseUrl + "findersprogram", Changefreq = "weekly", Priority = 0.7M },
                new SitemapUrlDto { Loc = baseUrl + "aboutus", Changefreq = "monthly", Priority = 0.6M },
                new SitemapUrlDto { Loc = baseUrl + "contactus", Changefreq = "monthly", Priority = 0.6M },
                new SitemapUrlDto { Loc = baseUrl + "recyclingservices", Changefreq = "weekly", Priority = 0.7M },
                new SitemapUrlDto { Loc = baseUrl + "selltous", Changefreq = "weekly", Priority = 0.7M },
                new SitemapUrlDto { Loc = baseUrl + "specializedcontracts", Changefreq = "monthly", Priority = 0.5M }
            };

                var sitemap = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                sitemap.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");


                foreach (var item in items)
                {

                    dynamic itemData = new
                    {
                       // industry = item.IndustryItem,
                        industry = item.IndustryName,
                        category = item.CategoryName,
                        type = item.TypeName,
                        description = item.Description,
                        location = item.Location,
                        assetNo = item.AssetNumber,
                        model = item.Model,
                        manufacturer = item.Menufacturer
                    };

                    string itemDetails = System.Net.WebUtility.UrlEncode(JsonConvert.SerializeObject(itemData));
                    string itemUrl = baseUrl + "InventoryDetail/" + item.Id + "?item=" + itemDetails;
                    sitemapUrls.Add(new SitemapUrlDto { Loc = itemUrl, Changefreq = "daily", Priority = 0.5M });
                }

                List<string> urlList = new List<string>();
                foreach (var url in sitemapUrls)
                {
                    sitemap.AppendLine("<url>");
                    sitemap.AppendLine("<loc>" + url.Loc + "</loc>");
                    sitemap.AppendLine("<changefreq>" + url.Changefreq + "</changefreq>");
                    sitemap.AppendLine("<priority>" + url?.Priority.ToString("F1", CultureInfo.InvariantCulture) + "</priority>");
                    sitemap.AppendLine("</url>");

                    urlList.Add(url.Loc);
                }
                
                await BingWebMasterSubmission(urlList);
                sitemap.AppendLine("</urlset>");
                string sitemapXml = sitemap.ToString();

               // await ReplaceFileContentAsync(siteName, userName, password, filePath, sitemapXml);
            //    await _azureStorage.UploadSitemapToBlobStorageAsync(sitemapXml);

                return true;
            }

            catch (Exception e)
            {

                throw e;
            }

        }

        static async Task ReplaceFileContentAsync(string siteName, string userName, string password, string filePath, string newContent)
        {
            try
            {
                string kuduApiUrl = $"https://{siteName}.scm.azurewebsites.net/api/vfs{filePath}";

                using (HttpClient client = new HttpClient())
                {
                    var byteArray = Encoding.ASCII.GetBytes($"{userName}:{password}");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                    // Get the current XML file content
                    HttpResponseMessage response = await client.GetAsync(kuduApiUrl);
                    response.EnsureSuccessStatusCode();

                    // Replace the content of the XML file with the new XML content
                    //response = await client.PutAsync(kuduApiUrl, new StringContent(newContent, Encoding.UTF8, "application/xml"));
                    //response.EnsureSuccessStatusCode();


                    // Extract the ETag header
                    string currentETag = response.Headers.ETag?.Tag;

                    // Replace the content of the XML file with the new XML content
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, kuduApiUrl);
                    request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    request.Headers.IfMatch.Add(EntityTagHeaderValue.Parse(currentETag));

                    request.Content = new StringContent(newContent, Encoding.UTF8, "application/xml");

                    response = await client.SendAsync(request);

                    Console.WriteLine("XML content replaced successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error replacing file content: {ex.Message}");
            }
        }
   
        public async Task UpdateXmlFileContentAsync(string siteName, string userName, string password, string filePath, string xmlTagName, string newXmlValue)
        {
            try
            {
                string kuduApiUrl = $"https://{siteName}.scm.azurewebsites.net/api/vfs{filePath}";

                using (HttpClient client = new HttpClient())
                {
                    var byteArray = Encoding.ASCII.GetBytes($"{userName}:{password}");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                    // Get the current XML file content
                    HttpResponseMessage response = await client.GetAsync(kuduApiUrl);
                    response.EnsureSuccessStatusCode();

                    string currentXmlContent = await response.Content.ReadAsStringAsync();

                    // Update the XML content
                    XDocument doc = XDocument.Parse(currentXmlContent);
                    XElement elementToUpdate = doc.Descendants(xmlTagName).FirstOrDefault();

                    if (elementToUpdate != null)
                    {
                        elementToUpdate.Value = newXmlValue;

                        // Replace the content of the XML file with the updated XML
                        response = await client.PutAsync(kuduApiUrl, new StringContent(doc.ToString(), Encoding.UTF8, "application/xml"));
                        response.EnsureSuccessStatusCode();

                        Console.WriteLine("XML content updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"XML element with tag '{xmlTagName}' not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating XML content: {ex.Message}");
            }
        }
    


        private static async Task BingWebMasterSubmission(List<string> urls)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                   // string apiKey = "d8fefea2ed9f4c8e9952c73187db775b";
                    string apiKey = "c0f2f630201f4189b876481c55dd97b9";
                    string url = "https://ssl.bing.com/webmaster/api.svc/json/SubmitUrlbatch?apikey=" + apiKey;
                    var requestData = new
                    {
                        siteUrl = "https://kingsurplus.com",
                        urlList = urls
                    };
                    string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);
                    var request = new HttpRequestMessage(HttpMethod.Post, url);
                    request.Content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.SendAsync(request);
                    string responseContent = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception )
            {

             throw;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork?.Dispose();
            }
        }
    }
}
