using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ButterflyManager : MonoBehaviour
{
    public bool used = false;
    public ColorType colorIUnlock;
    [SerializeField] private CutsceneInfo cutscene;
    [SerializeField] private GameObject Visual;
    [SerializeField] private CutsceneManager cutsceneManager;
    [SerializeField] private PostProcessingManager postprocessingManager;
    [SerializeField] private GameObject nameEmotion;
    [SerializeField] private GameObject explosion;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        nameEmotion.gameObject.SetActive(true);

        if (!used)
        {
            playCutscene();
            useButterfly();
            
            PlayerCore player = collision.GetComponent<PlayerCore>();

            player.PickColor(colorIUnlock.ToString());
        }
    }

    public void useButterfly()
    {
        Instantiate(explosion, this.transform.position, Quaternion.identity);
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
