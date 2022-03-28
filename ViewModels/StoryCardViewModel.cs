using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LhkEditor.Models;
using ReactiveUI;

namespace LhkEditor.ViewModels
{
    public class StoryCardViewModel : ViewModelBase
    {
        private StoryCard _card;

        public StoryCard Card
        {
            get { return _card;  }
            set { _card = value; }
        }
        
        public StoryCardViewModel( StoryCard card )
        {
            Console.WriteLine("Hello from SCVM ctor 2");
            Card = card;
        }
        
        public StoryCardViewModel( )
        {
            Console.WriteLine("Hello from SCVM ctor");
            Card = new StoryCard {Title = "ZZTest Card", StoryText = "ZZSimple Card"};
        }
    }
}