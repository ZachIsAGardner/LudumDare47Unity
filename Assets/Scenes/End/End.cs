using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class End : MonoBehaviour
{
    public TextMeshProUGUI pleasePress;
    private string currentLetter;
    private float tick = 0;
    private string hex = "ffffff";
    private float tickDestination = 1;
    private List<string> alphabet = new List<string>() { "A", "B", "C", "D", "E", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y" };
    private bool isTransitioning = false;

    void Start()
    {
        currentLetter = alphabet.Random();
        tickDestination = new FloatRange(0.1f, 2.5f).RandomValue();
        hex = ColorUtility.ToHtmlStringRGBA(new Color(
            new FloatRange(0.5f, 1).RandomValue(),
            new FloatRange(0.5f, 1).RandomValue(),
            new FloatRange(0.5f, 1).RandomValue(),
            1
        ));
    }

    async void Update()
    {
        tick += Time.deltaTime;

        if (tick > tickDestination)
        {
            currentLetter = alphabet.Random();
            tick = 0;
            tickDestination = new FloatRange(1.25f, 5f).RandomValue();
            hex = ColorUtility.ToHtmlStringRGBA(new Color(
                new FloatRange(0.5f, 1).RandomValue(),
                new FloatRange(0.5f, 1).RandomValue(),
                new FloatRange(0.5f, 1).RandomValue(),
                1
            ));
        }

        pleasePress.text = $"Please press <color=#{hex}>\"{currentLetter}\"</color> to exit this experience.";

        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(vKey) && vKey.ToString() == currentLetter && !isTransitioning)
            {
                isTransitioning = true;
                var sceneTransition = Instantiate(Prefabs.Get<SceneTransition>("FadeSceneTransition"));
                sceneTransition.Out();

                while (!sceneTransition.DidReachHalfway)
                {
                    await new WaitForUpdate();
                }
                
                Application.Quit();
            }
        }
    }
}
