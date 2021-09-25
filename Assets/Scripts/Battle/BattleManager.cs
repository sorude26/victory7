using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance { get; private set; }
        [SerializeField]
        PlayerControl m_player = default;
        [SerializeField]
        EnemyControl[] m_enemys = default;
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
            m_player?.StartSet();
            foreach (var enemy in m_enemys)
            {
                enemy?.StartSet();
            }
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
    }
}