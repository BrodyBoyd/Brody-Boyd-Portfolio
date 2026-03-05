using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using System.Reflection;
using System.Text.Json;

namespace Portfolio.Controllers
{
    public class BlackjackController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var json = HttpContext.Session.GetString("GameState");

            BlackjackPlayer model;

            if (string.IsNullOrEmpty(json))
            {
                // First visit — create new game
                model = new BlackjackPlayer();
                model.Balance = 1000; // starting balance

                HttpContext.Session.SetString("GameState", JsonSerializer.Serialize(model));
            }
            else
            {
                model = JsonSerializer.Deserialize<BlackjackPlayer>(json);
            }
            return View(model);
        }


        [HttpPost]
        public IActionResult Deal(BlackjackPlayer posted)
        {
            var json = HttpContext.Session.GetString("GameState");

            BlackjackPlayer model;

            if (string.IsNullOrEmpty(json))
            {
                // Safety fallback — should only happen if session expired
                model = new BlackjackPlayer();
                model.Balance = 1000;
            }
            else
            {
                model = JsonSerializer.Deserialize<BlackjackPlayer>(json);
            }

            // Apply posted bet
            model.Bet = posted.Bet;

            // Deduct bet
            model.Balance -= model.Bet;

            // Deal cards
            model.GetInitialCards();
            model.ShowDealerCards = false;
            model.Bust = false;

            // Save updated state
            HttpContext.Session.SetString("GameState", JsonSerializer.Serialize(model));

            return View("Index", model);
        }



        [HttpPost]
        public IActionResult Hit()
        {

            var json = HttpContext.Session.GetString("GameState");

            if (string.IsNullOrEmpty(json))
                return RedirectToAction("Index");

            var blackjack = JsonSerializer.Deserialize<BlackjackPlayer>(json);


            blackjack.Hit();

            // Save updated state
            HttpContext.Session.SetString("GameState", JsonSerializer.Serialize(blackjack));

            return View("Index", blackjack);

        }
        [HttpPost]
        public IActionResult Double()
        {

            var json = HttpContext.Session.GetString("GameState");
            var blackjack = JsonSerializer.Deserialize<BlackjackPlayer>(json);

            blackjack.Balance -= blackjack.Bet;
            blackjack.Bet += blackjack.Bet;
            blackjack.Hit();
            blackjack.Stand();


            // Save updated state
            HttpContext.Session.SetString("GameState", JsonSerializer.Serialize(blackjack));

            return View("Index", blackjack);

        }
        [HttpPost]
        public IActionResult ResetBalance()
        {
            var json = HttpContext.Session.GetString("GameState");

            BlackjackPlayer model;

            var blackjack = JsonSerializer.Deserialize<BlackjackPlayer>(json);
            if (string.IsNullOrEmpty(json))
            {
                // First visit — create new game
                model = new BlackjackPlayer();
                model.Balance = 1000; // starting balance

                HttpContext.Session.SetString("GameState", JsonSerializer.Serialize(model));
            }
            else
            {
                model = JsonSerializer.Deserialize<BlackjackPlayer>(json);
            }

            blackjack.Balance = 1000;

            // Save updated state
            HttpContext.Session.SetString("GameState", JsonSerializer.Serialize(blackjack));

            return View("Index", blackjack);

        }

        [HttpPost]
        public IActionResult Stand()
        {

            var json = HttpContext.Session.GetString("GameState");
            var blackjack = JsonSerializer.Deserialize<BlackjackPlayer>(json);

            blackjack.Stand();

            // Save updated state
            HttpContext.Session.SetString("GameState", JsonSerializer.Serialize(blackjack));

            return View("Index", blackjack);

        }

        [HttpPost]
        public IActionResult Split()
        {

            var json = HttpContext.Session.GetString("GameState");
            var blackjack = JsonSerializer.Deserialize<BlackjackPlayer>(json);

            var hand = blackjack.PlayerHands[blackjack.ActiveHandIndex];

            // Create two new hands
            var hand1 = new List<Dictionary<string, string>> { hand[0], blackjack.DrawCard() };
            var hand2 = new List<Dictionary<string, string>> { hand[1], blackjack.DrawCard() };

            blackjack.PlayerHands = new List<List<Dictionary<string, string>>> { hand1, hand2 };
            blackjack.ActiveHandIndex = 0;

            // Deduct second bet
            blackjack.Balance -= blackjack.Bet;
            HttpContext.Session.SetString("GameState", JsonSerializer.Serialize(blackjack));

            return View("Index", blackjack);
        }
    }
}
