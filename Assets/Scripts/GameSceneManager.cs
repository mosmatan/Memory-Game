using MemoryGame;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager s_SceneGameManager = null;
    private GameLogic m_GameLogicManager;

    private Slot[] m_ChoosenSlots;
    private AudioSource m_FoundPairAudio;

    [SerializeField] private int m_Rows;
    [SerializeField] private int m_Columns;

    public PlayerScoreTablos m_Tablos;
    public GameObject m_WinnerSparkels;
    public GameObject m_WinnerTablo;
    public Text m_WinnerNameTxt;
    
    public bool CanPress { get; private set; }
    public List<Player> PlayersList { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        if(s_SceneGameManager == null)
        {
            s_SceneGameManager = this;
            m_ChoosenSlots = new Slot[2];
            CanPress = true;
            PlayersList = new List<Player>();
            m_WinnerSparkels.SetActive(false);
            m_WinnerTablo.SetActive(false);

            m_FoundPairAudio = GameObject.FindWithTag("FoundPairAudio").GetComponent<AudioSource>();

            m_GameLogicManager = new GameLogic();
            m_GameLogicManager.InisilizeBoard(m_Rows, m_Columns);

            PlayersList.Add(PlayerDetails.s_Players[0]);
            PlayersList.Add(PlayerDetails.s_Players[1]);

            m_GameLogicManager.AddPlayer(PlayersList[0]);
            m_GameLogicManager.AddPlayer(PlayersList[1]);

            BotAI.ClearMemory();
        }
    }

    public void restartGame()
    {
        StopAllCoroutines();

        m_GameLogicManager.InisilizeBoard(m_Rows, m_Columns);

        m_GameLogicManager.RestartPlayerPointsAndCurrentPlayer();
        m_GameLogicManager.ClearBotsMemory();

        foreach (GameObject cardObject in GameObject.FindGameObjectsWithTag("Card"))
        {
            GameCard card = cardObject.GetComponent<GameCard>();
            card.ResetCard();
        }

        CanPress = m_GameLogicManager.CurrentPlayer.IsHuman;
        m_Tablos.ResetTablos();

    }

    public Slot GetSlotAt(int i_Row, int i_Colomn)
    {
        return m_GameLogicManager.Board.GetSlotInPlace(i_Row, i_Colomn);
    }

    public void AddCardToChoice(Slot i_CardSlot)
    {
        if (m_ChoosenSlots[0] ==null)
        {
            m_ChoosenSlots[0] = i_CardSlot;
        }
        else
        {
            CanPress = false;
            m_ChoosenSlots[1] = i_CardSlot;
            StartCoroutine(endTurn());
            m_GameLogicManager.MemorizePlayerChoose(m_ChoosenSlots);
        }
    }

    private IEnumerator endTurn()
    {
        if(m_GameLogicManager.checkPairsAndUpdateBoard(m_ChoosenSlots))
        {
            if(AudioControllerScript.s_AudioOn)
            {
                m_FoundPairAudio.Play();
            }

            BotAI.RemoveSlotFromDictionary(m_ChoosenSlots[0]);
            m_Tablos.PrintPlayersScores();
        }
        else
        {
            m_GameLogicManager.switchCurrentPlayer();
            m_Tablos.ChangeFlashingTablo();
            yield return new WaitForSeconds(1);
        }

        Debug.Log(m_ChoosenSlots[0].LogicValue + " is " + m_ChoosenSlots[0].State.ToString());
        Debug.Log(m_GameLogicManager.CurrentPlayer.Name);

        yield return new WaitForSeconds(0.01f);

        resetTurn();
    }

    private void showBoard()
    {
        foreach (GameObject cardObject in GameObject.FindGameObjectsWithTag("Card"))
        {
            GameCard card = cardObject.GetComponent<GameCard>();
            card.ChangeCardToHisState();
        }
    }

    private void resetTurn()
    {
        showBoard();

        m_ChoosenSlots[0] = m_ChoosenSlots[1] = null;

        StartCoroutine(botTurn());
    }

    private IEnumerator botTurn()
    {
        Debug.Log("botTurn called");

        bool botPlayed = false;

        while(!m_GameLogicManager.CurrentPlayer.IsHuman && !m_GameLogicManager.IsAllPairsFound())
        {
            m_Tablos.PrintPlayersScores();
            yield return new WaitForSeconds(1);
            Debug.Log("is bot turn");
            CanPress = false;
            Slot[] botChoosenSlots = BotAI.GetBotChoose(m_GameLogicManager.Board);


            botChoosenSlots[0].State = eSlotState.Bare;
            showBoard();

            yield return new WaitForSeconds(1);

            botChoosenSlots[1].State = eSlotState.Bare;
            showBoard();

            if(!m_GameLogicManager.checkPairsAndUpdateBoard(botChoosenSlots))
            {
                m_GameLogicManager.switchCurrentPlayer();
            }

            yield return new WaitForSeconds(1);
            showBoard();

            botPlayed = true;
        }
        yield return new WaitForSeconds(0.01f);

        CanPress = true;
        Debug.Log("Bot Turn ended");

        if(botPlayed)
        {
            m_Tablos.ChangeFlashingTablo();
        }

        checkWinner();
    }

    private void checkWinner()
    {
        if (m_GameLogicManager.IsAllPairsFound())
        {
            CanPress = false;
            Player winner = m_GameLogicManager.checkWhoIsWinner();
            if (winner != null)
            {
                Debug.Log("Thw winner is: " + winner.Name);
            }
            else
            {
                Debug.Log("Its a tie");
            }

            StartCoroutine(playWinnerAnimAndRestartGame(winner));
        }
    }

    private IEnumerator playWinnerAnimAndRestartGame(Player i_Player)
    {
        m_WinnerSparkels.SetActive(true);
        m_WinnerTablo.SetActive(true);

        if(i_Player != null)
        {
            m_WinnerNameTxt.text = i_Player.Name;
        }
        else
        {
            m_WinnerNameTxt.text = "Tie";
        }

        yield return new WaitForSeconds(2);

        m_WinnerSparkels.SetActive(false);
        m_WinnerTablo.SetActive(false);

        restartGame();
    }

}
