using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitColliderRay : MonoBehaviour
{

    public static PositionPiece selected = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CastRay();
    }

    public void CastRay()
    {
        if (PositionPiece.isMoving)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, LayerMask.GetMask("Stuff"));
        if (hit.collider != null)
        {
            if (selected)
                selected.graphics[0].color = Color.white;

            selected = hit.collider.GetComponent<PositionPiece>();
            hit.collider.GetComponent<PositionPiece>().graphics[0].color = Color.blue;
        }

        else
        {
            if (selected)
                selected.graphics[0].color = Color.white;
            selected = null;
        }
    }
}
