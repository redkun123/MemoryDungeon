using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI killThisRun;
    [SerializeField] private TextMeshProUGUI roomDiscoverThisRun;
    [SerializeField] private TextMeshProUGUI killTotal;
    [SerializeField] private TextMeshProUGUI roomDiscoverTotal;
    [SerializeField] private TextMeshProUGUI completionRate;
    [SerializeField] private RestartButton restartButton;

    public IEnumerator Init()
    {
        restartButton.gameObject.SetActive(false);
        var save = SaveManager.Instance;
        if (save.CurrentGame == null)
        {
            Debug.Log("Save is null");
            killThisRun.text = "0";
            roomDiscoverThisRun.text = "0";
            killTotal.text = "0";
            roomDiscoverTotal.text = "0";
            completionRate.text = "0%";
        }
        else if (save.CurrentRun == null)
        {
            Debug.Log("Current run save is null");
            killThisRun.text = "0";
            roomDiscoverThisRun.text = "0";
            killTotal.text = save.CurrentGame.killTotal.ToString();
            roomDiscoverTotal.text = save.CurrentGame.roomDiscoverTotal.ToString();
            var totalRoomCount = RunManager.Instance.roomDB.TotalRoomCount();
            float completionRates = int.Parse(roomDiscoverTotal.text) / totalRoomCount;
            completionRate.text = $"{completionRates.ToString()}%";
        }
        else
        {
            ShowStats();
        }
        yield return new WaitForSeconds(3f);
        yield return ShowRestartButton();
    }
    private void ShowStats()
    {
        var save = SaveManager.Instance;
        killThisRun.text = save.CurrentRun.killThisRun.ToString();
        roomDiscoverThisRun.text = save.CurrentRun.roomDiscoverThisRun.ToString();
        killTotal.text = save.CurrentGame.killTotal.ToString();
        roomDiscoverTotal.text = save.CurrentGame.roomDiscoverTotal.ToString();
        var totalRoomCount = RunManager.Instance.roomDB.TotalRoomCount();
        float completionRates = int.Parse(roomDiscoverTotal.text) / totalRoomCount;
        completionRate.text = $"{completionRates.ToString()}%";
    }
    public IEnumerator ShowRestartButton()
    {
        restartButton.gameObject.SetActive(true);
        yield return null;
    }
    private void OnDestroy()
    {
        restartButton.gameObject.SetActive(false);
    }
}
