using UnityEngine;
using TMPro;

public class NodeInfoPanel : MonoBehaviour
{
    [SerializeField]
    private MenuManager m_MenuManager = null;

    [SerializeField]
    private TMP_InputField m_IdInputField = null;
    [SerializeField]
    private TMP_InputField m_TextInputField = null;
    [SerializeField]
    private TMP_InputField m_AdditionalInfoInputField = null;
    [field:SerializeField]
    public TMP_Dropdown ShapesDropdown = null;


    [SerializeField]
    private JsonParser m_JsonParser = null;

    private string m_NodeKey = "";



    public void Open()
    {
        m_MenuManager.SetNodeInfoPanel();
    }

    public void SetKey(string key)
    {
        m_NodeKey = key;
    }

    public void SetIdText(string text)
    {
        m_IdInputField.text = text;
    }

    public void SetTextText(string text)
    {
        m_TextInputField.text = text;
    }

    public void SetAdditionalText(string text)
    {
        m_AdditionalInfoInputField.text = text;
    }

    public void SetDropdownShapeValue(string shape)
    {
        for (int i = 0; i < ShapesDropdown.options.Count; i++)
        {
            if (ShapesDropdown.options[i].text.Equals(shape))
            {
                ShapesDropdown.value = i;

                ShapesDropdown.onValueChanged.Invoke(i);

                break;
            }
        }
    }

    public void Save()
    {
        int selectedIndex = ShapesDropdown.value;
        string selectedOptionText = ShapesDropdown.options[selectedIndex].text;

        m_JsonParser.UpdateNodeInfo(m_NodeKey, m_IdInputField.text, m_TextInputField.text, m_AdditionalInfoInputField.text, selectedOptionText);
    }
}
