using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HocAvalon.Model
{
    public class Def2ex
    {
        public Def2ex() { }
        public Def2ex(string meaning, string example)
        {
            Meaning = meaning;
            Example = example;
        }

        public string Meaning { get; set; } = string.Empty;
        public string Example { get; set; } = String.Empty;
    }
    public class Definition
    {
        public string PartOfSpeech {  get; set; } = String.Empty;
        //public Dictionary<string, string> Def2ex { get; set; } = new Dictionary<string, string>();
        public ObservableCollection<Def2ex> Def2exs { get; set; } = new ObservableCollection<Def2ex>(); 
    }
}
