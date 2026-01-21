using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button newGameButton;

    private void Awake()
    {
        newGameButton.onClick.AddListener(OnNewGameClicked);
    }

    private void OnNewGameClicked()
    {
        //GameManager.Instance.NewGame();
        SceneManager.LoadScene("IntroScene");
    }
}
