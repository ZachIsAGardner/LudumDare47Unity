using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Story : SingleInstance<Story> 
{
    public static List<string> Names = new List<string>()
    {
        "Larry",
        "Mean Horris",
        "Horris",
        "Jimbob",
        "Patoonya",
        "Stinky Pete",
        "Shtink",
        "Stink",
        "Stank",
        "Shtonk",
        "Plop",
        "Crab Bag",
        "Georgie",
        "Big Daddy",
        "Fun Bucket",
        "Plop o' Choco",
        "Spicy Boi",
        "Spicy Gurl",
        "They Spicy",
        "Pants",
        "Soup Slurper",
        "Shirt",
        "Bucket o' Beans",
        "Bucko",
    };

    public static string Player { get { 
        if (Game.PlayerName.IsNullOrEmpty()) 
        {
            Game.PlayerName = (Game.NarratorName.IsNullOrEmpty()) 
                ? Names.Random()
                : Names.FindAll(n => n != Game.NarratorName).Random();
        }
        return $"<color=#{Game.PlayerColor}>{Game.PlayerName}</color>"; 
    } }

    public static string Narrator { get {
        if (Game.NarratorName.IsNullOrEmpty()) 
        {
            Game.NarratorName = (Game.PlayerName.IsNullOrEmpty()) 
                ? Names.Random()
                : Names.FindAll(n => n != Game.PlayerName).Random();
        }
        return $"<color=#{Game.NarratorColor}>{Game.NarratorName}</color>";
    } }

    public static List<string> Flags = new List<string>() {};
    
    public static async Task Greeting() 
    {
        if (Flags.Contains("Greeting")) return;
        Flags.Add("Greeting");

        var textBox = await Dialogue.Begin(new TextBoxModel(
            text: $"Good morning! It is I your best friend {Narrator}."
        ));

        await Dialogue.Next(textBox, new TextBoxModel(
            text: $"In case you forgot, {Player}, you and I are the bestest of friends. You are truly scrumptious... (They whisper to themselves)",
            speaker: $"{Narrator}"
        ));

        await Dialogue.End(textBox, new TextBoxModel(
            text: "Today I have something wonderful planned for you. Go outside and see my surprise.",
            speaker: $"{Narrator}"
        ));
    }

    public static async Task ButterKnifeGet() 
    {
        if (Flags.Contains("ButterKnifeGet")) return;
        Flags.Add("ButterKnifeGet");

        var textBox = await Dialogue.Begin(new TextBoxModel(
            text: $"What a wondeful butter knife. I made it myself.",
            speaker: $"{Narrator}"
        ));

        await Dialogue.End(textBox, new TextBoxModel(
            text: $"I was hoping you could do some yard work today {Player}. Your new gift should do the trick.",
            speaker: $"{Narrator}"
        ));
    }

    public static async Task DayExcercise() 
    {
        if (Flags.Contains("DayExcercise")) return;
        Flags.Add("DayExcercise");

        var textBox = await Dialogue.Begin(new TextBoxModel(
            text: $"Good morning!",
            speaker: $"{Narrator}"
        ));

        await Dialogue.End(textBox, new TextBoxModel(
            text: "Today I think you should get some exercise! Give me some jumping jacks!",
            speaker: $"{Narrator}"
        ));

        Game.Player.JumpCount = 0;
        Flags.Add("DayExcerciseEnd");
    }

    public static async Task DayTree() 
    {
        if (Flags.Contains("DayTree")) return;
        Flags.Add("DayTree");

        var textBox = await Dialogue.Begin(new TextBoxModel(
            text: $"Good morning!",
            speaker: $"{Narrator}"
        ));

        await Dialogue.End(textBox, new TextBoxModel(
            text: "Today I have another gift for you! It's outside by the trees.",
            speaker: $"{Narrator}"
        ));

        Game.Player.JumpCount = 0;
        Flags.Add("DayTreeEnd");
    }

    public static async Task AxeGet()
    {
        if (Flags.Contains("AxeGet")) return;
        Flags.Add("AxeGet");

        var textBox = await Dialogue.Begin(new TextBoxModel(
            text: $"You got the axe! Wow!",
            speaker: $"{Narrator}"
        ));

        await Dialogue.End(textBox, new TextBoxModel(
            text: "Use that to chop down that tree! The one with that's been getting chewed on by beavers.",
            speaker: $"{Narrator}"
        ));
    }
}