using DynamicData;
using WebRexErpAPI.BusinessServices.ShipEngine.Dto;
using RestSharp;
using System.Text.RegularExpressions;

namespace WebRexErpAPI.Common
{
    public static class helper
    {
       

       public static string RemoveInvalidCharacters(string input)
        {
            // Use a regular expression to replace invalid characters with an empty string
            return Regex.Replace(input, @"[^\u0009\u000a\u000d\u0020-\uD7FF\uE000-\uFFFD]", "");
        }
        public static async Task<RestResponse> QuickBasePostRequestAsync(string body)
        {
            var request = new RestRequest
            {
                Method = Method.Post
            };

            using (var client = new RestClient("https://api.quickbase.com/v1/records/query"))
            {
                request.AddHeader("Authorization", "QB-USER-TOKEN ");
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("QB-Realm-Hostname", "kingsurplus-9736.quickbase.com");
                request.AddParameter("application/json", body, ParameterType.RequestBody);

                try
                {
                    var response = await client.ExecuteAsync(request);
                    return response;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception during QuickBase request: {ex.Message}");
                    return null; 
                }
            }
        }

        public static async Task<RestResponse> QuickBasePostInsertRequestAsync(string body)
        {
            var request = new RestRequest
            {
                Method = Method.Post
            };

            using (var client = new RestClient("https://api.quickbase.com/v1/records"))
            {
                request.AddHeader("Authorization", "QB-USER-TOKEN ");
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("QB-Realm-Hostname", "kingsurplus-9736.quickbase.com");
                request.AddParameter("application/json", body, ParameterType.RequestBody);

                try
                {
                    var response = await client.ExecuteAsync(request);
                    return response;
                }
                catch (Exception ex)
                {
                    // Handle the exception (log it, throw a custom exception, etc.)
                    Console.WriteLine($"Exception during QuickBase insert request: {ex.Message}");
                    return null; // Or throw a custom exception
                }
            }
        }


        //public static RestResponse QuickBasePostInsertRequest(string body)
        //{
        //    var request = new RestRequest();
        //    request.Method = Method.Post;
        //    var client = new RestClient("https://api.quickbase.com/v1/records");
        //    request.AddHeader("Authorization", "QB-USER-TOKEN ");
        //    request.AddHeader("Content-Type", "application/json");
        //    request.AddHeader("QB-Realm-Hostname", "kingsurplus-9736.quickbase.com");
        //    request.AddParameter("application/json", body, ParameterType.RequestBody);
        //    var response = client.Execute(request);
        //    client.Dispose();

        //    return response;
        //}

        public static List<SECarrierInfo> getFrightCarriersIDs()
        {

            var IdsList = new List<SECarrierInfo>();

               var ODfl = new SECarrierInfo();
               ODfl.Name = "ODFL";
               ODfl.carrier_id = "";
               IdsList.Add(ODfl);

               var SAIA = new SECarrierInfo();
               SAIA.Name = "SAIA";
               SAIA.carrier_id = "";
               IdsList.Add(SAIA);


            var SEFL = new SECarrierInfo();
            SEFL.Name = "SEFL";
            SEFL.carrier_id = "";
            IdsList.Add(SEFL);

            return IdsList;

        } 

    }
}
