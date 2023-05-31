using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHook : MonoBehaviour
{
    private PlayerCore player;
    private Rigidbody2D rb;
    private LineRenderer line;

    [Header("Hook Variables")]
    [SerializeField] private float hookSpeed, hookMaxDistance;
    [SerializeField] private LayerMask grapplableMask;

    [Header("Drag Object Values")]
    [SerializeField] private float minimumDistanceToDrag, dragForce;

    [Header("Hook Starting")]
    [SerializeField] private Transform startingPoint;

    [Header("Valid Targets")]
    [SerializeField] private string dragableObject;
    [SerializeField] private string interactableObject;
    [SerializeField] private string terrain;

    private Vector2 hitLocation, grapplingEnd, directionn;
    private GameObject targetObject;
    private bool retractingGrapple = false, startedGrappling = false, finishedGrappling = false;
    private bool isHittingDragableObject = false, isTooFarFromObject = false;
    private string hittedObjectTag;
    public bool IsHookedObjectInFront { get => isHookedObjectInFront(); }
    void Awake()
    {
        player = GetComponent<PlayerCore>();
        line = GetComponent<LineRenderer>();
    }
    void Update()
    {
        if (player.Controller.TongueButton && !retractingGrapple)
        {
            //AudioManager.audioInstance.PlayAudioClip("Tongue/tongue2");
            StartHook();
        }
    }

    private void FixedUpdate()
    {
        if (retractingGrapple)
        {
            line.SetPosition(0, startingPoint.position);
            isHookedObjectInFront();

            float distanceToObject = Vector2.Distance(transform.position, line.GetPosition(1));

            if (!player.Controller.TongueButton || distanceToObject > hookMaxDistance)
            {
                finishedGrappling = true;
                StartCoroutine(returnGrapple());
            }

            if (hittedObjectTag == dragableObject && !finishedGrappling)
            {
                dragGrappableObject();
            }

            if (hittedObjectTag == interactableObject && !finishedGrappling)
            {
                player.ColorManager.TakeBlobColor(targetObject);
                StartCoroutine(returnGrapple());
            }
        }
    }
    public void StartHook()
    {
        Vector2 direction = new Vector2(player.Controller.LastAxis.x, 0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, hookMaxDistance, grapplableMask);
        if (hit.collider == null)
            return;

        hittedObjectTag = hit.transform.tag;
        hitLocation = hit.point;
        targetObject = hit.transform.gameObject;
        line.enabled = true;
        line.positionCount = 2;

        if(!startedGrappling)
            StartCoroutine(Grapple());
    }
    public void dragGrappableObject()
    {
        line.SetPosition(1, targetObject.transform.position);

        float distanceToTargetObject = Vector2.Distance(transform.position, targetObject.transform.position);
        if (distanceToTargetObject > minimumDistanceToDrag)
        {
            Vector2 direction = (transform.position - targetObject.transform.position);
            targetObject.GetComponent<Rigidbody2D>().AddForce(direction * dragForce);
        }
    }

    private bool isHookedObjectInFront() {
        Vector2 direction = (transform.position - line.GetPosition(1));
        if (direction.x >= 0)
            return true;
        else
            return false;
    }

    IEnumerator Grapple()
    {

        float time = 10;
        startedGrappling = true;

        line.SetPosition(0, startingPoint.position);
        

        for (float t = 0; t < time; t += hookSpeed * Time.deltaTime)
        {
            if(hittedObjectTag == dragableObject || hittedObjectTag == interactableObject)
                grapplingEnd = Vector2.Lerp(transform.position, targetObject.transform.position, t / time);
            else
                grapplingEnd = Vector2.Lerp(transform.position, hitLocation, t / time);
            line.SetPosition(0, startingPoint.position);
            line.SetPosition(1, grapplingEnd);
            yield return null;
        }
        startedGrappling = false;
        retractingGrapple = true;
    }
    IEnumerator returnGrapple()
    {  
        float time = 10;    

        for (float t = 0; t < time; t += hookSpeed * Time.deltaTime)
        {
            if (hittedObjectTag == dragableObject || hittedObjectTag == interactableObject)
                grapplingEnd = Vector2.Lerp(targetObject.transform.position, transform.position, t / time);
            else
                grapplingEnd = Vector2.Lerp(hitLocation, transform.position, t / time);
            line.SetPosition(0, startingPoint.position);
            line.SetPosition(1, grapplingEnd);
            
            yield return null;
        }
        if (Vector2.Distance(transform.position, grapplingEnd) < 0.5f)
        {
            retractingGrapple = false;
            line.enabled = false;
            finishedGrappling = false;
            isHittingDragableObject = false;
            hittedObjectTag = null;
        }

    }
}
