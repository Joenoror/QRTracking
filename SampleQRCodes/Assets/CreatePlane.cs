using Microsoft.MixedReality.QR;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreatePlane : MonoBehaviour
{
    public List<GameObject> qrList;

    public GameObject prefab;


    // Update is called once per frame
    void Update()
    {
        var collection = GameObject.FindGameObjectWithTag("QRCode");

        if (!qrList.Contains(collection) && collection != null)
        {
            Debug.Log("Detectado QR");
            qrList.Add(collection);
            Instantiate(prefab, collection.transform);
        }
    }
}
