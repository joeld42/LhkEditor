using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LhkEditor.Models
{

    public class StoryDeck
    {
        public string DeckName { get; set; } = "Deck";        
        public ObservableCollection<StoryCard> Cards { get; }
                = new ObservableCollection<StoryCard>();
    }

    public class StoryCard
    {
        public string StoryText { get; set; } = "";
        public string Title { get; set; } = "Untitled";
    }
}