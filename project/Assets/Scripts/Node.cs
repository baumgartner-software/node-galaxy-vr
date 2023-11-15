using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Cube = null;
    [SerializeField]
    private GameObject m_Sphere = null;
    [SerializeField]
    private GameObject m_Octahedron = null;
    [SerializeField]
    private GameObject m_Tetrahedron = null;
    [SerializeField]
    private GameObject m_Icosahedron = null;
    [SerializeField]
    private GameObject m_Dodecahedron = null;


    private NodeData m_NodeDataReference = null;
    private NodeInfoPanel m_NodeInfoPanel = null;

    private NodeObject m_CurrentShape = null;

    private string m_NodeKey = "";
    private string m_ShapeName = "";


    public void Initialize(string nodeKey, NodeData data, NodeInfoPanel nodeInfoPanel)
    {
        m_NodeKey = nodeKey;

        m_NodeDataReference = data;
        m_NodeInfoPanel = nodeInfoPanel;

        string shape = data.Shape.ToLower();
        SetShape(shape);

        Vector3 localPosition = new Vector3(data.Position.x, data.Position.z, data.Position.y);
        transform.localPosition = localPosition;

        transform.localScale = new Vector3(data.Size, data.Size, data.Size);

        
    }

    private void SetShape(string shape)
    {
        if (m_CurrentShape != null)
        {
            Destroy(m_CurrentShape.gameObject);
        }

        switch (shape)
        {
            case "cube":
                m_CurrentShape = Instantiate(m_Cube, transform).GetComponent<NodeObject>();
                m_ShapeName = "cube";
                break;
            case "sphere":
                m_CurrentShape = Instantiate(m_Sphere, transform).GetComponent<NodeObject>();
                m_ShapeName = "sphere";
                break;
            case "octahedron":
                m_CurrentShape = Instantiate(m_Octahedron, transform).GetComponent<NodeObject>();
                m_ShapeName = "octahedron";
                break;
            case "tetrahedron":
                m_CurrentShape = Instantiate(m_Tetrahedron, transform).GetComponent<NodeObject>();
                m_ShapeName = "tetrahedron";
                break;
            case "icosahedron":
                m_CurrentShape = Instantiate(m_Icosahedron, transform).GetComponent<NodeObject>();
                m_ShapeName = "icosahedron";
                break;
            case "dodecahedron":
                m_CurrentShape = Instantiate(m_Dodecahedron, transform).GetComponent<NodeObject>();
                m_ShapeName = "dodecahedron";
                break;
            default:
                m_CurrentShape = Instantiate(m_Cube, transform).GetComponent<NodeObject>();
                m_ShapeName = "cube";
                break;
        }

        m_CurrentShape.Initialize(m_NodeDataReference, this);
    }

    
    public void OnSelected()
    {
        m_NodeInfoPanel.ShapesDropdown.onValueChanged.RemoveAllListeners();

        m_NodeInfoPanel.SetKey(m_NodeKey);

        m_NodeInfoPanel.SetIdText(m_NodeDataReference.Id);
        m_NodeInfoPanel.SetTextText(m_NodeDataReference.Text);
        m_NodeInfoPanel.SetAdditionalText(m_NodeDataReference.Additional);
        m_NodeInfoPanel.SetDropdownShapeValue(m_ShapeName);

        m_NodeInfoPanel.ShapesDropdown.onValueChanged.AddListener(SetShape);
        m_NodeInfoPanel.Open();
    }

    private void SetShape(int shape)
    {
        switch (shape)
        {
            case 0:
                SetShape("cube");
                break;
            case 1:
                SetShape("sphere");
                break;
            case 2:
                SetShape("octahedron");
                break;
            case 3:
                SetShape("tetrahedron");
                break;
            case 4:
                SetShape("icosahedron");
                break;
            case 5:
                SetShape("dodecahedron");
                break;
        }
    }
}
