﻿using System.Collections;
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
    }
}
