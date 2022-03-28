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
            set
            {
                _card = value; 
                this.RaisePropertyChanged( "Title");
                this.RaisePropertyChanged("StoryText");
            }
        }

        public string Title
        {
            get { return _card.Title;  }
            set
            {
                if (_card.Title != value)
                {
                    _card.Title = value;
                    this.RaisePropertyChanged("Card"); // Is this right? Card itself hasn't changed
                    this.RaisePropertyChanged("Title");
                }
            }
        }

        public string StoryText
        {
            get { return _card.StoryText;  }
            set
            {
                if (_card.StoryText != value)
                {
                    _card.StoryText = value;
                    this.RaisePropertyChanged("Card"); 
                    this.RaisePropertyChanged("StoryText");
                }

            }
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