using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class UModbusTCPWriterConsignas : MonoBehaviour
{


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

    }


    public List<byte[]> bValues;

    public void WriteConsignasHolding(string address, float valueBar)
    {
        //Connection values
        string sIP_Input = "127.0.0.1"; //Variable con la ip introducida
        ushort usPort_Input = Convert.ToUInt16("502"); //Variable con el puerto introducido
        ushort usAddress_Input = Convert.ToUInt16(Int32.Parse(address) - 1); //Variable con el slot a escribir introducido
        if (!m_oUModbusTCP.connected) //Si no esta conectado, conecta a esa IP y puerto
        {
            m_oUModbusTCP.Connect(sIP_Input, usPort_Input);
        }

        int consignaEntera = (int)valueBar;
        Debug.Log("parte entera:" + consignaEntera);
        int consignaDecimal = DecimalToInt((double)valueBar);
        Debug.Log("parte decimal:" + consignaDecimal);

        byte[] bValue4 = UModbusTCPHelpers.GetBytesOfInt(consignaEntera);
        byte[] bValue5 = UModbusTCPHelpers.GetBytesOfInt(consignaDecimal);

        //Cada int se guarda en dos posiciones consecutivas del array de tipo byte
        byte[] bValue_Input = new byte[4];

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

    int DecimalToInt(double position)
    {

        position = Math.Round(position, 5);
        double result_double = (int)(((decimal)position % 1) * 1000);
        int result = Math.Abs((int)result_double);
        return result;

    }

    void UModbusTCPOnException(ushort _oID, byte _oUnit, byte _oFunction, byte _oException)
    {
        Debug.Log("Exception: " + _oException);
    }
}
