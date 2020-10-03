using UnityEngine;
using UnityEngine.UI;

public class Debugger : SingleInstance<Debugger> 
{
    public Text Text; 

    void Update()
    {
        Text.text = $"{Screen.width} {Screen.height}";
    }
}