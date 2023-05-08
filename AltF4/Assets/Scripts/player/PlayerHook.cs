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

    private Vector2 hitLocation, grapplingEnd;
    private GameObject targetObject;
    private bool isGrappling = false, retractingGrapple = false, startedGrappling = false, finishedGrappling = false;

    void Awake()
    {
        player = GetComponent<PlayerCore>();
        line = GetComponent<LineRenderer>();
    }
    void Update()
    {
        if (player.Input.TongueButton && !retractingGrapple)
        {
            StartHook();
        }

        if (retractingGrapple)
        {
            line.SetPosition(0, transform.position);
            if (!player.Input.TongueButton && !finishedGrappling)
            {
                finishedGrappling = true;
                StartCoroutine(returnGrapple());               
            }

            
            if (targetObject.CompareTag("GrappableObject") && !finishedGrappling)
            {
                dragGrappableObject();
            }
        }
    }
    public void StartHook()
    {
        Vector2 direction = new Vector2(1,0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, hookMaxDistance, grapplableMask);
        

        if (hit.collider == null)
            return;

        hitLocation = hit.point;
        isGrappling = true;
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
    IEnumerator Grapple()
    {
        Debug.Log("Joguei");
        float time = 10;
        startedGrappling = true;

        line.SetPosition(0, transform.position);
        grapplingEnd = transform.position;

        for (float t = 0; t < time; t += hookSpeed * Time.deltaTime)
        {
            grapplingEnd = Vector2.Lerp(transform.position, hitLocation, t / time);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, grapplingEnd);
            yield return null;
        }
        startedGrappling = false;
        retractingGrapple = true;
    }
    IEnumerator returnGrapple()
    {
        Debug.Log("Voltou");
        float time = 10;

        grapplingEnd = hitLocation;

        for (float t = 0; t < time; t += hookSpeed * Time.deltaTime)
        {
            grapplingEnd = Vector2.Lerp(hitLocation, transform.position, t / time);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, grapplingEnd);
            
            yield return null;
        }
        if (Vector2.Distance(transform.position, grapplingEnd) < 0.5f)
        {
            retractingGrapple = false;
            isGrappling = false;
            line.enabled = false;
            finishedGrappling = false;
        }

    }
}
