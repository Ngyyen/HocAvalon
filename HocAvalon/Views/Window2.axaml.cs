using Avalonia.Controls;
using HocAvalon.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;
using System.Net.Http;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks;
using System.Text.Json.Nodes;

namespace HocAvalon.Views
{
    public partial class Window2 : Window
    {
        private HttpClient httpClient;
        public Window2()
        {
            InitializeComponent();
            httpClient = new HttpClient();
            string input = ConvertString(ShareData.transText);
            transBox.Text = TranslateText(input, "auto", ShareData.langSecond);
        }
        public string TranslateText(string input, string lang_first, string lang_second)
        {
            // tạo link để gọi API
            string url = String.Format
            ("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}",
             lang_first, lang_second, Uri.EscapeUriString(input));
            // gọi API và lấy kết quả trả về
            string result = httpClient.GetStringAsync(url).Result;

            // trích xuất thông tin từ kiểu dữ liệu json được trả về
            var jsonData = JsonConvert.DeserializeObject<List<dynamic>>(result);
            var translationItems = jsonData[0];
            string translation = "";
            foreach (object item in translationItems)
            {
                IEnumerable<dynamic> translationLineObject = item as IEnumerable<dynamic>;
                IEnumerator<dynamic> translationLineString = translationLineObject.GetEnumerator();
                translationLineString.MoveNext();
                translation += string.Format(" {0}", Convert.ToString(translationLineString.Current));
            }
            if (translation.Length > 1) { translation = translation.Substring(1); };
            return translation;
        }
        string ConvertString (string input)
        {
            string convertInput = input.Replace(System.Environment.NewLine, "");
            return convertInput;
        }

        private void Window_Deactivated(object? sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
