using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LhkEditor.Models;

namespace LhkEditor.ViewModels
{
    public class StoryProjectViewModel : ViewModelBase
    {
        public StoryProjectViewModel(IEnumerable<StoryDeck> decks)
        {
            Decks = new ObservableCollection<StoryDeck>(decks);

            SelectedItems = new ObservableCollection<object>();

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
                }
            };
            
            
            Console.WriteLine("Hello from storyProject");
            
        }

        public ObservableCollection<StoryDeck> Decks { get; }
        public ObservableCollection<object> SelectedItems { get; }
    }
}