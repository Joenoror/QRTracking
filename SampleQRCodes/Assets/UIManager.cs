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
        //if (configInfo == null && FindObjectOfType<ReadFromQR>().configInfo.modbusList.Count == 0)
        //    configInfo = FindObjectOfType<ReadFromQR>().configInfo;
        if(configInfo != null)
            UpdateTextValue();
    }

    public void ConvertModbusValues() 
    {
        float dfrecuecyValue = configInfo.modbusList[6].holdingVar;
        frecuencyValue = configInfo.modbusList[5].holdingVar + (dfrecuecyValue / 1000);
        //Debug.Log("frecuencyValue = " + frecuencyValue);
        float dspeedValue = configInfo.modbusList[8].holdingVar;
        speedValue = configInfo.modbusList[7].holdingVar + (dspeedValue / 1000);
        //Debug.Log("speedValue = " + speedValue);
        float dintensityValue = configInfo.modbusList[10].holdingVar;
        intensityValue = configInfo.modbusList[9].holdingVar + (dintensityValue / 1000);
        //Debug.Log("intensityValue = " + intensityValue);
        float dtemperatureValue = configInfo.modbusList[12].holdingVar;
        temperatureValue = configInfo.modbusList[11].holdingVar + (dtemperatureValue / 1000);
        //Debug.Log("temperatureValue = " + temperatureValue);
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
