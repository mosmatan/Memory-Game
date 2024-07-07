using System;

namespace MemoryGame
{
    public class Player
    {
        private string m_Name;
        private int m_PairCounter;
        private bool m_IsHuman;

        public Player(string name, bool isHuman)
        {
            m_Name = name;
            m_IsHuman = isHuman;
            m_PairCounter = 0;
        }

        public string Name { get { return m_Name; } set { m_Name = value; } }
        public int Point { get { return m_PairCounter; } set { m_PairCounter = value; } }
        public bool IsHuman { get { return m_IsHuman; } }
    }
}
