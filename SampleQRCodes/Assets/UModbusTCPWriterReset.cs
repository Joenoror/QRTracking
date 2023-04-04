using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class UModbusTCPWriterReset : MonoBehaviour
{

    /////Private var///// 
    /*UModbusTCP m_oUModbusTCP;
    UModbusTCP.ExceptionData m_oUModbusTCPException;*/

    UModbusTCP m_oUModbusTCPReset;
    UModbusTCP.ExceptionData m_oUModbusTCPExceptionReset;

    void Awake() //Awake llama una sola vez al inicio de las escena, se está utilizando para poner valores por defecto
    {
        m_oUModbusTCPReset = null;
        m_oUModbusTCPExceptionReset = null;
        m_oUModbusTCPReset = UModbusTCP.Instance;
    }

    //UModbusTCPWriter umodbusInstanceConsigna;
    //UModbusTCPWriterReset umodbusInstanceReset;

    // Start is called before the first frame update
    private void Start()
    {
        //umodbusInstanceReset = GetComponent<UModbusTCPWriterReset>();
    }

    private void Update()
    {
        //umodbusInstanceReset.WriteResetHolding("1", FindObjectOfType<ReadFromQR>().configInfo);
    }

    public List<byte[]> bValues;

    public void WriteResetHolding(string address, bool resetValue)
    {
        //Connection values
        string sIP_Input = "10.103.125.10"; //Variable con la ip introducida
        ushort usPort_Input = Convert.ToUInt16("502"); //Variable con el puerto introducido
        ushort usAddress_Input = Convert.ToUInt16(Int32.Parse(address) - 1); //Variable con el slot a escribir introducido
        if (!m_oUModbusTCPReset.connected) //Si no esta conectado, conecta a esa IP y puerto
        {
            m_oUModbusTCPReset.Connect(sIP_Input, usPort_Input);
        }

        byte[] bValue1 = UModbusTCPHelpers.GetBytesOfInt(Convert.ToUInt16(resetValue));      

        //Exception callback
        if (m_oUModbusTCPExceptionReset != null) //Control de errores
        {
            m_oUModbusTCPReset.OnException -= m_oUModbusTCPExceptionReset;
        }
        m_oUModbusTCPExceptionReset = new UModbusTCP.ExceptionData(UModbusTCPOnException);
        m_oUModbusTCPReset.OnException += m_oUModbusTCPExceptionReset;

        //Write multiple inputs (Se escriben desde el registro marcado en adelante)
        byte[] result = m_oUModbusTCPReset.WriteSingleRegister(2, 1, usAddress_Input, bValue1);

    }


    void UModbusTCPOnException(ushort _oID, byte _oUnit, byte _oFunction, byte _oException)
    {
        Debug.Log("Exception: " + _oException);
    }
}
