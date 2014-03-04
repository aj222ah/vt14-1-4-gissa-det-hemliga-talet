using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _1._4.GissaDetHemligaTalet.Functionality
{
    public class SecretNumber
    {
        private int _number;
        private List<int> _previousGuesses;
        private const int MaxNoOfGuesses = 7;
        private const int MinValueAllowed = 1;
        private const int MaxValueAllowed = 100;

        public int Count { get; }
        // Kontrollerar hur många gissningar som gjorts
        public bool GuessAllowed {
            get
            {
                if (_previousGuesses.Count < MaxNoOfGuesses)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public IEnumerable<int> PreviousGuesses { get ; }

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
        }

        // Metod som hanterar gissning och returnerar resultatet
        public string MakeGuess(int guess)
        {
            if (guess >= MinValueAllowed && guess <= MaxValueAllowed)
            {
                if (GuessAllowed)
                {
                    return "Du får gissa";
                }
                else
                {
                    return "Du har redan gjort " +  MaxNoOfGuesses + " gissningar. Starta en ny omgång för att spela igen.";
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException("Talet måste vara mellan 1 och 100!");
            }
        }
    }
}