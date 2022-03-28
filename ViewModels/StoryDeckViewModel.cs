using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using LhkEditor.Models;

// Team -> StoryProject
// Conference -> StoryCard
// 

namespace LhkEditor.ViewModels
{
    public class StoryDeckViewModel : ViewModelBase
    {
        public StoryDeckViewModel( IEnumerable<StoryCard> cards )
        {
            cards = new ObservableCollection<StoryCard>(cards);
        }

        ObservableCollection<StoryCard> cards = new ObservableCollection<StoryCard>();       
        public ObservableCollection<StoryCard> Cards { 
            get 
            {
                return cards;
            }
        }
    }
}