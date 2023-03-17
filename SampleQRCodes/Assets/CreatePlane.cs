using Microsoft.MixedReality.QR;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreatePlane : MonoBehaviour
{
    public List<GameObject> qrList;

    public GameObject prefab;

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var collection = GameObject.FindGameObjectWithTag("QRCode");

        if (!qrList.Contains(collection))
        {
            Debug.Log("Detectado QR");
            qrList.Add(collection);
        }
    }
}
