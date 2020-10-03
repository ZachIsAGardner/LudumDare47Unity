using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Dialogue : SingleInstance<Dialogue>
{
    public static async Task<TextBox> Begin(TextBoxModel model)
    {
        model.CloseWhenDone = false;

        var prefab = Prefabs.Get("TextBox");
        var go = Game.NewCanvasElement(prefab);
        var textBox = go.GetComponent<TextBox>();
        await textBox.ExecuteAsync(model);
        await new WaitForUpdate();
        return textBox;
    }

    public static async Task<TextBox> Next(TextBox textBox, TextBoxModel model)
    {
        model.CloseWhenDone = false;

        await textBox.ExecuteAsync(model);
        await new WaitForUpdate();
        return textBox;
    }

    public static async Task End(TextBox textBox, TextBoxModel model)
    {
        model.CloseWhenDone = true;
        await textBox.ExecuteAsync(model);
    }

    public static async Task Single(TextBoxModel model)
    {
        model.CloseWhenDone = true;

        var prefab = Prefabs.Get("TextBox");
        var go = Game.NewCanvasElement(prefab);
        var textBox = go.GetComponent<TextBox>();
        await textBox.ExecuteAsync(model);
        await new WaitForUpdate();
    }
}
