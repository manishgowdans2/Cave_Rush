using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class signDisplay : MonoBehaviour
{

    public GameObject Dialog;
    public Text dialogtext;
    public string sentences;
    private bool inRange=false;
    public float typingspeed;
    private bool isPrinted = false;

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && inRange && !isPrinted)
        {
            Dialog.SetActive(true);
            StartCoroutine(Type());
            isPrinted = true;
            playerMovement.jumpAllowed = false;
        }
        if (inRange == false)
        {
            dialogtext.text = " ";
            Dialog.SetActive(false);
            playerMovement.jumpAllowed = true;
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        { 
            inRange = false;
            isPrinted = false;
            
        }
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences.ToCharArray())
        {
            dialogtext.text += letter;
            yield return new WaitForSeconds(typingspeed);

        }
    }
}
