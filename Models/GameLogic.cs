namespace BattleShipLiteLibrary.Models;

public static class GameLogic
{
    public static void InitializeGrid(PlayerInfo model)
    {
        List<string> letters = new List<string>()
        {
            "A","B","C","D","E"
        };

        List<int> numbers = new List<int>()
        {
            1,2,3,4,5
        };

        foreach (var letter in letters)
        {
            foreach (var number in numbers)
            {
               AddGridSpot(model, letter, number);
            }

        }

    }

    private static void AddGridSpot(PlayerInfo model, string letter, int number)
    {
        GridSpotModel spot = new GridSpotModel
        {
            SpotLetter = letter,
            SpotNumber = number,
            Status = GridSpotStatus.Empty
        };

        model.ShotGrid.Add(spot);
    }


    public static bool PlaceShip(PlayerInfo model, string? location)
    {
        throw new NotImplementedException();
    }
}