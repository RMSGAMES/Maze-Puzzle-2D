public class Node
{
    public bool walkable;
    public int gridX;
    public int gridY;
    public float penalty;

    public int gCost;
    public int hCost;
    public Node parent;

    public Node(float _price, int _gridX, int _gridY)
    {
        walkable = _price != 0.0f;
        penalty = _price;
        gridX = _gridX;
        gridY = _gridY;
    }

    public Node(bool _walkable, int _gridX, int _gridY)
    {
        walkable = _walkable;
        penalty = _walkable ? 1f : 0f;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}