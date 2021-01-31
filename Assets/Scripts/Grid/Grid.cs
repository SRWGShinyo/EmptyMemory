using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid grid;
    public List<GridCell> allGridCells = new List<GridCell>();
    public bool gameInCourse = true;
    public CanvasGroup victoryPanel;

    int[,] grid_matrix = new int[5,5] { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, 
                                        { 0, 0, 0, 0, 0 } };


    public List<Transform> positionsReset = new List<Transform>();

    private void Awake()
    {
        if (grid == null)
            grid = this;
        else
            Destroy(grid.gameObject);
    }

    private void Start()
    {
        Blocked[] blockades = FindObjectsOfType<Blocked>();
        foreach (Blocked b in blockades)
            grid_matrix[b.x, b.y] = 1;
    }

    private void Update()
    {
        if (gameInCourse)
            CheckVictory();
    }

    private void CheckVictory()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (grid_matrix[i, j] != 1)
                {
                    return;
                }
            }
        }
        PositionPiece.isMoving = false;
        gameInCourse = false;
        Victory();
    }

    private void Victory()
    {
        StartCoroutine(Win());
    }

    private IEnumerator Win()
    {
        victoryPanel.blocksRaycasts = true;
        victoryPanel.interactable = true;
        while (victoryPanel.alpha < 1)
        {
            victoryPanel.alpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(4f);
        Grid.grid = null;
        FindObjectOfType<LevelLoader>().LoadNextLevel();
    }

    public void SnapPieceToGrid(GridCell cell, PositionPiece piece)
    {

        List<(int, int)> positionsToOccupy = new List<(int, int)>();
        (int, int) positions = (cell.line, cell.column);
        for (int i = 0; i < piece.matrix.Count; i++)
        {
            positions.Item1 = i + cell.line;
            for (int j = 0; j < piece.matrix[i].column.Count; j++)
            {
                if (piece.matrix[i].column[j] == 0)
                    continue;

                positions.Item2 = j + cell.column;
                if (positions.Item1 >= 5 || positions.Item2 >= 5 || grid_matrix[positions.Item1, positions.Item2] == 1)
                {
                    Debug.Log(grid_matrix[positions.Item1, positions.Item2]);
                    Debug.Log("Cell is nope " + positions.Item1 + " " + positions.Item2);
                    FindObjectOfType<ShakeCamHandler>().Shake();
                    piece.isSnapped = false;
                    piece.transform.position = FindNewPosition();
                    return;
                }

                positionsToOccupy.Add((positions.Item1, positions.Item2));
            }


            positions.Item2 = cell.column;
        }

        piece.isSnapped = false;
        piece.select.position = new Vector3(cell.transform.position.x, cell.transform.position.y, piece.select.transform.position.z);
        piece.snappedTo = cell;
        SetPositionsToGrid(positionsToOccupy, 1);
        piece.positions = positionsToOccupy;
    }

    private Vector3 FindNewPosition()
    {
        Vector3 newPos = positionsReset[Random.Range(0, positionsReset.Count)].transform.position;
        return new Vector3(newPos.x, newPos.y, -3);
    }

    public void unSnapToGrid(PositionPiece piece, List<(int, int)> positions)
    {
        piece.snappedTo = null;
        piece.positions = null;
        SetPositionsToGrid(positions, 0);
    }

    public void SetPositionsToGrid(List<(int, int)> positions, int value)
    {
        foreach ((int, int) pos in positions)
            grid_matrix[pos.Item1, pos.Item2] = value;
    }
}
