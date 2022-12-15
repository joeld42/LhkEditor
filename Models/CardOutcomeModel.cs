using System.Collections.ObjectModel;

namespace LhkEditor.Models;

public class CardOutcomeModel
{
    public string Prompt { get; set; } = "Outcome Prompt";
    
    public ObservableCollection<CardEffectModel> Effects { get; }
        = new ObservableCollection<CardEffectModel>();
}