using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class UModbusTCPReader : MonoBehaviour
{


    /////Private var///// 
    UModbusTCP m_oUModbusTCP;
    UModbusTCP.ResponseData m_oUModbusTCPResponse;
    UModbusTCP.ExceptionData m_oUModbusTCPException;

    int[] m_iResponseValues = new int[13];

    void Awake() //Awake llama una sola vez al inicio de las escena, se está utilizando para poner valores por defecto
    {
        m_oUModbusTCP = null;
        m_oUModbusTCPResponse = null;
        m_oUModbusTCPException = null;


        m_oUModbusTCP = UModbusTCP.Instance;
    }

    UModbusTCPReader umodbusInstance;

    public float initialTime, repeatTime;

    private void Start()
    {
        InvokeRepeating(nameof(Update_modified), initialTime, repeatTime); //LLama a la función Update_modified cada 1 segundo
        umodbusInstance = GetComponent<UModbusTCPReader>();
    }



    private void Update_modified()
    {
        if (FindObjectOfType<ReadFromQR>()) //SI SE HA CARGADO EL QR
        {
            if (FindObjectOfType<ReadFromQR>().configInfo != null)
                umodbusInstance.ReadMultipleHolding(FindObjectOfType<ReadFromQR>().configInfo);
            else
                Debug.LogWarning("ERROR: No se encuentra el configInfo del QR");
        }
        else
            Debug.Log("ERROR: NO SE HA CARGADO AÚN EL QR");

    }

    public void ReadMultipleHolding(ConfigInfo configInfo)
    {
       

        //Connection values
        string sIP_Input = "127.0.0.1"; //Variable con la ip introducida
        ushort usPort_Input = Convert.ToUInt16("502"); //Variable con el puerto introducido
        if (!m_oUModbusTCP.connected) //Si no esta conectado, conecta a esa IP y puerto
        {
            m_oUModbusTCP.Connect(sIP_Input, usPort_Input);
        }

        if (m_oUModbusTCPResponse != null)
        {
            m_oUModbusTCP.OnResponseData -= m_oUModbusTCPResponse;
        }
        m_oUModbusTCPResponse = new UModbusTCP.ResponseData(UModbusTCPOnResponseData);
        m_oUModbusTCP.OnResponseData += m_oUModbusTCPResponse;

        //Exception callback
        if (m_oUModbusTCPException != null) //Control de errores
        {
            m_oUModbusTCP.OnException -= m_oUModbusTCPException;
        }
        m_oUModbusTCPException = new UModbusTCP.ExceptionData(UModbusTCPOnException);
        m_oUModbusTCP.OnException += m_oUModbusTCPException;


        //Read Inputs

        m_oUModbusTCP.ReadHoldingRegister(2, 1, Convert.ToUInt16(Int32.Parse("1") - 1), 13);
        
        for(int i = 0; i < 13; i++)
        {
            if (i != 3 && i != 4)
            {
                configInfo.modbusList[i].holdingVar = m_iResponseValues[i];
            }
            
        }
       

    }

    void UModbusTCPOnResponseData(ushort _oID, byte _oUnit, byte _oFunction, byte[] _oValues)
    {

        //Number of values
        int iNumberOfValues = _oValues[8];

        /*
        //Get values pair with 2
        int oCounter = 0;
        for(int i = 0; i < iNumberOfValues; i += 2) {
            byte[] oResponseFinalValues = new byte[2];
            for(int j = 0; j < 2; ++j) {
                oResponseFinalValues[j] = _oValues[9 + i + j];
            }
            ++oCounter; //More address
        }
        */

        //Get values
        byte[] oResponseFinalValues = new byte[iNumberOfValues];
        for (int i = 0; i < iNumberOfValues; ++i)
        {
            oResponseFinalValues[i] = _oValues[9 + i];
        }

        int[] iValues = UModbusTCPHelpers.GetIntsOfBytes(oResponseFinalValues);
        for(int i = 0; i<13; i++)
        {
            m_iResponseValues[i] = iValues[i];
        }
       
    }

    void UModbusTCPOnException(ushort _oID, byte _oUnit, byte _oFunction, byte _oException)
    {
        Debug.Log("Exception: " + _oException);
    }
}
