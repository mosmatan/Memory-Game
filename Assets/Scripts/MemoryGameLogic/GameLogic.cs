using System;
using System.Collections.Generic;
using System.Linq;

namespace MemoryGame
{
    public class GameLogic
    {
        private const int k_FirstPlayerIndex = 0;
        private const int k_StartGameScore = 0;
        private const int k_FirstChoosenSlot = 0;
        private const int k_SecondChoosenSlot = 1;
        private const int k_NoHiddenPairsLeft = 0;

        private Board m_Board;
        private int m_hiddenPairLeft;
        private List<Player> m_PlayerList;
        private int m_CurentPlayerIndex;

        public Player CurrentPlayer 
        {
            get 
            { 
                return m_PlayerList[m_CurentPlayerIndex]; 
            }
        }
        public Board Board 
        {
            get 
            {
                return m_Board;
            } 
        }

        public GameLogic()
        {
            m_PlayerList = new List<Player>();
            m_Board = new Board();
            m_CurentPlayerIndex = k_FirstPlayerIndex;
        }

        public void AddPlayer(Player i_Player)
        {
            m_PlayerList.Add(i_Player);
        }
        public void RemovePlayer(Player i_Player)
        {
            m_PlayerList.Remove(i_Player);
        }

        public void InisilizeBoard(int i_Rows,int i_Columns)
        {
            m_Board.CreateNewBoard(i_Rows, i_Columns);
            m_hiddenPairLeft = i_Rows * i_Columns / 2;
        }

        public bool IsAllPairsFound()
        {
            return m_hiddenPairLeft == k_NoHiddenPairsLeft;
        }

        public void switchCurrentPlayer()
        {
            m_CurentPlayerIndex = (m_CurentPlayerIndex + 1) % m_PlayerList.Count();
        }

        public bool checkPairsAndUpdateBoard(Slot[] i_ChossenSlots)
        {
            bool foundPair = false;

            if (i_ChossenSlots[k_FirstChoosenSlot].LogicValue == i_ChossenSlots[k_SecondChoosenSlot].LogicValue)
            {
                i_ChossenSlots[k_FirstChoosenSlot].State = eSlotState.Found;
                i_ChossenSlots[k_SecondChoosenSlot].State = eSlotState.Found;

                m_hiddenPairLeft--;
                CurrentPlayer.Point++;

                foundPair = true;
            }
            else
            {
                i_ChossenSlots[k_FirstChoosenSlot].State = eSlotState.Hidden;
                i_ChossenSlots[k_SecondChoosenSlot].State = eSlotState.Hidden;
            }

            return foundPair;
        }

        public Player checkWhoIsWinner()
        {
            Player winner = null;
            bool isTie = false;

            foreach (Player player in m_PlayerList)
            {
                if (winner == null || player.Point > winner.Point)
                {
                    winner = player;
                    isTie = false; 
                }
                else if (player.Point == winner.Point)
                {
                    isTie = true; 
                }
            }

            
            if (isTie)
            {
                winner = null;
            }

            return winner;
        }

        public static bool IsNumberOfSlotEven(int i_Row, int i_Column, out string o_ErrorMessage)
        {
            bool valid = true;

            if ((i_Column * i_Row) % 2 != 0)
            {
                o_ErrorMessage = "There must be an even number of slots. please try again";
                valid = false;
            }
            else
            {
                o_ErrorMessage = null;
            }

            return valid;
        }

        public Slot[] botChooseSlots()
        {
            Slot[] choosenSlots = null;

            if(!CurrentPlayer.IsHuman)
            {
                choosenSlots = BotAI.GetBotChoose(m_Board);
            }

            return choosenSlots;
        }

        public void MemorizePlayerChoose(Slot[] i_PlayerSlots)
        {
            for (int i = 0; i < i_PlayerSlots.Length; i++)
            {
                if (i_PlayerSlots[i] != null)
                {
                    BotAI.MemorizeSlot(i_PlayerSlots[i]);
                }
            }
        }

        public void RestartPlayerPointsAndCurrentPlayer()
        {
            foreach(Player player in m_PlayerList)
            {
                player.Point = k_StartGameScore;
            }
            m_CurentPlayerIndex = k_FirstPlayerIndex; 
        }

        public void ClearBotsMemory()
        {
            BotAI.ClearMemory();
        }
    }
}
