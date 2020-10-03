using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textProComponent;
    [SerializeField] private TextMeshProUGUI speakerProComponent;
    [SerializeField] private Image nextImage;

    private string displayedText = "";

    /// <summary>
    /// Whether or not the text box is currently crawling text.
    /// </summary>
    public bool IsActive { get; private set; }

    [HideInInspector]
    public bool SkipToEnd;

    /// <summary>Text to be displayed.</summary>
    [TextArea(3, 100)]
    public string Text; 

    /// <summary>Speaker to be displayed.</summary>
    public string Speaker; 

    public int ToneIntervalMax = 3;

    /// <summary>Text box closes automatically once it has finished crawling the text.</summary>
    public bool Auto; 

    /// <summary>Wait time between letters.</summary>
    public float CrawlTime; 

    /// <summary>The noise made when the text is crawling.</summary>
    public string Tone; 

    public bool CloseWhenDone;

    private int toneInterval;

    public bool PlayOnStart = false;

    async void Start()
    {
        if (PlayOnStart) await ExecuteAsync();
    }

    void Update()
    {
        if (Input.GetKeyDown("z"))
            SkipToEnd = true;

        if (textProComponent)
            textProComponent.text = displayedText;
    }

    /// <summary>
    /// Create a new TextBox.
    /// <param name="prefab">The prefab to make a copy of.</param>
    /// <param name="position">The position in which to spawn the new Text Box.</param>
    /// <param name="model">The model in which to generate the data from.</param>
    /// </summary>
    public static TextBox Initialize(
        TextBox prefab,
        Vector2 position,
        TextBoxModel model
    )
    {
        TextBox instance = Game.NewCanvasElement(prefab.gameObject, 4).GetComponent<TextBox>();

        if (model.Text != null)
            instance.Text = model.Text;

        if (model.Speaker != null)
            instance.Speaker = model.Speaker ?? instance.Speaker;

        if (model.Auto != null)
            instance.Auto = model.Auto == true;

        if (model.CrawlTime != null)
            instance.CrawlTime = model.CrawlTime ?? instance.CrawlTime;

        if (model.Tone != null)
            instance.Tone = model.Tone;

        if (model.CloseWhenDone != null)
            instance.CloseWhenDone = model.CloseWhenDone == true;

        if (model.ToneIntervalMax != null)
            instance.ToneIntervalMax = model.ToneIntervalMax.Value;

        return instance;
    }

    /// <summary>
    /// Provide new model and execute.
    /// </summary>
    public async Task ExecuteAsync(TextBoxModel model)
    {
        if (model.Text != null)
            Text = model.Text;

        if (model.Speaker != null)
            Speaker = model.Speaker;

        if (model.Auto != null)
            Auto = model.Auto == true;

        if (model.CrawlTime != null)
            CrawlTime = model.CrawlTime.Value;

        if (model.Tone != null)
            Tone = model.Tone;

        if (model.CloseWhenDone != null)
            CloseWhenDone = model.CloseWhenDone == true;

        if (model.ToneIntervalMax != null)
            ToneIntervalMax = model.ToneIntervalMax.Value;

        await ExecuteAsync();
    }

    /// <summary>
    /// Execute with existing data.
    /// </summary>
    public async Task ExecuteAsync()
    {
        SkipToEnd = false;

        if (speakerProComponent != null)
            speakerProComponent.text = Speaker ?? "";

        if (CrawlTime > 0)
            await DisplayTextAsync(Text);
        else
            displayedText = Text;
    }

    /// <summary>
    /// Close this Text Box.
    /// </summary>
    public void Close()
    {
        Destroy(gameObject);
    }

    // ---

    private async Task DisplayTextAsync(string text)
    {
        print("display text");
        IsActive = true;
        displayedText = "";

        foreach (char letter in text.ToCharArray())
        {
            if (SkipToEnd)
                break;

            displayedText += letter;
            toneInterval--;

            if (Tone != null && toneInterval <= 0)
            {
                // Sound.Play(Tone);
                toneInterval = ToneIntervalMax;
            }

            await WaitAsync(letter);
        }

        displayedText += "\n";
        

        IsActive = false;
        displayedText = Text;

        if (Auto)
        {
            await new WaitForUpdate();
        }
        else
        {
            nextImage.enabled = true;
            while(true) 
            {
                if (Input.GetKeyDown("z")) break;
                await new WaitForUpdate();
            }
        }
        nextImage.enabled = false;

        if (CloseWhenDone)
        {
            Close();
        }
    }

    // Wait an amount of time based on character
    private IEnumerator WaitAsync(char letter)
    {
        switch (letter)
        {
            case '.':
            case '?':
            case '!':
            case ':':
                yield return new WaitForSeconds(CrawlTime * 5.5f);
                break;
            case ',':
            case ';':
                yield return new WaitForSeconds(CrawlTime * 3f);
                break;
            case ' ':
                break;
            default:
                yield return new WaitForSeconds(CrawlTime);
                break;
        }
    }
}