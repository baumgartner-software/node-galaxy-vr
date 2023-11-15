using Oculus.Interaction;
using Oculus.Interaction.Surfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeObject : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer m_MeshRenderer = null;
    [SerializeField]
    private Material m_Material = null;
    [SerializeField]
    private Color m_HighlightColor;

    private Material m_MaterialInstance = null;
    private Color m_OriginalColor;

    private Node m_Node = null;

    

    private void Awake()
    {
        m_MaterialInstance = new Material(m_Material);
        m_MeshRenderer.material = m_MaterialInstance;
    }

    public void Initialize(NodeData data, Node node)
    {
        m_Node = node;

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
        m_Node.OnSelected();
    }
}
