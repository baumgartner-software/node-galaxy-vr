using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using TMPro;
using System;

public class ReadDataFromSD : MonoBehaviour
{
    [SerializeField]
    private JsonParser m_JsonParser = null;
    [SerializeField]
    private TMP_Dropdown m_DropDown;

    private List<string> m_DataFilesOnSd;

    private void Start()
    {
        m_DataFilesOnSd = new List<string>();

#if UNITY_ANDROID && !UNITY_EDITOR
        CheckFilesOnSd();
#endif
    }

    public void LoadFile()
    {
        if (m_DropDown.options[m_DropDown.value].text != "")
        {
            m_JsonParser.LoadNodes(m_DropDown.options[m_DropDown.value].text);
        }
    }

    private void CheckFilesOnSd()
    {
        try
        {
            DirectoryInfo di = new DirectoryInfo($"{Application.persistentDataPath}/");

            List<FileInfo> fileInfoList = new List<FileInfo>();
            fileInfoList.AddRange(di.GetFiles("*.json").ToList());

            List<TMP_Dropdown.OptionData> optionsData = new List<TMP_Dropdown.OptionData>();

            foreach (FileInfo fileInfo in fileInfoList)
            {
                m_DataFilesOnSd.Add(fileInfo.Name);

                TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData(fileInfo.Name);
                optionsData.Add(data);
            }

            m_DropDown.AddOptions(optionsData);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Test: Error while checking files on SD: {ex.Message}");
        }
    }
}
