using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[Serializable]
public class modbusVariable{
    public string nameVar;
    public int holdingVar;
}


[Serializable]
public class ConfigInfo
{
    public List<modbusVariable> modbusList;
    //public string nameVar1;
    //public int holdingVar1;
    //public string nameVar2;
    //public int holdingVar2;
    //public string nameVar3;
    //public int holdingVar3;
    //public string nameVar4;
    //public int holdingVar4;
    //public string nameVar5;
    //public int holdingVar5;
    //public string nameVar6;
    //public int holdingVar6;
    //public string nameVar7;
    //public int holdingVar7;
    //public string nameVar8;
    //public int holdingVar8;
    //public string nameVar9;
    //public int holdingVar9;
    //public string nameVar10;
    //public int holdingVar10;
    //public string nameVar11;
    //public int holdingVar11;
    //public string nameVar12;
    //public int holdingVar12;
    //public string nameVar13;
    //public int holdingVar13;
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
