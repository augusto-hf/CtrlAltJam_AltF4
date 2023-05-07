using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHook : MonoBehaviour
{
    private PlayerCore player;
    private Rigidbody2D rb;
    private LineRenderer line;

    [SerializeField] private float hookSpeed, hookMaxDistance;
    [SerializeField] private LayerMask grapplableMask;

    private Vector2 target;
    private bool isGrappling = false, returningGrapple = false;

    void Awake()
    {
        player = GetComponent<PlayerCore>();
        line = GetComponent<LineRenderer>();
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            StartHook();
        }
        
    }
    public void StartHook()
    {
        Vector2 direction = new Vector2(1,0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, hookMaxDistance, grapplableMask);


        if (hit.collider == null)
            return;
 
        isGrappling = true;
        target = hit.transform.position;
        line.enabled = true;
        line.positionCount = 2;

        StartCoroutine(Grapple());
    }

    IEnumerator Grapple()
    {
        float time = 10;
        Vector2 grapplingEnd = transform.position;

        line.SetPosition(0, transform.position);
        line.SetPosition(1, grapplingEnd);


        for (float t = 0; t < time; t += hookSpeed * Time.deltaTime)
        {
            grapplingEnd = Vector2.Lerp(transform.position, target, t / time);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, grapplingEnd);
            yield return null;
        }

        line.SetPosition(1, target);
        returningGrapple = true;
    }
}
