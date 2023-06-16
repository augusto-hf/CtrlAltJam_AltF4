using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEffect : MonoBehaviour
{

    [SerializeField] private GameObject solidColor;
    [SerializeField] private float timeToSpawn;
    [SerializeField] private float speed;
    [SerializeField] private Color blueColor;
    [SerializeField] private Color orangeColor;

    private List<GameObject> pool = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
