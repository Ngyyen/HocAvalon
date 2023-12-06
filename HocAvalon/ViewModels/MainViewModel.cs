using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HocAvalon.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";

    [ObservableProperty]
    public string[] myList = {"foo","bar","cha","arr"};

    [ObservableProperty]
    public Dictionary<string, string> languageDictionary = new Dictionary<string, string>
        {
            {"Afrikaans", "af"},
            {"Albanian", "sq"},
            {"Amharic", "am"},
            {"Arabic", "ar"},
            {"Azerbaijani", "az"},
            {"Bengali", "bn"},
            {"Bosnian", "bs"},
            {"Catalan", "ca"},
            {"Chinese (Simplified)", "zh-CN"},
            {"Chinese (Traditional)", "zh-TW"},
            {"Croatian", "hr"},
            {"Czech", "cs"},
            {"Danish", "da"},
            {"Dutch", "nl"},
            {"English", "en"},
            {"Esperanto", "eo"},
            {"Estonian", "et"},
            {"Finnish", "fi"},
            {"French", "fr"},
            {"Galician", "gl"},
            {"German", "de"},
            {"Greek", "el"},
            {"Gujarati", "gu"},
            {"Haitian Creole", "ht"},
            {"Hebrew", "he"},
            {"Hindi", "hi"},
            {"Hungarian", "hu"},
            {"Icelandic", "is"},
            {"Indonesian", "id"},
            {"Irish", "ga"},
            {"Italian", "it"},
            {"Japanese", "ja"},
            {"Kannada", "kn"},
            {"Kazakh", "kk"},
            {"Korean", "ko"},
            {"Latvian", "lv"},
            {"Lithuanian", "lt"},
            {"Macedonian", "mk"},
            {"Malay", "ms"},
            {"Malayalam", "ml"},
            {"Maltese", "mt"},
            {"Marathi", "mr"},
            {"Nepali", "ne"},
            {"Norwegian", "no"},
            {"Persian", "fa"},
            {"Polish", "pl"},
            {"Portuguese", "pt"},
            {"Punjabi", "pa"},
            {"Romanian", "ro"},
            {"Russian", "ru"},
            {"Serbian", "sr"},
            {"Slovak", "sk"},
            {"Slovenian", "sl"},
            {"Spanish", "es"},
            {"Swahili", "sw"},
            {"Swedish", "sv"},
            {"Tamil", "ta"},
            {"Telugu", "te"},
            {"Thai", "th"},
            {"Turkish", "tr"},
            {"Ukrainian", "uk"},
            {"Urdu", "ur"},
            {"Vietnamese", "vi"},
            {"Welsh", "cy"},
            {"Yiddish", "yi"},
            {"Zulu", "zu"}
        };

    public List<KeyValuePair<string, string>> LanguageList = new List<KeyValuePair<string, string>>();
}
