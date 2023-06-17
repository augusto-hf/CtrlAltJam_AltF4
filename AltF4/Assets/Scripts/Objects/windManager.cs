using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windManager : MonoBehaviour
{
    private AreaEffector2D effector;
    private Collider2D collider;
    [SerializeField] private bool iAmHorizontal;
    [SerializeField] private float runnableForceMagnitude, runnableDrag;
    private float originalForceMagnitude, originalDrag;
    private PlayerCore player;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        effector = GetComponent<AreaEffector2D>();
        originalForceMagnitude = effector.forceMagnitude;
        originalDrag = effector.drag;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            player = collision.GetComponent<PlayerCore>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && iAmHorizontal)
        {
            if (player.ColorManager.CurrentColor.ColorData.Type == ColorType.Orange && player.Controller.ColorButtonHold && player.Controller.Axis.x != 0) 
            {                
                effector.forceMagnitude = runnableForceMagnitude;
                effector.drag = runnableDrag;
            }
            else
            {
                effector.forceMagnitude = originalForceMagnitude;
                effector.drag = originalDrag;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && iAmHorizontal)
        {
            effector.forceMagnitude = originalForceMagnitude;
            effector.drag = originalDrag;
        }
    }
}
