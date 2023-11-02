using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NodeInfoCanvasManager : MonoBehaviour
{
    [SerializeField]
    private Camera m_MainCamera = null;
    [SerializeField]
    private GameObject m_CanvasObject = null;

    [SerializeField]
    private TextMeshProUGUI m_NameText = null;
    [SerializeField]
    private TextMeshProUGUI m_AdditionalInfoText = null;

    

    private void Update()
    {
        m_CanvasObject.transform.LookAt(m_MainCamera.transform);
        m_CanvasObject.transform.Rotate(0, 180, 0);
    }

    public void Open()
    {
        m_CanvasObject.SetActive(true);
    }

    public void Close()
    {
        m_CanvasObject.SetActive(false);
    }

    

    public void SetNameText(string text)
    {
        m_NameText.text = text;
    }

    public void SetAdditionalText(string text)
    {
        m_AdditionalInfoText.text = text;
    }
}
