using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JetBrains.Annotations;
using LhkEditor.LxParser;
using LhkEditor.Models;

namespace LhkEditor.Services
{
    public class StoryDB
    {
        ObservableCollection<StoryDeckModel> decks = new ObservableCollection<StoryDeckModel>();

        public void InitWithSampleDecks() {
            decks = new ObservableCollection<StoryDeckModel>();
            
            StoryDeckModel forestDeck = new StoryDeckModel { DeckName = "Forest" };
            forestDeck.Cards.Add( new StoryCardModel { Title = "Card A", StoryText = "Card Text A" } );
            forestDeck.Cards.Add( new StoryCardModel { Title = "Forest B", StoryText = "Card Text B" } );

            StoryDeckModel swampDeck = new StoryDeckModel { DeckName = "Swamp" };
            swampDeck.Cards.Add( new StoryCardModel { Title = "Swcard A", StoryText = "Card Text A" } );
            swampDeck.Cards.Add( new StoryCardModel { Title = "Swamp B", StoryText = "Card Text B" } );

            decks.Add( forestDeck );
            decks.Add( swampDeck );
        }
        
        

        public void InitWithGameDecks(string storyFilePath)
        {
            Console.WriteLine( $"In initWithGameDecks {storyFilePath}");
            
            decks = new ObservableCollection<StoryDeckModel>();

            // Go over all the storycard.lx files in the lighthouse base dir
            string[] storyFiles = System.IO.Directory.GetFiles(storyFilePath, "*.storycards.lx", System.IO.SearchOption.AllDirectories);
            foreach (string storyFile in storyFiles)
            {
                // Get filename from path
                string deckName = System.IO.Path.GetFileNameWithoutExtension(storyFile);
                if (deckName == "test")
                {
                    Console.WriteLine( $"Skipping test deck '{deckName}'");
                    continue;
                }
                
                string lxfileText = System.IO.File.ReadAllText(storyFile);
                if ( LxFormatParser.TryParse( lxfileText, out var value, out var error, out var errorPosition))
                {
                    Console.WriteLine("Parsed " + storyFile );

                    // remove .storycards extension
                    deckName = deckName.Substring(0, deckName.Length - 11); 
                    
                    //Print(value);
                    StoryDeckModel deck = DeckFromParsedLx( value, deckName );
                    decks.Add(deck);
                    Console.WriteLine( $"Added deck {deckName}, decks len {decks.Count}");
                }
                else
                {
                    Console.WriteLine( $"Failed to parse {storyFile}...");
                    // TODO find a better way to parse line-by-line
                    // Error context
                    var textLines = lxfileText.Split(new [] { '\r', '\n' });

                    for (int i = 0; i < 4; i++)
                    {
                        int ln = errorPosition.Line - 4 + i;
                        if (ln >= 0)
                        {
                            Console.WriteLine( $"{ln+1,3}: {textLines[ln]}");
                        }
                    }

                    Console.WriteLine($"    {new string(' ', errorPosition.Column)}^");
                    Console.WriteLine(error);
                }

                Console.WriteLine("----\n");
            } 
            
            Console.WriteLine( $"InitWithGameDecks done, read {decks.Count}");
        }
        
        static StoryDeckModel DeckFromParsedLx( object? doc, string title )
        {
            StoryDeckModel deck = new();
            deck.DeckName = title;
            
            // expect an array of objects
            if (doc is object[] cards)
            {
                foreach (object? cardObj in cards)
                {
                    if (cardObj is Dictionary<string, object?> cardDict)
                    {
                        StoryCardModel card = new();
                        foreach (KeyValuePair<string, object?> cardProp in cardDict)
                        {
                            switch (cardProp.Key)
                            {
                                case "title":
                                    card.Title = cardProp.Value as string;
                                    //Console.WriteLine($"Card Title: {card.Title}");
                                    break;
                                
                                case "text":
                                    card.StoryText = cardProp.Value as string;
                                    break;
                                
                                case "options":
                                    AddParsedOutcomes( ref card, cardProp.Value ); 
                                    break;
                                    
                                default:
                                    Console.WriteLine( $"Unknown card property {cardProp.Key}");
                                    break;
                            }
                            
                        }
                        
                        deck.Cards.Add(card);
                    }
                }
            }

            return deck;
        }

        static void AddParsedOutcomes(ref StoryCardModel card, [CanBeNull] object parsedOutcomes)
        {
            if (parsedOutcomes is object[] outcomes)
            {
                foreach (object? outcomeObj in outcomes)
                {
                    if (outcomeObj is Dictionary<string, object?> outcomeDict)
                    {
                        CardOutcomeModel outcome = new();
                        foreach (KeyValuePair<string, object?> outcomeProp in outcomeDict)
                        {
                            switch (outcomeProp.Key)
                            {
                                // case "cmd":
                                //     string cmdString = outcomeProp.Value as string;
                                //     Console.WriteLine( $"Command is {cmdString}");
                                //     break;
                                
                                case "prompt":
                                    outcome.Prompt = outcomeProp.Value as string;
                                    Console.WriteLine($"Card Prompt: {outcome.Prompt}");
                                    break;
                                
                                default:
                                    Console.WriteLine( $"Skipping outcome property {outcomeProp.Key}");
                                    break;
                            }
                        }
                        
                        card.Outcomes.Add(outcome);
                    }
                }
            }
        }
        
        
        static void Print(object? value, int indent = 0)
        {
            switch (value)
            {
                case null:
                    Indent(indent, "Null");
                    break;
                case true:
                    Indent(indent, "True");
                    break;
                case false:
                    Indent(indent, "False");
                    break;
                case double n:
                    Indent(indent, $"Number: {n}");
                    break;
                case string s:
                    Indent(indent, $"String: {s}");
                    break;
                case object[] a:
                    Indent(indent, "Array:");
                    foreach (var el in a)
                        Print(el, indent + 2);
                    break;
                case Dictionary<string, object> o:
                    Indent(indent, "Object:");
                    foreach (var p in o)
                    {
                        Indent(indent + 2, p.Key);
                        Print(p.Value, indent + 4);
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        static void Indent(int amount, string text)
        {
            Console.WriteLine($"{new string(' ', amount)}{text}");
        }

        public ObservableCollection<StoryDeckModel> GetAllDecks() => decks;
    }
}