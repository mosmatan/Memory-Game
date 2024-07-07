using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartGameButton : MonoBehaviour
{

    private Button m_Button;

    private void Start()
    {
        m_Button = GetComponent<Button>();

        m_Button.onClick.AddListener(OnClicked);
    }

    private void OnClicked()
    {
        GameSceneManager.s_SceneGameManager.restartGame();
    }
}
