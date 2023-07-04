using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coinscore : MonoBehaviour
{
    Text Cointext;
    public static int score; 
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        Cointext = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Cointext.text = score.ToString();
    }
}
