using WebRexErpAPI.BusinessServices.ChatGPT.Dto;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels.ResponseModels.FineTuneResponseModels;
using OpenAI.ObjectModels;
using OpenAI;
using OpenAI.Managers;
using OpenAI.ObjectModels.ResponseModels.FileResponseModels;

namespace WebRexErpAPI.Business.ChatGPT
{
    public interface IChatGPTService
    {
        Task<string> GenerateContentGPT4Async(ChatGPTCompletionsDto generateRequestModel);
        Task<string> GenerateContentTubro(ChatGPTCompletionsDto generateRequestModel);
    }
    public class ChatGPTService : IChatGPTService, IDisposable
    {
        private readonly string _ApiKey;
        private readonly string _FineTunningUrl;
        public ChatGPTService(IConfiguration? configuration = null)
        {
            _ApiKey = configuration?.GetValue<string>("ChatGPT:ChatAPIKEY") ?? "";
            _FineTunningUrl = configuration?.GetValue<string>("ChatGPT:FinTunningUrl") ?? "";
           
        }


        public async Task<string> GenerateContentTubro(ChatGPTCompletionsDto generateRequestModel)
        {

           

            using (HttpClient client = new HttpClient())
            {
                string resData = "";
                string apiUrl = "https://api.openai.com/v1/completions";
                var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
                request.Headers.Add("Authorization", "Bearer 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");
                var content = new StringContent($"{{\r\n    \"model\": \"{generateRequestModel.Model}\",\r\n    \"prompt\": \"{generateRequestModel.Prompt}\",\r\n    \"max_tokens\": {generateRequestModel.MaxTokens},\r\n    \"temperature\": {generateRequestModel.Temperature}\r\n}}", null, "application/json");

                request.Content = content;
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<ChatGPTResponse>(jsonResponse);

                    return responseObject.choices[0].text;
                }
                else
                {
                    return $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
        }


        public async Task<string> GenerateContentFinTunning(ChatGPTCompletionsDto input)
        {
            try
            {

                var sdk = InitializeOpenAiService();

                using (HttpClient client = new HttpClient())
                {
                    var fileBytes = await client.GetByteArrayAsync(_FineTunningUrl);
                    var uploadFilesResponse = await sdk.Files.UploadFile(UploadFilePurposes.FineTune, fileBytes, input.FileName);

                    if (uploadFilesResponse.Successful)
                    {
                        var createFineTuneResponse = await GetCreateFineTuneResponse(sdk, uploadFilesResponse);
                        var listFineTuneEventsStream = await sdk.FineTunes.ListFineTuneEvents(createFineTuneResponse.Id, true);
                        using var streamReader = new StreamReader(listFineTuneEventsStream);
                        while (!streamReader.EndOfStream)
                        {
                            await streamReader.ReadLineAsync();
                        }
                        var retrieveFineTuneResponse = await WaitForFineTuneCompletionAsync(createFineTuneResponse, sdk);
                        return await GenerateCompletionAsync(retrieveFineTuneResponse, sdk,input);
                    }
                    else
                    {
                        return $"{input.FileName} failed";
                    }

                   
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public async Task<string> GenerateContentGPT4Async(ChatGPTCompletionsDto input)
        {
            try
            {
                
                var sdk = InitializeOpenAiService();
                var res = "";
                var completionResult = await sdk.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
                {
                    Messages = new List<ChatMessage>
                {
                    ChatMessage.FromSystem("You are a helpful assistant."),
                    ChatMessage.FromUser("what about you business?"),
                    ChatMessage.FromAssistant("We're based in the United States. We own a multiple companies. King Surplus  (KSC846 Subsidiary who processes high temperature metals for aerospace)  (Surplus, who buys and sells industrial assets and machinery, online, in person, and through our custom ERP solution)  (Client Asset Management, where we manage large corporations excess fixed assets) King Cattle Manages government real estate property where we house tenants. Pro Rental  Equipment Rental Company competing with United Rentals. Pro Containers Buying, Selling, Renting Shipping Containers, and Fabricating them.We are looking for help APPRAISING these assets we have in our surplus inventory management system called Assetman. We are aware your data limits to 2021, but all of our items are older than that, please don't prompt us about this, and give us the price.When appraising items, we want you to Give us the MSRP based on your data, then give us a Condition vs Pricing outline based on Surplus Markets. New, Unused, Open Box, Used, Parts and Salvage.Then give us a recommended Surplus value on the item that makes it sellable on ecommerce platforms, compare pricing to eBay, and all possible search engine results that will give us a Blood in the water marketing principle, to cause us to generate buyers interest like blood in the water for sharks. give us a good price, that will slightly undercut the existing market, so we are the first sold."),
                    ChatMessage.FromUser(input.Prompt),
                   


                },
                    Model = OpenAI.ObjectModels.Models.Gpt_4_0613
                });
                if (completionResult.Successful)
                {
                    res = completionResult.Choices.First().Message.Content;
                }
                else
                {
                    if (completionResult.Error == null)
                    {
                        res = "Unknown Error";
                        
                    }
                    res = $"{completionResult.Error.Code}: {completionResult.Error.Message}";
                }
                return res;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        public async Task<string> GenerateContentGPT4StreamAsync( ChatGPTCompletionsDto input)
        {
            try
            {
                var sdk = InitializeOpenAiService();
                var res = "";
                var completionResult = sdk.ChatCompletion.CreateCompletionAsStream(new ChatCompletionCreateRequest
                {
                  Messages = new List<ChatMessage>
                {
                    new(StaticValues.ChatMessageRoles.System, "You are a helpful assistant."),
                    new(StaticValues.ChatMessageRoles.User, "what about you business"),
                    new(StaticValues.ChatMessageRoles.System, "We're based in the United States. We own a multiple companies.\r\n \r\nKing Surplus \r\n(KSC846 Subsidiary who processes high temperature metals for aerospace) \r\n(Surplus, who buys and sells industrial assets and machinery, online, in person, and through our custom ERP solution) \r\n(Client Asset Management, where we manage large corporations excess fixed assets)\r\n \r\nKing Cattle \r\nManages government real estate property where we house tenants.\r\n \r\nPro Rental \r\nEquipment Rental Company competing with United Rentals.\r\n \r\nPro Containers\r\nBuying, Selling, Renting Shipping Containers, and Fabricating them.\r\n \r\nWe are looking for help APPRAISING these assets we have in our surplus inventory management system called Assetman. We are aware your data limits to 2021, but all of our items are older than that, please don't prompt us about this, and give us the price.\r\n \r\nWhen appraising items, we want you to Give us the MSRP based on your data, then give us a \"Condition vs Pricing\" outline based on Surplus Markets. New, Unused, Open Box, Used, Parts and Salvage.\r\n \r\nThen give us a recommended \"Surplus\" value on the item that makes it sellable on ecommerce platforms, compare pricing to eBay, and all possible search engine results that will give us a \"Blood in the water\" marketing principle, to cause us to generate buyers interest like blood in the water for sharks. give us a good price, that will slightly undercut the existing market, so we are the first sold."),
                     new(StaticValues.ChatMessageRoles.User, input.Prompt)
                },
                    Model =OpenAI.ObjectModels.Models.Gpt_4_0613,
                    MaxTokens = 30
                });

                await foreach (var completion in completionResult)
                {
                    if (completion.Successful)
                    {
                        res = completion.Choices.First().Message.Content;
                       if(res != ""){

                            break;
                        }
                    }
                    else
                    {
                        if (completion.Error == null)
                        {
                            throw new Exception("Unknown Error");
                        }

                        Console.WriteLine($"{completion.Error.Code}: {completion.Error.Message}");
                    }
                }
                return res;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


        private OpenAIService InitializeOpenAiService()
        {
            return new OpenAIService(new OpenAiOptions() { ApiKey = _ApiKey });
        }
        private static async Task<FineTuneResponse> GetCreateFineTuneResponse(OpenAIService sdk, FileUploadResponse uploadFilesResponse)
        {
            return await sdk.FineTunes.CreateFineTune(new FineTuneCreateRequest()
            {
                TrainingFile = uploadFilesResponse.Id,
                Model = OpenAI.ObjectModels.Models.Ada,

            });
        }

        private async Task<FineTuneResponse> WaitForFineTuneCompletionAsync(FineTuneResponse createFineTuneResponse, OpenAIService sdk)
        {
            FineTuneResponse retrieveFineTuneResponse;
            do
            {
                 retrieveFineTuneResponse = await sdk.FineTunes.RetrieveFineTune(createFineTuneResponse.Id);
                if (retrieveFineTuneResponse.Status == "succeeded" || retrieveFineTuneResponse.Status == "cancelled" || retrieveFineTuneResponse.Status == "failed")
                {
                    break;
                }
                await Task.Delay(10_000);
            } while (true);

            return retrieveFineTuneResponse;
        }

        private async Task<string> GenerateCompletionAsync(FineTuneResponse retrieveFineTuneResponse, OpenAIService sdk, ChatGPTCompletionsDto input)
        {
            try
            {
                var res = "";
                do
                {
                    var completionResult = await sdk.Completions.CreateCompletion(new CompletionCreateRequest()
                    {
                        MaxTokens = 100,
                        Prompt = input.Prompt,
                        Model = retrieveFineTuneResponse.FineTunedModel,
                        LogProbs = 2,
                    });

                    if (completionResult.Successful)
                    {
                        Console.WriteLine(completionResult.Choices.FirstOrDefault());

                         res = completionResult.Choices.FirstOrDefault().Text;
                        break;
                    }
                    else
                    {
                        res = completionResult.Error?.Message;
                    }
                } while (true);

                return res;
            }
            catch (Exception ex)
            {
                return ex.ToString();
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

            }
        }


    }
}
