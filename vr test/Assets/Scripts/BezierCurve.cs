using UnityEngine;

public class BezierCurveRenderer : MonoBehaviour
{
    public Transform[] controlPoints;
    public LineRenderer lineRenderer;
    public int vertexCount = 50;

    public Vector3[] Points { get; private set; } // Added to provide access to the points

    private void Start()
    {
        Points = new Vector3[vertexCount];
        DrawCurve();
    }

    private void DrawCurve()
    {
        for (int i = 0; i < vertexCount; i++)
        {
            float t = i / (float)(vertexCount - 1);
            Points[i] = CalculateBezierPoint(t, controlPoints[0].position, controlPoints[1].position, controlPoints[2].position, controlPoints[3].position);
        }
        lineRenderer.positionCount = vertexCount;
        lineRenderer.SetPositions(Points);
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;
        return p;
    }
}