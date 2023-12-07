using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using HocAvalon.Model;
using CommunityToolkit.Mvvm.Input;
using Microsoft.CodeAnalysis.CSharp;

namespace HocAvalon.ViewModels
{
	public partial class SaveWordViewModel : ViewModelBase
	{
		[ObservableProperty]
		private List<SavedWord> wordList = new List<SavedWord>();

		[ObservableProperty]
		private SavedWord selectedWord;

		public SaveWordViewModel()
		{
			GetWords();
		}

		[RelayCommand]
		public async void GetWords()
		{
			WordList = await App.WordBookDatabase.GetWordsAsync();
		}

        [RelayCommand]
        public async void RemoveWord()
        {
            await App.WordBookDatabase.DeleteTask(SelectedWord);
			GetWords();
        }
    }
}