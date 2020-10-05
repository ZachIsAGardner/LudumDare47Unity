using UnityEngine;
using UnityEngine.SceneManagement;

public class CuttableGrass : MonoBehaviour 
{
    private PromptedTrigger promptedTrigger;
    private Animator animator;
    private BoxCollider collider;

    void Start()
    {
        promptedTrigger = GetComponentInChildren<PromptedTrigger>();
        animator = GetComponentInChildren<Animator>();
        collider = GetComponentInChildren<BoxCollider>();

        promptedTrigger.Accepted += (object source, bool hit) => {
            var effect = Game.New(Prefabs.Get("SimpleEffect"));
            effect.transform.position = Game.Player.transform.position;

            animator.SetInteger("State", 1);
            Sound.Play("WoodFalling", true, 0.25f);
            Game.CutGrassCount += 1;
        };
    }

    void Update()
    {
        if (Game.Inventory.Contains("ButterKnife"))
        {
            collider.enabled = true;
        }
        else 
        {
            collider.enabled = false;
        }
    }
}