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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ENCOSTO EM MIM");

        if (!used)
        {
            playCutscene();
            useButterfly();
            
            PlayerCore player = collision.GetComponent<PlayerCore>();

            player.PickColor(colorIUnlock.ToString().ToLower());
        }
    }

    public void useButterfly()
    {
        used = true;
        Visual.active = false;
        GetComponent<Collider2D>().enabled = false;
        MusicManager.Instance.TriggerMusicLayer(colorIUnlock);
    }
    private void playCutscene()
    {
        cutsceneManager.loadCutscene(cutscene);
    }
}
