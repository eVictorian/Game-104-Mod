using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public bool levelCompleted = false;

    private void Start()
    {
        /*
        if(winParticles) 
            winParticles.SetActive(false);
        if(winUI) 
            winUI.SetActive(false);
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && levelCompleted)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            /*
            collision.gameObject.GetComponent<Rigidbody2D>().simulated = false;
            if (winParticles) 
                winParticles.SetActive(true);
            Invoke("ShowWinUI", 1.5f);
            */
        }
    }

    void ShowWinUI()
    {
        /*
        if (winUI) 
            winUI.SetActive(false); winUI.SetActive(true);
        */
    }
}
