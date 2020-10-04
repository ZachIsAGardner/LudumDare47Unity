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
        "Crap Bag",
        "Siri",
        "Alexa",
        "Georgie",
        "Big Daddy",
        "CrumBucket",
        "Plop o' Choco",
        "Spicy Boi",
        "Spicy Gurl",
        "They Spicy",
        "Pants Tent",
        "Tickle Me",
        "Sparkling Unicorn",
        "xxBitchin_Bad_Boixx",
        "Fuzzy Sex Pants",
        "Pants",
        "Soup Slurper",
        "Shirt",
        "Bucket o' Beans",
    };

    public static string Player => $"<color=#{Game.PlayerColor}>{Game.PlayerName}</color>";
    public static string Narrator => $"<color=#{Game.NarratorColor}>{Game.NarratorName}</color>";
    
    public static async Task Greeting() 
    {
        Game.PlayerName = Names.Random();
        Game.NarratorName = Names.FindAll(n => n != Game.PlayerName).Random();

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
}