using System;
using System.Collections;
using System.Collections.Generic;

namespace MemoryGame
{
    internal class BotAI
    {
        private static Dictionary<int, Slot> s_SeenSlotsDictionary = new Dictionary<int, Slot>();
        private static Queue s_FoundPairsSlotsQueue = new Queue();

        public static void MemorizeSlot(Slot i_Slot)
        {
            if (!s_SeenSlotsDictionary.ContainsKey(i_Slot.LogicValue))
            {
                s_SeenSlotsDictionary.Add(i_Slot.LogicValue, i_Slot);
            }
            else if(s_SeenSlotsDictionary[i_Slot.LogicValue] != i_Slot)
            {
                s_FoundPairsSlotsQueue.Enqueue((s_SeenSlotsDictionary[i_Slot.LogicValue], i_Slot));
                s_SeenSlotsDictionary.Remove(i_Slot.LogicValue);
            }
        }

        internal static void RemoveSlotFromDictionary(Slot i_Slot)
        {
            if(s_SeenSlotsDictionary.ContainsKey(i_Slot.LogicValue))
            {
                s_SeenSlotsDictionary.Remove(i_Slot.LogicValue);
            }
        }

        internal static void ClearMemory()
        {
            s_FoundPairsSlotsQueue.Clear();
            s_SeenSlotsDictionary.Clear();
        }

        private static Slot[] getPairFromQueue()
        {
            Slot[] choosenSlots = new Slot[2];
            choosenSlots[0] = null;
            do
            {
                (choosenSlots[0], choosenSlots[1]) = ((Slot, Slot))s_FoundPairsSlotsQueue.Dequeue();

            } while (choosenSlots[0].State != eSlotState.Hidden && s_FoundPairsSlotsQueue.Count > 0);

            return choosenSlots;
        }

        private static Slot[] chooseTwoSlots(Board i_Board)
        {
            Slot[] choosenSlots = new Slot[2];

            choosenSlots[0] = getBotChooseSlot(i_Board);
            MemorizeSlot(choosenSlots[0]);

            if (s_FoundPairsSlotsQueue.Count > 0)
            {
                (choosenSlots[0], choosenSlots[1]) = ((Slot, Slot))s_FoundPairsSlotsQueue.Dequeue();
            }
            else
            {
                choosenSlots[0].State = eSlotState.Bare;
                choosenSlots[1] = getBotChooseSlot(i_Board);
                MemorizeSlot(choosenSlots[1]);
                choosenSlots[0].State = eSlotState.Hidden;
            }

            return choosenSlots;
        }

        public static Slot[] GetBotChoose(Board i_Board)
        {
            Slot[] choosenSlots = new Slot[2];
            choosenSlots[0] = null;

            if(s_FoundPairsSlotsQueue.Count > 0)
            {
                choosenSlots = getPairFromQueue();
            }

            if(choosenSlots[0] == null || choosenSlots[0].State != eSlotState.Hidden)
            {
                choosenSlots = chooseTwoSlots(i_Board);
            }

            return choosenSlots;
        }

        private static Slot getBotChooseSlot(Board i_Board)
        {
            Random random = new Random();
            string errorMessage;
            int row, column;

            do
            {
                row = random.Next(i_Board.Rows);
                column = random.Next(i_Board.Columns);

            } while (!i_Board.IsValidAndHiddenSlot(row, column, out errorMessage));

            return i_Board.GetSlotInPlace(row, column);
        }

    }
}
