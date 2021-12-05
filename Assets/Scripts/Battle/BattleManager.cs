using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace victory7
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance { get; private set; }
        [Header("遷移先シーン名")]
        [SerializeField]
        private string m_targetScene = "MapScene";
        [SerializeField]
        PlayerControl m_player = default;
        [SerializeField]
        Transform[] m_enemyPos = default;
        [SerializeField]
        EnemyControl[] m_enemys = default;
        [SerializeField]
        SlotMachine m_normalSlot = default;
        [SerializeField]
        SlotMachine m_sevenSlot = default;
        [SerializeField]
        BuildControl buildControl = default;
        [SerializeField]
        int m_maxFeverCount = 5;
        int m_feverCount = 0;
        bool m_start = false;
        Stack<Action> m_battleActions = default;
        public Stack<Action> BattleActions { get => m_battleActions; }
        public bool BattleEnd { get; private set; }
        public PlayerControl Player { get => m_player; }
        private void Awake()
        {
            Instance = this;
        }
        void Start()
        {
            StartSet();
            buildControl.StartSet();
            FadeController.Instance.StartFadeIn(StartBattle);
        }
        private void Update()
        {
            if (BattleEnd)
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.Q) && m_start)
            {
                Player.UseSkill();
            }
        }
        void StartBattle()
        {
            var m = Instantiate(EffectManager.Instance.Text);
            m.transform.position = new Vector2(1, 0);
            m.View("Start!!",Color.red,80);
            m_start = true;
            StartCoroutine(Battle());
        }
        
        IEnumerator Battle()
        {
            m_normalSlot.StartSlot();
            m_normalSlot.StopSlot += EnemyUpdate;
            while (!BattleEnd)
            {
                yield return BattleAction();
                yield return SlotWait();                
            }
            m_normalSlot.StopSlot -= EnemyUpdate;
            PlayerData.SetData(m_player.CurrentHP, m_player.CurrentSP);
        }
        void EnemyUpdate()
        {
            foreach (var enemy in m_enemys)
            {
                enemy?.CharacterUpdate();
            }
        }
        void StartSet()
        {
            if (BattleData.Enemys != null && BattleData.Enemys.Length > 0)
            {
                m_enemys = new EnemyControl[BattleData.Enemys.Length];
                for (int i = 0; i < BattleData.Enemys.Length; i++)
                {
                    var enemy = Instantiate(BattleData.Enemys[i]);
                    enemy.transform.position = m_enemyPos[i].position;
                    m_enemys[i] = enemy;
                }
            }
            m_battleActions = new Stack<Action>();
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
            var enemys = m_enemys.Where(e => !e.IsDead).ToArray();
            int r = UnityEngine.Random.Range(0, enemys.Length);
            if (enemys.Length > 0)
            {
                enemys[r].Damage(m_player.GetPower(slotPower));
            }
            SoundManager.Play(SEType.PaylineAttack);
        }
        public void AttackPlayer(int damege)
        {
            m_player.Damage(damege);
            SoundManager.Play(SEType.Attack);
        }
        public void ChargeFeverTime()
        {
            SoundManager.Play(SEType.Jackpot);
            StartCoroutine(FeverMode());
        }
        void AddCount()
        {
            m_feverCount++;
        }
        public void CheckBattle()
        {
            if (BattleEnd)
            {
                return;
            }
            if (Player.CurrentHP <= 0)
            {
                Debug.Log("ゲームオーバー");
                FadeController.Instance.StartFadeOut(LoadResult);
                BattleEnd = true;
                return;
            }
            else
            {
                foreach (var enemy in m_enemys)
                {
                    if (!enemy.IsDead)
                    {
                        return;
                    }
                }
                Debug.Log("ステージClear");
                BattleEnd = true;
            }
            buildControl.gameObject.SetActive(true);
        }
        IEnumerator FeverMode()
        {
            while (m_normalSlot.CheckNow)
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
            m_sevenSlot.StartSlot();
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
            m_normalSlot.StartSlot();
        }
        public void NextScene()
        {
            FadeController.Instance.StartFadeOut(Next);
        }
        void Next()
        {
            SceneManager.LoadScene(m_targetScene);
        }
        void LoadResult()
        {
            SceneManager.LoadScene("Resultα");
        }
        IEnumerator BattleAction()
        {
            while (m_battleActions.Count > 0)
            {
                m_battleActions.Pop()?.Invoke();
                yield return new WaitForSeconds(1f);
            }
        }
        IEnumerator SlotWait()
        {
            while (m_normalSlot.Stop && m_sevenSlot.Stop)
            {
                yield return null;
            }
        }
    }
}