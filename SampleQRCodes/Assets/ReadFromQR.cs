using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[Serializable]
public class ModbusVariable{
    public string nameVar;
    public int holdingVar;
}


[Serializable]
public class ConfigInfo
{
    public List<ModbusVariable> modbusList;
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
        //Si el PATH está vacío, es decir, no tenemos información de los nodos del PLC a configurar Y contamos con información del JSON en el QR
        if(path == String.Empty && gameObject.GetComponent<TextMesh>().text != string.Empty)
        {
            LoadFromQR(); //CARGO DESDE EL QR
        }
        else
        {
            Load(); //CARGO EL ARCHIVO POR DEFECTO PREPARADO
        }
        if (FindObjectOfType<UIManager>().configInfo.modbusList.Count == 0 && configInfo.modbusList.Count != 0)
        {
            FindObjectOfType<UIManager>().configInfo = configInfo;
        }
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
        gameObject.GetComponent<TextMesh>().text = path;
        configInfo = JsonUtility.FromJson<ConfigInfo>(configInfoJson);
        //Actualizo el UIManager para poder escribir bien los datos de la UI
        FindObjectOfType<UIManager>().configInfo = configInfo;



        //FindObjectOfType<AlmazaraManager>().Load();
    }

    [ContextMenu("LoadFromQR")]
    public void LoadFromQR()
    {
        if (gameObject.GetComponent<TextMesh>().text != string.Empty)
        {
            path = gameObject.GetComponent<TextMesh>().text;
            configInfo = JsonUtility.FromJson<ConfigInfo>(path);
            FindObjectOfType<UIManager>().configInfo = configInfo;
        }
        else
        {

        }
    }




}
