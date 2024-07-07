using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMenuButton : MonoBehaviour
{
    private Button m_Button;

    private void Start()
    {
        m_Button = GetComponent<Button>();

        m_Button.onClick.AddListener(OnClicked);
    }

    private void OnClicked()
    {
        PlayerDetails.s_Players = null;

        Destroy(GameObject.Find("PlayersDetails"));

        SceneManager.LoadScene(0);
    }
}
