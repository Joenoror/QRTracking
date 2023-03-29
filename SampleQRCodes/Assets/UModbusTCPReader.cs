using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class UModbusTCPReader : MonoBehaviour
{

    public string sIP_Input = "127.0.0.1";

    /////Private var///// 
    UModbusTCP m_oUModbusTCP;
    UModbusTCP.ResponseData m_oUModbusTCPResponse;
    UModbusTCP.ExceptionData m_oUModbusTCPException;

    int m_iResponseValue;

    void Awake() //Awake llama una sola vez al inicio de las escena, se está utilizando para poner valores por defecto
    {
        m_oUModbusTCP = null;
        m_oUModbusTCPResponse = null;
        m_oUModbusTCPException = null;
        m_iResponseValue = -1;

        m_oUModbusTCP = UModbusTCP.Instance;
    }

    UModbusTCPReader umodbusInstance;

    private void Start()
    {
        umodbusInstance = GetComponent<UModbusTCPReader>();

    }

    // Update is called once per frame
    void Update()
    {
        //umodbusInstance.
        umodbusInstance.ReadMultipleHolding(FindObjectOfType<ReadFromQR>().configInfo);
    }

    public void ReadMultipleHolding(ConfigInfo configInfo)
    {
        //Reset response
        
        m_iResponseValue = -1;
       

        //Connection values
        //sIP_Input = "127.0.0.1"; //Variable con la ip introducida
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
        m_oUModbusTCP.ReadHoldingRegister(2, 1, Convert.ToUInt16(Int32.Parse("1") - 1), 1);
        configInfo.modbusList[0].valueVar = m_iResponseValue;
        Debug.Log("modbusList[0]: " + configInfo.modbusList[0].valueVar);

        m_oUModbusTCP.ReadHoldingRegister(2, 1, Convert.ToUInt16(Int32.Parse("2") - 1), 1);
        configInfo.modbusList[0].valueVar = m_iResponseValue;
        Debug.Log("modbusList[1]: " + configInfo.modbusList[1].valueVar);

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
        m_iResponseValue = iValues[0];
    }

    void UModbusTCPOnException(ushort _oID, byte _oUnit, byte _oFunction, byte _oException)
    {
        Debug.Log("Exception: " + _oException);
    }
}
