using System.Collections.Generic;
using System.Collections.ObjectModel;

using LhkEditor.Models;

namespace LhkEditor.Services
{
    public class StoryDB
    {
        ObservableCollection<StoryDeck> decks = new ObservableCollection<StoryDeck>();

        public void InitWithSampleDecks() {
            decks = new ObservableCollection<StoryDeck>();
            
            StoryDeck forestDeck = new StoryDeck { DeckName = "Forest" };
            forestDeck.Cards.Add( new StoryCard { Title = "Card A", StoryText = "Card Text A" } );
            forestDeck.Cards.Add( new StoryCard { Title = "Forest B", StoryText = "Card Text B" } );

            StoryDeck swampDeck = new StoryDeck { DeckName = "Swamp" };
            swampDeck.Cards.Add( new StoryCard { Title = "Swcard A", StoryText = "Card Text A" } );
            swampDeck.Cards.Add( new StoryCard { Title = "Swamp B", StoryText = "Card Text B" } );

            decks.Add( forestDeck );
            decks.Add( swampDeck );
        }

        public ObservableCollection<StoryDeck> GetAllDecks() => decks;
    }
}