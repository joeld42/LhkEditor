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
            //db.InitWithSampleDecks();
            db.InitWithGameDecks( "/Users/joeld/Projects/luxe_projects/lighthouse/story/");
            
            DeckList = new StoryProjectViewModel(db.GetAllDecks());
            SelectedCardDetails = new StoryCardViewModel( new StoryCardModel { Title = "ZZMainCard", StoryText = "Card from Mainview" } );

            DeckList.OnCardChanged += (StoryCardModel card) =>
            {
                Console.WriteLine( $" Card Changed ---> {card.Title}");
                SelectedCardDetails.Card = card;
            };
        }

        public StoryProjectViewModel DeckList { get; }
        public StoryCardViewModel SelectedCardDetails { get; }
    }
}
