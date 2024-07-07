using System;
using System.Reflection;

namespace MemoryGame
{
    public class Slot
    {
        public static readonly int sr_Empty = -1;

        private int m_LogicValue;
        private eSlotState m_State;

        public int LogicValue { 
            get { return m_LogicValue; } 
            set 
            { 
                if(m_LogicValue == sr_Empty)
                {
                    m_LogicValue = value;
                }
            } 
        }
        public eSlotState State { get {  return m_State; }  set { m_State = value; } }

        public Slot() 
        {
            m_State = eSlotState.Hidden;
            m_LogicValue = sr_Empty;
        }

    }
}
