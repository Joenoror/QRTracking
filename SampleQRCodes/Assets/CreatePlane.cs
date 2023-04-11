using Microsoft.MixedReality.QR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreatePlane : MonoBehaviour
{
    public List<GameObject> qrList;

    public GameObject prefab;
    public double maxValue;
    public List<GameObject> futuredeletedQRs;


    // Update is called once per frame
    void Update()
    {
        var collection = GameObject.FindGameObjectsWithTag("TimeStamp");

        //SE TIENE QUE HACER CON EL TIMESTAMP, ME TENGO QUE QUEDAR SIEMRE CON EL MÁS NUEVO
        //HACER UN TAG PARA LAS TIMESTAMPS

        foreach(var item in collection)
        {
            if (!qrList.Contains(item) && collection != null)
            {
                Debug.Log("Detectado QR");
                qrList.Add(item);
                Instantiate(prefab, item.transform);
            }

            Debug.Log("TimeStamp = " + item.GetComponent<TextMesh>().text);
            string[] timeTimeStamp = item.GetComponent<TextMesh>().text.Split();

            for(int i = 0; i < timeTimeStamp.Length; i++)
            {
                Debug.Log(i + ": "+ timeTimeStamp[i]);
                var str = timeTimeStamp[i];
                var charsToRemove = new string[] { ":","." };
                foreach (var c in charsToRemove)
                {
                    str = str.Replace(c, string.Empty);
                }
                timeTimeStamp[i] = str;
                Debug.Log("NEW: " + i + ": " + timeTimeStamp[i]);
                if (i == 1)
                {

                    if (Convert.ToInt32(timeTimeStamp[i]) >= maxValue)
                    {

                        if(item.GetComponent<DeleteTimeStamp>().qrID.GetComponent<TextMesh>().text == "NodeId:0a922032-b384-46e5-a008-449720e19ae2")
                        {
                            maxValue = Convert.ToInt32(timeTimeStamp[i]);
                            Debug.Log("Guardamos "+ item + ". QR Correcto encontrado");
                        }
                        else
                        {
                            Debug.Log("Borramos " + item);
                            if (!futuredeletedQRs.Contains(item))
                                futuredeletedQRs.Add(item);
                            Destroy(item.GetComponent<DeleteTimeStamp>().gameObject);
                            futuredeletedQRs.Remove(item);
                            qrList.Remove(item);
                        }
                    }

                    else
                    {
                        Debug.Log("Borramos " + item);
                        if(!futuredeletedQRs.Contains(item))
                        futuredeletedQRs.Add(item);
                        Destroy(item.GetComponent<DeleteTimeStamp>().gameObject);
                        futuredeletedQRs.Remove(item);
                        qrList.Remove(item);
                    }
                        

                }

            }

        }



    }
}
