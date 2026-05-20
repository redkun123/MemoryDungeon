using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    [SerializeField] DialogueData dialogueData;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI dialogueSpeaker;
    [SerializeField] StartRunButton startRunButton;
    [SerializeField] GameOverPopup gameOverPopupPrefab;
    private List<DialogueLine> currentStory;
    private string currentText;
    private string currentSpeaker;
    private float typingSpeed = 0.03f;
    private Coroutine typingCoroutine;
    int lineNumber;
    private bool isClicking;
    private bool convoEnded;
    void Awake()
    {
        if (currentStory == null)
        {
            currentStory = new List<DialogueLine>();
        }
        else currentStory.Clear();
        for (int i = 0; i < dialogueData.dialogueList.Count; i++)
        {
            currentStory.Add(dialogueData.dialogueList[i]);
        }
        lineNumber = 0;
        convoEnded = false;
        ProgressStory();
    }
    public void ShowText(string text)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeText(text));
    }
    private void EndConversation()
    {
        if (lineNumber < currentStory.Count - 1)
        {
            return;
        }
        else
        {
            convoEnded = true;
            startRunButton.gameObject.SetActive(true);
        }
    }

    private IEnumerator TypeText(string text)
    {
        dialogueSpeaker.text = currentSpeaker;
        isClicking = true;
        dialogueText.text = "";
        foreach (char letter in text)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isClicking = false;
    }

    public void OnClick()
    {
        if (convoEnded)
        {
            return; 
        }
        if (isClicking)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = currentText;
            isClicking = false;
            return;
        }
        ProgressStory();
    }

    private void ProgressStory()
    {
        EndConversation();
        Debug.Log("Next line");
        currentText = currentStory[lineNumber].dialogue;
        currentSpeaker = currentStory[lineNumber].speaker;
        ShowText(currentText);
        lineNumber++;
    }
    private void GameOver()
    {
        Instantiate(gameOverPopupPrefab);
    }
}