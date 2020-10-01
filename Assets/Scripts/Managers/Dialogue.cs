using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Dialogue : SingleInstance<Dialogue>
{
    public static async Task<bool> Create(DialogueModel model)
    {
        return true;
    }
}

public class DialogueModel 
{

}
