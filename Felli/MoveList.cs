namespace Felli
{
    /// <summary>
    /// A enumerate that will refer to the possibility of movement of a certain
    /// piece at the board. Used primarily for Tile.cs CanMoveBetweenTile() 
    /// method.
    /// </summary>
    public enum MoveList
    {
        Impossible,
        Possible,
        Enemy
    }
}
