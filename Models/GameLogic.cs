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
        var output = false;

        var (row, colum) = SplitShotIntoRowAndColumn(location ?? "");

        bool isValidPlacement = ValidateGridLocation(row, colum);

        bool isSpotOpen = ValidateShipLocation(model, row, colum);


        if (isValidPlacement && isSpotOpen)
        {
            model.ShipGrid.Add(new GridSpotModel()
            {
                SpotLetter = row.ToUpper(),
                SpotNumber = colum,
                Status = GridSpotStatus.Ship
            }); 

            output = true;
        }

        return output;


    }

    private static bool ValidateShipLocation(PlayerInfo model, string row, int colum)
    {
        bool isValidLocation = true;
        foreach (var ship in model.ShipGrid)
        {
            if (ship.SpotLetter == row.ToUpper() && ship.SpotNumber == colum)
            {
                isValidLocation = false;
            }
        }

        return isValidLocation;
    }

    private static bool ValidateGridLocation(PlayerInfo model, string row, int colum)
    {
        bool isValidLocation = false;
        foreach (var spot in model.ShotGrid)
        {
            if (spot.SpotLetter == row.ToUpper() && spot.SpotNumber == colum)
            {
                isValidLocation = true;
            }
        }
        return isValidLocation;

    }

    public static bool PlayerStillActive(PlayerInfo player)
    {
        bool isACtive = false;

        foreach (var ship in player.ShipGrid)
        {
            if (ship.Status != GridSpotStatus.Sunk)
            {
                isACtive = true;

            }

        }

        return isACtive;
    }

    public static int GetShotCount(PlayerInfo player)
    {
        int shotCount = 0;
        foreach (var shot in player.ShotGrid)
        {
            
            if (shot.Status is GridSpotStatus.Hit or GridSpotStatus.Miss)
            {
                shotCount++;
            }
        }

        return shotCount;

    }

    public static (string row, int column) SplitShotIntoRowAndColumn(string shot)
    {
        
        if (shot.Length != 2)
        {
            throw new ArgumentException("Invalid shot format", shot);
        }


        string row = shot.Substring(0, 1).ToUpper();
        var column = int.Parse(shot.Substring(1, 1));
        return (row, column);

    }

    public static bool ValidateShot(PlayerInfo activePlayer, string row, int column)
    {
        bool isValidShot = false;

        foreach (var spot in activePlayer.ShotGrid)
        {
            if (spot.SpotLetter != row.ToUpper() || spot.SpotNumber != column) continue;
            if (spot.Status == GridSpotStatus.Empty)
            {
                isValidShot = true;
            }

        }

        return isValidShot;

    }

    public static bool IdentifyShotResult(PlayerInfo opponent, string row, int column)
    {
        bool isAHit = false;
        foreach (var ship in opponent.ShipGrid)
        {
            if (ship.SpotLetter == row.ToUpper() && ship.SpotNumber == column)
            {
                isAHit = true;
            }
        }
        return isAHit;

    }

    public static void MarkShotResult(PlayerInfo activePlayer, string row, int column, bool isAHit)
    {
        foreach (var spot in activePlayer.ShotGrid)
        {
            if (spot.SpotLetter == row.ToUpper() && spot.SpotNumber == column)
            {
                spot.Status = isAHit ? GridSpotStatus.Hit : GridSpotStatus.Miss;
            }
        }
    }
}