using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endGame : MonoBehaviour
{
    [SerializeField] private FadeScript fadeScript;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            playerMovement.StopAllMovement();
            StartCoroutine(WaitForCredits());
        }
    }

    IEnumerator WaitForCredits()
    {
        fadeScript.CallFade(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("credits");
    }
}
