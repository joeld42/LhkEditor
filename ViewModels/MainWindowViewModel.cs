using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using LhkEditor.Services;
using LhkEditor.Models;



namespace LhkEditor.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel( StoryDB db)
        {
            db.InitWithSampleDecks();
            DeckList = new StoryProjectViewModel(db.GetAllDecks());
            SelectedCard = new StoryCardViewModel( new StoryCard { Title = "ZZMainCard", StoryText = "Card from Mainview" } );
        }

        public StoryProjectViewModel DeckList { get; }
        public StoryCardViewModel SelectedCard { get; }
    }
}
