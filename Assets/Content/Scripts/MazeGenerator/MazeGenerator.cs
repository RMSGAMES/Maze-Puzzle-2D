using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float sampleSize = 1;

    [Header("References")]
    [SerializeField] private SpriteRenderer sample;

    [HideInInspector] public Node[,] nodes;

    #region Private
    private int[,] map;
    private List<CellingGird> cellingGirds = new List<CellingGird>();
    #endregion

    private void Start()
    {
        CreateMap();
    }

    private void Update()
    {
        if(RefreshMazeButton)
        {
            CreateMap();
        }
    }

    public void Clear()
    {
        cellingGirds.Clear();
        SpriteRenderer[] ren = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < ren.Length; i++)
        {
            DestroyImmediate(ren[i].gameObject);
        }
    }

    public void CreateMap()
    {
        Clear();

        map = Maze.Generate(width, height);
        nodes = new Node[width, height];

        width = Maze.Round(width);
        height = Maze.Round(height);
        //CreateNodes(width, height); //CreateNodes(map.GetLength(0), map.GetLength(1));

        float posX = -sampleSize * width / 2f - sampleSize / 2f;
        float posY = sampleSize * height / 2f - sampleSize / 2f;
        float Xreset = posX;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                posX += sampleSize;

                bool walkable = (map[x, y] == 1) ? true : false;
                nodes[x, y] = new Node(walkable, x, y);

                SpriteRenderer clone = Instantiate(sample, new Vector3(posX, posY, 0), Quaternion.identity, transform) as SpriteRenderer;

                CellingGird cellingGird = new CellingGird();
                cellingGird.Setup(clone, new Vector2(x, y));
                cellingGirds.Add(cellingGird);

                clone.transform.name = "Block (" + cellingGirds.Count + ")";
                clone.color = (walkable == true) ? Color.white : Color.grey;
            }
            posY -= sampleSize;
            posX = Xreset;
        }
    }

    private void CreateNodes(int t_width, int t_height)
    {
        nodes = new Node[t_width, t_height];

        width = Maze.Round(t_width);
        height = Maze.Round(t_height);
    }

    public System.Collections.IEnumerable GetNeighbours(Node node)
    {
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                var neighbor = AddNodeNeighbour(x, y, node);
                if (neighbor != null) { yield return neighbor; }
            }
        }
    }


    private Node AddNodeNeighbour(int x, int y, Node node)
    {
        if (x == 0 && y == 0)
        {
            return null;
        }

        int checkX = node.gridX + x;
        int checkY = node.gridY + y;

        if (checkX >= 0 && checkX < width && checkY >= 0 && checkY < height)
        {
            return nodes[checkX, checkY];
        }

        return null;
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + (width - 1) / 2) / (width - 1);
        float percentY = Mathf.Abs(worldPosition.y - (height - 1) / 2) / (height - 1);
        int x = Mathf.RoundToInt((width - 1) * percentX);
        int y = Mathf.RoundToInt((height - 1) * percentY);
        return nodes[x, y];
    }

    public void ChangeSpriteColor(Color color, Vector2 pos)
    {
        for (int i = 0; i < cellingGirds.Count; i++)
        {
            if (cellingGirds[i].pos == pos)
            {
                cellingGirds[i].sample.color = color;
            }
        }
    }

    public bool RefreshMazeButton
    {
        get
        {
            return Input.GetKey(KeyCode.R);
        }
    }
}

[SerializeField]
public class CellingGird
{
    public SpriteRenderer sample;
    public Vector2 pos;

    public void Setup(SpriteRenderer t_sample, Vector2 t_pos)
    {
        sample = t_sample;
        pos = t_pos;
    }
}