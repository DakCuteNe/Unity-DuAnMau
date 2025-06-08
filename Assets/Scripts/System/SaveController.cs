using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveController : MonoBehaviour
{
    private string savePath;
    public TMP_InputField nameInputField;

    void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "saveData.json");
    }

    public void SaveGame()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found!");
            return;
        }

        SaveData data = new SaveData();
        data.playerPosition = player.transform.position;
        data.mapBoundary = "MapBoundaryName";
        data.playerName = nameInputField != null ? nameInputField.text : "Unkown";

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
        Debug.Log("Game Saved! Player Name: " + data.playerName);
    }

    public void LoadGame()
    {
        if (!File.Exists(savePath))
        {
            Debug.LogWarning("No save file found!");
            return;
        }

        string json = File.ReadAllText(savePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && data != null)
        {
            player.transform.position = data.playerPosition;
        }
        Debug.Log("Game Loaded!");
    }
}
