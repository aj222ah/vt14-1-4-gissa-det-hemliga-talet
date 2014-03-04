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
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GuessButton_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (Session["Game"] == null)
                {
                    SecretNumber game = new SecretNumber();
                    Session["Game"] = game;
                }
                
                var currentGame = (SecretNumber)Session["Game"];
                var inputValue = int.Parse(CurrentGuess.Text);
                var guessResult = currentGame.MakeGuess(inputValue);

                switch (guessResult.ToString())
                {
                    case "Low":
                        ResultLabel.Text = "Din gissning är lägre än det hemliga talet.";
                        break;
                    case "Correct":
                        ResultLabel.Text = "Du har gissat rätt. Det tog dig " + currentGame.Count + " gissningar.";
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

                IEnumerable<int> prevGuesses = currentGame.PreviousGuesses;
                var prevGuessesString = "";

                if (prevGuesses != null)
                {
                    foreach (var i in prevGuesses)
                    {
                        prevGuessesString = i + ", ";
                    }
                }

                PreviousGuessesLabel.Text = prevGuessesString;
                PreviousGuessesLabel.Visible = true;
                ResultLabel.Visible = true;
            }

        }

        protected void RestartButton_Click(object sender, EventArgs e)
        {
            var newGame = (SecretNumber)Session["Game"];
            newGame.Initialize();

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