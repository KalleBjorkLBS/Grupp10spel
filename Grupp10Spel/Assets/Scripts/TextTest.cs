using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTest : MonoBehaviour
{
    

    private Text UIText;
    public string TextToType = "Hello World";
    public float TimeToType = 3.0f;
    private float textPercentage = 0;

    private bool toType = false;

    void Awake()
    {
        UIText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.H))
        {
            toType = true;
        }
        if(Input.GetKey(KeyCode.J))
        {
            UIText.enabled = false;
        }
        if(toType == true)
        {
            int numberOfLettersToShow = (int)(TextToType.Length * textPercentage);
            UIText.text = TextToType.Substring(0, numberOfLettersToShow);
            textPercentage += Time.deltaTime / TimeToType;
            textPercentage = Mathf.Min(1.0f, textPercentage);
        }

    }
}
