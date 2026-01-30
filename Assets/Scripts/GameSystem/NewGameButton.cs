using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameButton : MonoBehaviour
{
    [SerializeField] private Button _newGameButton;
    public void Awake()
    {
        _newGameButton.onClick.AddListener(OnClickNewGame);
    }
    public void OnClickNewGame()
    {
        GameManager.Instance.NewGame();
    }
}
