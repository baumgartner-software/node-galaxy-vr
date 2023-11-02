using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class JsonParser1 : MonoBehaviour
{
    [SerializeField]
    private GameObject m_NodePrefab = null;

    //void Start()
    //{
    //    TextAsset jsonFile = Resources.Load<TextAsset>("nodes");  // Without the .json extension
    //    string jsonString = jsonFile.text;

    //    Dictionary<string, NodeInfo> nodeInfoDictionary =
    //        JsonConvert.DeserializeObject<Dictionary<string, NodeInfo>>(jsonString);

    //    foreach (var entry in nodeInfoDictionary)
    //    {
    //        //GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    //        //go.transform.parent = transform;
    //        //go.transform.localPosition = Vector3.zero;
    //        Debug.Log($"Key: {entry.Key}, Id: {entry.Value.Id}, Size: {entry.Value.Size}, edges: {entry.Value.Edges.Count},label: {entry.Value.Label},text: {entry.Value.Text},color: {entry.Value.Color},additional: {entry.Value.Additional},");
    //    }
    //}


    private Dictionary<string, NodeData> m_nodeInfoDictionary;
    private HashSet<string> m_visitedNodes;
    private List<HashSet<string>> m_groups;

    private List<GameObject> m_Spheres;
    private List<LineRenderer> m_LineRenderers;

    void Start()
    {
        

        TextAsset jsonFile = Resources.Load<TextAsset>("Nodes");  // Without the .json extension
        string jsonString = jsonFile.text;

        m_nodeInfoDictionary = JsonConvert.DeserializeObject<Dictionary<string, NodeData>>(jsonString);
        m_visitedNodes = new HashSet<string>();
        m_groups = new List<HashSet<string>>();
        m_Spheres = new List<GameObject>();
        m_LineRenderers = new List<LineRenderer>();

        //foreach (var entry in m_nodeInfoDictionary)
        //{
        //    GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //    go.transform.parent = transform;
        //    go.transform.localPosition = Vector3.zero;

        //    //if (!m_visitedNodes.Contains(entry.Key))
        //    //{
        //    //    HashSet<string> newGroup = new HashSet<string>();
        //    //    DFS(entry.Key, newGroup);
        //    //    m_groups.Add(newGroup);
        //    //}
        //}


        int count = 0;
        foreach (var entry in m_nodeInfoDictionary)
        {
            if (count >= 27 * 27)
            {
                break; // Stop if we've filled the grid
            }

            GameObject go = Instantiate(m_NodePrefab); //GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.transform.parent = transform;


            float scale = 10;
            Vector3 localPosition = new Vector3(entry.Value.Position.x * scale, entry.Value.Position.z * scale, entry.Value.Position.y * scale);
            go.transform.localPosition = localPosition;

            m_Spheres.Add(go);



            foreach (var connection in entry.Value.Edges)
            {
                GameObject lrObject = new GameObject();
                lrObject.transform.parent = go.transform;
                lrObject.transform.localPosition = Vector3.zero;

                LineRenderer lr = lrObject.AddComponent<LineRenderer>();
                Vector3[] positions = new Vector3[2];
                positions[0] = go.transform.position;
                Vector3 connectionPosition = m_nodeInfoDictionary[connection].Position;
                positions[1] = new Vector3(connectionPosition.x * scale, connectionPosition.z * scale, connectionPosition.y * scale);
                lr.SetPositions(positions);
                lr.SetWidth(0.1f, 0.1f);
            }



            count++;

            //if (!m_visitedNodes.Contains(entry.Key))
            //{
            //    HashSet<string> newGroup = new HashSet<string>();
            //    DFS(entry.Key, newGroup);
            //    m_groups.Add(newGroup);
            //}
        }



        //for (int i = 0; i < m_Spheres.Count; i++)
        //{
        //    LineRenderer lr = m_Spheres[i].AddComponent<LineRenderer>();
        //    m_LineRenderers.Add(lr);
        //}






        return;
        //-------------------------------




        Debug.Log($"Number of separate groups: {m_groups.Count}");

        // Example to show nodes in each group
        int groupNumber = 1;
        foreach (var group in m_groups)
        {
            Debug.Log($"Group {groupNumber}: {string.Join(", ", group)}");
            groupNumber++;
        }





        if (m_groups.Count > 0)
        {
            HashSet<string> firstGroup = m_groups[0];
            Vector3 spawnPosition = Vector3.zero;

            foreach (string nodeId in firstGroup)
            {
                NodeData nodeInfo = m_nodeInfoDictionary[nodeId];
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = spawnPosition;
                sphere.name = nodeId; // Naming the sphere to match the node ID for easier identification

                // Setting the color based on the node's Color field
                if (!string.IsNullOrEmpty(nodeInfo.Color))
                {
                    Color color;
                    if (ColorUtility.TryParseHtmlString(nodeInfo.Color, out color))
                    {
                        sphere.GetComponent<Renderer>().material.color = color;
                    }
                }

                // Update spawnPosition for the next sphere
                spawnPosition += new Vector3(1.5f, 0, 0);
            }
        }
    }


    //private void Update()
    //{
    //    // Update LineRenderers to connect each GameObject to the next.
    //    for (int i = 0; i < m_Spheres.Count; i++)
    //    {
    //        m_LineRenderers[i].SetPosition(0, m_Spheres[i].transform.position);

    //        // If it's the last game object, connect to the first.
    //        if (i == m_Spheres.Count - 1)
    //        {
    //            m_LineRenderers[i].SetPosition(1, m_Spheres[0].transform.position);
    //        }
    //        else
    //        {
    //            m_LineRenderers[i].SetPosition(1, m_Spheres[i + 1].transform.position);
    //        }
    //    }
    //}



    void DFS(string nodeId, HashSet<string> currentGroup)
    {
        if (string.IsNullOrEmpty(nodeId) || !m_nodeInfoDictionary.ContainsKey(nodeId))
        {
            return;
        }

        if (m_visitedNodes.Contains(nodeId))
        {
            return;
        }

        m_visitedNodes.Add(nodeId);
        currentGroup.Add(nodeId);

        NodeData node = m_nodeInfoDictionary[nodeId];

        if (node.Edges == null)
        {
            return;
        }

        foreach (var edge in node.Edges)
        {
            DFS(edge, currentGroup);
        }
    }
}