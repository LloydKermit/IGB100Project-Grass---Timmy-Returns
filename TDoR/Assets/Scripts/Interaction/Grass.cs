using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour, IInteractable
{
    [SerializeField] private Material GrassGlow;
    [SerializeField] private Material GrassNormal;
    [SerializeField] private Renderer grassRender;
    [SerializeField] private string _prompt;

    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Touching Grass");
        return true;
    }

    public void Start()
    {
        grassRender = this.GetComponent<Renderer>();
    }

    public void Update()
    {
        if (WinLose.canInteract == true)
        {
            grassRender.material = GrassGlow;
        }
        else if (WinLose.canInteract == false)
        {
            grassRender.material = GrassNormal;
        }
    }
}
