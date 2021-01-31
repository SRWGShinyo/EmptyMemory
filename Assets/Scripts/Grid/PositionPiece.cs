using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionPiece : MonoBehaviour
{
    [System.Serializable]
    public class VectorStored
    {
        public List<int> column;
    }


    public Transform select;
    public List<SpriteRenderer> graphics;
    public List<VectorStored> matrix = new List<VectorStored>();
    public List<(int, int)> positions = new List<(int, int)>();

    public GridCell snappedTo;
    public bool isSnapped;
    public static bool isMoving;


    public void CastRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, LayerMask.GetMask("Stuff"));
        if (hit.collider != null)
        {

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Grid.grid && !Grid.grid.gameInCourse)
        {
            if (HitColliderRay.selected == this)
                graphics[0].color = Color.white;
            HitColliderRay.selected = null;
            return;
        }

        if (isSnapped)
        {
            Vector2 mousePOsition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePOsition.x, mousePOsition.y, transform.position.z);
        }

        if (Input.GetMouseButtonDown(0) && HitColliderRay.selected == this)
        {
            isMoving = true;
            if (!snappedTo)
                isSnapped = true;
            else
            {
                Debug.Log("Snap1");
                isSnapped = true;
                Grid.grid.unSnapToGrid(this, positions);
            }
        }

        else if (Input.GetMouseButtonDown(1) && isSnapped)
        {
            isMoving = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, LayerMask.GetMask("Cell"));
            Grid.grid.SnapPieceToGrid(FindTheProperCell(), this);
        }
    }

    private GridCell FindTheProperCell()
    {
        GridCell gc = Grid.grid.allGridCells[0];
        float minDist = Mathf.Infinity;
        foreach(GridCell g in Grid.grid.allGridCells)
        {
            float distanceNew = Mathf.Sqrt(Mathf.Pow((select.transform.position.x - g.transform.position.x), 2) + Mathf.Pow((select.transform.position.y - g.transform.position.y), 2));
            if (distanceNew < minDist)
            {
                gc = g;
                minDist = distanceNew;
            }
        }

        return gc;
    }
}
