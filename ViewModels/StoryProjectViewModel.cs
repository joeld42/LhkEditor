using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LhkEditor.Models;

namespace LhkEditor.ViewModels
{
    public class StoryProjectViewModel : ViewModelBase
    {
        public StoryCard Card;

        public delegate void CardChangedDelegate(StoryCard card);

        public CardChangedDelegate OnCardChanged;
        
        public StoryProjectViewModel(IEnumerable<StoryDeck> decks)
        {
            Decks = new ObservableCollection<StoryDeck>(decks);

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

        public ObservableCollection<StoryDeck> Decks { get; }
        public ObservableCollection<object> SelectedItems { get; }
    }
}