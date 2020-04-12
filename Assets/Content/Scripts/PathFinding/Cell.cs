using UnityEngine;

public class Cell : MonoBehaviour
{
    public int x;
    public int y;

    public Cell()
    {
        x = 0;
        y = 0;
    }

    public Cell(int t_x, int t_y)
    {
        x = t_x;
        y = t_y;
    }

    public Cell(Cell t_cell)
    {
        x = t_cell.x;
        y = t_cell.y;
    }

    public override int GetHashCode()
    {
        return x ^ y;
    }

    public override bool Equals(object obj)
    {
        Cell a = (Cell)obj;

        if(ReferenceEquals(null, a))
        {
            return false;
        }

        return x == a.x && y == a.y;
    }

    public bool Equals(Cell t_cell)
    {
        if (ReferenceEquals(null, t_cell))
        {
            return false;
        }

        return x == t_cell.x && y == t_cell.y;
    }

    public static bool operator ==(Cell a, Cell b)
    {
        if(ReferenceEquals(a, b))
        {
            return true;
        }
        if (ReferenceEquals(null, a))
        {
            return false;
        }
        if (ReferenceEquals(null, b))
        {
            return false;
        }

        return a.x == b.x && a.y == b.y;
    }

    public static bool operator !=(Cell a, Cell b)
    {
        return !(a == b);
    }

    public Cell Set(int t_x, int t_y)
    {
        this.x = t_x;
        this.y = t_y;
        return this;
    }
}