using System.Net.Http.Headers;
using Unity.Mathematics;
using UnityEngine;

public class WallClimber : MonoBehaviour
{/*

    public float climbForce;
    public float smallestEdge = 0.25f;
    public float maxAngle = 30;
    public ClimbingSort currentsort;

    public Transform handTrans;
    public Animator animator;
    public Rigidbody rb;
    public float minDistance;
    public LayerMask spotLayer;
    public LayerMask currentSpotLayer;
    public LayerMask CheckLayerReachable;

    private Vector3 targetPoint;
    private Vector3 targetNormal;

    private float lastTime;
    private float beginDistance;
    private RaycastHit hit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckForSpots(Vector3 spotLocation, Vector3 dir, float range, CheckingSort sort)
    {
        bool foundSpot = false;

        if(Physics.Raycast(spotLocation - transform.right * smallestEdge / 2, dir, out hit, range, spotLayer))
        {
            if (Vector3.Distance(handTrans.position, hit.point) > minDistance)
            {
                foundSpot = true;


            }
        }

        if (!foundSpot)
        {
            if (Physics.Raycast(spotLocation + transform.right * smallestEdge / 2, dir, out hit, range, spotLayer))
            {
                if (Vector3.Distance(handTrans.position, hit.point) > minDistance)
                {
                    foundSpot = true;


                }
            }   
        }
        if (!foundSpot)
        {
            if (Physics.Raycast(spotLocation + transform.right * smallestEdge / 2 + transform.forward * smallestEdge, dir, out hit, range, spotLayer))
            {
                if (Vector3.Distance(handTrans.position, hit.point) > minDistance)
                {
                    foundSpot = true;


                }
            }
        }
        if (!foundSpot)
        {
            if (Physics.Raycast(spotLocation - transform.right * smallestEdge / 2 + transform.forward * smallestEdge, dir, out hit, range, spotLayer))
            {
                if (Vector3.Distance(handTrans.position, hit.point) > minDistance)
                {
                    foundSpot = true;


                }
            }
        }
    }

    public void FindSpot(RaycastHit h, CheckingSort sort)
    {
        if(Vector3.Angle(h.normal, Vector3.up) < maxAngle)
        {
            RayInfo ray = new RayInfo();

            if (sort == CheckingSort.Normal)
            {
                ray = GetClosestPoint(h.transform, h.point + new Vector3(0,-0.1f,0), transform.forward / 2.5f);
            }
            else if (sort == CheckingSort.Turning)
            {
                ray = GetClosestPoint(h.transform, h.point + new Vector3(0, -0.1f, 0), transform.forward / 2.5f - transform.right * Input.GetAxis("Horizontal"));
            }
            else if (sort == CheckingSort.Falling)
            {
                ray = GetClosestPoint(h.transform, h.point + new Vector3(0, -0.1f, 0), -transform.forward / 2.5f);
            }

            targetPoint = ray.point;
            targetNormal = ray.normal;
        }
    }

    public RayInfo GetClosestPoint(Transform trans, Vector3 pos, Vector3 dir)
    {
        RayInfo curRay = new RayInfo();

        RaycastHit hit2;

        int oldLayer = trans.gameObject.layer;

        //Ændre Layer
        trans.gameObject.layer = 14;

        if (Physics.Raycast(pos - dir, dir, out hit2, dir.magnitude * 2, currentSpotLayer))
        {
            curRay.point = hit2.point;
            curRay.normal = hit2.normal;

            if (!Physics.Linecast(handTrans.position + transform.rotation * new Vector3(0,0.05f, -0.05f), curRay.point + new Vector3(0,0.5f,0f), out hit2, CheckLayerReachable))
            {
                if(!Physics.Linecast(curRay.point - Quaternion.Euler(new Vector3(0,90,0)) * curRay.normal * 0.35f + 0.1f * curRay.point, curRay.point + Quaternion.Euler(new Vector3(0, 90, 0)) * curRay.normal * 0.35f + 0.1f * curRay.point, out hit2, CheckLayerReachable))
                {
                    if (!Physics.Linecast(curRay.point + Quaternion.Euler(new Vector3(0, 90, 0)) * curRay.normal * 0.35f + 0.1f * curRay.point, curRay.point - Quaternion.Euler(new Vector3(0, 90, 0)) * curRay.normal * 0.35f + 0.1f * curRay.point, out hit2, CheckLayerReachable))
                    {
                        curRay.canGoToPoint = true;
                    }
                    else
                    {
                        curRay.canGoToPoint = false;
                    }
                }
                else
                {
                    curRay.canGoToPoint = false;
                }
            }
            else
            {
                curRay.canGoToPoint = false;
            }
            trans.gameObject.layer = oldLayer;
            return curRay;
        }
        else
        {
            curRay.canGoToPoint = false;
            trans.gameObject.layer = oldLayer;
            return curRay;
        }
    }

    public void MoveTowardsPoint()
    {
        transform.position = Vector3.Lerp(transform.position, (targetPoint - transform.rotation * handTrans.localPosition), Time.deltaTime * climbForce);

        Quaternion lookrotation = Quaternion.LookRotation(-targetNormal);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookrotation, Time.deltaTime * climbForce);

        float distance = Vector3.Distance(transform.position, (targetPoint - transform.rotation * handTrans.localPosition));
        float percent = -9 * (beginDistance - distance) / beginDistance;

        if (distance <= 0.01f && currentsort == ClimbingSort.ClimbingTowardsPoint)
        {
            transform.position = targetPoint - transform.rotation * handTrans.localPosition;
            transform.rotation = lookrotation;

            lastTime = Time.time;
            currentsort = ClimbingSort.Climbing;
        }
        if (distance <= 0.25f && currentsort == ClimbingSort.ClimbingTowardsPlateau)
        {
            transform.position = targetPoint - transform.rotation * handTrans.localPosition;
            transform.rotation = lookrotation;

            lastTime = Time.time;
            currentsort = ClimbingSort.Walking;
        }
    }

    [System.Serializable]
    public enum ClimbingSort
    {
        Walking,
        Jumping,
        Falling,
        Climbing,
        ClimbingTowardsPoint,
        ClimbingTowardsPlateau,
        CheckingForClimbStart
    }
    [System.Serializable]
    public class RayInfo
    {
        public Vector3 point;
        public Vector3 normal;
        public bool canGoToPoint;
    }
    [System.Serializable]
    public enum CheckingSort
    {
        Normal,
        Turning,
        Falling,
    }
*/
}
