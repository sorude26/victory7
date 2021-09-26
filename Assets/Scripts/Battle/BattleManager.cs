using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace victory7
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance { get; private set; }
        [SerializeField]
        PlayerControl m_player = default;
        [SerializeField]
        EnemyControl[] m_enemys = default;
        [SerializeField]
        SlotMachine m_normalSlot = default;
        [SerializeField]
        SlotMachine m_sevenSlot = default;
        [SerializeField]
        int m_maxFeverCount = 5;
        int m_feverCount = 0;
        public PlayerControl Player { get => m_player; }
        private void Awake()
        {
            Instance = this;
        }
        void Start()
        {
            StartSet();
        }

        void Update()
        {
            foreach (var enemy in m_enemys)
            {
                enemy?.CharacterUpdate();
            }
        }

        void StartSet()
        {
            m_normalSlot.StartSet();
            m_sevenSlot.StartSet();
            m_sevenSlot.StopSlot += AddCount;
            m_player?.StartSet();
            foreach (var enemy in m_enemys)
            {
                enemy?.StartSet();
            }
            m_sevenSlot.gameObject.SetActive(false);
        }
        public void AttackEnemy(int slotPower)
        {
            int r = Random.Range(0, m_enemys.Length);
            m_enemys[r].Damage(m_player.GetPower(slotPower));
        }
        public void AttackPlayer(int damege)
        {
            m_player.Damage(damege);
        }
        public void ChargeFeverTime()
        {
            StartCoroutine(FeverMode());
        }
        void AddCount()
        {
            m_feverCount++;
        }
        IEnumerator FeverMode()
        {
            while (m_normalSlot.Chack)
            {
                yield return null;
            }
            m_feverCount = 0;
            m_normalSlot.StopAll();
            while (!m_normalSlot.Stop)
            {
                yield return null;
            }
            m_normalSlot.gameObject.SetActive(false);
            m_sevenSlot.gameObject.SetActive(true);
            while (m_feverCount < m_maxFeverCount)
            {
                yield return null;
            }
            m_sevenSlot.StopAll();
            while (!m_sevenSlot.Stop)
            {
                yield return null;
            }
            m_normalSlot.gameObject.SetActive(true);
            m_sevenSlot.gameObject.SetActive(false);
        }
    }
}