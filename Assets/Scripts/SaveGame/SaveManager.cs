using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    private string runSavePath;
    private string gameSavePath;
    public RunSaveData CurrentRun;
    public GameSaveData CurrentGame;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            runSavePath = Path.Combine(Application.persistentDataPath, "run_save.json");
            gameSavePath = Path.Combine(Application.persistentDataPath, "game_save.json");
            LoadGame();
            LoadRun();
        }
        else
        {
            Destroy(gameObject);
        }
        Debug.Log(Application.persistentDataPath);
    }
    public void SaveRun()
    {
        string json = JsonUtility.ToJson(CurrentRun, true);
        File.WriteAllText(runSavePath, json);
    }

    public void LoadRun()
    {
        if (File.Exists(runSavePath))
        {
            string json = File.ReadAllText(runSavePath);
            CurrentRun = JsonUtility.FromJson<RunSaveData>(json);
        }
        else
        {
            CurrentRun = null;
        }
    }

    public void ClearRun()
    {
        if (File.Exists(runSavePath))
            File.Delete(runSavePath);

        CurrentRun = null;
    }
    public void SaveGame()
    {
        string json = JsonUtility.ToJson(CurrentGame, true);
        File.WriteAllText(gameSavePath, json);
    }

    public void LoadGame()
    {
        if (File.Exists(gameSavePath))
        {
            string json = File.ReadAllText(gameSavePath);
            CurrentGame = JsonUtility.FromJson<GameSaveData>(json);
        }
        else
        {
            CurrentGame = new GameSaveData();
        }
    }
}
