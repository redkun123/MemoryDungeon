using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInputHandler : MonoBehaviour
{
    [SerializeField] private DialogueController dialogueController;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dialogueController.OnClick();
        }
    }
}