using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    /*
    <>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<Variables>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>
    */
    public string fileName;
    public int difficulty = 1;
    public string user;
    public class GameData
    {
        public string nameHolder = "";
        public int score = -1;
        public bool newGame = true;
    }
    public GameData data = new GameData();
    /*
    <>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<Singelton class>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>
    */
    public static GameObject instance;
    void Awake()
    {
        if(instance == null)
        {
            instance = gameObject;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /*
    <>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<Initiate>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>
    */
    private void Start()
    {
        fileName = "savedData.tht";
        GameData loadedData = LoadData(fileName);
        if(loadedData != null)
        {
            this.data = loadedData;
        }
        else
        {
            Debug.Log("null Loaded");
        }
    }
    /*
    <>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<Exitation Sequence>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>
    */
    private void OnApplicationQuit()
    {
        if (this.data.score > -1)
        {
            this.data.newGame = false;
            SaveData(data, fileName);
            Debug.Log("Saved");
        }
        else
        {
            Debug.Log("Nothing To Save");
        }
    }
    /*
    <>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<File Handler>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>
    */
    public void SaveData(GameData data, string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        string dataToSave = JsonUtility.ToJson(data);
        try
        {
            File.WriteAllText(path, dataToSave);
        }
        catch (Exception e)
        {

            Debug.LogError($"Error Saving the data to the path: <{path}> Error data: {e}");
        }
    }

    public GameData LoadData(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        GameData retrieved = null;
        if (File.Exists(path))
        {
            try
            {
                string dataToLoad = File.ReadAllText(path);
                retrieved = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error Loading the data from the path: <{path}> Error data: {e}");
            }
        }
        return retrieved;
    }
}