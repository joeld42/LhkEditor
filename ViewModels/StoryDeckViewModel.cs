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
        public StoryDeckViewModel( IEnumerable<StoryCardModel> cards )
        {
            cards = new ObservableCollection<StoryCardModel>(cards);
        }

        ObservableCollection<StoryCardModel> cards = new ObservableCollection<StoryCardModel>();       
        public ObservableCollection<StoryCardModel> Cards { 
            get 
            {
                return cards;
            }
        }
    }
}