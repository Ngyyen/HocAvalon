using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using HocAvalon.Model;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using LibVLCSharp.Shared;
using System.Security.Policy;
using LibVLCSharp.Avalonia;
using System.Threading.Tasks;
using System.Linq;

namespace HocAvalon.ViewModels
{
	public partial class WordWindowViewModel : ViewModelBase
	{
        private HttpClient httpClient = new HttpClient();
        public SavedWord findedWord = new SavedWord();

        [ObservableProperty]
        public string buttonContent = "Add";

        [ObservableProperty]
        public string buttonColor = "Blue";

        [ObservableProperty]
        public bool isHasAudio = false;

        [ObservableProperty]
        public string word = "empty";

        [ObservableProperty]
        public string sound = "default";

        [ObservableProperty]
        public string phonetic = "muted";

        [ObservableProperty]
        public ObservableCollection<Definition> definitions = new ObservableCollection<Definition> { };

        //[ObservableProperty]
        //public Dictionary<string, ObservableCollection<Def2ex>> definitions = new Dictionary<string, ObservableCollection<Def2ex>>();

        public WordWindowViewModel() 
		{
            string input = ConvertString(ShareData.transText);
            TranslateWord(input);
            Task<List<SavedWord>> findedList = App.WordBookDatabase.GetaTask(input);
            if (findedList.Result.Count>0)
            {
                findedWord = findedList.Result[0];
                ButtonColor = "Red";
                ButtonContent = "Remove";
            }
        }
        public WordWindowViewModel(SavedWord selectedWord)
        {
            findedWord = selectedWord;
            TranslateWord(selectedWord.Content);
            ButtonContent = "Remove";
            ButtonColor = "Red";
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
                // lay word
                Word = entry["word"].ToString();
                // lay cach doc
                if (entry["phonetic"] != null)
                {
                    Phonetic = entry["phonetic"].ToString();
                }
                // lay file audio
                JToken phonetics = entry["phonetics"];
                foreach (JToken phonetic in phonetics)
                {
                    if (phonetic["audio"].ToString() != "")
                    {
                        Sound = phonetic["audio"].ToString();
                        IsHasAudio = true;
                        break;
                    }
                }
                // lay phan dich nghia va dong nghia, trai nghia
                JToken meanings = entry["meanings"];
                foreach (JToken meaning in meanings)
                {
                    // mỗi item là 1 loại từ
                    Definition item = new Definition();

                    // lấy loại từ
                    item.PartOfSpeech = meaning["partOfSpeech"].ToString();

/*                    string partOfSpeech = meaning["partOfSpeech"].ToString();
                    if (!Definitions.ContainsKey(partOfSpeech))
                    {
                        Definitions[partOfSpeech] = new ObservableCollection<Def2ex>();
                    }*/
                    
                    // lấy danh sách các định nghĩa và ví dụ
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

                    // lấy từ đồng nghĩa và trái nghĩa
                    JToken synonyms = meaning["synonyms"];                   
                    foreach (JToken synonym in synonyms)
                    {
                        item.IsHasSynonym = true;
                        item.Synonyms.Add(synonym.ToString());
                    }
                    JToken antonyms = meaning["antonyms"];
                    foreach (JToken antonym in antonyms)
                    {
                        item.IsHasAntonym = true;
                        item.Antonyms.Add(antonym.ToString());
                    }

                    Definitions.Add(item);
                }
            }
        }

        [RelayCommand]
        public void AudioPlay()
        {
            var libvlc = new LibVLC(enableDebugLogs: true);
            var media = new Media(libvlc, new Uri(Sound));
            var mediaplayer = new MediaPlayer(media);
            mediaplayer.Play();
        }

        [RelayCommand]
        public async void AddRemoveWord()
        {
            if (findedWord.Content == String.Empty)
            {
                SavedWord newWord = new SavedWord(Word);
                var r = App.WordBookDatabase.SaveWordAsync(newWord);
                findedWord = newWord;
                ButtonContent = "Remove";
                ButtonColor = "Red";
            }
            else
            {
                await App.WordBookDatabase.DeleteTask(findedWord);
                findedWord.Content = String.Empty;
                ButtonContent = "Add";
                ButtonColor = "Blue";
            }
        }
        string ConvertString(string input)
        {
            string convertInput = input.Replace(System.Environment.NewLine, "");
            return convertInput;
        }
    }
}