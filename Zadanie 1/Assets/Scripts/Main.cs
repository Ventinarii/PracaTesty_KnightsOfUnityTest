using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Main : MonoBehaviour
{
    public List<GameObject> Balls;

    private Camera Camera;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private readonly float RadiusSquare = Mathf.Sqrt(1f);
    private readonly float Force = 100f;
    private GameObject DraggedBall;
    private bool IsHolding;
    void Update()
    {
        var mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 mousePosition2D = mousePosition;
        if (Input.GetMouseButton(0) && !IsHolding)
        {
            IsHolding = true;
            DraggedBall = Balls.AsEnumerable()
                .Where(x => (mousePosition2D - (Vector2)(x.transform.position)).sqrMagnitude <= RadiusSquare)
                .First();
        }
        if (!Input.GetMouseButton(0) && IsHolding)
        {
            IsHolding = false;
            DraggedBall = null;
        }
        if (IsHolding && DraggedBall != null) {
            var Direction = mousePosition - DraggedBall.transform.position;
            Direction = Direction.normalized;
            var ForceVector = Direction * Force;
            DraggedBall.GetComponent<Rigidbody2D>().AddForce(ForceVector);
        }
    }
}
