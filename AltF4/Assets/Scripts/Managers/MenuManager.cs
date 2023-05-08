using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPrincipal;
    [SerializeField] private GameObject menuGaming;
    
    public void ChangeMenu(bool active)
    {
        menuPrincipal.SetActive(!active);
        menuGaming.SetActive(active);
    }
}
