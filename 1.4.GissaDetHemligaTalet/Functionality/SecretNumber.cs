using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _1._4.GissaDetHemligaTalet.Functionality
{
    public class SecretNumber : System.Web.UI.Page
    {
        private int _number;
        private List<int> _previousGuesses = new List<int>();
        public enum Outcome { Low, Correct, High, NoMoreGuesses, PreviousGuess };

        // Konstanter för antal gissningar och min- och maxnummer för gissning
        private const int MaxNoOfGuesses = 7;
        private const int MinValueAllowed = 1;
        private const int MaxValueAllowed = 100;

        public int AddGuess
        {
            get { return _previousGuesses[_previousGuesses.Count]; }
            private set { _previousGuesses[_previousGuesses.Count] = value; }
        }

        public int? Count { 
            get { return Session["Count"] as int?; }
            private set { Session["Count"] = value; }
        }
        // Kontrollerar hur många gissningar som gjorts
        public bool GuessAllowed {
            get
            {
                if (Count < MaxNoOfGuesses)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<int> PreviousGuesses
        {
            get { return Session["PreviousGuesses"] as List<int>; }
            private set { Session["PreviousGuesses"] = _previousGuesses; }
        }

        // Konstruktor som anropar initialiseringsmetoden
        public SecretNumber()
        {
            Initialize();
        }

        // Metod som initialiserar nytt hemligt tal och rensar eventuella tidigare gissningar
        public void Initialize()
        {
            _number = new Random().Next(MinValueAllowed, MaxValueAllowed);

            if (_previousGuesses != null)
            {
                _previousGuesses.Clear();
            }
            Count = 0;
        }

        // Metod som hanterar gissning och returnerar resultatet
        public Outcome MakeGuess(int guess)
        {
            if (guess >= MinValueAllowed && guess <= MaxValueAllowed)
            {
                return CheckGuess(guess); 
            }
            else
            {
                Count += 1;
                throw new ArgumentOutOfRangeException("Talet måste vara mellan 1 och 100!");
            }
        }

        // Metod som jämför användarens gissning med det hemliga talet och returnerar resultatet
        public Outcome CheckGuess(int guess)
        {
            Outcome result = new Outcome();

            // Är gissning tillåten?
            if (GuessAllowed)
            {
                if (guess == _number)
                {
                    result = Outcome.Correct;
                }
                else if (guess < _number)
                {
                    result = Outcome.Low;
                }
                else if (guess > _number)
                {
                    result = Outcome.High;
                }

                // Loopa igenom tidigare gissningar för att se om anv. redan gissat på samma tal
                if (_previousGuesses != null)
                {
                    IEnumerable<int> guesses = PreviousGuesses;

                    foreach (int i in guesses)
                    {
                        if (guess == i)
                        {
                            result = Outcome.PreviousGuess;
                        }
                    }
                }
            }
            else
            {
                result = Outcome.NoMoreGuesses;
            }

            AddGuess = guess;
            Count += 1;
            return result;
        }
    }
}