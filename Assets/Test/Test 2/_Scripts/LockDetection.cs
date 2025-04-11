using System.Collections.Generic;
using UnityEngine;

public class LockDetection : MonoBehaviour {
    public float viewAngle = 60f;
    public float detectionRange = 5;

    public LayerMask detectableLayer;
    Collider[] Results = new Collider[20];

    //---------- Variable modify target
    public bool hasTarget;
    [SerializeField] Transform Target;
    public float smoothTime = 0.3f;
    private Vector3 nearestPoint ;
    private Vector3 reftVelocity = Vector3.zero; 

    void Update()
    {
        DetectTarget();
        Target.position = Vector3.SmoothDamp( Target.position, nearestPoint,ref reftVelocity, smoothTime);
    }

    void DetectTarget(){

        hasTarget = false;

        int count = Physics.OverlapSphereNonAlloc(this.transform.position + (transform.forward*detectionRange),
        detectionRange, Results, detectableLayer);

        if(count <= 0) return;
        float minDistance = float.MaxValue;
        for (int i = 0; i < count; i++)
        {
            Vector3 directionToTarget = Results[i].transform.position - this.transform.position;
            float angle = Vector3.Angle(this.transform.forward, directionToTarget);

            if (angle <= viewAngle / 2f) 
            {
                
                float distance = ( directionToTarget.sqrMagnitude );
                if(minDistance >  distance ){
                    minDistance = distance;
                    nearestPoint = Results[i].transform.position;
                    hasTarget = true;
                }
            }
        }
    }

    void OnDrawGizmos()
    {

        Gizmos.DrawWireSphere(transform.position + (transform.forward*detectionRange) ,detectionRange);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(nearestPoint,0.15f);

    }
}