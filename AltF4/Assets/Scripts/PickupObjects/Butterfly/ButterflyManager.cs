using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyManager : MonoBehaviour
{
    public bool used = false;
    public ColorType colorIUnlock;
    [SerializeField] private CutsceneInfo cutscene;
    [SerializeField] private GameObject VisualAndCollider;
    [SerializeField] private CutsceneManager cutsceneManager;

    private void Awake()
    {
        if (used)
            VisualAndCollider.active = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ENCOSTO EM MIM");
        if (!used)
            useButterfly();
    }

    private void useButterfly()
    {
        cutsceneManager.loadCutscene(cutscene);
        VisualAndCollider.active = false;
    }
}
