using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MemoryGame;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    public Text m_FirstNameTxt;
    public Text m_SecondNameTxt = null;

    [SerializeField] private int m_SceneIndex;

    private void OnMouseDown()
    {
        PlayerDetails playerDetails = GameObject.Find("PlayersDetails").GetComponent<PlayerDetails>();

        PlayerDetails.s_Players[0] = new Player(m_FirstNameTxt.text, true);

        if(m_SecondNameTxt != null)
        {
            PlayerDetails.s_Players[1] = new Player(m_SecondNameTxt.text, true);
        }
        else
        {
            PlayerDetails.s_Players[1] = new Player("Robot", false);
        }

        SceneManager.LoadScene(m_SceneIndex);
    }
}
