using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryDrawer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private int resolution = 30;
    private List<Vector3> linePoints = new List<Vector3>(); 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PathDrawing(float Vx, float Vy, Vector3 initPos)
    {
        float total_t = 2 * Vy / 9.81f;
        float step = total_t / resolution;
        linePoints.Clear();
        for (int i = 0; i < resolution; i++)
        {
            float t = i * step;
            Vector3 displacement = new Vector3(Vx * t, 
                                               Vy * t - 0.5f * Physics.gravity.y * t * t,
                                               0.0f);
            linePoints.Add(-displacement + initPos);
        }
        lineRenderer.positionCount = linePoints.Count;
        lineRenderer.SetPositions(linePoints.ToArray());
    }
}
