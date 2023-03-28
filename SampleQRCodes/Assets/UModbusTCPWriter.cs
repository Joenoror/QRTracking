using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class UModbusTCPWriter : MonoBehaviour
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

    UModbusTCPWriter UmodbusInstance;
    // Start is called before the first frame update
    private void Start()
    {
        UmodbusInstance = gameObject.AddComponent<UModbusTCPWriter>();
    }

    private void Update()
    {
        //TODO -----> UmodbusInstance.WriteMultipleHolding("1", VARIABLES);
    }


    public void WriteMultipleHolding(string address, int val1, int val2, int val3, int val4, int val5, int val6, int val7, int val8, int val9, int val10, int val11, int val12, int val13)
    {

        //Connection values
        //sIP_Input = "127.0.0.1"; //Variable con la ip introducida
        ushort usPort_Input = Convert.ToUInt16("502"); //Variable con el puerto introducido
        ushort usAddress_Input = Convert.ToUInt16(Int32.Parse(address) - 1); //Variable con el slot a escribir introducido

        //Input values from string to byte[]
        byte[] bValue1 = UModbusTCPHelpers.GetBytesOfInt(val1);
        byte[] bValue2 = UModbusTCPHelpers.GetBytesOfInt(val2);
        byte[] bValue3 = UModbusTCPHelpers.GetBytesOfInt(val3);
        byte[] bValue4 = UModbusTCPHelpers.GetBytesOfInt(val4);
        byte[] bValue5 = UModbusTCPHelpers.GetBytesOfInt(val5);
        byte[] bValue6 = UModbusTCPHelpers.GetBytesOfInt(val6);
        byte[] bValue7 = UModbusTCPHelpers.GetBytesOfInt(val7);
        byte[] bValue8 = UModbusTCPHelpers.GetBytesOfInt(val8);
        byte[] bValue9 = UModbusTCPHelpers.GetBytesOfInt(val9);
        byte[] bValue10 = UModbusTCPHelpers.GetBytesOfInt(val10);
        byte[] bValue11 = UModbusTCPHelpers.GetBytesOfInt(val11);
        byte[] bValue12 = UModbusTCPHelpers.GetBytesOfInt(val12);
        byte[] bValue13 = UModbusTCPHelpers.GetBytesOfInt(val13);
        

        //Cada int se guarda en dos posiciones consecutivas del array de tipo byte
        byte[] bValue_Input = new byte[26];
        bValue1.CopyTo(bValue_Input, 0);
        bValue2.CopyTo(bValue_Input, 2);
        bValue3.CopyTo(bValue_Input, 4);
        bValue4.CopyTo(bValue_Input, 6);
        bValue5.CopyTo(bValue_Input, 8);
        bValue6.CopyTo(bValue_Input, 10);
        bValue7.CopyTo(bValue_Input, 12);
        bValue8.CopyTo(bValue_Input, 14);
        bValue9.CopyTo(bValue_Input, 16);
        bValue10.CopyTo(bValue_Input, 18);
        bValue11.CopyTo(bValue_Input, 20);
        bValue12.CopyTo(bValue_Input, 22);
        bValue13.CopyTo(bValue_Input, 24);
        

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

        //Write multiple inputs (Se escriben desde el registro marcado en adelante)
        byte[] result = m_oUModbusTCP.WriteMultipleRegister(2, 1, usAddress_Input, bValue_Input);

    }

    void UModbusTCPOnException(ushort _oID, byte _oUnit, byte _oFunction, byte _oException)
    {
        Debug.Log("Exception: " + _oException);
    }
}
