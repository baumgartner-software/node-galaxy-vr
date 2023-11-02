using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonParser : MonoBehaviour
{
    [SerializeField]
    private GameObject m_NodePrefab = null;
    [SerializeField]
    private Material m_LineMaterial = null;

    private Dictionary<string, NodeData> m_nodeInfoDictionary;
    private HashSet<string> m_visitedNodes;
    private List<HashSet<string>> m_groups;

    private List<LineRenderer> m_LineRenderers;



    public void LoadNodes(string fileName)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        string jsonString = ReadExternalFile($"{Application.persistentDataPath}/{fileName}");

        m_nodeInfoDictionary = JsonConvert.DeserializeObject<Dictionary<string, NodeData>>(jsonString);
        m_visitedNodes = new HashSet<string>();
        m_groups = new List<HashSet<string>>();
        m_LineRenderers = new List<LineRenderer>();

        foreach (var entry in m_nodeInfoDictionary)
        {
            Node node = Instantiate(m_NodePrefab).GetComponent<Node>();
            node.transform.parent = transform;            

            node.Initialize(entry.Value);

            float lineWidth = m_NodePrefab.transform.localScale.x / 5f;
            foreach (var connection in entry.Value.Edges)
            {
                GameObject lrObject = new GameObject();
                lrObject.transform.parent = node.transform;
                lrObject.transform.localPosition = Vector3.zero;

                LineRenderer lr = lrObject.AddComponent<LineRenderer>();
                lr.material = m_LineMaterial;
                Vector3[] positions = new Vector3[2];
                positions[0] = node.transform.position;
                Vector3 connectionPosition = m_nodeInfoDictionary[connection].Position;
                positions[1] = new Vector3(connectionPosition.x, connectionPosition.z, connectionPosition.y);
                lr.SetPositions(positions);
                lr.startWidth = lineWidth;
                lr.endWidth = lineWidth;
            }
        }       
    }


    public string ReadExternalFile(string path)
    {
        AndroidJavaClass environmentClass = new AndroidJavaClass("android.os.Environment");
        AndroidJavaObject externalStorageDirectory = environmentClass.CallStatic<AndroidJavaObject>("getExternalStorageDirectory");
        string externalPath = externalStorageDirectory.Call<string>("getAbsolutePath");

        string fullPath = Path.Combine(externalPath, path);
        if (File.Exists(fullPath))
        {
            return File.ReadAllText(fullPath);
        }
        return null;
    }
}