using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windManager : MonoBehaviour
{
    private AreaEffector2D effector;
    private Collider2D collider;
    [SerializeField] private bool iAmHorizontal;
    [SerializeField] private float horizontalRunnableForceMagnitude;
    private float idealForceMagnitude;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        effector = GetComponent<AreaEffector2D>();
        idealForceMagnitude = effector.forceMagnitude;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && iAmHorizontal)
        {
            effector.forceMagnitude = horizontalRunnableForceMagnitude;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && iAmHorizontal)
        {
            effector.forceMagnitude = idealForceMagnitude;
        }
    }
}
