using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTongueAbility : MonoBehaviour
{

    private PlayerCore player;
    private LineRenderer line;

    [Header("Tongue Variables")]
    [SerializeField] private float tongueSpeed = 120, tongueMaxDistance = 7, baseWidth, minWidth, maxWidth;
    [SerializeField] private LayerMask grapplableMask;
    public bool isTongueOut = false;

    [Header("Drag Object Values")]
    [SerializeField] private float minimumDistanceToPull = 0.2f, pullForce = 25;
    [SerializeField] private string pullableTag = "GrappableObject";

    [Header("Tongue Origin")]
    [SerializeField] private Transform tongueOriginPoint;


    private Vector2 tongueTarget;
    private GameObject targetObject;

    private bool isTongueGoing = false, isTooFarFromObject = false, isTheTargetAObject;

    public bool IsHookedObjectInFront { get => isHookedObjectInFront(); }

    void Awake()
    {
        player = GetComponent<PlayerCore>();
        line = GetComponent<LineRenderer>();
    }


    void Update()
    {
        if (player.Controller.TongueButtonDown && !isTongueOut)
        {
            StartTongue();
        }
    }

    private void FixedUpdate()
    {
        if (isTongueOut)
        {
            float newTongueWidth = baseWidth / Vector2.Distance(line.GetPosition(0), line.GetPosition(1));
            newTongueWidth = Mathf.Clamp(newTongueWidth, minWidth, maxWidth);
            line.startWidth = line.endWidth = newTongueWidth;
        }
    }
    private void StartTongue()
    {
        line.enabled = true;
        Vector2 playerTransform = player.tf.position;
        Vector2 direction = tongueOriginPoint.right;
        RaycastHit2D hit = Physics2D.Raycast(playerTransform, direction, tongueMaxDistance, grapplableMask);
        
        isTheTargetAObject = hit.collider != null;

        tongueTarget = isTheTargetAObject ? hit.point: playerTransform + direction * tongueMaxDistance;

        targetObject = isTheTargetAObject ? hit.collider.gameObject : null;


        StartCoroutine(tongueMovement(tongueTarget));
    }

    private bool isHookedObjectInFront()
    {
        Vector2 direction = (line.GetPosition(1) - player.tf.position);
        if (direction.x >= 0)
            return true;
        else
            return false;
    }

    IEnumerator tongueMovement(Vector2 target)
    {
        isTongueGoing = true;
        isTongueOut = true;
        line.SetPosition(0, tongueOriginPoint.position);

        float time = 10;
        for (float t = 0; t < time; t += tongueSpeed * Time.deltaTime)
        {
            Vector2 tongueEndPoint = Vector2.Lerp(tongueOriginPoint.position, target, t / time);

            line.SetPosition(0, tongueOriginPoint.position);
            line.SetPosition(1, tongueEndPoint);

            yield return null;
        }

        isTongueGoing = false;

        if (isTheTargetAObject)
        {
            Vector2 directionToPull = (transform.position - targetObject.transform.position);
            Debug.Log(targetObject);
            targetObject.GetComponent<Rigidbody2D>().AddForce(directionToPull * pullForce);
        }

        for (float t = 0; t < time; t += tongueSpeed * Time.deltaTime)
        {
            Vector2 tongueEndPoint = Vector2.Lerp(target, tongueOriginPoint.position, t / time);

            line.SetPosition(0, tongueOriginPoint.position);
            line.SetPosition(1, tongueEndPoint);

            yield return null;
        }
        isTongueOut = false;
        line.enabled = false;
    }
}
