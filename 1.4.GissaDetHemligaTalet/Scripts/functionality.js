"use strict"

/* Funktion som hittar textboxen och (om den är enablad) ger den fokus och markerar eventuell text i den,
    annars ges knappen för ny omgång fokus. */
var SecretNumberGuesser = {
    init: function () {
        var newGameButton;
        var textField = document.getElementById("CurrentGuess");

        if (textField.getAttribute('disabled') === true) {
            newGameButton = document.getElementById("RestartButton");
            newGameButton.focus();
        }
        else {
            textField.focus();
            textField.select();
        }
    },
};

window.addEventListener("load", SecretNumberGuesser.init, false);