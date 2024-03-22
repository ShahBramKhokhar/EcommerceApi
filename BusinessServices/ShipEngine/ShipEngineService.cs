using WebRexErpAPI.BusinessServices.ShipEngine.Dto;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace WebRexErpAPI.BusinessServices.ShipEngine
{
    public class ShipEngineService : IShipEngineService, IDisposable
    {
        private readonly string _ShipEnginebaseUrl;
        private readonly string _ShipEngineProductKey;
        private readonly RestClient _client;
        public ShipEngineService(IConfiguration configuration)
        {
            _ShipEnginebaseUrl = configuration.GetValue<string>("ShipEngineBaseUrl");
            _ShipEngineProductKey = configuration.GetValue<string>("ShipEngineProductKey");
            _client = new RestClient(_ShipEnginebaseUrl);
        }

        public async Task<string> GetRates(SERequest input)
        {

            var request = new RestRequest("/v1/rates", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Api-Key", _ShipEngineProductKey);
            string packagesJson = "";
            foreach (var package in input.packages)
            {
                string packageJson = @"{
                ""dimensions"": {
                    ""width"": " + package.dimensions.width + @",
                    ""height"": " + package.dimensions.height + @",
                    ""length"": " + package.dimensions.length + @",
                    ""unit"": ""inch""
                },
                ""weight"": {   
                    ""value"": " + package.weight.value + @",
                    ""unit"": ""pound""
                },
                ""quantity"": " + package.quantity + @"
                 }";
                packagesJson += packageJson + ",";
            }
            if (input.packages.Count > 0)
            {
                packagesJson = packagesJson.Remove(packagesJson.Length - 1);
            }


            #region body
            var body = @"{" + "\n" +
                 @"  ""rate_options"": {" + "\n" +
                 @"    ""carrier_ids"": [" + "\n" +
                 @"      """ + input.CarrierId + @"""" + "\n" +
                 @"    ]" + "\n" +
                 @"  }," + "\n" +
                 @"  ""shipment"": {" + "\n" +
                 @"    ""ship_to"": {" + "\n" +
                 @"      ""name"":  """ + input.ToAddress.name + @"""" + "," + "\n" +
                 @"      ""phone"":  """ + input.ToAddress.phone + @"""" + "," + "\n" +
                 @"      ""address_line1"":  """ + input.ToAddress.address_line1 + @"""" + "," + "\n" +
                 @"      ""city_locality"":  """ + input.ToAddress.city_locality + @"""" + "," + "\n" +
                 @"      ""state_province"": """ + input.ToAddress.state_province + @"""" + "," + "\n" +
                 @"      ""postal_code"":  """ + input.ToAddress.postal_code + @"""" + "," + "\n" +
                 @"      ""country_code"":  """ + input.ToAddress.country_code + @"""" + "," + "\n" +
                 @"    }," + "\n" +
                 @"    ""ship_from"": {" + "\n" +
                 @"      ""company_name"":  """ + input.FromAddress.company_name + @"""" + "," + "\n" +
                 @"      ""name"":  """ + input.FromAddress.name + @"""" + "," + "\n" +
                 @"      ""phone"":  """ + input.FromAddress.phone + @"""" + "," + "\n" +
                 @"      ""address_line1"":  """ + input.FromAddress.address_line1 + @"""" + "," + "\n" +
                 @"      ""address_line2"": """ + input.FromAddress.address_line2 + @"""" + "," + "\n" +
                 @"      ""city_locality"": """ + input.FromAddress.city_locality + @"""" + "," + "\n" +
                 @"      ""state_province"": """ + input.FromAddress.state_province + @"""" + "," + "\n" +
                 @"      ""postal_code"":  """ + input.FromAddress.postal_code + @"""" + "," + "\n" +
                 @"      ""country_code"": """ + input.FromAddress.country_code + @"""" + "," + "\n" +
                 @"    }," + @"  ""packages"": [" + packagesJson + @"] "+   
                 @"  }" +
                 @"}";


            #endregion



            request.AddStringBody(body, DataFormat.Json);
            var response = await _client.ExecuteAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"ShipEngine API returned {response.StatusCode}: {response.Content}");
            }
            var jsonResponse = JsonConvert.SerializeObject(response.Content);
            return jsonResponse;
        }

        public async Task<string> GetRatesTLT(SELTLRequest input)
        {
            string valuesOptions = "";
            int? lastNumber = input.shipmentRequest?.optionValues?.Count();

            valuesOptions = valuesOptions + "\"options\":[";
            int i = 1;
            foreach (var optionVal in input.shipmentRequest.optionValues)
            {
                valuesOptions = valuesOptions + "{" +
                                              @"""code"" : """ + optionVal.code + @"""," +
                                              @"""attributes"" : {" +
                                              @"""name"" : """ + optionVal.shipEngineAttributes.name + @"""," +
                                              @"""phone"" : """ + optionVal.shipEngineAttributes.phone + @"""" +
                                                       @"}";

                if (i != lastNumber)
                {
                    valuesOptions = valuesOptions + "},";
                }
                else
                {
                    valuesOptions = valuesOptions + "}";
                }
                i++;
            }
            valuesOptions = valuesOptions + "],";

            var options = new RestClientOptions("https://api.shipengine.com");
            var client = new RestClient(options);

            var request = new RestRequest("/v-beta/ltl/quotes/" + input.CarrierId, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("API-Key", _ShipEngineProductKey);



            string packagesJson = "";
            foreach (var package in input.shipmentRequest.packages)
            {
                string packageJson = @"{
                ""code"": """ + package.package_code + @""",
                ""freight_class"": " + package.freight_class + @",
                ""description"": """ + package.description + @""",
                ""dimensions"": {
                    ""width"": " + package.dimensions.width + @",
                    ""height"": " + package.dimensions.height + @",
                    ""length"": " + package.dimensions.length + @",
                    ""unit"": """ + package.dimensions.unit + @"""
                },
                ""weight"": {   
                    ""value"": " + package.weight.value + @",
                    ""unit"": """ + package.weight.unit + @"""
                },
                ""quantity"": " + package.quantity + @",
                ""stackable"": false,
                ""hazardous_materials"": false
                 }";
                packagesJson += packageJson + ",";
            }
            if (input.shipmentRequest.packages.Count > 0)
            {
                // Remove the last comma from the packages array 
                packagesJson = packagesJson.Remove(packagesJson.Length - 1);
            }


            #region body
            var body = @"{
                " + "\n" +
                            @"  ""shipment"": {
                " + "\n" +
                            @"        ""service_code"": """ + input.shipmentRequest.service_code + @""",
                " + "\n" +
                            @"        ""pickup_date"":  """ + input.shipmentRequest.pickup_date + @""",
                " + "\n" +
                            @"  ""packages"": [" + packagesJson + @"],
                " + valuesOptions +

                            @"        ""ship_to"": {


                " + "\n" +
                            @"            ""contact"": {
                " + "\n" +
                            @"                ""name"": """ + input.shipmentRequest.to_address.shipeEngineContact.name + @""",
                " + "\n" +
                            @"                ""phone_number"":  """ + input.shipmentRequest.to_address.shipeEngineContact.phone_number + @""",
                " + "\n" +
                            @"                ""email"": """ + input.shipmentRequest.to_address.shipeEngineContact.email + @"""
                " + "\n" +
                            @"            },
                " + "\n" +
                            @"            ""address"": {
                " + "\n" +
                            @"            ""company_name"": """ + input.shipmentRequest.to_address.name + @""",
                " + "\n" +
                            @"            ""address_line1"": """ + input.shipmentRequest.to_address.address_line1 + @""",
                " + "\n" +
                            @"            ""city_locality"": """ + input.shipmentRequest.to_address.city_locality + @""",
                " + "\n" +
                            @"            ""state_province"": """ + input.shipmentRequest.to_address.state_province + @""",
                " + "\n" +
                            @"            ""postal_code"":  """ + input.shipmentRequest.to_address.postal_code + @""",
                " + "\n" +
                            @"            ""country_code"": """ + input.shipmentRequest.to_address.country_code + @"""
                " + "\n" +
                            @"            }
                " + "\n" +
                            @"        },
                " + "\n" +


                                   @"        ""ship_from"": {


                " + "\n" +
                            @"            ""contact"": {
                " + "\n" +
                            @"                ""name"": """ + input.shipmentRequest.from_address.shipeEngineContact.name + @""",
                " + "\n" +
                            @"                ""phone_number"":  """ + input.shipmentRequest.from_address.shipeEngineContact.phone_number + @""",
                " + "\n" +
                            @"                ""email"": """ + input.shipmentRequest.from_address.shipeEngineContact.email + @"""
                " + "\n" +
                            @"            },
                " + "\n" +
                            @"            ""address"": {
                " + "\n" +
                            @"            ""company_name"": """ + input.shipmentRequest.from_address.name + @""",
                " + "\n" +
                            @"            ""address_line1"": """ + input.shipmentRequest.from_address.address_line1 + @""",
                " + "\n" +
                            @"            ""city_locality"": """ + input.shipmentRequest.from_address.city_locality + @""",
                " + "\n" +
                            @"            ""state_province"": """ + input.shipmentRequest.from_address.state_province + @""",
                " + "\n" +
                            @"            ""postal_code"":  """ + input.shipmentRequest.from_address.postal_code + @""",
                " + "\n" +
                            @"            ""country_code"": """ + input.shipmentRequest.from_address.country_code + @"""
                " + "\n" +
                            @"            }
                " + "\n" +
                            @"        },
                " + "\n" +

                            @"            ""contact"": {
                " + "\n" +
                            @"                ""name"": """ + input.shipmentRequest.to_address.shipeEngineContact.name + @""",
                " + "\n" +
                            @"                ""phone_number"":  """ + input.shipmentRequest.to_address.shipeEngineContact.phone_number + @""",
                " + "\n" +
                            @"                ""email"": """ + input.shipmentRequest.to_address.shipeEngineContact.email + @"""
                " + "\n" +
                            @"            },
                " + "\n" +
                            @"            ""address"": {
                " + "\n" +
                            @"            ""company_name"": """ + input.shipmentRequest.to_address.name + @""",
                " + "\n" +
                            @"            ""address_line1"": """ + input.shipmentRequest.to_address.address_line1 + @""",
                " + "\n" +
                            @"            ""city_locality"": """ + input.shipmentRequest.to_address.city_locality + @""",
                " + "\n" +
                            @"            ""state_province"": """ + input.shipmentRequest.to_address.state_province + @""",
                " + "\n" +
                            @"            ""postal_code"":  """ + input.shipmentRequest.to_address.postal_code + @""",
                " + "\n" +
                            @"            ""country_code"": """ + input.shipmentRequest.to_address.country_code + @"""
                " + "\n" +
                            @"            }
                " + "\n" +
                            @"        ,
                " + "\n" +
                            @"        ""bill_to"": {
                " + "\n" +
                            @"            ""type"": """ + input.shipmentRequest.bill.type + @""",
                " + "\n" +
                            @"            ""payment_terms"": """ + input.shipmentRequest.bill.payment_terms + @""",
                " + "\n" +
                            @"            ""account"": """ + input.shipmentRequest.bill.account + @""",
                " + "\n" +
                            @"            ""address"": {
                " + "\n" +
                            @"            ""company_name"": """ + input.shipmentRequest.bill.address.company_name + @""",
                " + "\n" +
                            @"            ""address_line1"":  """ + input.shipmentRequest.bill.address.address_line1 + @""",
                " + "\n" +
                            @"            ""city_locality"": """ + input.shipmentRequest.bill.address.city_locality + @""",
                " + "\n" +
                            @"            ""state_province"": """ + input.shipmentRequest.bill.address.state_province + @""",
                " + "\n" +
                            @"            ""postal_code"": """ + input.shipmentRequest.bill.address.postal_code + @""",
                " + "\n" +
                            @"            ""country_code"": """ + input.shipmentRequest.bill.address.country_code + @"""
                " + "\n" +
                            @"            },
                " + "\n" +
                            @"            ""contact"": {
                " + "\n" +
                            @"                ""name"": """ + input.shipmentRequest.bill.contact.name + @""",
                " + "\n" +
                            @"                ""phone_number"": """ + input.shipmentRequest.bill.contact.phone_number.Trim() + @""",
                " + "\n" +
                            @"                ""email"":  """ + input.shipmentRequest.bill.contact.email + @"""
                " + "\n" +
                            @"            }
                " + "\n" +
                            @"        },
                " + "\n" +
                            @"        ""requested_by"": {
                " + "\n" +
                            @"            ""company_name"":  """ + input.shipmentRequest.shipeEngineRequestedBy.company_name + @""",
                " + "\n" +
                            @"            ""contact"": {
                " + "\n" +
                            @"            ""name"": """ + input.shipmentRequest.shipeEngineRequestedBy.contact.name + @""",
                " + "\n" +
                            @"            ""phone_number"": """ + input.shipmentRequest.shipeEngineRequestedBy.contact.phone_number + @""",
                " + "\n" +
                            @"            ""email"": """ + input.shipmentRequest.shipeEngineRequestedBy.contact.email + @"""
                " + "\n" +
                            @"            }
                " + "\n" +
                            @"        }
                " + "\n" +
                            @"    },
                " + "\n" +
                            @"    ""shipment_measurements"": {
                " + "\n" +
                            @"        ""total_linear_length"": {
                " + "\n" +
                            @"        ""value"": " + input.measurements.total_linear_length.value + @",
                " + "\n" +
                            @"        ""unit"": """ + input.measurements.total_linear_length.unit + @"""
                " + "\n" +
                            @"        },
                " + "\n" +
                            @"        ""total_width"": {
                " + "\n" +
                            @"        ""value"":" + input.measurements.total_width.value + @",
                " + "\n" +
                            @"        ""unit"":""" + input.measurements.total_width.unit + @"""
                " + "\n" +
                            @"        },
                " + "\n" +
                            @"        ""total_height"": {
                " + "\n" +
                         @"          ""value"":" + input.measurements.total_height.value + @",
                " + "\n" +
                            @"        ""unit"":""" + input.measurements.total_height.unit + @"""
                " + "\n" +
                            @"        },
                " + "\n" +
                            @"        ""total_weight"": {
                " + "\n" +
                         @"          ""value"":" + input.measurements.total_weight.value + @",
                " + "\n" +
                            @"        ""unit"":""" + input.measurements.total_weight.unit + @"""
                " + "\n" +
                            @"        }
                " + "\n" +
                            @"    }
                " + "\n" +
                            @"}";

            #endregion

            request.AddStringBody(body, DataFormat.Json);
            var response = await client.ExecuteAsync(request);

            var CarriesRes = JsonConvert.SerializeObject(response.Content);

            return CarriesRes;


        }

        public async Task<List<SECarrier>> ListCarriersAsync()
        {

            var request = new RestRequest("/v1/carriers", Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Api-Key", _ShipEngineProductKey);
            var response = await _client.ExecuteAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"ShipEngine API returned {response.StatusCode}: {response.Content}");
            }
            var res = new List<SECarrier>();
            CarriersResponse carriersResponse = JsonConvert.DeserializeObject<CarriersResponse>(response.Content);
            res = carriersResponse.Carriers;
            return res;
        }


        public async Task<SECarrierInfo> ODFL()
        {

            var options = new RestClientOptions(_ShipEnginebaseUrl);

            var client = new RestClient(options);
            var request = new RestRequest("/v-beta/ltl/connections/odfl", Method.Post);
            request.AddHeader("API-Key", _ShipEngineProductKey);
            request.AddHeader("Content-Type", "application/json");
            var body = @"{" + "\n" +
            @"  ""credentials"": {" + "\n" +
            @"     ""username"": ""KngSurplus""," + "\n" +
            @"     ""password"": ""CAzydV0Qk18fOp@Z""," + "\n" +
            @"     ""account_number"": ""12823507""" + "\n" +
            @"  }" + "\n" +
            @"}" + "\n" +
            @"";
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.ExecuteAsync(request);

            var carrierInfo = JsonConvert.DeserializeObject<SECarrierInfo>(response.Content);

            return carrierInfo;
        }

        public async Task<SECarrierInfo> SAIA()
        {

            var options = new RestClientOptions(_ShipEnginebaseUrl);

            var client = new RestClient(options);
            var request = new RestRequest("/v-beta/ltl/connections/saia", Method.Post);
            request.AddHeader("API-Key", _ShipEngineProductKey);
            request.AddHeader("Content-Type", "application/json");
            var body = @"{" + "\n" +
            @"  ""credentials"": {" + "\n" +
            @"     ""username"": ""KingSurplusCo.""," + "\n" +
            @"     ""password"": ""Shipping01!""," + "\n" +
            @"     ""account_number"": ""998593""" + "\n" +
            @"  }" + "\n" +
            @"}" + "\n" +
            @"";
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.ExecuteAsync(request);

            var carrierInfo = JsonConvert.DeserializeObject<SECarrierInfo>(response.Content);

            return carrierInfo;
        }

        public async Task<SECarrierInfo> XPOID()
        {

            var options = new RestClientOptions(_ShipEnginebaseUrl);

            var client = new RestClient(options);
            var request = new RestRequest("/v-beta/ltl/connections/XPO", Method.Post);
            request.AddHeader("API-Key", _ShipEngineProductKey);
            request.AddHeader("Content-Type", "application/json");
            var body = @"{" + "\n" +
            @"  ""credentials"": {" + "\n" +
            @"     ""username"": ""Shipping@kingsurplus.com""," + "\n" +
            @"     ""password"": ""Shipping01""," + "\n" +
            @"     ""key"": ""KIAIY001900""," + "\n" +
            @"     ""account_number"": ""707359499""" + "\n" +
            @"  }" + "\n" +
            @"}" + "\n" +
            @"";
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.ExecuteAsync(request);

            var carrierInfo = JsonConvert.DeserializeObject<SECarrierInfo>(response.Content);

            return carrierInfo;
        }
        //public async Task<List<SEAddressVerificationResponse>> ValidateAddress(SERequestAddress input)
        //{
        //    var options = new RestClientOptions("https://api.shipengine.com");
        //    var client = new RestClient(options);
        //    var request = new RestRequest("/v1/addresses/validate", Method.Post);
        //    request.AddHeader("API-Key", "TEST_38q5dvW68gTZCNz2+fiZ7aoVWHNFKNgptNw+UFmSTVo");
        //    request.AddHeader("Content-Type", "application/json");
        //    var body = @"[" + "\n" +
        //    @"  {" + "\n" +
        //    @"    ""address_line1"": """ + input.address_line1 + @"""," + "\n" +
        //    @"    ""city_locality"":""" + input.city_locality + @"""," + "\n" +
        //    @"    ""state_province"":""" + input.state_province + @"""," + "\n" +
        //    @"    ""postal_code"":""" + input.postal_code + @"""," + "\n" +
        //    @"    ""country_code"": """ + input.country_code + @"""," + "\n" +
        //    @"  }" + "\n" +
        //    @"]";


        //    request.AddStringBody(body, DataFormat.Json);
        //    RestResponse response = await client.ExecuteAsync(request);

        //    var result = new List<SEAddressVerificationResponse>();
        //    result = JsonConvert.DeserializeObject<List<SEAddressVerificationResponse>>(response.Content);

        //    return result;
        //}

        public async Task<string> TLTCarrierSupported(string carrierId)
        {
            var options = new RestClientOptions(_ShipEnginebaseUrl);
            var client = new RestClient(options);
            var request = new RestRequest("/v-beta/ltl/carriers/" + carrierId, Method.Get);
            request.AddHeader("API-Key", _ShipEngineProductKey);
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"ShipEngine API returned {response.StatusCode}: {response.Content}");
            }
            return JsonConvert.SerializeObject(response.Content);


        }
        public async Task<string> ValidateAddressAsync(SEAddressDTO input)
        {

            var options = new RestClientOptions(_ShipEnginebaseUrl);
            var client = new RestClient(options);
            var request = new RestRequest("/v1/addresses/validate", Method.Post);

            request.AddHeader("Host", "api.shipengine.com");
            request.AddHeader("API-Key", _ShipEngineProductKey);
            request.AddHeader("Content-Type", "application/json");
            var body = @"[" + "\n" +
            @"  {" + "\n" +
            @"    ""address_line1"": """ + input.address_line1 + @"""," + "\n" +
            @"    ""city_locality"":""" + input.city_locality + @"""," + "\n" +
            @"    ""state_province"":""" + input.state_province + @"""," + "\n" +
            @"    ""postal_code"":""" + input.postal_code + @"""," + "\n" +
            @"    ""country_code"": """ + input.country_code + @"""," + "\n" +
            @"  }" + "\n" +
            @"]";


            request.AddStringBody(body, DataFormat.Json);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            var response = await client.ExecuteAsync(request);
            return response.Content;
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
                // Dispose managed resources
                _client?.Dispose();
                
            }
        }
    }
}
