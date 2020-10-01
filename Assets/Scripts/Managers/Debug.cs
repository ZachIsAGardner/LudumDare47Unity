using UnityEngine;
using UnityEngine.UI;

public class Debug : SingleInstance<Debug> 
{
    public Text Text; 

    void Update()
    {
        Text.text = $"{Screen.width} {Screen.height}";
    }
}