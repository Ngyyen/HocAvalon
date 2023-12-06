using Avalonia.Controls;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API.Completions;
using OpenAI_API;
using System.Threading.Tasks;
using System;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HocAvalon.Views
{
    public partial class SumWindow : Window
    {
        private HttpClient httpClient;
        public SumWindow()
        {
            InitializeComponent();
            httpClient = new HttpClient();
            string input = ConvertString(ShareData.transText);           
            sumBox.Text = SummarizeText(input);
        }

        string ConvertString(string input)
        {
            string convertInput = input.Replace(System.Environment.NewLine, " ");
            return convertInput;
        }
        public string SummarizeText(string input)
        {
            var YOUR_API_KEY = "gAAAAABlZErp8klOmJLj0tYZwK9XD3waVoUGOcpfeD94pVkNx1dmpoNTySCyrBfhlN-jaXmWcejKbAzG_pBhTObpujyqUBmMhv-CnA1IDRGCnw-pUwspGwXftUVT66bMDnOx27ctgC0K";
            var client = new RestClient("https://api.textcortex.com/v1");
            var request = new RestRequest("texts/summarizations", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {YOUR_API_KEY}");
            //request.AddParameter("application/json", $"{{\n  \"target_lang\": \"en\", \n  \"text\": \"{input}\"\n}}", ParameterType.RequestBody);
            request.AddJsonBody(new {model= "gpt-3.5-turbo-16k", target_lang = "en", text = input});
            //request.AddParameter("application/json", new { target_lang = "en", text = input }, ParameterType.RequestBody);
            var response = client.Execute(request);
            var responseData = JObject.Parse(response.Content);
            string output = responseData["data"]["outputs"][0]["text"].ToString();
            return output;
        }

        private void Window_Deactivated(object? sender, System.EventArgs e)
        {
            this.Close();
        }

        /*        public string SummarizeText(string input, int limit)
                {
                    var YOUR_API_KEY = "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";
                    string userInput = "Summarize following content: " + ShareData.transText;
                    var client = new RestClient("https://api.openai.com/v1");
                    var request = new RestRequest("engines/text-davinci-003/completions", Method.Post);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Authorization", $"Bearer {YOUR_API_KEY}");
                    request.AddJsonBody(new {prompt = userInput, max_tokens = 4000, temperature = 0});
                    var response = client.Execute(request);
                    var responseData = JObject.Parse(response.Content);
                    string output = responseData["choices"][0]["text"].ToString();
                    return output;
                }*/

        /*        public string SummarizeText(string input, int limit)
                {
                    // tạo link để gọi API
                    string url = String.Format
                    ("https://api.meaningcloud.com/summarization-1.0?key=607933f54d404a1083e629c15ed8dbd9&txt={0}&lang=auto&limit={1}",
                     Uri.EscapeUriString(input), limit.ToString());
                    // gọi API và lấy kết quả trả về
                    string result = httpClient.GetStringAsync(url).Result;
                    JObject jsonObject = JObject.Parse(result);
                    string summary = (string)jsonObject["summary"];
                    return summary;
                }*/
    }
}

