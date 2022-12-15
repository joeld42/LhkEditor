using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LhkEditor.Models
{
    
    public class StoryCardModel
    {
        public string StoryText { get; set; } = "";
        public string Title { get; set; } = "Untitled";
        
        public ObservableCollection<CardOutcomeModel> Outcomes { get; }
            = new ObservableCollection<CardOutcomeModel>();
        
        public void MakeTestOutcomes()
        {
            Console.WriteLine("Hello from MakeTestPrompts");
            Outcomes.Add( new CardOutcomeModel { Prompt = "Prompt 1" } );
            Outcomes.Add( new CardOutcomeModel { Prompt = "Prompt 2" } );
            Outcomes.Add( new CardOutcomeModel { Prompt = "Prompt 3" } );
        }
    }
}