using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LhkEditor.Models;

namespace LhkEditor.ViewModels
{
    public class StoryProjectViewModel : ViewModelBase
    {
        public StoryCardModel Card;

        public delegate void CardChangedDelegate(StoryCardModel card);

        public CardChangedDelegate OnCardChanged;
        
        public StoryProjectViewModel(IEnumerable<StoryDeckModel> decks)
        {
            Decks = new ObservableCollection<StoryDeckModel>(decks);

            SelectedItems = new ObservableCollection<object>();

            /*
            SelectedItems.CollectionChanged += (sender, args) =>
            {
                Console.WriteLine( $"Deck changed, length now {SelectedItems.Count}");
                StoryCard nextSelectedCard = null;
                for (int i = 0; i < SelectedItems.Count; i++)
                {
                    if (SelectedItems[i] is StoryDeck)
                    {
                        StoryDeck deck = SelectedItems[i] as StoryDeck;
                        Console.WriteLine($"Selected deck {deck.DeckName}");
                    } 
                    else if (SelectedItems[i] is StoryCard)
                    {
                        StoryCard card = SelectedItems[i] as StoryCard;
                        Console.WriteLine($"Selected card {card.Title}");

                        nextSelectedCard = card;
                    }
                }
                
                if (nextSelectedCard != null)
                {
                    // TODO here how to set StoryCardView.Card to nextSelectedCard??
                    Console.WriteLine( $"TODO: Set storyCardView to {nextSelectedCard.Title}");
                    Card = nextSelectedCard;

                    if (OnCardChanged != null)
                    {
                        OnCardChanged( Card );
                    }
                }
            };
            */
            
            
            Console.WriteLine("Hello from storyProject");
            
        }

        public void AddCard()
        {
            Console.WriteLine($"In AddCard: Card is {Card.Title}");
        }

        public ObservableCollection<StoryDeckModel> Decks { get; }
        public ObservableCollection<object> SelectedItems { get; }
    }
}