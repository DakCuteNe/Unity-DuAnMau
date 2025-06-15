using System.IO;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class SaveController : MonoBehaviour
{
    private string savePath;
    private InventoryController inventoryController;
    private InventorySaveData inventorySaveData;
    public TMP_InputField nameInputField;

    void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "saveData.json");
        inventoryController = FindObjectOfType<InventoryController>();

        LoadGame();
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
        data.inventorySaveData = inventoryController.GetInventoryItem();

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
        inventoryController.SetInventoryItem(data.inventorySaveData);   
    }
}
