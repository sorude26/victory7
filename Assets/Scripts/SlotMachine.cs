using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour
{
    [Header("一回転に掛かる時間")]
    [SerializeField]
    float m_oneRotaionTime = 0.85f;
    [SerializeField]
    Slot[] m_testSlot = default;
    [SerializeField]
    SlotLine m_leftLine = default;
    bool[] m_stopF = default;
    Slot[,] m_currentSlot = new Slot[3,3];
    void Start()
    {
        m_leftLine.RotationTime = m_oneRotaionTime;
        foreach (var item in m_testSlot)
        {
            m_leftLine.SetSlot(item);
        }
        m_leftLine.StartSlot();
    }
    private void OnValidate()
    {
        m_leftLine.RotationTime = m_oneRotaionTime;
    }
    void MoveLeftLine()
    {

    }
    public void StopLeftLine()
    {

    }
    public void StopCenterLine()
    {

    }
    public void StopRightLine()
    {

    }
}
