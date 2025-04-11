using UnityEngine;
using UnityEngine.Animations.Rigging;


public class AnimationZombieHandle : MonoBehaviour {
    // ----------- componet
    [SerializeField] Transform objectTolockAt;
    FollowPathCubic followPath;
    LockDetection lockDetection;
    [SerializeField] MultiAimConstraint headAnim;
    [SerializeField] MultiAimConstraint bodyAnim;
    Animator animator;

    //------------- variable look 
    [SerializeField] float weight = 0;
    float refWeight;
    public float smoothTimeLook = 0.3f;

    //------------- variable move 
    float speed;
    Vector3 localVel;

    void Awake()
    {
        followPath = GetComponent<FollowPathCubic>();
        lockDetection = GetComponent<LockDetection>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        CheckMove();

        if( lockDetection.hasTarget )
        {
            if( weight <= 1) {
                weight = Mathf.SmoothDamp(weight,1,ref refWeight,smoothTimeLook);
            }
            
        }
        else
        {
            if( weight >= 0) {
                weight = Mathf.SmoothDamp(weight,0,ref refWeight,smoothTimeLook);
            }
            
        }
        HeadlookTaget( weight );
        BodylookTaget( weight );


    }
    void CheckMove(){
        localVel = transform.InverseTransformDirection(followPath.velocity);
        speed = localVel.z;
        if( followPath.Direction != Vector3.zero ){
            transform.forward = followPath.Direction;
        }
        animator.SetFloat("Speed",speed);
    }

    void HeadlookTaget(float _weight){
        headAnim.weight = _weight;
    }
    void BodylookTaget(float _weight){
        bodyAnim.weight = _weight;
    }

}