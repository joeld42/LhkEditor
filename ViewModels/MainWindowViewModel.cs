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
            SelectedCardDetails = new StoryCardViewModel( new StoryCard { Title = "ZZMainCard", StoryText = "Card from Mainview" } );

            DeckList.OnCardChanged += (StoryCard card) =>
            {
                Console.WriteLine( $" Card Changed ---> {card.Title}");
                SelectedCardDetails.Card = card;
            };
        }

        public StoryProjectViewModel DeckList { get; }
        public StoryCardViewModel SelectedCardDetails { get; }
    }
}
