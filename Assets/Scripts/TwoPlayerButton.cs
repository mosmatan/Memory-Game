using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayerButton : MonoBehaviour
{
    public GameObject m_MenuButtonsPanel;
    public GameObject m_TwoPlayerPanel;

    private void OnMouseDown()
    {
        m_TwoPlayerPanel.SetActive(true);
        m_MenuButtonsPanel.SetActive(false);
    }
}
