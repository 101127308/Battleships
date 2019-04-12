//using System.Drawing;
using SwinGameSDK;

namespace CodeConversion
{
    /*
    CLASS: Ending Game Controller
    PURPOSE: The EndingGameController is responsible for managing the interactions at the end of a game.
    */
    static class EndingGameController
    {
        /*
        FUNCTION:Draw End Of Game
        PURPOSE: Draw the end of the game screen, shows the win/lose state.
        */
        public static void DrawEndOfGame()
        {
            Rectangle toDraw = new Rectangle();
            string whatShouldIPrint;

            UtilityFunctions.DrawField(GameController.ComputerPlayer.PlayerGrid, GameController.ComputerPlayer, true);
            UtilityFunctions.DrawSmallField(GameController.HumanPlayer.PlayerGrid, GameController.HumanPlayer);

            toDraw.X = 0;
            toDraw.Y = 250;
            toDraw.Width = SwinGame.ScreenWidth();
            toDraw.Height = SwinGame.ScreenHeight();

            if (GameController.HumanPlayer.IsDestroyed)
                whatShouldIPrint = "YOU LOSE!";
            else
                whatShouldIPrint = "-- WINNER --";

            UtilityFunctions.DrawTextLines(whatShouldIPrint, Color.White, Color.Transparent, GameResources.GameFont("ArialLarge"), FontAlignment.AlignCenter, toDraw);
        }

        /*
        FUNCTION:Handle End Of Game Input
        PURPOSE: Handle the input during the end of the game. Any interaction will result in it reading in the highsSwinGame.
        */
        public static void HandleEndOfGameInput()
        {
            if (SwinGame.MouseClicked(MouseButton.LeftButton) || SwinGame.KeyTyped(KeyCode.ReturnKey) || SwinGame.KeyTyped(KeyCode.EscapeKey))
            {
                HighScoreController.ReadHighScore(GameController.HumanPlayer.Score);
                GameController.EndCurrentState();
            }
        }
    }
}
