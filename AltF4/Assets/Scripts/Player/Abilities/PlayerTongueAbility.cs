using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTongueAbility : MonoBehaviour
{

    private PlayerCore player;
    private LineRenderer line;

    [Header("Hook Variables")]
    [SerializeField] private float tongueSpeed = 120, tongueMaxDistance = 7;
    [SerializeField] private LayerMask grapplableMask;

    [Header("Drag Object Values")]
    [SerializeField] private float minimumDistanceToPull = 0.2f, pullForce = 5;

    [Header("Tongue Origin")]
    [SerializeField] private Transform tongueOriginPoint;


    private Vector2 tongueTarget;
    private GameObject targetObject;

    private bool isTongueOut = false, isTongueGoing = false, isTooFarFromObject = false, isTheTargetAObject;

    public bool IsHookedObjectInFront { get => isHookedObjectInFront(); }

    void Awake()
    {
        player = GetComponent<PlayerCore>();
        line = GetComponent<LineRenderer>();
    }


    void Update()
    {
        if (player.Controller.TongueButton && !isTongueOut)
        {
            StartTongue();
        }
    }

    private void FixedUpdate()
    {
        if (isTongueOut)
        {
            if (isTongueGoing)
            {

            }
            else
            {


            }
        }

    }
    private void StartTongue()
    {
        Vector2 direction = new Vector2(player.Controller.LastAxis.x, 0);
        RaycastHit2D hit = Physics2D.Raycast(player.tf.position, direction, tongueMaxDistance, grapplableMask);
        tongueTarget = hit.point;
        
        isTheTargetAObject = hit.collider != null;

        targetObject = isTheTargetAObject? hit.collider.gameObject : null;

        StartCoroutine(tongueGoing(tongueTarget));
    }
    private void EndTongue()
    {

    }

    private bool isHookedObjectInFront()
    {
        Vector2 direction = (line.GetPosition(1) - player.tf.position);
        if (direction.x >= 0)
            return true;
        else
            return false;
    }

    IEnumerator tongueGoing(Vector2 target)
    {
        isTongueGoing = true;
        line.SetPosition(0, tongueOriginPoint.position);


        float time = 10;
        for (float t = 0; t < time; t += tongueSpeed * Time.deltaTime)
        {
            Vector2 tongueEndPoint = Vector2.Lerp(transform.position, target, t / time);

            line.SetPosition(0, tongueOriginPoint.position);
            line.SetPosition(1, tongueEndPoint);

            yield return null;
        }

        isTongueGoing = false;
    }

    IEnumerator tongueReturning()
    {
        Vector2 target = tongueOriginPoint.position;

        float time = 10;
        for (float t = 0; t < time; t += tongueSpeed * Time.deltaTime)
        {
            Vector2 tongueEndPoint = Vector2.Lerp(transform.position, target, t / time);

            line.SetPosition(0, tongueOriginPoint.position);
            line.SetPosition(1, tongueEndPoint);

            yield return null;
        }
    }

}
