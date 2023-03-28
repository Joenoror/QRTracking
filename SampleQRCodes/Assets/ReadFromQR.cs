using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[Serializable]
public class ConfigInfo
{
    public string nameVar1;
    public int holdingVar1;
    public string nameVar2;
    public int holdingVar2;
    public string nameVar3;
    public int holdingVar3;

}

public class ReadFromQR : MonoBehaviour
{
    [Tooltip("Busca el nombre del archivo con extensión .json")]
    public string fileName;



    [SerializeField]
    public ConfigInfo configInfo;

    public string path;



    private void Update()
    {
        if(path == String.Empty)
            LoadFromQR();
    }



    [ContextMenu("Save")]
    public void Save()
    {
        string configInfoJson = JsonUtility.ToJson(configInfo);
        string path = fileName + ".json";
        //Fichero
        File.WriteAllText(path, configInfoJson);
    }




    [ContextMenu("Load")]
    public void Load()
    {
        string path = fileName + ".json";
        string configInfoJson = File.ReadAllText(path);
        configInfo = JsonUtility.FromJson<ConfigInfo>(configInfoJson);



        //FindObjectOfType<AlmazaraManager>().Load();
    }

    [ContextMenu("LoadFromQR")]
    public void LoadFromQR()
    {
        if (gameObject.GetComponent<TextMesh>().text != string.Empty)
        {
            path = gameObject.GetComponent<TextMesh>().text;
            configInfo = JsonUtility.FromJson<ConfigInfo>(path);
        }



    }




}
