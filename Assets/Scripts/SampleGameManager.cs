using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MemoryGame;

public class SampleGameManager : MonoBehaviour
{
    private GameLogic m_GameLogicManager;

    [SerializeField] private int m_Rows;
    [SerializeField] private int m_Columns;

    // Start is called before the first frame update
    void Start()
    {
        m_GameLogicManager = new GameLogic();
        initializeBoard();
        m_GameLogicManager.AddPlayer(new Player("Robot", false));

        StartCoroutine(runSample());
    }

    private void initializeBoard()
    {
        m_GameLogicManager.InisilizeBoard(m_Rows, m_Columns);
        m_GameLogicManager.ClearBotsMemory();
    }

    private void restartSample()
    {
        initializeBoard();

        foreach (GameObject cardObject in GameObject.FindGameObjectsWithTag("Card"))
        {
            GameCard card = cardObject.GetComponent<GameCard>();
            card.ResetCard();
        }
    }

    public Slot getSlotInPlace(int i_Row,int i_Column)
    {
        return m_GameLogicManager.Board.GetSlotInPlace(i_Row,i_Column);
    }

    private IEnumerator runSample()
    {
        const float timeBetweenTurns = 0.5f;

        Debug.Log("botTurn called");
        while (true)
        {
            while (!m_GameLogicManager.IsAllPairsFound())
            {
                yield return new WaitForSeconds(timeBetweenTurns);
                Debug.Log("is bot turn");

                Slot[] botChoosenSlots = BotAI.GetBotChoose(m_GameLogicManager.Board);

                botChoosenSlots[0].State = eSlotState.Bare;
                showBoard();

                yield return new WaitForSeconds(timeBetweenTurns);

                botChoosenSlots[1].State = eSlotState.Bare;
                showBoard();

                yield return new WaitForSeconds(timeBetweenTurns);
                m_GameLogicManager.checkPairsAndUpdateBoard(botChoosenSlots);
                showBoard();
                
            }
            yield return new WaitForSeconds(0.01f);

            restartSample();
            showBoard();
        }
    }

    private void showBoard()
    {
        foreach (GameObject cardObject in GameObject.FindGameObjectsWithTag("Card"))
        {
            GameCard card = cardObject.GetComponent<GameCard>();
            card.ChangeCardToHisState();
        }
    }
}
