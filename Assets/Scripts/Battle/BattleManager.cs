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
        [SerializeField]
        float m_actionInterval = 1f;
        int m_feverCount = 0;
        bool m_fever = false;
        bool m_start = false;
        bool m_waitNow = false;
        Stack<Action> m_battleActions = default;
        Stack<Action> m_effectActions = default;
        public Stack<Action> BattleActions { get => m_battleActions; }
        public Stack<Action> EffectActions { get => m_effectActions; }
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
                if (!m_waitNow)
                {
                    return;
                }
                Player.UseSkill();
            }
        }
        /// <summary>
        /// 戦闘開始
        /// </summary>
        void StartBattle()
        {
            GameManager.Instance.SlotSpeedChange();
            var m = Instantiate(EffectManager.Instance.Text);
            m.transform.position = new Vector2(1, 0);
            m.View("Start!!",Color.red,80);
            m_start = true;
            StartCoroutine(BattleUpdate());
        }
        /// <summary>
        /// 敵全体の行動を進める
        /// </summary>
        void EnemyUpdate()
        {
            foreach (var enemy in m_enemys)
            {
                enemy.CharacterUpdate();
            }
        }
        /// <summary>
        /// 初期化処理
        /// </summary>
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
            m_effectActions = new Stack<Action>();
            m_normalSlot.StartSet();
            m_sevenSlot.StartSet();
            m_sevenSlot.StopSlot += AddCount;
            m_player.StartSet();
            foreach (var enemy in m_enemys)
            {
                enemy.StartSet();
            }
            m_sevenSlot.gameObject.SetActive(false);
        }
        /// <summary>
        /// 生きているランダムな敵を返す
        /// </summary>
        /// <returns></returns>
        private EnemyControl GetRandomEnemy()
        {
            var enemys = m_enemys.Where(e => !e.IsDead).ToArray();
            int r = UnityEngine.Random.Range(0, enemys.Length);
            if (enemys.Length > 0)
            {
                return enemys[r];
            }
            return null;
        }
        /// <summary>
        /// 敵に指定ダメージを与える攻撃
        /// </summary>
        /// <param name="slotPower"></param>
        public void AttackEnemy(int slotPower)
        {
            GetRandomEnemy()?.Damage(m_player.GetPower(slotPower));
            SoundManager.Play(SEType.PaylineAttack);
        }
        /// <summary>
        /// 敵に割合ダメージを与える攻撃
        /// </summary>
        /// <param name="percentage"></param>
        public void AttackEnemyPercentage(int percentage)
        {
            GetRandomEnemy()?.PercentageDamage(percentage);
            SoundManager.Play(SEType.PaylineAttack);
        }
        /// <summary>
        /// 敵の回避計算を含めた割合ダメージ攻撃を行う
        /// </summary>
        /// <param name="percentage"></param>
        public void AttackEnemyCritical(int percentage)
        {
            GetRandomEnemy()?.CheckPercentageDamage(percentage);
            SoundManager.Play(SEType.PaylineAttack);
        }
        /// <summary>
        /// 指定カウント分敵の行動を遅らせる
        /// </summary>
        /// <param name="addCount"></param>
        public void AddEnemyActionCount(int addCount = 1)
        {
            GetRandomEnemy()?.AddAttackCount(addCount);
            SoundManager.Play(SEType.PaylineCharge);
        }
        /// <summary>
        /// プレイヤーに指定ダメージを与える
        /// </summary>
        /// <param name="damege"></param>
        public void AttackPlayer(int damege)
        {
            m_player.Damage(damege);
            SoundManager.Play(SEType.Attack);
        }
        /// <summary>
        /// Fever状態に変更する
        /// </summary>
        public void ChargeFeverTime()
        {
            SoundManager.Play(SEType.Jackpot);
            m_fever = true;
        }
        /// <summary>
        /// Fever状態でのカウントを増やす
        /// </summary>
        void AddCount()
        {
            m_feverCount++;
        }
        /// <summary>
        /// バトル終了の判定を行う
        /// </summary>
        public void CheckBattle()
        {
            if (BattleEnd)
            {
                return;
            }
            if (Player.CurrentHP <= 0)
            {
                //Debug.Log("ゲームオーバー");
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
                //Debug.Log("ステージClear");
                BattleEnd = true;
                MapData.AddBattleCount();
            }
            buildControl.gameObject.SetActive(true);
        }
        /// <summary>
        /// フェードアウトし、シーン遷移する
        /// </summary>
        public void NextScene()
        {
            FadeController.Instance.StartFadeOut(Next);
        }
        /// <summary>
        /// 戦闘後の指定シーン遷移処理
        /// </summary>
        void Next()
        {
            if (BattleData.Next)
            {
                BattleData.Next = false;
                MapData.AddClearCount();
                m_targetScene = BattleData.NextMap;
            }
            SceneManager.LoadScene(m_targetScene);
        }
        void LoadResult()
        {
            SceneManager.LoadScene("Result");
        }
        /// <summary>
        /// 戦闘中のループ
        /// </summary>
        /// <returns></returns>
        IEnumerator BattleUpdate()
        {
            while (!BattleEnd)
            {
                m_normalSlot.StartSlot();
                yield return WaitTime(0.5f);
                m_waitNow = true;
                m_normalSlot.SlotStartInput();
                yield return SlotWait();
                yield return SlotChackWait();
                Player.CharacterUpdate();
                EnemyUpdate(); 
                yield return null;
                yield return BattleAction();
            }
            PlayerData.SetData(m_player.CurrentHP, m_player.CurrentSP);
        }
        /// <summary>
        /// 演出の順次再生を行う
        /// </summary>
        /// <returns></returns>
        IEnumerator EffectAction()
        {
            while (m_effectActions.Count > 0 && !BattleEnd)
            {
                m_effectActions.Pop()?.Invoke();
                yield return WaitTime(0.2f);
            }
            yield return WaitTime(0.2f);
        }
        /// <summary>
        /// 戦闘の順次実行を行う
        /// </summary>
        /// <returns></returns>
        IEnumerator BattleAction()
        {
            while (m_battleActions.Count > 0 && !BattleEnd)
            {
                m_battleActions.Pop()?.Invoke();
                yield return WaitTime();
            }
            yield return WaitTime(0.2f);
        }
        /// <summary>
        /// スロット停止を待つ
        /// </summary>
        /// <returns></returns>
        IEnumerator SlotWait()
        {
            while (m_normalSlot.Stop && m_sevenSlot.Stop && !BattleEnd)
            {
                yield return null;
            }
        }
        /// <summary>
        /// スロット停止後の演出と戦闘処理終了を待つ
        /// </summary>
        /// <returns></returns>
        IEnumerator SlotChackWait()
        {
            while (m_normalSlot.SpinNow && !BattleEnd)
            {
                yield return null;
            }
            m_waitNow = false;
            yield return null;
            yield return EffectAction();
            yield return null;
            yield return BattleAction();
            if (m_fever)
            {
                yield return null;
                yield return FeverMode();
                m_fever = false;
            }
        }
        /// <summary>
        /// Fever状態での演出待機処理
        /// </summary>
        /// <returns></returns>
        IEnumerator SlotChackFever()
        {
            while (m_sevenSlot.SpinNow && !BattleEnd)
            {
                yield return null;
            }
            yield return null;
            yield return EffectAction();
            yield return null;
            yield return BattleAction();
        }
        /// <summary>
        /// インターバル時間の待機
        /// </summary>
        /// <returns></returns>
        IEnumerator WaitTime()
        {
            float timer = m_actionInterval;
            while (timer > 0 && !BattleEnd)
            {
                timer -= Time.deltaTime;
                yield return null;
            }
        }
        /// <summary>
        /// 指定時間待機する
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        IEnumerator WaitTime(float time)
        {
            float timer = time;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                yield return null;
            }
        }
        /// <summary>
        /// Fever状態のアップデート
        /// </summary>
        /// <returns></returns>
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
            while (m_feverCount < m_maxFeverCount)
            {
                m_sevenSlot.StartSlot();
                yield return WaitTime(0.5f);
                m_sevenSlot.SlotStartInput();
                yield return SlotWait();
                yield return SlotChackFever();
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