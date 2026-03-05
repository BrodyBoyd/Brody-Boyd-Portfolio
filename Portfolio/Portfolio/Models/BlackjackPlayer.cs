using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json;

namespace Portfolio.Models
{
    public class BlackjackPlayer
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Bet must be greater than 0")]
        public int Bet { get; set; }


        [Range(0, int.MaxValue, ErrorMessage = "Bet must be greater than 0")]
        public int Balance { get; set; }

        //public List<Dictionary<string, string>> PlayerCards { get; set; } = new(); 

        public List<List<Dictionary<string, string>>> PlayerHands { get; set; } = new();
        public int ActiveHandIndex { get; set; } = 0;
        public List<int> PlayerTotal { get; set; }
        public List<Dictionary<string, string>> DealerCards { get; set; } = new();
        public int DealerTotal { get; set; } = 0;

        public List<Dictionary<string, string>> BaseDeck = new()
{
    // Clubs
    new() { { "Rank", "A" }, { "Suit", "♣" }, { "Value", "11" } },
    new() { { "Rank", "2" }, { "Suit", "♣" }, { "Value", "2" } },
    new() { { "Rank", "3" }, { "Suit", "♣" }, { "Value", "3" } },
    new() { { "Rank", "4" }, { "Suit", "♣" }, { "Value", "4" } },
    new() { { "Rank", "5" }, { "Suit", "♣" }, { "Value", "5" } },
    new() { { "Rank", "6" }, { "Suit", "♣" }, { "Value", "6" } },
    new() { { "Rank", "7" }, { "Suit", "♣" }, { "Value", "7" } },
    new() { { "Rank", "8" }, { "Suit", "♣" }, { "Value", "8" } },
    new() { { "Rank", "9" }, { "Suit", "♣" }, { "Value", "9" } },
    new() { { "Rank", "10" }, { "Suit", "♣" }, { "Value", "10" } },
    new() { { "Rank", "J" }, { "Suit", "♣" }, { "Value", "10" } },
    new() { { "Rank", "Q" }, { "Suit", "♣" }, { "Value", "10" } },
    new() { { "Rank", "K" }, { "Suit", "♣" }, { "Value", "10" } },

    // Diamonds
    new() { { "Rank", "A" }, { "Suit", "♦" }, { "Value", "11" } },
    new() { { "Rank", "2" }, { "Suit", "♦" }, { "Value", "2" } },
    new() { { "Rank", "3" }, { "Suit", "♦" }, { "Value", "3" } },
    new() { { "Rank", "4" }, { "Suit", "♦" }, { "Value", "4" } },
    new() { { "Rank", "5" }, { "Suit", "♦" }, { "Value", "5" } },
    new() { { "Rank", "6" }, { "Suit", "♦" }, { "Value", "6" } },
    new() { { "Rank", "7" }, { "Suit", "♦" }, { "Value", "7" } },
    new() { { "Rank", "8" }, { "Suit", "♦" }, { "Value", "8" } },
    new() { { "Rank", "9" }, { "Suit", "♦" }, { "Value", "9" } },
    new() { { "Rank", "10" }, { "Suit", "♦" }, { "Value", "10" } },
    new() { { "Rank", "J" }, { "Suit", "♦" }, { "Value", "10" } },
    new() { { "Rank", "Q" }, { "Suit", "♦" }, { "Value", "10" } },
    new() { { "Rank", "K" }, { "Suit", "♦" }, { "Value", "10" } },

    // Hearts
    new() { { "Rank", "A" }, { "Suit", "♥" }, { "Value", "11" } },
    new() { { "Rank", "2" }, { "Suit", "♥" }, { "Value", "2" } },
    new() { { "Rank", "3" }, { "Suit", "♥" }, { "Value", "3" } },
    new() { { "Rank", "4" }, { "Suit", "♥" }, { "Value", "4" } },
    new() { { "Rank", "5" }, { "Suit", "♥" }, { "Value", "5" } },
    new() { { "Rank", "6" }, { "Suit", "♥" }, { "Value", "6" } },
    new() { { "Rank", "7" }, { "Suit", "♥" }, { "Value", "7" } },
    new() { { "Rank", "8" }, { "Suit", "♥" }, { "Value", "8" } },
    new() { { "Rank", "9" }, { "Suit", "♥" }, { "Value", "9" } },
    new() { { "Rank", "10" }, { "Suit", "♥" }, { "Value", "10" } },
    new() { { "Rank", "J" }, { "Suit", "♥" }, { "Value", "10" } },
    new() { { "Rank", "Q" }, { "Suit", "♥" }, { "Value", "10" } },
    new() { { "Rank", "K" }, { "Suit", "♥" }, { "Value", "10" } },

    // Spades
    new() { { "Rank", "A" }, { "Suit", "♠" }, { "Value", "11" } },
    new() { { "Rank", "2" }, { "Suit", "♠" }, { "Value", "2" } },
    new() { { "Rank", "3" }, { "Suit", "♠" }, { "Value", "3" } },
    new() { { "Rank", "4" }, { "Suit", "♠" }, { "Value", "4" } },
    new() { { "Rank", "5" }, { "Suit", "♠" }, { "Value", "5" } },
    new() { { "Rank", "6" }, { "Suit", "♠" }, { "Value", "6" } },
    new() { { "Rank", "7" }, { "Suit", "♠" }, { "Value", "7" } },
    new() { { "Rank", "8" }, { "Suit", "♠" }, { "Value", "8" } },
    new() { { "Rank", "9" }, { "Suit", "♠" }, { "Value", "9" } },
    new() { { "Rank", "10" }, { "Suit", "♠" }, { "Value", "10" } },
    new() { { "Rank", "J" }, { "Suit", "♠" }, { "Value", "10" } },
    new() { { "Rank", "Q" }, { "Suit", "♠" }, { "Value", "10" } },
    new() { { "Rank", "K" }, { "Suit", "♠" }, { "Value", "10" } }
};

        public List<Dictionary<string, string>> ChangingDeck = new()
{
    // Clubs
    new() { { "Rank", "A" }, { "Suit", "♣" }, { "Value", "11" } },
    new() { { "Rank", "2" }, { "Suit", "♣" }, { "Value", "2" } },
    new() { { "Rank", "3" }, { "Suit", "♣" }, { "Value", "3" } },
    new() { { "Rank", "4" }, { "Suit", "♣" }, { "Value", "4" } },
    new() { { "Rank", "5" }, { "Suit", "♣" }, { "Value", "5" } },
    new() { { "Rank", "6" }, { "Suit", "♣" }, { "Value", "6" } },
    new() { { "Rank", "7" }, { "Suit", "♣" }, { "Value", "7" } },
    new() { { "Rank", "8" }, { "Suit", "♣" }, { "Value", "8" } },
    new() { { "Rank", "9" }, { "Suit", "♣" }, { "Value", "9" } },
    new() { { "Rank", "10" }, { "Suit", "♣" }, { "Value", "10" } },
    new() { { "Rank", "J" }, { "Suit", "♣" }, { "Value", "10" } },
    new() { { "Rank", "Q" }, { "Suit", "♣" }, { "Value", "10" } },
    new() { { "Rank", "K" }, { "Suit", "♣" }, { "Value", "10" } },

    // Diamonds
    new() { { "Rank", "A" }, { "Suit", "♦" }, { "Value", "11" } },
    new() { { "Rank", "2" }, { "Suit", "♦" }, { "Value", "2" } },
    new() { { "Rank", "3" }, { "Suit", "♦" }, { "Value", "3" } },
    new() { { "Rank", "4" }, { "Suit", "♦" }, { "Value", "4" } },
    new() { { "Rank", "5" }, { "Suit", "♦" }, { "Value", "5" } },
    new() { { "Rank", "6" }, { "Suit", "♦" }, { "Value", "6" } },
    new() { { "Rank", "7" }, { "Suit", "♦" }, { "Value", "7" } },
    new() { { "Rank", "8" }, { "Suit", "♦" }, { "Value", "8" } },
    new() { { "Rank", "9" }, { "Suit", "♦" }, { "Value", "9" } },
    new() { { "Rank", "10" }, { "Suit", "♦" }, { "Value", "10" } },
    new() { { "Rank", "J" }, { "Suit", "♦" }, { "Value", "10" } },
    new() { { "Rank", "Q" }, { "Suit", "♦" }, { "Value", "10" } },
    new() { { "Rank", "K" }, { "Suit", "♦" }, { "Value", "10" } },

    // Hearts
    new() { { "Rank", "A" }, { "Suit", "♥" }, { "Value", "11" } },
    new() { { "Rank", "2" }, { "Suit", "♥" }, { "Value", "2" } },
    new() { { "Rank", "3" }, { "Suit", "♥" }, { "Value", "3" } },
    new() { { "Rank", "4" }, { "Suit", "♥" }, { "Value", "4" } },
    new() { { "Rank", "5" }, { "Suit", "♥" }, { "Value", "5" } },
    new() { { "Rank", "6" }, { "Suit", "♥" }, { "Value", "6" } },
    new() { { "Rank", "7" }, { "Suit", "♥" }, { "Value", "7" } },
    new() { { "Rank", "8" }, { "Suit", "♥" }, { "Value", "8" } },
    new() { { "Rank", "9" }, { "Suit", "♥" }, { "Value", "9" } },
    new() { { "Rank", "10" }, { "Suit", "♥" }, { "Value", "10" } },
    new() { { "Rank", "J" }, { "Suit", "♥" }, { "Value", "10" } },
    new() { { "Rank", "Q" }, { "Suit", "♥" }, { "Value", "10" } },
    new() { { "Rank", "K" }, { "Suit", "♥" }, { "Value", "10" } },

    // Spades
    new() { { "Rank", "A" }, { "Suit", "♠" }, { "Value", "11" } },
    new() { { "Rank", "2" }, { "Suit", "♠" }, { "Value", "2" } },
    new() { { "Rank", "3" }, { "Suit", "♠" }, { "Value", "3" } },
    new() { { "Rank", "4" }, { "Suit", "♠" }, { "Value", "4" } },
    new() { { "Rank", "5" }, { "Suit", "♠" }, { "Value", "5" } },
    new() { { "Rank", "6" }, { "Suit", "♠" }, { "Value", "6" } },
    new() { { "Rank", "7" }, { "Suit", "♠" }, { "Value", "7" } },
    new() { { "Rank", "8" }, { "Suit", "♠" }, { "Value", "8" } },
    new() { { "Rank", "9" }, { "Suit", "♠" }, { "Value", "9" } },
    new() { { "Rank", "10" }, { "Suit", "♠" }, { "Value", "10" } },
    new() { { "Rank", "J" }, { "Suit", "♠" }, { "Value", "10" } },
    new() { { "Rank", "Q" }, { "Suit", "♠" }, { "Value", "10" } },
    new() { { "Rank", "K" }, { "Suit", "♠" }, { "Value", "10" } }
};

        public bool Bust { get; set; } = false;
        public bool CanSplit { get; set; } = false;
        public bool AllHandsStand { get; set; } = false;
        public bool ShowDealerCards { get; set; } = false;

        private static readonly Random random = new();



        public string PlayerHandsJson
        {
            get => JsonSerializer.Serialize(PlayerHands);
            set => PlayerHands = string.IsNullOrEmpty(value)
                ? new List<List<Dictionary<string, string>>>()
                : JsonSerializer.Deserialize<List<List<Dictionary<string, string>>>>(value)!;
        }

        public string DealerCardsJson
        {
            get => JsonSerializer.Serialize(DealerCards);
            set => DealerCards = string.IsNullOrEmpty(value)
                ? new List<Dictionary<string, string>>()
                : JsonSerializer.Deserialize<List<Dictionary<string, string>>>(value)!;
        }

        public string DeckJson
        {
            get => JsonSerializer.Serialize(ChangingDeck);
            set => ChangingDeck = string.IsNullOrEmpty(value)
                ? new List<Dictionary<string, string>>()
                : JsonSerializer.Deserialize<List<Dictionary<string, string>>>(value)!;
        }

        public void GetInitialCards()
        {
            var firstHand = new List<Dictionary<string, string>>();
            AllHandsStand = false;
            ShowDealerCards = false;

            DealerCards = new List<Dictionary<string, string>>();
            if (ChangingDeck.Count < 10)
            {
                ChangingDeck = BaseDeck.Select(card => new Dictionary<string, string>(card)).ToList();
            }
            //Balance = Balance - Bet;
            for (int i = 0; i < 2; i++)
            {
                firstHand?.Add(DrawCard());
                DealerCards.Add(DrawCard());
            }
            PlayerHands = new List<List<Dictionary<string, string>>> { firstHand };
            if (firstHand[0]["Rank"] == firstHand[1]["Rank"])
            {
                CanSplit = true;
            }
            ActiveHandIndex = 0;
        }


        public void Hit()
        {
            var card = DrawCard();

            PlayerHands[ActiveHandIndex].Add(card);

            int handTotal = GetHandTotal(PlayerHands[ActiveHandIndex]);
            if (handTotal > 21)
            {
                if (ActiveHandIndex == PlayerHands.Count - 1)
                {
                    Bust = true;
                    DealerTotal = DealerCards.Sum(card => int.Parse(card["Value"]));

                }
                if (ActiveHandIndex < PlayerHands.Count - 1)
                {
                    ActiveHandIndex++;
                }



            }
        }

        public void DealerPlay()
        {
            DealerTotal = DealerCards.Sum(card => int.Parse(card["Value"]));

            if (DealerTotal < 16)
            {
                DealerCards.Add(DrawCard());
            }
        }

        public bool Stand()
        {
            if (ActiveHandIndex == PlayerHands.Count - 1)
            {
                DealerPlay();
                AllHandsStand = true;
                ShowDealerCards = true;
                for (int i = 0; i < PlayerHands.Count; i++)
                {
                    EvaluateHand(PlayerHands[i]);
                }

            }
            if (PlayerHands[ActiveHandIndex].Sum(card => int.Parse(card["Value"])) > DealerTotal)
            {
                bool handWon = true;
                if (ActiveHandIndex < PlayerHands.Count - 1)
                {
                    ActiveHandIndex++;
                }
                return handWon;

            }
            else if (PlayerHands[ActiveHandIndex].Sum(card => int.Parse(card["Value"])) < DealerTotal)
            {
                bool handWon = false;
                if (ActiveHandIndex < PlayerHands.Count - 1)
                {
                    ActiveHandIndex++;
                }
                return handWon;

            }
            else
            {
                bool handWon = false;
                if (ActiveHandIndex < PlayerHands.Count - 1)
                {
                    ActiveHandIndex++;
                }
                return handWon;
            }
        }

        public string EvaluateHand(List<Dictionary<string, string>> handIndex)
        {
            int playerTotal = GetHandTotal(handIndex);
            DealerTotal = DealerCards.Sum(card => int.Parse(card["Value"]));

            bool playerBust = playerTotal > 21;
            bool dealerBust = DealerTotal > 21;

            // Player busts
            if (playerBust)
            {
                return "Bust";
            }

            // Dealer busts
            else if (dealerBust)
            {
                Balance += Bet * 2;
                return "Win";
            }

            // Blackjack check (optional)
            else if (playerTotal == 21 && handIndex.Count == 2)
            {
                Balance += Bet * 3;
                return "Blackjack";
            }

            // Compare totals
            else if (playerTotal > DealerTotal)
            {
                Balance += Bet * 2;
                return "Win";
            }

            else if (playerTotal < DealerTotal)
            {
                return "Lose";

            }
            else
            {
                Balance += Bet;
                return "Push";

            }
        }

        public Dictionary<string, string> DrawCard()
        {
            if (ChangingDeck == null || ChangingDeck.Count == 0)
                throw new InvalidOperationException("Deck is empty.");

            var index = random.Next(ChangingDeck.Count);
            var card = ChangingDeck[index];
            ChangingDeck.RemoveAt(index);

            return card;
        }

        public static int GetHandTotal(List<Dictionary<string, string>> hand)
        {
            int total = hand.Sum(card => int.Parse(card["Value"]));
            int aceCount = hand.Count(card => card["Rank"] == "A");

            // Reduce Ace values from 11 → 1 as needed
            while (total > 21 && aceCount > 0)
            {
                total -= 10; // convert one Ace from 11 to 1
                aceCount--;
            }

            return total;
        }
    }
}
