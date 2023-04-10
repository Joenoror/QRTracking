using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeResetColor : MonoBehaviour
{

    private MeshRenderer m_Renderer;

    public void ButtonResetGreen()
    {
        m_Renderer = GetComponent<MeshRenderer>();
        Color greenColor = new Color(0f, 1f, 0f, 1f);

        m_Renderer.material.color = greenColor;


    }

    public void ButtonResetRed()
    {
        m_Renderer = GetComponent<MeshRenderer>();
        Color redColor = new Color(1f, 0f, 0f, 1f);

        m_Renderer.material.color = redColor;


    }
}
