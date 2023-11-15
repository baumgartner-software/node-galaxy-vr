using System;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_SampleNode = null;

    [SerializeField]
    private Panel m_PanelActiveAtStart = null;
    [SerializeField]
    private Panel m_NodeInfoPanel = null;

    private Panel m_ActivePanel = null;

    private void Awake()
    {
        Panel[] panels = FindObjectsOfType(typeof(Panel), true) as Panel[];
        Array.ForEach(panels, panel => panel.SetActive(false));

        m_PanelActiveAtStart.SetActive(true);
        m_ActivePanel = m_PanelActiveAtStart;
    }

    public void SetPanelActive(Panel panel)
    {
        if (m_ActivePanel != null)
        {
            m_ActivePanel.SetActive(false);
        }

        panel.SetActive(true);
        m_ActivePanel = panel;
    }

    public void SetNodeInfoPanel()
    {
        SetPanelActive(m_NodeInfoPanel);
    }


    //float y = 0.5f;

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        PlaceHundred();
    //    }
    //}

    //public void PlaceHundred()
    //{
    //    for (int x = 0; x < 10; x++)
    //    {
    //        for (int z = 0; z < 10; z++)
    //        {
    //            GameObject go = Instantiate(m_SampleNode, new Vector3(x * 0.007f, y,1 + (z * 0.007f)), Quaternion.identity);
    //            LineRenderer[] lr = go.GetComponentsInChildren<LineRenderer>();
    //            lr[0].SetPosition(0, Vector3.zero);
    //            lr[0].SetPosition(1, go.transform.position);

    //            lr[1].SetPosition(0, new Vector3(go.transform.position.x, go.transform.position.y - 0.05f, go.transform.position.z));
    //            lr[1].SetPosition(1, go.transform.position);

    //            lr[2].SetPosition(0, new Vector3(go.transform.position.x, go.transform.position.y + 0.05f, go.transform.position.z));
    //            lr[2].SetPosition(1, go.transform.position);
    //        }
    //    }

    //    y+= 0.1f;
    //}
}
