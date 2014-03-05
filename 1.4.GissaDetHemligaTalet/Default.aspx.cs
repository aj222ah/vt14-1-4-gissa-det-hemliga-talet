using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _1._4.GissaDetHemligaTalet.Functionality;

namespace _1._4.GissaDetHemligaTalet
{
    public partial class Default : System.Web.UI.Page
    {
        private SecretNumber _secretNumber;

        public SecretNumber SecretNumber
        {
            get {
                if (_secretNumber == null)
                {
                    _secretNumber = Session["SecretNumber"] as SecretNumber;
                    if (_secretNumber == null)
                    {
                        _secretNumber = new SecretNumber();
                        Session["SecretNumber"] = _secretNumber;
                    }
                }
                return _secretNumber;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GuessButton_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                var inputValue = int.Parse(CurrentGuess.Text);
                var guessResult = SecretNumber.MakeGuess(inputValue);

                // Kontrollerar resultatet av gissningen
                switch (guessResult.ToString())
                {
                    case "Low":
                        ResultLabel.Text = "Din gissning är lägre än det hemliga talet.";
                        break;
                    case "Correct":
                        ResultLabel.Text = "Du har gissat rätt. Det tog dig " + SecretNumber.Count + " gissningar.";
                        CurrentGuess.Enabled = false;
                        GuessButton.Enabled = false;
                        RestartButton.Visible = true;
                        break;
                    case "High":
                        ResultLabel.Text = "Din gissning är högre än det hemliga talet.";
                        break;
                    case "NoMoreGuesses":
                        ResultLabel.Text = "Du har gissat 7 gånger och har inga gissningar kvar.";
                        CurrentGuess.Enabled = false;
                        GuessButton.Enabled = false;
                        RestartButton.Visible = true;
                        break;
                    case "PreviousGuess":
                        ResultLabel.Text = "Du har redan gissat på det här numret.";
                        break;
                }

                // Kontrollera om det finns tidigare gissningar
                if (SecretNumber.Count > 0 && guessResult.ToString() != "Correct")
                {
                    // Samla gissningarna i en sträng och visa den
                    IEnumerable<int> prevGuesses = SecretNumber.PreviousGuesses;
                    var prevGuessesString = "Tidigare gissningar: ";

                    foreach (var i in prevGuesses)
                    {
                        prevGuessesString += String.Format("{0} ", i);
                    }

                    PreviousGuessesLabel.Text = prevGuessesString;
                    PreviousGuessesLabel.Visible = true;
                }

                ResultLabel.Visible = true;
            }
        }

        protected void RestartButton_Click(object sender, EventArgs e)
        {
            // Nollställ SecretNumber
            SecretNumber.Initialize();

            // Återställ formuläret
            PreviousGuessesLabel.Text = "";
            PreviousGuessesLabel.Visible = true;
            ResultLabel.Text = "";
            ResultLabel.Visible = true;
            RestartButton.Visible = false;
            CurrentGuess.Enabled = true;
            GuessButton.Enabled = true;

        }
    }
}