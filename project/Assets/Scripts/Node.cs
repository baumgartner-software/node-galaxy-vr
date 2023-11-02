using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer m_MeshRenderer = null;
    [SerializeField]
    private Material m_Material = null;
    [SerializeField]
    private Color m_HighlightColor;

    private Material m_MaterialInstance = null;
    private Color m_OriginalColor;

    private NodeData m_NodeDataReference = null;
    private NodeInfoCanvasManager m_NodeInfoCanvasManager = null;

    private void Awake()
    {
        m_NodeInfoCanvasManager = GameObject.Find("Node Info Canvas").GetComponent<NodeInfoCanvasManager>();


        m_MaterialInstance = new Material(m_Material);
        m_MeshRenderer.material = m_MaterialInstance;
    }

    public void Initialize(NodeData data)
    {
        m_NodeDataReference = data;

        Vector3 localPosition = new Vector3(data.Position.x, data.Position.z, data.Position.y);
        transform.localPosition = localPosition;

        transform.localScale = new Vector3(data.Size, data.Size, data.Size);

        m_OriginalColor = HexToColor(data.Color);
        m_Material.color = m_OriginalColor;
    }

    public Color HexToColor(string hex)
    {
        hex = hex.Replace("#", "");

        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        byte a = (hex.Length == 8) ? byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber) : (byte)255;

        return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
    }

    public void OnHighlight(bool onHover)
    {
        if (onHover)
        {
            m_MaterialInstance.color = m_HighlightColor;
        }
        else
        {
            m_MaterialInstance.color = m_OriginalColor;
        }
    }

    public void OnSelected()
    {
        m_NodeInfoCanvasManager.SetNameText(m_NodeDataReference.Id);
        m_NodeInfoCanvasManager.SetAdditionalText(m_NodeDataReference.Additional);

        m_NodeInfoCanvasManager.transform.position = transform.position;

        m_NodeInfoCanvasManager.Open();
    }
}
