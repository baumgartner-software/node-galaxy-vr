using System;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private Panel m_PanelActiveAtStart = null;

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
}
