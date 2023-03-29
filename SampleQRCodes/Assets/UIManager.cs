using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public List<TMP_Text> texts;
    public ConfigInfo configInfo;

    public float frecuencyValue, speedValue, intensityValue, temperatureValue;

    private void Update()
    {
        //Buscamos el archivo configinfo correspondiente al QR
        if (configInfo == null)
            configInfo = FindObjectOfType<ReadFromQR>().configInfo;
        UpdateTextValue();
    }

    public void ConvertModbusValues() 
    {
        frecuencyValue = configInfo.modbusList[5].valueVar + (configInfo.modbusList[6].valueVar / 1000);
        speedValue = configInfo.modbusList[7].valueVar + (configInfo.modbusList[8].valueVar / 1000);
        intensityValue = configInfo.modbusList[9].valueVar + (configInfo.modbusList[10].valueVar / 1000);
        temperatureValue = configInfo.modbusList[11].valueVar + (configInfo.modbusList[12].valueVar / 1000);
    }


    public void UpdateTextValue()
    {
        ConvertModbusValues();
        texts[0].text = frecuencyValue.ToString();
        texts[1].text = speedValue.ToString();
        texts[2].text = intensityValue.ToString();
        texts[3].text = temperatureValue.ToString();
    }

    public void ButtonResetPressed()
    {
        Debug.Log("BOTÓN DE RESET PRESIONADO");
        //TODO
    }
    public void ButtonMarcha1Pressed()
    {
        Debug.Log("BOTÓN DE MARCHA_1 PRESIONADO");
        //TODO
    }
    public void ButtonMarcha2Pressed()
    {
        Debug.Log("BOTÓN DE MARCHA_2 PRESIONADO");
        //TODO
    }

}
