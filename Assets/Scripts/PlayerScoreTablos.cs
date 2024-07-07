using MemoryGame;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreTablos : MonoBehaviour
{
    public Text m_FirstPlayerText;
    public Text m_SecondPlayerText;

    public Animator m_FirstPlayerAnimator;
    public Animator m_SecondPlayerAnimator;

    private bool m_FirstPlayerFlash;

    // Start is called before the first frame update
    void Start()
    {
        ResetTablos();
    }
    
    private string getPlayerStringFormat(Player i_Player)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(i_Player.Name);
        stringBuilder.AppendLine(string.Format("Score: {0}", i_Player.Point));

        return stringBuilder.ToString();
    }

    public void PrintPlayersScores()
    {
        m_FirstPlayerText.text = getPlayerStringFormat(GameSceneManager.s_SceneGameManager.PlayersList[0]);
        m_SecondPlayerText.text = getPlayerStringFormat(GameSceneManager.s_SceneGameManager.PlayersList[1]);
    }

    public void ChangeFlashingTablo()
    {
        if (m_FirstPlayerFlash)
        {
            m_FirstPlayerFlash = false;
            m_FirstPlayerAnimator.StopPlayback();
            m_SecondPlayerAnimator.StartPlayback();
        }
        else
        {
            m_FirstPlayerFlash = true;
            m_SecondPlayerAnimator.StopPlayback();
            m_FirstPlayerAnimator.StartPlayback();
        }
    }

    public void ResetTablos()
    {
        m_FirstPlayerFlash = true;

        ChangeFlashingTablo();
        PrintPlayersScores();
    }
    
        
}
