using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonParser : MonoBehaviour
{
    [SerializeField]
    private GameObject m_NodePrefab = null;
    [SerializeField]
    private Material m_LineMaterial = null;
    [SerializeField]
    private NodeInfoPanel m_NodeInfoPanel = null;

    private Dictionary<string, NodeData> m_nodeInfoDictionary;

    private string m_CurrentFileName = "";


#if UNITY_EDITOR
    private void Start()
    {
        LoadNodes("");
    }
#endif

    public void UpdateNodeInfo(string key, string id, string text, string additional, string shape)
    {
        m_nodeInfoDictionary[key].Id = id;
        m_nodeInfoDictionary[key].Text = text;
        m_nodeInfoDictionary[key].Additional = additional;
        m_nodeInfoDictionary[key].Shape = shape;

        SaveNodes();
    }


    public void LoadNodes(string fileName)
    {
        m_CurrentFileName = fileName;

        // Clear out the existing Nodes
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }


#if UNITY_EDITOR
        TextAsset textAsset = Resources.Load<TextAsset>("Nodes");
        string jsonString = textAsset.text;

        // -----------------
#else
        string jsonString = ReadExternalFile($"{Application.persistentDataPath}/{fileName}");
#endif
        // ------------------

        m_nodeInfoDictionary = JsonConvert.DeserializeObject<Dictionary<string, NodeData>>(jsonString);

        foreach (var entry in m_nodeInfoDictionary)
        {
            Node node = Instantiate(m_NodePrefab).GetComponent<Node>();
            node.transform.parent = transform;            

            node.Initialize(entry.Key, entry.Value, m_NodeInfoPanel);

            float lineWidth = entry.Value.Size / 5f;

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


    public void SaveNodes()
    {
        // Serialize the dictionary to a JSON string
        string jsonString = JsonConvert.SerializeObject(m_nodeInfoDictionary, Formatting.Indented);

        // Write the JSON string to an external file
        WriteExternalFile($"{Application.persistentDataPath}/{m_CurrentFileName}", jsonString);
    }

    public void WriteExternalFile(string path, string dataToWrite)
    {
        // Get the external storage directory just like in the ReadExternalFile method
        AndroidJavaClass environmentClass = new AndroidJavaClass("android.os.Environment");
        AndroidJavaObject externalStorageDirectory = environmentClass.CallStatic<AndroidJavaObject>("getExternalStorageDirectory");
        string externalPath = externalStorageDirectory.Call<string>("getAbsolutePath");

        // Combine the external directory with your file path
        string fullPath = Path.Combine(externalPath, path);

        // Make sure the directory exists before writing to prevent exceptions
        string directory = Path.GetDirectoryName(fullPath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Write the JSON string to the specified path
        File.WriteAllText(fullPath, dataToWrite);
    }
}