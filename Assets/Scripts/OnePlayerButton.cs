using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnePlayerButton : MonoBehaviour
{
    public GameObject m_MenuButtonsPanel;
    public GameObject m_OnePlayerPanel;

    private void OnMouseDown()
    {
        m_OnePlayerPanel.SetActive(true);
        m_MenuButtonsPanel.SetActive(false);
    }
}
