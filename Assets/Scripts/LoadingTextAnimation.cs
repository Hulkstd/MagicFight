using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingTextAnimation : MonoBehaviour
{
    Text text;
    int i;

    void Start()
    {
        text = GetComponent<Text>();
        i = 0;
        text.color = Color.yellow;

        InvokeRepeating("ChangeText", 0, 0.75f);
    }

    void ChangeText()
    {
        if(i == 3)
        {
            i = 0;
            text.text = "Now Loading";
        }

        i++;
        text.text += '.';
    }
}
