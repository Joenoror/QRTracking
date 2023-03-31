using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class UModbusTCPWriterConsignas : MonoBehaviour
{

    public string sIP_Input = "127.0.0.1";

    /////Private var///// 
    UModbusTCP m_oUModbusTCP;
    UModbusTCP.ExceptionData m_oUModbusTCPException;

    /*UModbusTCP m_oUModbusTCPReset;
    UModbusTCP.ExceptionData m_oUModbusTCPExceptionReset;*/

    void Awake() //Awake llama una sola vez al inicio de las escena, se está utilizando para poner valores por defecto
    {
        m_oUModbusTCP = null;
        m_oUModbusTCPException = null;
        m_oUModbusTCP = UModbusTCP.Instance;

        /*m_oUModbusTCPReset = null;
        m_oUModbusTCPExceptionReset = null;
        m_oUModbusTCPReset = UModbusTCP.Instance;*/

    }

    UModbusTCPWriterConsignas umodbusInstanceConsigna;
    //UModbusTCPWriterConsignas umodbusInstanceReset;

    // Start is called before the first frame update
    private void Start()
    {
        //UmodbusInstance = gameObject.AddComponent<UModbusTCPWriterConsignas>();
        umodbusInstanceConsigna = GetComponent<UModbusTCPWriterConsignas>();
        //umodbusInstanceReset = GetComponent<UModbusTCPWriterConsignas>();

    }

    private void Update()
    {
        //umodbusInstanceReset.WriteResetHolding("1", FindObjectOfType<ReadFromQR>().configInfo);
        umodbusInstanceConsigna.WriteConsignasHolding("4", FindObjectOfType<ReadFromQR>().configInfo);
        
    }

    public List<byte[]> bValues;

    public void WriteConsignasHolding(string address, ConfigInfo configInfo)
    {
        //Connection values
        //sIP_Input = "127.0.0.1"; //Variable con la ip introducida
        ushort usPort_Input = Convert.ToUInt16("502"); //Variable con el puerto introducido
        ushort usAddress_Input = Convert.ToUInt16(Int32.Parse(address) - 1); //Variable con el slot a escribir introducido
        if (!m_oUModbusTCP.connected) //Si no esta conectado, conecta a esa IP y puerto
        {
            m_oUModbusTCP.Connect(sIP_Input, usPort_Input);
        }

        //Input values from string to byte[]
        /*byte[] bValue1 = UModbusTCPHelpers.GetBytesOfInt(configInfo.modbusList[0].holdingVar);
        byte[] bValue2 = UModbusTCPHelpers.GetBytesOfInt(configInfo.modbusList[1].holdingVar);
        byte[] bValue3 = UModbusTCPHelpers.GetBytesOfInt(configInfo.modbusList[2].holdingVar);*/
        byte[] bValue4 = UModbusTCPHelpers.GetBytesOfInt(configInfo.modbusList[3].holdingVar);
        byte[] bValue5 = UModbusTCPHelpers.GetBytesOfInt(configInfo.modbusList[4].holdingVar);

        //Cada int se guarda en dos posiciones consecutivas del array de tipo byte
        byte[] bValue_Input = new byte[4];

        /*bValue1.CopyTo(bValue_Input, 0);
        bValue2.CopyTo(bValue_Input, 2);
        bValue3.CopyTo(bValue_Input, 4);*/
        bValue4.CopyTo(bValue_Input, 0);
        bValue5.CopyTo(bValue_Input, 2);
        

        //Exception callback
        if (m_oUModbusTCPException != null) //Control de errores
        {
            m_oUModbusTCP.OnException -= m_oUModbusTCPException;
        }
        m_oUModbusTCPException = new UModbusTCP.ExceptionData(UModbusTCPOnException);
        m_oUModbusTCP.OnException += m_oUModbusTCPException;

        //Write multiple inputs (Se escriben desde el registro marcado en adelante)
        byte[] result = m_oUModbusTCP.WriteMultipleRegister(2, 1, usAddress_Input, bValue_Input);

    }

    /*public void WriteResetHolding(string address, ConfigInfo configInfo)
    {
        //Connection values
        ushort usPort_Input = Convert.ToUInt16("502"); //Variable con el puerto introducido
        ushort usAddress_Input = Convert.ToUInt16(Int32.Parse(address) - 1); //Variable con el slot a escribir introducido
        if (!m_oUModbusTCPReset.connected) //Si no esta conectado, conecta a esa IP y puerto
        {
            m_oUModbusTCPReset.Connect(sIP_Input, usPort_Input);
        }

        byte[] bValue1 = UModbusTCPHelpers.GetBytesOfInt(configInfo.modbusList[0].holdingVar);      

        //Exception callback
        if (m_oUModbusTCPExceptionReset != null) //Control de errores
        {
            m_oUModbusTCPReset.OnException -= m_oUModbusTCPExceptionReset;
        }
        m_oUModbusTCPException = new UModbusTCP.ExceptionData(UModbusTCPOnException);
        m_oUModbusTCPReset.OnException += m_oUModbusTCPExceptionReset;

        //Write multiple inputs (Se escriben desde el registro marcado en adelante)
        byte[] result = m_oUModbusTCPReset.WriteSingleRegister(2, 1, usAddress_Input, bValue1);

    }*/


    void UModbusTCPOnException(ushort _oID, byte _oUnit, byte _oFunction, byte _oException)
    {
        Debug.Log("Exception: " + _oException);
    }
}
