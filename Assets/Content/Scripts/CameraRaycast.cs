using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    [SerializeField] private MazeGenerator mazeGenerator;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    #region Private
    private Cell from;
    private Cell to;
    private List<Cell> pathCache = new List<Cell>();

    private Camera camera;
    #endregion

    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (StartPathButton)
        {
            FindPath();
        }

        if(RefreshPathButton)
        {
            RefreshPath();
        }
    }

    private void FindPath()
    {
        pathCache.Clear();
        RaycastHit2D hit = Physics2D.Raycast(camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        Node hitedNode = mazeGenerator.NodeFromWorldPoint(hit.collider.transform.position);
        if (hitedNode.walkable)
        {
            if (!startPoint)
            {
                startPoint = hit.collider.transform;
                from = new Cell(hitedNode.gridX, hitedNode.gridY);

                mazeGenerator.ChangeSpriteColor(Color.green, new Vector2(hitedNode.gridX, hitedNode.gridY));
            }
            else if (!endPoint)
            {
                endPoint = hit.collider.transform;
                to = new Cell(hitedNode.gridX, hitedNode.gridY);

                mazeGenerator.ChangeSpriteColor(Color.red, new Vector2(hitedNode.gridX, hitedNode.gridY));

                if (startPoint && endPoint)
                {
                    List<Cell> path = PathFinding.FindPath(mazeGenerator, from, to);
                    pathCache = path;

                    for (int i = 1; i < path.Count - 1; i++)
                    {
                        mazeGenerator.ChangeSpriteColor(Color.blue, new Vector2(path[i].x, path[i].y));
                    }
                }
            }
        }
    }

    private void RefreshPath()
    {
        startPoint = null;
        endPoint = null;

        for (int i = 0; i < pathCache.Count; i++)
        {
            mazeGenerator.ChangeSpriteColor(Color.white, new Vector2(pathCache[i].x, pathCache[i].y));
        }
    }

    public bool StartPathButton
    {
        get
        {
            return Input.GetMouseButtonDown(0);
        }
    }

    public bool RefreshPathButton
    {
        get
        {
            return Input.GetMouseButtonDown(1);
        }
    }
}