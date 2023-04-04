using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;

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

    public bool resetValue;
    [ContextMenu("PressReset")]
    public void ButtonResetPressed() 
    {
        Debug.Log("BOTÓN DE RESET PRESIONADO");
        if(resetValue == true)
        {
            FindFirstObjectByType<UModbusTCPWriterReset>().WriteResetHolding("1", resetValue);
            resetValue = false;
            FindFirstObjectByType<UModbusTCPWriterReset>().WriteResetHolding("1", resetValue); //PREGUNTAR SI DESDE PLC LO PONEN A 0
        }

       
    }
    public List<bool> marchaList;
    public List<int> consignaList;
    [ContextMenu("PressMarchaIzquierda")]
    public void ButtonMarcha1Pressed()
    {
        Debug.Log("BOTÓN DE MARCHA_1 PRESIONADO");
        marchaList[0] = true;
        marchaList[1] = false;
        FindFirstObjectByType<UModbusTCPWriterMarchas>().WriteMarchasHolding("2", marchaList); //1 y 0
        
    }
    [ContextMenu("PressMarchaDerecha")]
    public void ButtonMarcha2Pressed()
    {
        Debug.Log("BOTÓN DE MARCHA_2 PRESIONADO");
        marchaList[0] = false;
        marchaList[1] = true;
        FindFirstObjectByType<UModbusTCPWriterMarchas>().WriteMarchasHolding("2", marchaList); //0 y 1
                                                                                               

        //HACER UN BOTÓN PARO QUE PONGA MARCHA 1 y MARCHA 2 a 0 y luego un RESET a 1

    }
    [ContextMenu("PressConsigna")]
    public void ButtonConsignaPressed()
    {
        //Función de escribir la consigna, posteriormente habría que leer la frecuencia. En el caso de que esto se lea constantemente no hay problema
        Debug.Log("BOTÓN DE CONSIGNA PRESIONADO");
        Debug.Log("inicial: " + FindObjectOfType<PinchSlider>().SliderValue);
        float valueBar = FindObjectOfType<PinchSlider>().SliderValue*50;
        Debug.Log("modificado: " + valueBar);
        FindFirstObjectByType<UModbusTCPWriterConsignas>().WriteConsignasHolding("4", valueBar);
    }

}
