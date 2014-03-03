<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_1._4.GissaDetHemligaTalet.Default" ViewStateMode="Disabled"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <link href="/css/basic.css" rel="stylesheet" type="text/css" />
        <title>Gissa det hemliga talet</title>
    </head>
    <body>
        <div id="display">
            <h1>Gissa det hemliga talet!</h1>
            <form id="form1" runat="server">
                <div>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                    <asp:Label ID="InstructionLabel" runat="server" Text="Ange ett tal mellan 1 och 100:  "></asp:Label>

                    <%-- Textbox för gissning - med kontroller av det inmatade värdet. --%>
                    <asp:TextBox ID="CurrentGuess" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="CurrentGuessRequired" runat="server" ErrorMessage="Du måste ange en gissning." ControlToValidate="CurrentGuess" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="CurrentGuessValidNumber" runat="server" ErrorMessage="Din gissning måste vara ett heltal mellan  1 och 100." Display="Dynamic" ControlToValidate="CurrentGuess" Type="Integer" MinimumValue="1" MaximumValue="100"></asp:RangeValidator>
            
                    <asp:Button ID="GuessButton" runat="server" Text="Skicka" OnClick="GuessButton_Click" CssClass="button" /><br />
                    <asp:Label ID="EndResultLabel" runat="server" Text="" Visible="False"></asp:Label>
                    <asp:Label ID="PreviousGuessesLabel" runat="server" Text="" Visible="False"></asp:Label><br />
                    <asp:Button ID="RestartButton" runat="server" Text="Slumpa fram ett nytt hemligt tal" Visible="False" OnClick="RestartButton_Click" CssClass="button" />
                </div>
            </form>
        </div>
    </body>
</html>
