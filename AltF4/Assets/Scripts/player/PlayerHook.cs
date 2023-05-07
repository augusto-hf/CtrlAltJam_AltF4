using UnityEngine;

public class PlayerHook : MonoBehaviour
{
    private PlayerCore player;

    [SerializeField] private Transform hookStarterPoint, hookEndPoint;
    [SerializeField] private float hookSpeed, hookMaxDistance, maxDistance;
    [SerializeField] private LayerMask grapplableMask;


    private Rigidbody2D rb;
    void Start()
    {
        player = GetComponent<PlayerCore>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.O))
        {
            StartHook();
        }
    }
    public void StartHook()
    {
        Vector2 direction = transform.forward;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, grapplableMask);

        if (hit.collider == null)
            return;

    }
}
