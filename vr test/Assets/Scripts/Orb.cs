using UnityEngine;

public class SphereFollower : MonoBehaviour
{
    public BezierCurveRenderer bezierCurveRenderer;
    public float speed = 5f;
    private float t = 0f;

    void Update()
    {
        if (bezierCurveRenderer.Points == null || bezierCurveRenderer.Points.Length == 0)
            return;

        // Update the sphere position along the curve
        t += Time.deltaTime * speed / TotalCurveLength();
        if (t > 1f) t -= 1f; // Loop the movement if it reaches the end of the curve

        Vector3 position = CalculatePosition(t);
        transform.position = position;
    }

    Vector3 CalculatePosition(float t)
    {
        int index = Mathf.FloorToInt(t * (bezierCurveRenderer.Points.Length - 1));
        int nextIndex = (index + 1) % bezierCurveRenderer.Points.Length;
        float localT = (t * (bezierCurveRenderer.Points.Length - 1)) - index;
        return Vector3.Lerp(bezierCurveRenderer.Points[index], bezierCurveRenderer.Points[nextIndex], localT);
    }

    float TotalCurveLength()
    {
        float length = 0f;
        for (int i = 1; i < bezierCurveRenderer.Points.Length; i++)
        {
            length += Vector3.Distance(bezierCurveRenderer.Points[i - 1], bezierCurveRenderer.Points[i]);
        }
        return length;
    }
}