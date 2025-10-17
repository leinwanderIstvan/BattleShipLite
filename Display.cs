using BattleShipLiteLibrary.Models;

namespace BattleShipLight;

public static class Display
{
    public static void DisplayWelcomeScreen()
    {
        Console.Clear();
        WelcomeMessage();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        PlayerInfo activePlayer = CreatePlayer("Player 1");
        PlayerInfo opponent= CreatePlayer("Player 2");

        PlayerInfo winner = null;

        do
        {
            //Display the grids for player 1
            DisplayShotGrid(activePlayer);
            //Ask player 1 for their shot
            //Determine if its a valid shot
            //Record the shot
            RecordPlayerShot(activePlayer, opponent);
            //Check for win condition
            bool doesGameContinue = GameLogic.PlayerStillActive(opponent);
            // If game is over set player one as winner
            // Else go to player 2
            //Clear the screen


        } while (winner == null);

        Console.Clear();





    }

    private static void RecordPlayerShot(PlayerInfo activePlayer, PlayerInfo opponent)
    {
        //Ask for the shot
        // Determan what row and colum that is  - split it a part 
        // Determan if its a hit or miss
        // Go back to begging if its not valid shot 
        // Determin shot results 
        // Record results on shot grid
        
    }

    private static void DisplayShotGrid(PlayerInfo activePlayer)
    {

        var currentRow = activePlayer.ShotGrid[0].SpotLetter;

        foreach (var gridSpot in activePlayer.ShotGrid)
        {

            if (gridSpot.SpotLetter != currentRow)
            {
                Console.WriteLine();
            }

            currentRow = gridSpot.SpotLetter;

            if (gridSpot.Status == GridSpotStatus.Empty)
            {
                Console.WriteLine($"{gridSpot.SpotLetter} - {gridSpot.SpotNumber}");
            }
            else if (gridSpot.Status == GridSpotStatus.Miss)
            {
                Console.Write(" M ");
            }
            else if (gridSpot.Status == GridSpotStatus.Hit)
            {
                Console.Write(" H ");
            }
            else
            {
                Console.WriteLine("?");
            }


        }
         
    }

    private static void WelcomeMessage()
    {
        Console.WriteLine("Welcome to Battleship Lite!");
        Console.WriteLine("The game is played on a 5x5 grid. Each player has 5 ships.");
        Console.WriteLine();
    }

    private static PlayerInfo CreatePlayer(string playerTitle)
    {
        PlayerInfo output = new PlayerInfo();

        // Get player name
        output.PlayerName = GetUserName();
        // Load up shot Grid 
        GameLogic.InitializeGrid(output);
        // Ask the user for their 5 ship placement
        PlaceShips(output); 
        //Clear
        Console.Clear();

        return output;

    }

    private static string GetUserName()
    {
        string? userName = "";
        while (string.IsNullOrWhiteSpace(userName))
        {
            Console.Write("Please enter your name: ");
            userName = Console.ReadLine();
        }
        return userName;
    }

    private static void PlaceShips(PlayerInfo model)
    {
        do
        {

            Console.Write($"Where do you want to place ship number {model.ShipGrid.Count + 1}: ");

            string? location = Console.ReadLine();

            bool isValid = GameLogic.PlaceShip(model, location);

            if (isValid == false)
            {
                Console.WriteLine("This is not a valid location , pleas try again!");
            }


        } while (model.ShipGrid.Count < 5);
    }

}