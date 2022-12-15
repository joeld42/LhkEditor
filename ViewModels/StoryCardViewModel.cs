using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LhkEditor.Models;
using ReactiveUI;

namespace LhkEditor.ViewModels
{
    public class StoryCardViewModel : ViewModelBase
    {
        
        
        private CardOutcomeModel _selectedOutcome;
        public CardOutcomeModel SelectedOutcome
        {
            get { return _selectedOutcome; }
            set
            {
                _selectedOutcome = value;
                Console.WriteLine( $"SVCM: selected outcome: {_selectedOutcome?.Prompt}" );

                this.RaisePropertyChanged( "SelectedCardOutcome");
            }
        }

        public ObservableCollection<CardEffectModel> SelectedEffects
        {
            get
            {
                if (_selectedOutcome != null)
                {
                    return _selectedOutcome.Effects;
                } else {
                    return new ObservableCollection<CardEffectModel>();
                }
                
            }
        }

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
                this.RaisePropertyChanged( "Outcomes");
                
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

        public ObservableCollection<CardOutcomeModel> Outcomes
        {
            get
            {
                Console.WriteLine( $"SCVM: In outcomes getter, have {_card.Outcomes.Count} outcomes");
                return _card.Outcomes;
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