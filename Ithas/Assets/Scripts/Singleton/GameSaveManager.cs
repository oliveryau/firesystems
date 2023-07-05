using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager Instance;
    public List<ScriptableObject> objects = new List<ScriptableObject>();
    private Dictionary<string, ScriptableObject> savedObjects = new Dictionary<string, ScriptableObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this); //only destroy script
        SceneManager.sceneLoaded += OnSceneLoaded; //subscribe to OnSceneLoaded()
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; //unsubscribe from OnSceneLoaded()
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "StartMenu")
        {
            Destroy(this.gameObject); //destroy the script if go to startMenu
        }
    }

    public void SaveData(ScriptableObject scriptableObject)
    {
        string key = scriptableObject.GetType().ToString();
        if (savedObjects.ContainsKey(key))
        {
            savedObjects[key] = scriptableObject;
        }
        else
        {
            savedObjects.Add(key, scriptableObject);
        }
    }

    public T LoadData<T>() where T : ScriptableObject
    {
        string key = typeof(T).ToString();
        if (savedObjects.TryGetValue(key, out ScriptableObject scriptableObject))
        {
            return scriptableObject as T;
        }
        else
        {
            return null;
        }
    }
}
