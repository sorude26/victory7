using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public class StartSlotDataView : MonoBehaviour
    {
        [SerializeField]
        RectTransform[] m_slotBases = default;
        public void StartSet(PlayerParameter player)
        {
            foreach (var slot in player.StartSlotL)
            {
                Instantiate(slot, m_slotBases[0]);
            }
            foreach (var slot in player.StartSlotC)
            {
                Instantiate(slot, m_slotBases[1]);
            }
            foreach (var slot in player.StartSlotR)
            {
                Instantiate(slot, m_slotBases[2]);
            }
        }
    }
}