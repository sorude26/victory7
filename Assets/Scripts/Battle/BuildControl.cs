using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public class BuildControl : MonoBehaviour
    {
        [SerializeField]
        Slot[] popSlot;
        [SerializeField]
        RectTransform m_base = default;
        [SerializeField]
        RectTransform m_lineL = default;
        [SerializeField]
        RectTransform m_lineC = default;
        [SerializeField]
        RectTransform m_lineR = default;
        List<Slot> m_line = new List<Slot>();
        void StartSet()
        {
            for (int i = 0; i < popSlot.Length; i++)
            {
                int r = Random.Range(0, popSlot.Length);
                Slot a = popSlot[i];
                popSlot[i] = popSlot[r];
                popSlot[r] = a;
            }
            for (int i = 0; i < 3; i++)
            {
                var oneSlot = Instantiate(popSlot[i]);
                oneSlot.SlotRect.transform.position = m_base.position;
                oneSlot.transform.SetParent(m_base);
                m_line.Add(oneSlot);
            }
        }
        void Update()
        {

        }
    }
}