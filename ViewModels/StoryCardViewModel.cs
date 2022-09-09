using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LhkEditor.Models;
using ReactiveUI;

namespace LhkEditor.ViewModels
{
    public class StoryCardViewModel : ViewModelBase
    {
        private StoryCardModel _card;
        
        public StoryCardModel Card
        {
            get { return _card;  }
            set
            {
                Console.WriteLine( $"SCVM: card setter {value.Title}");
                
                _card = value; 
                this.RaisePropertyChanged( "Title");
                this.RaisePropertyChanged( "StoryText");
                
            }
        }

        public string Title
        {
            get { return _card.Title;  }
            set
            {
                Console.WriteLine( $"SCVM: card TITLE setter {value}");
                if (_card.Title != value)
                {
                    _card.Title = value;
                    this.RaisePropertyChanged("Title");
                }
            }
        }

        public string StoryText
        {
            get { return _card.StoryText;  }
            set
            {
                Console.WriteLine( $"SCVM: card STORYTEXT setter {value}");
                if (_card.StoryText != value)
                {
                    _card.StoryText = value;
                    this.RaisePropertyChanged("StoryText");
                }

            }
        }
        
        public StoryCardViewModel( StoryCardModel card )
        {
            Console.WriteLine( $"Hello from SCVM ctor 2, Card title is {card.Title}");
            Card = card;
        }
        
        public StoryCardViewModel( )
        {
            Console.WriteLine("Hello from SCVM ctor");
            Card = new StoryCardModel {Title = "ZZTest Card", StoryText = "ZZSimple Card"};
        }
    }
}