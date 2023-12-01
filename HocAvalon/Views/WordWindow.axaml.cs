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
using Avalonia.Controls.ApplicationLifetimes;

namespace HocAvalon.Views
{
    public partial class WordWindow : Window
    {
        //private HttpClient httpClient;
/*        private Dictionary<string, string> type2def = new Dictionary<string, string>();
        private Dictionary<string, string> type2ex = new Dictionary<string, string>();*/
        //private Dictionary<string, Dictionary<string,string>> type2def2ex = new Dictionary<string, Dictionary<string, string>>();
        public WordWindow()
        {
            InitializeComponent();
            //httpClient = new HttpClient();
            //string input = ConvertString(ShareData.transText);
            //transBox.Text = TranslateWord(input);
            DataContext = new WordWindowViewModel();
        }
 /*       public string TranslateWord(string input)
        {
            // tạo link để gọi API
            string url = "https://api.dictionaryapi.dev/api/v2/entries/en/" + input;
            // gọi API và lấy kết quả trả về
            string result = httpClient.GetStringAsync(url).Result;
            JArray jsonArray = JArray.Parse(result);
            string word = "", sound="default";
            foreach (JToken entry in jsonArray)
            {
                word = entry["word"].ToString();
                //definition = entry["meanings"][0]["definitions"][0]["definition"].ToString();
                JToken meanings = entry["meanings"];
                foreach (JToken meaning in meanings)
                {
                    string type = meaning["partOfSpeech"].ToString();
                    type2def2ex[type] = new Dictionary<string, string>();
                    JToken definitions = meaning["definitions"];
                    foreach (JToken definition in definitions)
                    {
*//*                        type2def[type] = definition["definition"].ToString();
                        if (definition["example"] != null)
                        {
                            type2ex[type] = definition["example"].ToString();
                            break;
                        }*//*
                        if (definition["example"] != null)
                        {
                            type2def2ex[type][definition["definition"].ToString()] = definition["example"].ToString();
                        }
                        else
                        {
                            type2def2ex[type][definition["definition"].ToString()] = "";
                        }
                    }
                }
                JToken phonetics = entry["phonetics"];
                foreach (JToken phonetic in phonetics)
                {
                    if (phonetic["audio"].ToString() != "")
                    {
                        sound = phonetic["audio"].ToString();
                        break;
                    }
                }

            }
            string rawResult = jsonArray.ToString();
            string output = word + "\nsound: " + sound + "\n";
*//*            foreach (KeyValuePair<string, string> meaning in type2def)
            {
                output += $"{meaning.Key} definition:\n{meaning.Value}\n";
                if (type2ex.ContainsKey(meaning.Key))
                {
                    output += $"example: {type2ex[meaning.Key]}\n";
                }
            }*//*
            foreach (KeyValuePair<string, Dictionary<string,string>> meaning in type2def2ex)
            {
                output += $"{meaning.Key} definition:\n\n";
                foreach(KeyValuePair<string,string> def2ex in meaning.Value)
                {
                    output += def2ex.Key + "\n";
                    if (def2ex.Value != "")
                    {
                        output += "example: " + def2ex.Value.ToString() + "\n\n";
                    }
                    else
                        output += "\n";
                }
            }
            return output;
        }
        string ConvertString(string input)
        {
            string convertInput = input.Replace(System.Environment.NewLine, "");
            return convertInput;
        }*/
        private void Window_Deactivated(object? sender, System.EventArgs e)
        {
            //this.Close();
        }
    }
}
