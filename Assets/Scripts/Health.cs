using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int health;
    public int currentHealth;
    public int noOfHealthBars;

    public Image[] bars;
    private void Start()
    {
       
        currentHealth = health;
    }
    private void Update()
    {
        for (int i = 0; i < bars.Length; i++)
        {
            if (i < currentHealth)
            {
                bars[i].enabled = true;
            }
            else
            {
                bars[i].enabled = false;
            }
        }
    }

    public void takeDamage()
    {
        currentHealth--;
        
        if (currentHealth <= 0)
        {
            var playerMove = gameObject.GetComponent<playerMovement>();
            playerMove.canMove = false;
            Invoke("GameOver", 1.5f);
        }
    }

    void GameOver()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(2);

    }


}
