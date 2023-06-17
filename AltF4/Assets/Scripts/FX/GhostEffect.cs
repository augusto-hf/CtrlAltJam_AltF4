using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEffect : MonoBehaviour
{

    [SerializeField] private GameObject solidColor;
    [SerializeField] private float speed;
    [SerializeField] private Color blueColor;
    [SerializeField] private Color orangeColor;

    private List<GameObject> pool = new List<GameObject>();
    private float timer = 0;

    public void ShowGhostEffect()
    {
        timer += speed * Time.deltaTime;   
        if(timer > 1)
        {
            GetGhost();
            timer = 0;
        }

    }

    private GameObject GetGhost()
    {
        
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].GetComponent<SolidSprite>().ActivateSprite();
                pool[i].GetComponent<SpriteRenderer>().color = blueColor;
                pool[i].transform.position = this.transform.position;
                pool[i].transform.rotation = this.transform.rotation;
                pool[i].GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;

                return pool[i];
            }
        }

        GameObject obj = Instantiate(solidColor, this.transform.position, Quaternion.identity);
        obj.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
        obj.GetComponent<SpriteRenderer>().color = blueColor;
        pool.Add(obj);
        return obj;

    }

}
