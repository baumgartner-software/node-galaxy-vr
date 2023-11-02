using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup m_CanvasGroup = null;

    private void OnValidate()
    {
        if (m_CanvasGroup == null)
            Debug.LogError($"CanvasGroup not set on {gameObject.name}");
    }

    public void SetActive(bool isPanelActive)
    {
        m_CanvasGroup.interactable = isPanelActive;
        m_CanvasGroup.blocksRaycasts = isPanelActive;

        gameObject.SetActive(isPanelActive);
    }
}
