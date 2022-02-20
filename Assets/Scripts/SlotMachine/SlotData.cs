using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7 
{
    public class SlotData
    {
        public static List<Slot> LeftSlotData { get; private set; }
        public static List<Slot> CenterSlotData { get; private set; }
        public static List<Slot> RightSlotData { get; private set; }
        public static List<Slot> SevenSlotData { get; private set; }
        public static void StartSet(Slot[] left, Slot[] center, Slot[] right,Slot[] seven)
        {
            if (LeftSlotData != null)
            {
                LeftSlotData.Clear();
            }
            if (CenterSlotData != null)
            {
                CenterSlotData.Clear();
            }
            if (RightSlotData != null)
            {
                RightSlotData.Clear();
            }
            if (SevenSlotData != null)
            {
                SevenSlotData.Clear();
            }
            LeftSlotData = new List<Slot>();
            foreach (var slot in left)
            {
                LeftSlotData.Add(slot);
            }
            CenterSlotData = new List<Slot>();
            foreach (var slot in center)
            {
                CenterSlotData.Add(slot);
            }
            RightSlotData = new List<Slot>();
            foreach (var slot in right)
            {
                RightSlotData.Add(slot);
            }
            SevenSlotData = new List<Slot>();
            foreach (var slot in seven)
            {
                SevenSlotData.Add(slot);
            }
        }
        public static void AddSlot(Slot slot,int target)
        {
            if (target > 2 || target < 0 || !slot)
            {
                return;
            }
            if (target == 0)
            {
                int r = Random.Range(0, LeftSlotData.Count);
                LeftSlotData.Insert(r, slot);
            }
            else if (target == 1)
            {
                int r = Random.Range(0, CenterSlotData.Count);
                CenterSlotData.Insert(r, slot);
            }
            else
            {
                int r = Random.Range(0, RightSlotData.Count);
                RightSlotData.Insert(r, slot);
            }
        }
        public static void ShuffleSlot(int target)
        {
            if (target > 2 || target < 0)
            {
                return;
            }
            if (target == 0)
            {
                for (int i = 0; i < LeftSlotData.Count; i++)
                {
                    int r = Random.Range(0, LeftSlotData.Count);
                    Slot a = LeftSlotData[i];
                    LeftSlotData[i] = LeftSlotData[r];
                    LeftSlotData[r] = a;
                }
            }
            else if (target == 1)
            {
                for (int i = 0; i < CenterSlotData.Count; i++)
                {
                    int r = Random.Range(0, CenterSlotData.Count);
                    Slot a = CenterSlotData[i];
                    CenterSlotData[i] = CenterSlotData[r];
                    CenterSlotData[r] = a;
                }
            }
            else
            {
                for (int i = 0; i < RightSlotData.Count; i++)
                {
                    int r = Random.Range(0, RightSlotData.Count);
                    Slot a = RightSlotData[i];
                    RightSlotData[i] = RightSlotData[r];
                    RightSlotData[r] = a;
                }
            }
        }
        public static void RemoveSlot(int slot, int target)
        {
            if (target > 2 || target < 0 || slot < 0)
            {
                return;
            }
            if (target == 0)
            {
                if (LeftSlotData.Count > slot)
                {
                    LeftSlotData.RemoveAt(slot);
                }
            }
            else if (target == 1)
            {
                if (CenterSlotData.Count > slot)
                {
                    CenterSlotData.RemoveAt(slot);
                }
            }
            else
            {
                if (RightSlotData.Count > slot)
                {
                    RightSlotData.RemoveAt(slot);
                }
            }
        }
        public static bool LevelUpSlot(int slot, int target)
        {
            if (target > 2 || target < 0 || slot < 0)
            {
                return false;
            }
            if (target == 0)
            {
                if (LeftSlotData.Count > slot)
                {
                    var levelUp = LeftSlotData[slot].LevelUpTarget;
                    if (levelUp == null)
                    {
                        return false;
                    }
                    LeftSlotData.RemoveAt(slot);
                    LeftSlotData.Insert(slot, levelUp);
                    return true;
                }
            }
            else if (target == 1)
            {
                if (CenterSlotData.Count > slot)
                {
                    var levelUp = CenterSlotData[slot].LevelUpTarget;
                    if (levelUp == null)
                    {
                        return false;
                    }
                    CenterSlotData.RemoveAt(slot);
                    CenterSlotData.Insert(slot, levelUp);
                    return true;
                }
            }
            else
            {
                if (CenterSlotData.Count > slot)
                {
                    var levelUp = RightSlotData[slot].LevelUpTarget;
                    if (levelUp == null)
                    {
                        return false;
                    }
                    RightSlotData.RemoveAt(slot);
                    RightSlotData.Insert(slot, levelUp);
                    return true;
                }
            }
            return false;
        }
        public static int SevenSlotCount()
        {
            int count = 0;
            count += LeftSlotData.Count(s => s.Type == SkillType.Seven);
            count += CenterSlotData.Count(s => s.Type == SkillType.Seven);
            count += RightSlotData.Count(s => s.Type == SkillType.Seven);
            return count;
        }
    }
}
