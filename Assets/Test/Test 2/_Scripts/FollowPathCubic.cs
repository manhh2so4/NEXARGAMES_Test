using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowPathCubic : MonoBehaviour
{
    //----------- variable path
    [SerializeField] Transform pathcontainer;
    [SerializeField] List<Transform> points;
    public int segmentResolution = 20;
    
    //----------- variable Move
    public float speed = 1.0f;
    public Vector3 velocity;
    public Vector3 Direction;
    [SerializeField] Image image;

    private bool isDone = false;
    private float currentT = 0.0f;
    private int currentCurveIndex = 0;
    private Vector3 p0,p1,p2,p3;

    void Awake()
    {
        foreach (Transform child in pathcontainer)
        {
            points.Add(child);
        }

        if(points.Count < 4)
        {
            Debug.LogError("Cần ít nhất 4 điểm để tạo đường cong cubic!");
            enabled = false;
            return;
        }
    }
    
    void Start()
    {
        p0 = points[currentCurveIndex].position;
        p1 = points[currentCurveIndex + 1].position;
        p2 = points[currentCurveIndex + 2].position;
        p3 = points[currentCurveIndex + 3].position;
    }
    void Update()
    {
        MovoToCurveu();
    }
    void MovoToCurveu(){

        velocity = Vector3.zero;

        if(isDone) return;

        currentT += speed * Time.deltaTime;

        float easedT = Mathf.SmoothStep(0f, 1f, currentT);
        image.fillAmount = easedT;

        if( easedT > 1f ){
            isDone = true;
        }

        Vector3 lastPosition = transform.position;
        transform.position = CalculateCubicBezierPoint( easedT, p0, p1, p2, p3 );
        velocity = (transform.position - lastPosition) / Time.deltaTime;
        if( velocity.sqrMagnitude > 0.01f ){
            Direction = velocity.normalized;
        }
        
    }
    
    Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        if(t > 1) t=1;
        
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

    void OnDrawGizmos()
    {
        if(points.Count < 4) return;
        
        Gizmos.color = Color.green;
        
        // Vẽ các điểm
        foreach(Transform point in points)
        {
            if(point != null)
                Gizmos.DrawSphere(point.position, 0.1f);
        }
        
        Gizmos.color = Color.white;

        Vector3 lastPoint = p0;
        for(int j = 1; j <= segmentResolution; j++)
        {
            float t = (float)j / segmentResolution;
            Vector3 point = CalculateCubicBezierPoint(t, p0, p1, p2, p3);
            Gizmos.DrawLine(lastPoint, point);
            lastPoint = point;
        }
        
    }
}
