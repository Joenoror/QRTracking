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

    UModbusTCPWriter umodbusInstance;
    // Start is called before the first frame update
    private void Start()
    {
        //UmodbusInstance = gameObject.AddComponent<UModbusTCPWriter>();
        umodbusInstance = GetComponent<UModbusTCPWriter>();

    }

    private void Update()
    {
        umodbusInstance.WriteMultipleHolding("1", FindObjectOfType<ReadFromQR>().configInfo);
    }


    public List<byte[]> bValues;

    public void WriteMultipleHolding(string address, ConfigInfo configInfo)
    {
        //Connection values
        //sIP_Input = "127.0.0.1"; //Variable con la ip introducida
        ushort usPort_Input = Convert.ToUInt16("502"); //Variable con el puerto introducido
        ushort usAddress_Input = Convert.ToUInt16(Int32.Parse(address) - 1); //Variable con el slot a escribir introducido
        if (!m_oUModbusTCP.connected) //Si no esta conectado, conecta a esa IP y puerto
        {
            m_oUModbusTCP.Connect(sIP_Input, usPort_Input);
        }

        //foreach (var modbusvar in configInfo.modbusList){

        //    var it = 0;
        //    Debug.Log("valor del entero -->" + modbusvar.holdingVar);
        //    Debug.Log("bValues antes de convertir -->" + UModbusTCPHelpers.GetBytesOfInt(modbusvar.holdingVar));
        //    bValues[it] = UModbusTCPHelpers.GetBytesOfInt(modbusvar.holdingVar);
        //    Debug.Log("bValues convertido -->" + bValues[it]);
        //    it++;
        //}
        //Input values from string to byte[]
        byte[] bValue1 = UModbusTCPHelpers.GetBytesOfInt(configInfo.modbusList[0].holdingVar);
        byte[] bValue2 = UModbusTCPHelpers.GetBytesOfInt(configInfo.modbusList[1].holdingVar);
        byte[] bValue3 = UModbusTCPHelpers.GetBytesOfInt(configInfo.modbusList[2].holdingVar);
        byte[] bValue4 = UModbusTCPHelpers.GetBytesOfInt(configInfo.modbusList[3].holdingVar);
        byte[] bValue5 = UModbusTCPHelpers.GetBytesOfInt(configInfo.modbusList[4].holdingVar);
        byte[] bValue6 = UModbusTCPHelpers.GetBytesOfInt(configInfo.modbusList[5].holdingVar);
        byte[] bValue7 = UModbusTCPHelpers.GetBytesOfInt(configInfo.modbusList[6].holdingVar);
        byte[] bValue8 = UModbusTCPHelpers.GetBytesOfInt(configInfo.modbusList[7].holdingVar);
        byte[] bValue9 = UModbusTCPHelpers.GetBytesOfInt(configInfo.modbusList[8].holdingVar);
        byte[] bValue10 = UModbusTCPHelpers.GetBytesOfInt(configInfo.modbusList[9].holdingVar);
        byte[] bValue11 = UModbusTCPHelpers.GetBytesOfInt(configInfo.modbusList[10].holdingVar);
        byte[] bValue12 = UModbusTCPHelpers.GetBytesOfInt(configInfo.modbusList[11].holdingVar);
        byte[] bValue13 = UModbusTCPHelpers.GetBytesOfInt(configInfo.modbusList[12].holdingVar);

        //Cada int se guarda en dos posiciones consecutivas del array de tipo byte
        byte[] bValue_Input = new byte[26];

        //foreach (var modbusvar in configInfo.modbusList)
        //{
        //    var it = 0; var it2 = 0;
        //    bValues[it].CopyTo(bValue_Input, it2);
        //    it++;
        //    it2 += 2;
        //}

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
