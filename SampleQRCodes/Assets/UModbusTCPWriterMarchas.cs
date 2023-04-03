using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class UModbusTCPWriterMarchas : MonoBehaviour
{
    public string sIP_Input = "127.0.0.1";

    /////Private var///// 
    /*UModbusTCP m_oUModbusTCP;
    UModbusTCP.ExceptionData m_oUModbusTCPException;*/

    UModbusTCP m_oUModbusTCPMarchas;
    UModbusTCP.ExceptionData m_oUModbusTCPExceptionMarchas;

    void Awake() //Awake llama una sola vez al inicio de las escena, se está utilizando para poner valores por defecto
    {
        m_oUModbusTCPMarchas = null;
        m_oUModbusTCPExceptionMarchas = null;
        m_oUModbusTCPMarchas = UModbusTCP.Instance;
    }

    public List<byte[]> bValues;

    public void WriteMarchasHolding(string address, List<bool> marchaList)
    {
        //Connection values
        ushort usPort_Input = Convert.ToUInt16("502"); //Variable con el puerto introducido
        ushort usAddress_Input = Convert.ToUInt16(Int32.Parse(address) - 1); //Variable con el slot a escribir introducido
        if (!m_oUModbusTCPMarchas.connected) //Si no esta conectado, conecta a esa IP y puerto
        {
            m_oUModbusTCPMarchas.Connect(sIP_Input, usPort_Input);
        }

        byte[] bValue2 = UModbusTCPHelpers.GetBytesOfInt(Convert.ToUInt16(marchaList[0]));
        byte[] bValue3 = UModbusTCPHelpers.GetBytesOfInt(Convert.ToUInt16(marchaList[1]));

        //Cada int se guarda en dos posiciones consecutivas del array de tipo byte
        byte[] bValue_Input = new byte[4];

        bValue2.CopyTo(bValue_Input, 0);
        bValue3.CopyTo(bValue_Input, 2);

        //Exception callback
        if (m_oUModbusTCPExceptionMarchas != null) //Control de errores
        {
            m_oUModbusTCPMarchas.OnException -= m_oUModbusTCPExceptionMarchas;
        }
        m_oUModbusTCPExceptionMarchas = new UModbusTCP.ExceptionData(UModbusTCPOnException);
        m_oUModbusTCPMarchas.OnException += m_oUModbusTCPExceptionMarchas;

        //Write multiple inputs (Se escriben desde el registro marcado en adelante)
        byte[] result = m_oUModbusTCPMarchas.WriteMultipleRegister(2, 1, usAddress_Input, bValue_Input);

    }


    void UModbusTCPOnException(ushort _oID, byte _oUnit, byte _oFunction, byte _oException)
    {
        Debug.Log("Exception: " + _oException);
    }
}
