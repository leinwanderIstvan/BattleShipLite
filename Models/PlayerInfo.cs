namespace BattleShipLiteLibrary.Models;

public class PlayerInfo
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? PlayerName { get; set; }

    public List<GridSpotModel> ShotGrid { get; set; } = new List<GridSpotModel>();

    public List<GridSpotModel> ShipGrid { get; set; } = new List<GridSpotModel>();

}