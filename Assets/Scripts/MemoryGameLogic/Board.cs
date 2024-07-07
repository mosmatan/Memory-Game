using System;
using System.Collections.Generic;

namespace MemoryGame
{
    public class Board
    {
        private const int k_FirstValidRow = 0;
        private const int k_FirstValidColumn = 0;

        private Slot[,] m_Slots;
        private int m_Rows, m_Columns, m_ValuesCount;

        public int Rows { get { return m_Rows; } }
        public int Columns { get { return m_Columns; } }

        public void CreateNewBoard(int i_Row,int i_Column)
        {
            m_Slots = new Slot[i_Row, i_Column];
            m_Rows = i_Row;
            m_Columns = i_Column;
            m_ValuesCount = i_Row * i_Column / 2;

            InitilizeBoard();
        }

        private void InitilizeBoard()
        {
            Random random = new Random();
            List<int> valuesList = new List<int>();

            for (int row=0; row<m_Rows; row++)
            {
                for(int column=0; column<m_Columns; column++)
                {
                    if (m_Slots[row, column] == null)
                    {
                        int pairRow, pairColomn;
                        Slot thisSlot = new Slot();
                        Slot pairSlot = new Slot();

                        thisSlot.LogicValue = getRandomUnusedLogicValue(valuesList, random);
                        m_Slots[row, column] = thisSlot;

                        (pairRow, pairColomn) = getRandomValidSlotSpot(random);

                        pairSlot.LogicValue = thisSlot.LogicValue;
                        m_Slots[pairRow, pairColomn] = pairSlot;
                    }
                }
            }
        }

        private (int, int)getRandomValidSlotSpot(Random i_Random)
        {
            int row = i_Random.Next(m_Rows);
            int column = i_Random.Next(m_Columns);

            while (m_Slots[row, column] != null)
            {
                row = i_Random.Next(m_Rows);
                column = i_Random.Next(m_Columns);
            }

            return (row, column);
        }

        private int getRandomUnusedLogicValue(List<int> i_indexesList,Random i_Random)
        {
            int index = i_Random.Next(m_ValuesCount);

            while(i_indexesList.Contains(index))
            {
                index = i_Random.Next(m_ValuesCount);
            }

            i_indexesList.Add(index); 

            return index;
        }

        public Slot GetSlotInPlace(int i_Row,int i_Column)
        {
            return m_Slots[i_Row,i_Column];
        }

        public bool IsValidAndHiddenSlot(int i_Row, int i_Column, out string o_ErrorMessage)
        {
            bool valid = true;

            if ((i_Row < k_FirstValidRow) || (i_Row >= m_Rows))
            {
                o_ErrorMessage = "Row is invalid. please try again";
                valid = false;
            }
            else if ((i_Column < k_FirstValidColumn) || (i_Column >= m_Columns))
            {
                o_ErrorMessage = "Column is invalid. please try again";
                valid = false;
            }
            else if (m_Slots[i_Row, i_Column].State != eSlotState.Hidden)
            {
                o_ErrorMessage = "The slot you chose is already visible. please try again";
                valid = false;
            }
            else
            {
                o_ErrorMessage = null;
            }

            return valid;
        }
    }
}
