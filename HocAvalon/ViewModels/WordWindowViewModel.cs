using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using HocAvalon.Model;
using System.Collections.ObjectModel;

namespace HocAvalon.ViewModels
{
	public partial class WordWindowViewModel : ViewModelBase
	{
        private HttpClient httpClient = new HttpClient();

        [ObservableProperty]
        public string sound = "default";

        [ObservableProperty]
        public string word = "empty";

        [ObservableProperty]
        public ObservableCollection<Definition> definitions = new ObservableCollection<Definition> { };

        //[ObservableProperty]
        //public Dictionary<string, ObservableCollection<Def2ex>> definitions = new Dictionary<string, ObservableCollection<Def2ex>>();

        public WordWindowViewModel() 
		{
            string input = ConvertString(ShareData.transText);
            TranslateWord(input);
        }
        public void TranslateWord(string input)
        {
            // tạo link để gọi API
            string url = "https://api.dictionaryapi.dev/api/v2/entries/en/" + input;
            // gọi API và lấy kết quả trả về
            string result = httpClient.GetStringAsync(url).Result;
            JArray jsonArray = JArray.Parse(result);
            string rawResult = jsonArray.ToString();
            foreach (JToken entry in jsonArray)
            {
                Word = entry["word"].ToString();

                JToken meanings = entry["meanings"];
                foreach (JToken meaning in meanings)
                {
                    Definition item = new Definition();
                    item.PartOfSpeech = meaning["partOfSpeech"].ToString();

/*                    string partOfSpeech = meaning["partOfSpeech"].ToString();
                    if (!Definitions.ContainsKey(partOfSpeech))
                    {
                        Definitions[partOfSpeech] = new ObservableCollection<Def2ex>();
                    }*/

                    JToken definitions = meaning["definitions"];
                    foreach (JToken definition in definitions)
                    {
                        Def2ex def2ex = new Def2ex();
                        def2ex.Meaning = "Definition: " + definition["definition"].ToString();
                        if (definition["example"] != null)
                        {
                            def2ex.Example = "Example: " + definition["example"].ToString() +"\n";
                            def2ex.IsHasExample = true;
                        }
                        else
                        {
                            def2ex.Example = "Example: " + "nan";
                        }
                        item.Def2exs.Add(def2ex);
                        //Definitions[partOfSpeech].Add(def2ex);
                    }
                    Definitions.Add(item);
                }

                JToken phonetics = entry["phonetics"];
                foreach (JToken phonetic in phonetics)
                {
                    if (phonetic["audio"].ToString() != "")
                    {
                        Sound = phonetic["audio"].ToString();
                        break;
                    }
                }
            }
        }
        string ConvertString(string input)
        {
            string convertInput = input.Replace(System.Environment.NewLine, "");
            return convertInput;
        }
    }
}