using WebRexErpAPI.Business.ChatGPT;
using WebRexErpAPI.BusinessServices.ChatGPT.Dto;
using WebRexErpAPI.Common;
using WebRexErpAPI.Data.UnitOfWork;
using WebRexErpAPI.Helper;
using WebRexErpAPI.Models;
using WebRexErpAPI.Services.Account.Dto;
using WebRexErpAPI.Services.Common;
using WebRexErpAPI.Services.Pagednation;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace WebRexErpAPI.Services.QuickBase
{
    public class QuickBaseService : IQuickBaseService,IDisposable
    {
        #region private variable
        private readonly IUnitOfWork _unitOfWork;
        private readonly string selectQB = "\"select\":[3,115,116,406,427,424,467,461," +
            "316,426,457, 311, 337, 133,134,135,350,340,341,413,351,302,200,11, 69, " +
            "71, 208, 176,387,386,189, 178, 240, 113, 114, 56, 57, 116, 58, 59, 60, 61, 63, " +
            "66, 179, 77, 78, 79, 80,407,81, 82, 300, 355, 356, 357, 370, 371, 369, 342,589, " +
            "343, 344, 345, 368,309,310,321,409,240,173,314,223,175,177,315,69.507,505,506,518,521,523,524,105,255,561,569],\r\n";
        #endregion

        private readonly IChatGPTService _chatGPTService;
        public QuickBaseService(IUnitOfWork unitOfWork, IChatGPTService ChatGPTService)
        {
             _unitOfWork = unitOfWork;
             _chatGPTService = ChatGPTService;
        }
        public async Task<CustomerQBDto> FindCustomerQBBusiness(string businessName)
        {

            #region body request
            var body = @"{
                " + "\n" +
                @"  ""from"": ""brx4xayqd"",
                " + "\n" +
                @"  ""select"": [ 51,6,107,3,101],
                ""where"":""{'51'.EX.'" + businessName + @"'}"",
                " + "\n" +
                @"  ""options"": {
                " + "\n" +
                @"    ""skip"": 0,
                " + "\n" +
                @"    ""top"": 0,
                " + "\n" +
                @"    ""compareWithAppLocalTime"": false
                " + "\n" +
                @"  }
                " + "\n" +
            @"}";

            #endregion
            var response = await helper.QuickBasePostRequestAsync(body);

            if (response.StatusCode == HttpStatusCode.OK && response.Content != null)
            {
                   var customers = JsonConvert.DeserializeObject<QuickBaseDto>(response.Content);
                   var customDto = new CustomerQBDto();
                if (customers.data.Count() > 0 && customers.data[0]["3"] != null && customers.data[0]["3"].value != null)
                {
                    customDto.CustomerId = CustomExtension.IsNumberAndNotNull(customers.data[0]["107"]?.value);
                    customDto.QBId = CustomExtension.IsNumberAndNotNull(customers.data[0]["3"]?.value);
                    customDto.CustomerName = CustomExtension.StringNullHandel(customers.data[0]["51"]?.value);

                }
                return customDto;
            }
            else
            {
                return null;
            }

        }
        public async Task<SO_ItemQBDto> CreateQBSOItem(SO_ItemQBDto input)
        {
            var body = "";

            if(input.ShippingType == "postal")
            {
                #region body request
                body = @"{""to"":""br5qxj47n"",
                         ""data"":[{
                          ""6"":{""value"":" + input.ItemRecordId + @"},
                          ""10"":{""value"":" + input.OrderId + @"},
                          ""65"":{ ""value"":" + input.Packing + @"},
                          ""66"":{ ""value"":" + input.Postal + @"},
                          ""15"":{""value"":" + input.SelectedQty + @"}}],
                          ""fieldsToReturn"":[3,6,15]}";

                #endregion
            }
            else
            {
                #region body request
                body = @"{""to"":""br5qxj47n"",
                         ""data"":[{
                          ""6"":{""value"":" + input.ItemRecordId + @"},
                          ""10"":{""value"":" + input.OrderId + @"},
                          ""65"":{ ""value"":" + input.Packing + @"},
                          ""66"":{ ""value"":" + input.Freight + @"},
                          ""15"":{""value"":" + input.SelectedQty + @"}}],
                          ""fieldsToReturn"":[3,6,15]}";

                #endregion
            }

            var response = await helper.QuickBasePostInsertRequestAsync(body);
            if (response.Content != null)
            {
                var typesResult = JsonConvert.DeserializeObject<QuickBaseDto>(response.Content);
                foreach (var item in typesResult.data)
                {
                    input.QBId = CustomExtension.IsNumberAndNotNull(item["3"]?.value);
                }
            }

            return input;

        }
        public async Task<ContactQBDto> CreateQBContact(ContactQBDto input)
        {
            var body = "";
            if(input.QBId > 0)
            {
                #region body request
                body = @"{""to"":""br4gbm28k"",
                  ""data"":[
                 {""3"":{""value"":""" + input.QBId + @"""},
                  ""12"":{""value"":""" + input.CustomerLocation.Address + @"""},
                  ""13"":{""value"":""" + input.CustomerLocation.Address2 + @"""},
                  ""14"":{""value"":""" + input.CustomerLocation.City + @"""},
                  ""15"":{""value"":""" + input.CustomerLocation.State + @"""},
                  ""16"":{""value"":""" + input.CustomerLocation.PostalCode + @"""},
                  ""17"":{""value"":""" + input.CustomerLocation.Country + @"""}
  
                }],
                  ""fieldsToReturn"":[3]}";
                #endregion
            }
            else
            {
                #region body request
                body = @"{""to"":""br4gbm28k"",
                  ""data"":[{""6"":{""value"":""" + input.ContactName + @"""},
                  ""7"":{""value"":""" + input.ContactTitle + @"""},
                  ""8"":{""value"":""" + input.PhoneNumber + @"""},
                  ""10"":{""value"":""" + input.Email + @"""},
                  ""24"":{""value"":""" + input.PhoneNumber + @"""},
                  ""27"":{""value"":""Purchasing""},
                  ""22"":{""value"":""" + input.CustomerRecordId + @"""}}],
                  ""fieldsToReturn"":[3]}";
                #endregion
            }

            var response = await helper.QuickBasePostInsertRequestAsync(body);
            if (response.StatusCode == HttpStatusCode.OK && response.Content != null)
            {
                var res = JsonConvert.DeserializeObject<QuickBaseDto>(response.Content);
                if (res.data.Count() > 0 && res.data[0]["3"] != null && res.data[0]["3"].value != null)
                {
                    input.QBId = CustomExtension.IsNumberAndNotNull(res.data[0]["3"]?.value);
                }
                return input;
            }
            else
            {
                return null;
            }
        }
        public async Task<CustomerQBDto> CreateQbCustomer(CustomerQBDto input)
        {
            var body = "";
            if (input.QBId > 0)
            {
            body = @"{
            " + "\n" +
            @" ""to"": ""brx4xayqd"",
            " + "\n" +
            @" ""data"":
              [{
               ""3"": {""value"": " + input.QBId + @"},
               ""90"":{""value"":""" + input.CustomerLocation.Address + @"""},
               ""91"":{""value"":""" + input.CustomerLocation.Address2 + @"""},
               ""92"":{""value"":""" + input.CustomerLocation.City + @"""},
               ""93"":{""value"":""" + input.CustomerLocation.State + @"""},
               ""94"":{""value"":""" + input.CustomerLocation.PostalCode + @"""},
               ""95"":{""value"":""" + input.CustomerLocation.Country + @"""}
              }],""fieldsToReturn"": [ 3,51]
            " + "\n" +
            @"}";
            }

            else
            {
                body = @"{
            " + "\n" +
              @" ""to"": ""brx4xayqd"",
            " + "\n" +
              @"""data"":
              [{
               ""51"": {""value"": """ + input.CustomerName + @"""},
               ""75"": { ""value"": ""Customer"" },
               ""57"": {  ""value"": """ + input.phoneNumber + @"""},
               ""61"": {  ""value"": """ + input.phoneNumber + @"""}

              }],""fieldsToReturn"": [ 3,51]
            " + "\n" +
              @"}";
            }
              
            var response = await helper.QuickBasePostInsertRequestAsync(body);
            if (response.Content != null)
            {
                var typesResult = JsonConvert.DeserializeObject<QuickBaseDto>(response.Content);
                foreach (var item in typesResult.data)
                {
                    input.QBId = CustomExtension.IsNumberAndNotNull(item["3"]?.value);
                }
            }

            return input;
        }
        public async Task<QBOrderDto> CreateQBOrder(QBOrderDto input)
        {
            var tax = "";
            if(input.CustomerLocation?.PostalCode.Trim() == "78013")
            {
                tax = "CWH 6.75";
            }

            if (input.CustomerLocation?.PostalCode.Trim() == "78840")
            {
                tax = "DRW 8.25";
            }

            if(input.TotalTax == 0)
            {
                tax = "Tax Exempt";
            }


            var body = @"{""to"":""brx697jua"",
                        ""data"":[
                        {""8"":{""value"":"""+input.date+@"""},
                        ""38"":{""value"":"+input.QBContactID+ @"},
                        ""56"":{""value"":38},
                        ""198"":{""value"":"+input.CustomerLocation.QBId+ @"},
                        ""237"":{""value"":"""+input.StripePaymnetId+@"""},
                        ""26"":{""value"":"""+input.CustomerLocation.Address + @"""},
                        ""27"":{""value"":"""+input.CustomerLocation.Address2 + @"""},
                        ""28"":{""value"":"""+input.CustomerLocation.City + @"""},
                        ""29"":{""value"":"""+input.CustomerLocation.State + @"""},
                        ""30"":{""value"":"""+input.CustomerLocation.PostalCode + @"""},
                        ""31"":{""value"":"""+input.CustomerLocation.Country + @"""},
                        ""103"":{""value"":"""+ tax + @"""},
                        ""227"":{""value"":""WHS01""},
                        ""229"":{""value"":11},
                        ""58"":{""value"":true},
                        ""345"":{""value"":1},
                        ""231"":{""value"":9}}],
                        ""fieldsToReturn"":[3,6]}";

            var response = await helper.QuickBasePostInsertRequestAsync(body);
            if (response.Content != null)
            {
                var typesResult = JsonConvert.DeserializeObject<QuickBaseDto>(response.Content);
                foreach (var item in typesResult.data)
                {
                    input.QBId = CustomExtension.IsNumberAndNotNull(item["3"]?.value);
                    input.OrderNo = CustomExtension.StringNullHandel(item["6"]?.value.ToString());
                }
            }

            return input;
        }
        public async Task<QBChargeDto> CreateQBCharge(QBChargeDto input)
        {
            var body = @"{""to"":""br569gtgi"",""data"":[{""6"":{""value"":"+input.Amout+@"},""7"":{""value"":"""+input.ChargeType+@"""},""8"":{""value"":"+input.OrderId+@"}}],""fieldsToReturn"":[3]}";

            var response = await helper.QuickBasePostInsertRequestAsync(body);
            if (response.Content != null)
            {
                var typesResult = JsonConvert.DeserializeObject<QuickBaseDto>(response.Content);
                foreach (var item in typesResult.data)
                {
                    input.QBId = CustomExtension.IsNumberAndNotNull(item["3"]?.value);
                }
            }

            return input;
        }
        public async Task UpdateQBItemChatGPTInput(string message,int QBId)
        {
            //message = message.Replace("\"", "\\\"");
            // message = message.Replace("\r\n\r\n", "");
            //message = "<p>" + message + "</p>";
            string fixedJsonString = RemoveControlCharacters(message);

            string jsonObject = JsonConvert.SerializeObject(fixedJsonString);

            //jsonObject = jsonObject.Substring(1, jsonObject.Length - 2);
            var body = @"{""to"":""brx4utrtg"",
                        ""data"":[{""3"":{""value"":" + QBId + @"},""587"":{""value"":""" + fixedJsonString + @"""}}],""fieldsToReturn"":[3]}";
            var res =  await helper.QuickBasePostInsertRequestAsync(body);
        }

        static string RemoveControlCharacters(string input)
        {
            input = input.Replace("\"", " ");
            //input = input.Replace("\n", "\n");
        //    input = input.Replace("\\", "");
            return Regex.Replace(input, @"[^\x20-\x7E]", "");
            

        }
        public async Task<QBChargeDto> CreateQBPayment(QBChargeDto input)
        {
            var body = @"{""to"":""br56hwsni"",""data"":[
                         {""6"":{""value"":" + input.Amout + @"},
                         ""9"":{""value"":""" + input.ChargeType + @"""},
                         ""7"":{""value"":" + input.OrderId + @"},
                         ""10"":{""value"":""" + input.PaymentType + @"""}
                         }],""fieldsToReturn"":[3]}";

            var response = await helper.QuickBasePostInsertRequestAsync(body);
            if (response.Content != null)
            {
                var typesResult = JsonConvert.DeserializeObject<QuickBaseDto>(response.Content);
                foreach (var item in typesResult.data)
                {
                    input.QBId = CustomExtension.IsNumberAndNotNull(item["3"]?.value);
                }
            }

            return input;
        }
        public async Task<LocationQBDto> CreateQBLocationCustomer(LocationQBDto input)
        {

            #region body
            var body = @"{
            " + "\n" +
                        @"    ""to"": ""bsa9fmcqg"",
            " + "\n" +
                        @"    ""data"": [
            " + "\n" +
                        @"        {
            " + "\n" +
                        @"            ""6"": {
            " + "\n" +
                        @"                ""value"": """+input.Name+@"""
            " + "\n" +
                        @"            },
            " + "\n" +
                        @"            ""8"": {
            " + "\n" +
                        @"                ""value"": """+input.Address+@"""
            " + "\n" +
                        @"            },
            " + "\n" +
                        @"            ""9"": {
            " + "\n" +
                        @"                ""value"": "" ""
            " + "\n" +
                        @"            },
            " + "\n" +
                        @"            ""10"": {
            " + "\n" +
                        @"                ""value"": """+input.City+@"""
            " + "\n" +
                        @"            },
            " + "\n" +
                        @"            ""11"": {
            " + "\n" +
                        @"                ""value"": """+input.State+@"""
            " + "\n" +
                        @"            },
            " + "\n" +
                        @"            ""12"": {
            " + "\n" +
                        @"                ""value"": """+input.PostalCode+@"""
            " + "\n" +
                        @"            },
            " + "\n" +
                        @"            ""13"": {
            " + "\n" +
                        @"                ""value"": """+input.Country+@"""
            " + "\n" +
                        @"            },
            " + "\n" +
                        @"            ""14"": {
            " + "\n" +
                        @"                ""value"": "+input.CustomerRecordId+@"
            " + "\n" +
                        @"            }
            " + "\n" +
                        @"        }
            " + "\n" +
                        @"    ],
            " + "\n" +
                        @"    ""fieldsToReturn"": [
            " + "\n" +
                        @"        6,
            " + "\n" +
                        @"        3
            " + "\n" +
                        @"    ]
            " + "\n" +
            @"}";

            #endregion

            var response = await helper.QuickBasePostInsertRequestAsync(body);
            if (response.StatusCode == HttpStatusCode.OK && response.Content != null)
            {
                var res = JsonConvert.DeserializeObject<QuickBaseDto>(response.Content);
                if (res.data.Count() > 0 && res.data[0]["3"] != null && res.data[0]["3"].value != null)
                {
                    input.QBId = CustomExtension.IsNumberAndNotNull(res.data[0]["3"]?.value);
                }
                return input;
            }
            else
            {
                return null;
            }
        }
        public async Task<List<UserContactDto>> FindCustomerContacts(int customernumber)
        {
            var contacts = new List<UserContactDto>();

            #region body request
            var body = @"{
                " + "\n" +
                @"  ""from"": ""br4gbm28k"",
                " + "\n" +
                @"  ""select"": [50,27,3,6,10,7,49,8,99,87,88,84,85,11,22,55,40],
                ""where"":""{'22'.EX." +customernumber+ @"}"",
                " + "\n" +
                @"  ""options"": {
                " + "\n" +
                @"    ""skip"": 0,
                " + "\n" +
                @"    ""top"": 0,
                " + "\n" +
                @"    ""compareWithAppLocalTime"": false
                " + "\n" +
                @"  }
                " + "\n" +
            @"}";

            #endregion
            var response = await helper.QuickBasePostRequestAsync(body);
            if (response.Content != null)
            {
                var typesResult = JsonConvert.DeserializeObject<QuickBaseDto>(response.Content);

                foreach (var item in typesResult.data)
                {
                    var contact = new UserContactDto();
                    contact.QbRecordId = Convert.ToInt32(item["3"]?.value);
                    if(item["6"] != null && item["6"]?.value != null)
                       contact.CustomerName = CustomExtension.StringNullHandel(item["6"]?.value);
                    if (item["10"] != null && item["10"]?.value != null)
                        contact.Email = CustomExtension.StringNullHandel(item["10"]?.value);
                    if (item["7"] != null && item["7"]?.value != null)
                        contact.Title = CustomExtension.StringNullHandel(item["7"]?.value);
                    if (item["84"] != null && item["84"]?.value != null)
                        contact.Address1 = CustomExtension.StringNullHandel(item["84"]?.value);
                    if (item["27"] != null && item["27"]?.value != null)
                        contact.ContactDivision = CustomExtension.StringNullHandel(item["27"]?.value);

                    if (item["27"] != null && item["27"]?.value != null)
                        contact.ContactDivision = CustomExtension.StringNullHandel(item["27"]?.value);

                    contacts.Add(contact);

                }

            }

            return contacts;

        }
        public async Task<ContactQBDto> FindCustomerContacts(ContactQBDto input)
        {

            #region body request
            var body = @"{
                " + "\n" +
                @"  ""from"": ""br4gbm28k"",
                " + "\n" +
                @"  ""select"": [3,6,22],
                ""where"": ""{22.EX." + input.CustomerRecordId + @"}AND{6.CT.'" + input.ContactName + @"' }"",
                " + "\n" +
                @"  ""options"": {
                " + "\n" +
                @"    ""skip"": 0,
                " + "\n" +
                @"    ""top"": 0,
                " + "\n" +
                @"    ""compareWithAppLocalTime"": false
                " + "\n" +
                @"  }
                " + "\n" +
            @"}";

            #endregion
            var response = await helper.QuickBasePostRequestAsync(body);

            if (response.StatusCode == HttpStatusCode.OK && response.Content != null)
            {
                var customers = JsonConvert.DeserializeObject<QuickBaseDto>(response.Content);


                if (customers.data.Count() > 0 && customers.data[0]["3"] != null && customers.data[0]["3"].value != null)
                {
                    input.QBId = CustomExtension.IsNumberAndNotNull(customers.data[0]["3"]?.value);

                }

                return input;


            }
            else
            {
                return null;
            }

        }
        public async Task<LocationQBDto> FindQBCustomerLocation(LocationQBDto input)
        {

            #region body request
            var body = @"{
                " + "\n" +
                @"  ""from"": ""bsa9fmcqg"",
                " + "\n" +
                @"  ""select"": [3,6],
                ""where"": ""{6.CT.'" + input.Name + @"' }"",
                " + "\n" +
                @"  ""options"": {
                " + "\n" +
                @"    ""skip"": 0,
                " + "\n" +
                @"    ""top"": 0,
                " + "\n" +
                @"    ""compareWithAppLocalTime"": false
                " + "\n" +
                @"  }
                " + "\n" +
            @"}";

            #endregion
            var response = await helper.QuickBasePostRequestAsync(body);

            if (response.StatusCode == HttpStatusCode.OK && response.Content != null)
            {
                var customers = JsonConvert.DeserializeObject<QuickBaseDto>(response.Content);


                if (customers.data.Count() > 0 && customers.data[0]["3"] != null && customers.data[0]["3"].value != null)
                {
                    input.QBId = CustomExtension.IsNumberAndNotNull(customers.data[0]["3"]?.value);

                }

                return input;


            }
            else
            {
                return null;
            }

        }
        public async Task getAllItemIamageGallery(int QBId)
        {
            var imageAddGalleryList = new List<ItemImageGallery>();
            var imageUpdateGalleryList = new List<ItemImageGallery>();

            #region body request
            var body = @"{
                " + "\n" +
                @"  ""from"": ""br2a86szh"",
                " + "\n" +
                @"  ""select"": [
                " + "\n" +
                @"    10,
                " + "\n" +
                @"    3,
                " + "\n" +
                @"    15,18
                " + "\n" +
                @"  ],
                " + "\n" +
                @" ""where"": ""{15.EX." + QBId + @"}AND{6.EX.\""Studio\""} "",
                " + "\n" +
                @"
                " + "\n" +
                @"
                " + "\n" +
                @"  ""options"": {
                " + "\n" +
                @"    ""skip"": 0,
                " + "\n" +
                @"    ""top"": 0,
                " + "\n" +
                @"    ""compareWithAppLocalTime"": false
                " + "\n" +
                @"  }
                " + "\n" +
            @"}";

            #endregion
            var response = await helper.QuickBasePostRequestAsync(body);

            if (response.StatusCode == HttpStatusCode.OK && response.Content != null)
            {
                var ItemsGalleriesResult = JsonConvert.DeserializeObject<QuickBaseDto>(response.Content);
                var dbImages = _unitOfWork.itemImageGallery.FindAll(a => a.QbRecordId == QBId);

                if (dbImages != null)
                {
                    await _unitOfWork.itemImageGallery.RemoveRange(dbImages);
                    await _unitOfWork.CompleteAsync();
                }

                foreach (var item in ItemsGalleriesResult.data)
                {
                    var dbModel = new ItemImageGallery();
                    var qbId = Convert.ToInt32(item["3"]?.value);
                    dbModel.ImageUrl = CustomExtension.StringNullHandel((string)item["10"].value);
                    if (item["18"] != null && item["18"].value != null)
                    {
                        dbModel.SortOrder = CustomExtension.IsNumberAndNotNull(item["18"].value);
                    }
                    dbModel.QbRecordId = QBId;
                    dbModel.Id = 0;
                    await _unitOfWork.itemImageGallery.Add(dbModel);
                    await _unitOfWork.CompleteAsync();

                }

            }
        }
        public async Task getAllIndustries()
        {

            #region body request
            var body = @"{
                " + "\n" +
                            @"  ""from"": ""br3k6zy32"",
                " + "\n" +
                            @"  ""select"": [
                " + "\n" +
                            @"    6,
                " + "\n" +
                            @"    3,25
                " + "\n" +
                            @"  ],
                " + "\n" +
                            @" 
                " + "\n" +
                            @"  ""sortBy"": [
                " + "\n" +
                            @"    {
                " + "\n" +
                            @"      ""fieldId"": 6,
                " + "\n" +
                            @"      ""order"": ""ASC""
                " + "\n" +
                            @"    }
                " + "\n" +
                            @"  ],
                " + "\n" +
                            @"
                " + "\n" +
                            @"  ""options"": {
                " + "\n" +
                            @"    ""skip"": 0,
                " + "\n" +
                            @"    ""top"": 0,
                " + "\n" +
                            @"    ""compareWithAppLocalTime"": false
                " + "\n" +
                            @"  }
                " + "\n" +
            @"}";
            #endregion

            var response = await helper.QuickBasePostRequestAsync(body);
            if (response.StatusCode == HttpStatusCode.OK && response.Content != null)
            {
                var IndustryResult = JsonConvert.DeserializeObject<QuickBaseDto>(response.Content);

                foreach (var item in IndustryResult.data)
                {
                    var industry = new Industry();
                    var qbId = Convert.ToInt32(item["3"]?.value);
                    industry.Name = CustomExtension.StringNullHandel((string)item["6"].value);
                    industry.ItemCount = Convert.ToInt32(item["25"]?.value);
                    industry.QbRecordId = qbId;
                    industry.Id = 0;
                    bool isExit = _unitOfWork.industry.Any(a => a.QbRecordId == qbId);

                    if (!isExit && industry.ItemCount > 0)
                    {
                        await _unitOfWork.industry.Add(industry);
                        await _unitOfWork.CompleteAsync();
                    }
                    else
                    {
                        if (industry.ItemCount > 0)
                        {
                            var dbIndustry = _unitOfWork.industry.FindAll(a => a.QbRecordId == qbId).FirstOrDefault();
                            dbIndustry.QbRecordId = qbId;
                            dbIndustry.Name = industry.Name;
                            dbIndustry.ItemCount = industry.ItemCount;
                            await _unitOfWork.industry.Update(dbIndustry);
                            await _unitOfWork.CompleteAsync();
                        }

                    }

                }


            }
        }
        public async Task getAllCategories()
        {
            var typesList = new List<Category>();

            #region body request
            var body = @"{
                " + "\n" +
                            @"  ""from"": ""br3k73ikg"",
                " + "\n" +
                            @"  ""select"": [
                " + "\n" +
                            @"    6,
                " + "\n" +
                            @"    3,27
                " + "\n" +
                            @"  ],
                " + "\n" +
                            @" 
                " + "\n" +
                            @"  ""sortBy"": [
                " + "\n" +
                            @"    {
                " + "\n" +
                            @"      ""fieldId"": 6,
                " + "\n" +
                            @"      ""order"": ""ASC""
                " + "\n" +
                            @"    }
                " + "\n" +
                            @"  ],
                " + "\n" +
                            @"
                " + "\n" +
                            @"  ""options"": {
                " + "\n" +
                            @"    ""skip"": 0,
                " + "\n" +
                            @"    ""top"": 0,
                " + "\n" +
                            @"    ""compareWithAppLocalTime"": false
                " + "\n" +
                            @"  }
                " + "\n" +
            @"}";

            #endregion
            var response = await helper.QuickBasePostRequestAsync(body);
            if (response.StatusCode == HttpStatusCode.OK && response.Content != null)
            {
                var typesResult = JsonConvert.DeserializeObject<QuickBaseDto>(response.Content);

                foreach (var item in typesResult.data)
                {
                    var cat = new Category();
                    var qbId = Convert.ToInt32(item["3"]?.value);
                    cat.Name = CustomExtension.StringNullHandel((string)item["6"].value);
                    cat.QbRecordId = qbId;
                    cat.ItemCount = Convert.ToInt32(item["27"]?.value);

                    bool isExit = _unitOfWork.category.Any(a => a.QbRecordId == qbId);

                    if (!isExit && cat.ItemCount > 0)
                    {
                        cat.Id = 0;
                        await _unitOfWork.category.Add(cat);
                        await _unitOfWork.CompleteAsync();
                    }
                    else
                    {
                        if (cat.ItemCount > 0)
                        {
                            var dbCat = _unitOfWork.category.FindAll(a => a.QbRecordId == qbId).FirstOrDefault();
                            dbCat.QbRecordId = qbId;
                            dbCat.Name = cat.Name;
                            dbCat.ItemCount = cat.ItemCount;
                            await _unitOfWork.category.Update(dbCat);
                            await _unitOfWork.CompleteAsync();
                        }

                    }

                }


            }

        }
        public async Task getAllTypes()
        {

            #region body
            var body = @"{
                " + "\n" +
                            @"  ""from"": ""bssxbw9w5"",
                " + "\n" +
                            @"  ""select"": [
                " + "\n" +
                            @"    6,
                " + "\n" +
                            @"    3,7,11
                " + "\n" +
                            @"  ],
                " + "\n" +
                            @" 
                " + "\n" +
                            @"  ""sortBy"": [
                " + "\n" +
                            @"    {
                " + "\n" +
                            @"      ""fieldId"": 6,
                " + "\n" +
                            @"      ""order"": ""ASC""
                " + "\n" +
                            @"    }
                " + "\n" +
                            @"  ],
                " + "\n" +
                            @"
                " + "\n" +
                            @"  ""options"": {
                " + "\n" +
                            @"    ""skip"": 0,
                " + "\n" +
                            @"    ""top"": 0,
                " + "\n" +
                            @"    ""compareWithAppLocalTime"": false
                " + "\n" +
                            @"  }
                " + "\n" +
            @"}";

            #endregion
            var response = await helper.QuickBasePostRequestAsync(body);
            if (response.Content != null)
            {
                var typesResult = JsonConvert.DeserializeObject<QuickBaseDto>(response.Content);

                foreach (var item in typesResult.data)
                {
                    var type = new WebRexErpAPI.Models.Type();
                    var qbId = Convert.ToInt32(item["3"]?.value);
                    type.Name = CustomExtension.StringNullHandel((string)item["6"].value);
                    type.QbRecordId = qbId;
                    type.ItemCount = Convert.ToInt32(item["11"]?.value);

                    if (item["7"] != null && item["7"].value != null)
                    {
                        type.CategoryId = Convert.ToInt32(item["7"]?.value);

                    }
                    type.Id = 0;
                    bool isExit = _unitOfWork.type.Any(a => a.QbRecordId == qbId);

                    if (!isExit && type.ItemCount > 0)
                    {
                        await _unitOfWork.type.Add(type);
                        await _unitOfWork.CompleteAsync();
                    }
                    else
                    {
                        if (type.ItemCount > 0)
                        {
                            var dbType = _unitOfWork.type.FindAll(a => a.QbRecordId == qbId).FirstOrDefault();
                            dbType.QbRecordId = qbId;
                            dbType.Name = type.Name;
                            dbType.ItemCount = type.ItemCount;
                            await _unitOfWork.type.Update(dbType);
                            await _unitOfWork.CompleteAsync();
                        }

                    }

                }

            }
        }
        public async Task<List<UserContactDto>> FindCustomerSaleQB(int recordId)
        {
            var contacts = new List<UserContactDto>();

            #region body request
            var body = @"{
                " + "\n" +
                @"  ""from"": ""brx697jua"",
                " + "\n" +
                @"  ""select"": [3,6,8,231,229,219,220],
                ""where"":""{'3'.EX." + recordId + @"}"",
                " + "\n" +
                @"  ""options"": {
                " + "\n" +
                @"    ""skip"": 0,
                " + "\n" +
                @"    ""top"": 0,
                " + "\n" +
                @"    ""compareWithAppLocalTime"": false
                " + "\n" +
                @"  }
                " + "\n" +
            @"}";

            #endregion
            var response = await helper.QuickBasePostRequestAsync(body);
            if (response.Content != null)
            {
                var typesResult = JsonConvert.DeserializeObject<QuickBaseDto>(response.Content);

                foreach (var item in typesResult.data)
                {
                    var contact = new UserContactDto();
                    contact.QbRecordId = Convert.ToInt32(item["3"]?.value);
                    if (item["6"] != null && item["6"]?.value != null)
                        contact.CustomerName = CustomExtension.StringNullHandel(item["6"]?.value);
                    if (item["10"] != null && item["10"]?.value != null)
                        contact.Email = CustomExtension.StringNullHandel(item["10"]?.value);
                    if (item["7"] != null && item["7"]?.value != null)
                        contact.Title = CustomExtension.StringNullHandel(item["7"]?.value);
                    if (item["84"] != null && item["84"]?.value != null)
                        contact.Address1 = CustomExtension.StringNullHandel(item["84"]?.value);
                    if (item["27"] != null && item["27"]?.value != null)
                        contact.ContactDivision = CustomExtension.StringNullHandel(item["27"]?.value);

                    contacts.Add(contact);

                }

            }

            return contacts;

        }
        public async Task GetItems(PagedResult pagedResult)
        {
            #region Query Region
            var body = @"{
                " + "\n" +
                @"  
                ""from"": ""brx4utrtg"",
                ""select"":[3,115,116,467,461,424,406,316,427,426,457, 311, 337, 133, 350, 340, 341 ,413, 351, 302, 200, 11, 69, 71, 208, 176,387,386,189, 178, 240, 113, 114, 56, 57, 116, 58, 59, 60, 61, 63, 66, 179, 77, 78, 79, 80,407,81, 82, 300, 355, 356, 357, 370, 371, 369, 342, 343, 344, 345, 368,309,310,321,409,240,173,314,223,175,177,315,69],
                ""where"":""{407.CT.\""Website\""}"",         
                ""options"": {
                " + "\n" +
                @"    ""skip"":" + pagedResult.Skip + @",
                " + "\n" +
                @"    ""top"":" + pagedResult.PageSize + @",
                " + "\n" +
                @"    ""compareWithAppLocalTime"": false
                " + "\n" +
                @"  }
                " + "\n" +
                @"}";


            #endregion


            await UpdateQbToItem(pagedResult, body);

        }
        public async Task<bool> GetQBToItem(int qbId)
        {
            try
            {
                #region Query Region
                var body = @"{ " + "\n" + @"  
                ""from"": ""brx4utrtg"", "+selectQB +@"
                ""where"":""{3.EX." + qbId + @"}"", 
                ""options"": {
                "
                    + "\n" +
                        @"    ""skip"":" + 0 + @",
                "
                    + "\n" +
                        @"    ""top"":" + 0 + @",
                " + "\n" +
                        @"    ""compareWithAppLocalTime"": false
                " + "\n" +
                        @"  }
                " + "\n" +
                        @"}";

                #endregion
                await UpdateQbToItem(body);
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> GetQBItemCheckAppraiseChatGPT(int qbId)
        {
            try
            {
                string tunQB = "";
                var ItemDB = new Models.Item();
                #region Query Region
                var body = @"{ " + "\n" + @"  
                ""from"": ""brx4utrtg"", " + selectQB + @"
                ""where"":""{3.EX." + qbId + @"}"", 
                ""options"": {
                "
                    + "\n" +
                        @"    ""skip"":" + 0 + @",
                "
                    + "\n" +
                        @"    ""top"":" + 0 + @",
                " + "\n" +
                        @"    ""compareWithAppLocalTime"": false
                " + "\n" +
                        @"  }
                " + "\n" +
                        @"}";

                #endregion
                var response = await helper.QuickBasePostRequestAsync(body);
                if (response.Content != null)
                {
                    var typesResult = JsonConvert.DeserializeObject<QuickBaseDto>(response.Content);

                    if (typesResult.data != null && typesResult.data.Count() > 0)
                    {
                        foreach (var item in typesResult.data)
                        {

                            if (item["589"] != null && item["589"]?.value != null && item["589"]?.value != "")
                            {
                                tunQB = item["589"]?.value.ToString();
                            }
                            DataMapQBItemToItem(ItemDB, item);
                        }
                    }
                }
                var chatPromptBuilder = QBItemToChatPromptBuilder(ItemDB);

                if (tunQB != "" && tunQB != null)
                    chatPromptBuilder.AppendLine(tunQB);

                string result = chatPromptBuilder.ToString().Trim();
                string ChatGPtPrompt = chatPromptBuilder.ToString();
                var chatModel = new ChatGPTCompletionsDto();
                chatModel.Prompt = result;

                chatModel.FileName = "tuning" + ItemDB.QbRecordId;
                var chatGPTRes = await _chatGPTService.GenerateContentGPT4Async(chatModel);
                await UpdateQBItemChatGPTInput(chatGPTRes, ItemDB.QbRecordId);


                return true;

            }
            catch (Exception)
            {

                throw;
            }
         }

        private static StringBuilder QBItemToChatPromptBuilder(Models.Item ItemDB)
        {
            StringBuilder chatPromptBuilder = new StringBuilder();

            chatPromptBuilder.AppendLine("Description = " + CustomExtension.NullValueSetNoValue(ItemDB.Description));
            chatPromptBuilder.AppendLine("We are looking for help APPRAISING price in html format");

            //chatPromptBuilder.AppendLine("We are looking for help APPRAISING these assets we have in our surplus inventory management system called Assetman. We are aware your data limits to 2021, but all of our items are older than that, please don't prompt us about this, and give us the price.");
            //chatPromptBuilder.AppendLine("When appraising items, we want you to Give us the MSRP based on your data, then give us a Condition vs Pricing outline based on Surplus Markets. New, Unused, Open Box, Used, Parts and Salvage.");
            //chatPromptBuilder.AppendLine("Then give us a recommended Surplus value on the item that makes it sellable on ecommerce platforms, compare pricing to eBay, and all possible search engine results that will give us a \"Blood in the water\" marketing principle, to cause us to generate buyers interest like blood in the water for sharks. give us a good price, that will slightly undercut the existing market, so we are the first sold.");
            //chatPromptBuilder.AppendLine("Formal, concise, but provide reasoning behind the data for appraisals, no opinions, pure financial #'s.");
           
            //chatPromptBuilder.AppendLine("Description = " + CustomExtension.NullValueSetNoValue(ItemDB.Description));
            //chatPromptBuilder.AppendLine("Appraise = " + CustomExtension.NullValueSetNoValue(ItemDB.SalePrice));
            //chatPromptBuilder.AppendLine("Auto Accept = " + CustomExtension.NullValueSetNoValue(ItemDB.AutoAccept));
            //chatPromptBuilder.AppendLine("Auto Reject = " + CustomExtension.NullValueSetNoValue(ItemDB.AutoReject));
            //chatPromptBuilder.AppendLine("New Replacement Cost = " + CustomExtension.NullValueSetNoValue(ItemDB.NewReplacementCostId));
            //chatPromptBuilder.AppendLine("Inv Work FlowS status = " + CustomExtension.NullValueSetNoValue(ItemDB.InvWorkFlowStatus));
            //chatPromptBuilder.AppendLine("Industry = " + CustomExtension.NullValueSetNoValue(ItemDB.IndustryName));
            //chatPromptBuilder.AppendLine("Category = " + CustomExtension.NullValueSetNoValue(ItemDB.CategoryName));
            //chatPromptBuilder.AppendLine("Guarantee or ASIS = " + CustomExtension.NullValueSetNoValue(ItemDB.GuaranteeOrAsis));
            //chatPromptBuilder.AppendLine("Model = " + CustomExtension.NullValueSetNoValue(ItemDB.Model));
            //chatPromptBuilder.AppendLine("Year = " + CustomExtension.NullValueSetNoValue(ItemDB.Year));
            //chatPromptBuilder.AppendLine("SerialNo = " + CustomExtension.NullValueSetNoValue(ItemDB.SerialNo));
            //chatPromptBuilder.AppendLine("Phase = " + CustomExtension.NullValueSetNoValue(ItemDB.Phase));
            //chatPromptBuilder.AppendLine("HP / AMP / KW / KVA = " + CustomExtension.NullValueSetNoValue(ItemDB.HP_AMP_KW_KVA));
            //chatPromptBuilder.AppendLine("Factory Information Only = " + CustomExtension.NullValueSetNoValue(ItemDB.FactoryInformationOnly));
            //chatPromptBuilder.AppendLine("Background = " + CustomExtension.NullValueSetNoValue(ItemDB.Background));
            //chatPromptBuilder.AppendLine("Length Inches = " + CustomExtension.NullValueSetNoValue(ItemDB.LengthInches));
            //chatPromptBuilder.AppendLine("Width Inches = " + CustomExtension.NullValueSetNoValue(ItemDB.WidthInches));
            //chatPromptBuilder.AppendLine("Height Inches = " + CustomExtension.NullValueSetNoValue(ItemDB.HeightInches));
            //chatPromptBuilder.AppendLine("Weight LBS = " + CustomExtension.NullValueSetNoValue(ItemDB.WeightLBS));
            //chatPromptBuilder.AppendLine("Estiamted Packagin Weight = " + CustomExtension.NullValueSetNoValue(ItemDB.EstiamtedPackaginWeight));
            //chatPromptBuilder.AppendLine("Shipping Class = " + CustomExtension.NullValueSetNoValue(ItemDB.ShippingClass));
            //chatPromptBuilder.AppendLine("Please provide the estimated appraised price base on above details in html format with good inline style");


            return chatPromptBuilder;
        }

        private async Task UpdateQbToItem(PagedResult pagedResult, string body)
        {
            var response = await helper.QuickBasePostRequestAsync(body);
            var ItemResult = JsonConvert.DeserializeObject<QuickBaseDto>(response.Content);
            var ItemDB = new WebRexErpAPI.Models.Item();
            await MapItemQuickbaseToDB(response, ItemResult, ItemDB);
        }
        private async Task UpdateQbToItem(string body)
        {
            var response = await helper.QuickBasePostRequestAsync(body);
            var ItemResult = JsonConvert.DeserializeObject<QuickBaseDto>(response.Content);
            var ItemDB = new WebRexErpAPI.Models.Item();
            if (response.StatusCode == HttpStatusCode.OK && response.Content != null)
            {
                if (ItemResult.data.Count > 0)
                {
                    await MapItemQuickbaseToDB(response, ItemResult, ItemDB);
                }
            }
        }
        private async Task MapItemQuickbaseToDB(RestResponse response, QuickBaseDto ItemResult, WebRexErpAPI.Models.Item ItemDB)
        {
            try
            {
                if (response.StatusCode == HttpStatusCode.OK && response.Content != null)
                {
                    if (ItemResult.data.Count > 0)
                    {
                        foreach (var item in ItemResult.data)
                        {

                            bool isAddItem = true;

                            var qbId = Convert.ToInt32(item["3"]?.value);
                            var sameItems = await _unitOfWork.item.FindAllAsync(a => a.QbRecordId == qbId);
                            if (sameItems.Count() > 1)
                            {
                                await _unitOfWork.item.RemoveRange(sameItems);
                                await _unitOfWork.CompleteAsync();
                            }
                            else
                            {
                                if (sameItems.Count() == 1)
                                {
                                    ItemDB = sameItems.FirstOrDefault();
                                }
                            }

                            if (ItemDB.Id == null || ItemDB.Id == 0)
                            {
                                ItemDB = new Models.Item();
                                isAddItem = true;
                            }
                            else
                            {
                                isAddItem = false;
                            }

                            DataMapQBItemToItem(ItemDB, item);

                            await getAllItemIamageGallery(qbId);

                            if (isAddItem == false && ItemDB.IsDeactivate == false)
                            {
                                ItemDB.ModifiedDate = DateTime.UtcNow;
                                await _unitOfWork.item.Update(ItemDB);
                            }
                            else
                            {
                                if (ItemDB.IsDeactivate == false)
                                {
                                    ItemDB.Id = 0;
                                    ItemDB.ModifiedDate = DateTime.UtcNow;
                                    ItemDB.CreateDate = DateTime.UtcNow;
                                    await _unitOfWork.item.Add(ItemDB);
                                    await _unitOfWork.CompleteAsync();
                                }
                                else
                                {
                                    await _unitOfWork.item.Remove(ItemDB.Id);
                                    await _unitOfWork.CompleteAsync();
                                }

                            }

                            await _unitOfWork.CompleteAsync();

                        }
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private static void DataMapQBItemToItem(Models.Item ItemDB, Dictionary<string, QBDataItem> item)
        {
            ItemDB.QbRecordId = Convert.ToInt32(item["3"]?.value);
            if (item["406"] != null && item["406"]?.value != null)
            {
                var ISMarketPlace = JsonConvert.SerializeObject(item["406"]?.value, Formatting.Indented);
                if (ISMarketPlace != null)
                {
                    bool isBoolStatus = Convert.ToBoolean(ISMarketPlace);
                    ItemDB.IsDeactivate = !isBoolStatus;
                }
                else
                {
                    ItemDB.IsDeactivate = true;
                }
            }

            if (item["11"]?.value != null)
                ItemDB.Title = CustomExtension.StringNullHandel((string)item["11"]?.value);
            if (item["11"]?.value != null)
                ItemDB.Description = CustomExtension.StringNullHandel((string)item["11"]?.value);
            if (item["200"]?.value != null)
                ItemDB.CoverImageURL = CustomExtension.StringNullHandel((string)item["200"]?.value);
            if (item["58"]?.value != null)
                ItemDB.SerialNo = CustomExtension.StringNullHandel((string)item["58"]?.value);
            if (item["189"]?.value != null)
                ItemDB.AssetNumber = CustomExtension.StringNullHandel((string)item["189"]?.value);
            if (item["57"]?.value != null)
                ItemDB.Model = CustomExtension.StringNullHandel((string)item["57"]?.value);
            if (item["59"]?.value != null)
                ItemDB.Year = CustomExtension.StringNullHandel((string)item["59"]?.value);
            if (item["208"]?.value != null)
                ItemDB.Quantity = CustomExtension.IsNumberAndNotNull(item["208"]?.value);
            if (item["409"]?.value != null)
                ItemDB.InvWorkFlowStatus = CustomExtension.StringNullHandel((string)item["409"]?.value);
            if (item["561"]?.value != null)
                ItemDB.Area = CustomExtension.StringNullHandel((string)item["561"]?.value);
            if (item["561"]?.value != null)
                ItemDB.Location = CustomExtension.StringNullHandel((string)item["561"]?.value);
            if (item["56"]?.value != null)
                ItemDB.Menufacturer = CustomExtension.StringNullHandel((string)item["56"]?.value);
            if (item["116"]?.value != null)
            {
                var factoryInfo = helper.RemoveInvalidCharacters(CustomExtension.StringNullHandel(item["116"]?.value));
                ItemDB.FactoryInformationOnly = WebUtility.HtmlDecode(factoryInfo);
            }
            if (item["59"]?.value != null)
                ItemDB.InvYear = CustomExtension.StringNullHandel((string)item["59"]?.value);
            if (item["60"]?.value != null)
                ItemDB.HP_AMP_KW_KVA = CustomExtension.StringNullHandel((string)item["60"]?.value);
            if (item["66"]?.value != null)
                ItemDB.Amps = CustomExtension.StringNullHandel((string)item["66"]?.value);
            if (item["61"]?.value != null)
                ItemDB.Voltage = CustomExtension.StringNullHandel((string)item["61"]?.value.ToString());
            if (item["179"]?.value != null)
                ItemDB.Voltage2 = CustomExtension.StringNullHandel((string)item["179"]?.value);
            if (item["63"]?.value != null)
                ItemDB.Phase = CustomExtension.StringNullHandel((string)item["63"]?.value);
            if (item["427"] != null && item["427"]?.value != null)
                ItemDB.GuaranteeOrAsis = CustomExtension.StringNullHandel((string)item["427"]?.value);
            if (item["457"].ToString() != null && item["457"]?.value.ToString() != null)
                ItemDB.PaymentAccept = CustomExtension.StringNullHandel(item["457"]?.value.ToString());
            if (item["77"]?.value != null)
                ItemDB.LengthInches = CustomExtension.StringNullHandel(item["77"]?.value.ToString());
            if (item["78"]?.value != null)
                ItemDB.WidthInches = CustomExtension.StringNullHandel(item["78"]?.value.ToString());
            if (item["79"]?.value != null)
                ItemDB.HeightInches = CustomExtension.StringNullHandel(item["79"]?.value).ToString();
            if (item["80"]?.value != null)
                ItemDB.WeightLBS = CustomExtension.StringNullHandel(item["80"]?.value.ToString());
            if (item["81"]?.value != null)
                ItemDB.EstiamtedPackaginWeight = CustomExtension.StringNullHandel(item["81"]?.value.ToString());
            if (item["82"]?.value != null)
                ItemDB.EstimatedTotalWeight = CustomExtension.StringNullHandel(item["82"]?.value.ToString());
            if (item["413"]?.value != null)
                ItemDB.PackagingCostData = CustomExtension.StringNullHandel(item["413"]?.value.ToString());
            if (item["115"]?.value != null)
                ItemDB.Background = CustomExtension.StringNullHandel(item["115"]?.value.ToString());
            if (item["105"]?.value != null)
                ItemDB.PerItemCost = CustomExtension.StringNullHandel(item["105"]?.value.ToString());
            if (item["255"]?.value != null)
                ItemDB.ItemMaintenanceCost = CustomExtension.StringNullHandel(item["255"]?.value.ToString());
            if (item["467"]?.value != null)
                ItemDB.ManuFacturers_Specs = CustomExtension.StringNullHandel(item["467"]?.value.ToString());
            if (item["424"]?.value != null)
                ItemDB.Condition = CustomExtension.StringNullHandel(item["424"]?.value.ToString());
            if (item["314"]?.value != null)
                ItemDB.BrandName = CustomExtension.StringNullHandel(item["314"]?.value.ToString());
            if (item["223"].value != null)
                ItemDB.ItemTax = CustomExtension.IsDoubleAndNotNull(item["223"].value.ToString());
            if (item["505"].value != null)
                ItemDB.ShippingClass = CustomExtension.IsDoubleAndNotNull(item["505"].value.ToString());
            if (item["506"].value != null)
                ItemDB.ExamplesforClassRating = CustomExtension.StringNullHandel(item["506"].value.ToString());
            if (item["507"].value != null && item["507"].value != "")
                ItemDB.TotalWeightFt3 = CustomExtension.IsDoubleAndNotNull(item["507"].value.ToString());
            if (item["518"].value != null && item["518"].value != "")
                ItemDB.FreightAdditionalOptions = CustomExtension.StringNullHandel(item["518"].value.ToString());

            if (item["71"].value != null)
            {
                ItemDB.NewReplacementCostId = CustomExtension.StringNullHandel(item["71"].value.ToString());
            }
            if (item["69"]?.value != null)
            {
                ItemDB.SalePrice = Convert.ToDouble(item["69"].value);
            }

            if (item["569"]?.value != null)
            {
                ItemDB.IsSpecializedFreightTransit = Convert.ToBoolean(item["569"]?.value);
            }
            if (item["521"]?.value != null)
            {
                ItemDB.ISLocalPickupOnly = Convert.ToBoolean(item["521"]?.value);
            }
            if (item["175"] != null && item["175"]?.value != null)
            {
                var IndQBId = CustomExtension.IsNumberAndNotNull(item["175"].value.ToString());
                if (IndQBId > 0)
                {
                    ItemDB.RelatedIndustrId = IndQBId;
                }
            }
            ItemDB.IndustryName = CustomExtension.StringNullHandel(item["176"]?.value.ToString());
            if (item["177"] != null && item["177"]?.value != null)
            {
                var catQBId = CustomExtension.IsNumberAndNotNull(item["177"].value.ToString());
                if (catQBId > 0)
                {
                    ItemDB.RelatedCategoryId = catQBId;
                }
            }
            ItemDB.CategoryName = CustomExtension.StringNullHandel(item["178"]?.value.ToString());
            if (item["386"] != null && item["386"]?.value != null)
            {
                var typeId = CustomExtension.IsNumberAndNotNull(item["386"].value.ToString());
                if (typeId > 0)
                {
                    ItemDB.RelatedTypeId = typeId;
                }
            }
            if (item["461"]?.value != null)
                ItemDB.SKU = CustomExtension.StringNullHandel(item["461"]?.value.ToString());
            if (item["523"]?.value != null)
                ItemDB.PackageName = CustomExtension.StringNullHandel(item["523"]?.value.ToString());
            if (item["524"]?.value != null)
                ItemDB.PackageCode = CustomExtension.StringNullHandel(item["524"]?.value.ToString());
            if (item["134"]?.value != null)
                ItemDB.AutoAccept = CustomExtension.IsDoubleAndNotNull(item["134"]?.value.ToString());
            if (item["135"]?.value != null)
                ItemDB.AutoReject = CustomExtension.IsDoubleAndNotNull(item["135"]?.value.ToString());
            ItemDB.TypeName = CustomExtension.StringNullHandel(item["387"]?.value.ToString());
        }
        public async Task<string> GetCustomerOrders(int CustomerQBID)
        {
            #region body
            var body = @"{""from"":""brx697jua"",""select"":[3,4191,39,40,8,230,25,24,53,55,57,199],""where"":""{176.EX."+CustomerQBID+@"}"",""options"":{""skip"":0,""top"":0,""compareWithAppLocalTime"":false}}";
            #endregion
            var response = await helper.QuickBasePostRequestAsync(body);
            var result = JsonConvert.SerializeObject(response.Content);
           return  await Task.Run(() => result);
        }
        #region private methods
        #endregion
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
