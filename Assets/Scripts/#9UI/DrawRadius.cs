using UnityEngine;

public class DrawRadius : MonoBehaviour
{
    public LineRenderer line;
    private float range = 8;
    private int subdivisions = 60;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        DrawAttackRange();
    }
    private void DrawAttackRange()
    {
        float angleStep = 2f * Mathf.PI / subdivisions;

        line.positionCount = subdivisions;

        for (int i = 0; i < subdivisions; i++)
        {
            float x = range * Mathf.Cos(angleStep * i);
            float z = range * Mathf.Sin(angleStep * i);

            Vector3 pointInCircle = new Vector3(transform.position.x + x, 0.5f
                , transform.position.z + z);
            line.SetColors(Color.red, Color.red);

            line.SetPosition(i, pointInCircle);
        }
    }

}
