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
    UModbusTCP.ExceptionData m_oUModbusTCPException;

    void Awake() //Awake llama una sola vez al inicio de las escena, se está utilizando para poner valores por defecto
    {
        m_oUModbusTCP = null;
        m_oUModbusTCPException = null;
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
        //Connection values
        //sIP_Input = "127.0.0.1"; //Variable con la ip introducida
        ushort usPort_Input = Convert.ToUInt16("502"); //Variable con el puerto introducido
        if (!m_oUModbusTCP.connected) //Si no esta conectado, conecta a esa IP y puerto
        {
            m_oUModbusTCP.Connect(sIP_Input, usPort_Input);
        }

        //Exception callback
        if (m_oUModbusTCPException != null) //Control de errores
        {
            m_oUModbusTCP.OnException -= m_oUModbusTCPException;
        }
        m_oUModbusTCPException = new UModbusTCP.ExceptionData(UModbusTCPOnException);
        m_oUModbusTCP.OnException += m_oUModbusTCPException;

        int[] iValue1 = UModbusTCPHelpers.GetIntsOfBytes(m_oUModbusTCP.ReadHoldingRegister(2, 1, 1, 1));
        int[] iValue2 = UModbusTCPHelpers.GetIntsOfBytes(m_oUModbusTCP.ReadHoldingRegister(2, 1, 2, 1));
        int[] iValue3 = UModbusTCPHelpers.GetIntsOfBytes(m_oUModbusTCP.ReadHoldingRegister(2, 1, 3, 1));
        int[] iValue4 = UModbusTCPHelpers.GetIntsOfBytes(m_oUModbusTCP.ReadHoldingRegister(2, 1, 4, 1));
        int[] iValue5 = UModbusTCPHelpers.GetIntsOfBytes(m_oUModbusTCP.ReadHoldingRegister(2, 1, 5, 1));
        int[] iValue6 = UModbusTCPHelpers.GetIntsOfBytes(m_oUModbusTCP.ReadHoldingRegister(2, 1, 6, 1));
        int[] iValue7 = UModbusTCPHelpers.GetIntsOfBytes(m_oUModbusTCP.ReadHoldingRegister(2, 1, 7, 1));
        int[] iValue8 = UModbusTCPHelpers.GetIntsOfBytes(m_oUModbusTCP.ReadHoldingRegister(2, 1, 8, 1));
        int[] iValue9 = UModbusTCPHelpers.GetIntsOfBytes(m_oUModbusTCP.ReadHoldingRegister(2, 1, 9, 1));
        int[] iValue10 = UModbusTCPHelpers.GetIntsOfBytes(m_oUModbusTCP.ReadHoldingRegister(2, 1, 10, 1));
        int[] iValue11 = UModbusTCPHelpers.GetIntsOfBytes(m_oUModbusTCP.ReadHoldingRegister(2, 1, 11, 1));
        int[] iValue12 = UModbusTCPHelpers.GetIntsOfBytes(m_oUModbusTCP.ReadHoldingRegister(2, 1, 12, 1));
        int[] iValue13 = UModbusTCPHelpers.GetIntsOfBytes(m_oUModbusTCP.ReadHoldingRegister(2, 1, 13, 1));

        configInfo.modbusList[0].valueVar = iValue1[0];
        configInfo.modbusList[1].valueVar = iValue2[0];
        configInfo.modbusList[2].valueVar = iValue3[0];
        configInfo.modbusList[3].valueVar = iValue4[0];
        configInfo.modbusList[4].valueVar = iValue5[0];
        configInfo.modbusList[5].valueVar = iValue6[0];
        configInfo.modbusList[6].valueVar = iValue7[0];
        configInfo.modbusList[7].valueVar = iValue8[0];
        configInfo.modbusList[8].valueVar = iValue9[0];
        configInfo.modbusList[9].valueVar = iValue10[0];
        configInfo.modbusList[10].valueVar = iValue11[0];
        configInfo.modbusList[11].valueVar = iValue12[0];
        configInfo.modbusList[12].valueVar = iValue13[0];

    }


    void UModbusTCPOnException(ushort _oID, byte _oUnit, byte _oFunction, byte _oException)
    {
        Debug.Log("Exception: " + _oException);
    }
}
