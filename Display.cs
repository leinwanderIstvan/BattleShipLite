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
        PlayerInfo player1 = CreatePlayer("Player 1");
        PlayerInfo player2 = CreatePlayer("Player 2");
        Console.Clear();

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