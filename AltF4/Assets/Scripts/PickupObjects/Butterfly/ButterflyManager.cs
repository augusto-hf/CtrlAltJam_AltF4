using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyManager : MonoBehaviour
{
    public bool used = false;
    public ColorType colorIUnlock;
    [SerializeField] private CutsceneInfo cutscene;
    [SerializeField] private GameObject Visual;
    [SerializeField] private CutsceneManager cutsceneManager;

    private void Awake()
    {
        if (used)
            useButterfly();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ENCOSTO EM MIM");
        if (!used)
        {
            playCutscene();
            useButterfly();
        }
    }

    private void useButterfly()
    {
        MusicManager.Instance.FadeOutVolume();
        Visual.active = false;
        GetComponent<Collider2D>().enabled = false;
    }
    private void playCutscene()
    {
        cutsceneManager.loadCutscene(cutscene);
    }
}
