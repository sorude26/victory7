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
        UnityEngine.UI.Image m_background = default;
        [SerializeField]
        int m_maxFeverCount = 5;
        [SerializeField]
        float m_actionInterval = 0.2f;
        [SerializeField]
        float m_waitInterval = 0.2f;
        [SerializeField]
        BGMType m_buildBGM = BGMType.Build;
        [SerializeField]
        BGMType m_resultBGM = BGMType.Result;
        [SerializeField]
        BGMType m_feverBGM = BGMType.Battle8;

        const float SlotWaitTime = 0.5f;

        float m_waitAction = 0.2f;
        int m_feverCount = 0;
        bool m_fever = false;
        bool m_start = false;
        bool m_waitNow = false;
        BGMType m_battleBGM = default;

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
            FadeController.Instance.StartFadeIn(() => TutorialController.Instance.PlayTutorial(TutorialType.Battle, StartBattle));
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
                m_player.UseSkill();
            }
        }
        /// <summary>
        /// 戦闘開始
        /// </summary>
        void StartBattle()
        {
            GameManager.Instance.SlotSpeedChange();
            EffectManager.Instance.PlayEffect(EffectType.Start, Vector2.zero);
            SoundManager.Play(SEType.StartSpin);
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
                    var enemy = Instantiate(BattleData.Enemys[i],transform);
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
            m_background.sprite = MapData.CurrentMap.BattleBackground;
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
            var enemy = GetRandomEnemy();
            if (enemy)
            {
                m_player.ActionStack.Push(() => enemy.Damage(m_player.GetPower(slotPower)));
                m_player.AttackAction(enemy);
                m_waitAction = m_player.ActionTime;
            }
        }
        /// <summary>
        /// 敵に割合ダメージを与える攻撃
        /// </summary>
        /// <param name="percentage"></param>
        public void AttackEnemyPercentage(int percentage)
        {
            var enemy = GetRandomEnemy();
            if (enemy)
            {
                m_player.ActionStack.Push(() => enemy.PercentageDamage(percentage));
                m_player.AttackAction(enemy);
                SoundManager.Play(SEType.SkillAttack);
                m_waitAction = m_player.ActionTime;
            }
        }
        /// <summary>
        /// 敵の回避計算を含めた割合ダメージ攻撃を行う
        /// </summary>
        /// <param name="percentage"></param>
        public void AttackEnemyCritical(int percentage)
        {
            var enemy = GetRandomEnemy();
            if (enemy)
            {
                m_player.ActionStack.Push(() => enemy.CheckPercentageDamage(percentage));
                m_player.AttackAction(enemy);
                SoundManager.Play(SEType.SkillAttack);
                m_waitAction = m_player.ActionTime;
            }
        }
        /// <summary>
        /// 指定カウント分敵の行動を遅らせる
        /// </summary>
        /// <param name="addCount"></param>
        public void AddEnemyActionCount(int addCount = 1)
        {
            var enemy = GetRandomEnemy();
            if (enemy)
            {
                m_player.ActionStack.Push(() => enemy.AddAttackCount(addCount));
                m_player.AttackAction(enemy);
                SoundManager.Play(SEType.PaylineCharge);
                m_waitAction = m_player.ActionTime;
            }
        }
        /// <summary>
        /// プレイヤーに指定ダメージを与える
        /// </summary>
        /// <param name="damege"></param>
        public void AttackPlayer(int damege,EffectType attackEffect)
        {
            m_player.Damage(damege);
            EffectManager.Instance.PlayEffect(attackEffect, m_player.CenterPos.position);
            if (m_player.CurrentGP >= 1)
            {
                SoundManager.Play(SEType.Guard);
            }
            else
            {
                //SoundManager.Play(SEType.Attack);
            }
        }
        public void SetActionTime(CharacterControl character)
        {
            m_waitAction = character.ActionTime;
        }
        public void SetActionTime(float actionTime)
        {
            m_waitAction = actionTime;
        }
        /// <summary>
        /// Fever状態に変更する
        /// </summary>
        public void ChargeFeverTime()
        {
            m_player.Fever();
            EffectManager.Instance.PlayEffect(EffectType.Fever, Vector2.zero);
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
            if (m_player.CurrentHP <= 0)
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
                m_player.Win();
            }
        }
        public void BuildPanelOpen()
        {
            buildControl.StartBuild();
            SoundManager.PlayBGM(m_buildBGM);
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
            ShakeController.ResetEvent();
            if (BattleData.Next)
            {
                BattleData.Next = false;
                MapData.SetNextMap();
                MapData.AddClearCount();
            }
            SceneManager.LoadScene(m_targetScene);
        }
        void LoadResult()
        {
            ShakeController.ResetEvent();
            SoundManager.PlayBGM(m_resultBGM);
            SceneManager.LoadScene("Result");
        }
        /// <summary>
        /// 戦闘中のループ
        /// </summary>
        /// <returns></returns>
        IEnumerator BattleUpdate()
        {
            m_battleBGM = SoundManager.CurrentBGM;
            while (!BattleEnd)
            {
                m_player.SkillCheck();
                m_normalSlot.StartSlot();
                yield return WaitTime(SlotWaitTime);
                m_waitNow = true;
                m_normalSlot.SlotStartInput();
                yield return SlotWait();
                yield return SlotChackWait();
                m_player.CharacterUpdate();
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
                yield return WaitTime();
            }
            m_waitAction = m_waitInterval;
            yield return WaitTime();
        }
        /// <summary>
        /// 戦闘の順次実行を行う
        /// </summary>
        /// <returns></returns>
        IEnumerator BattleAction()
        {
            while (m_battleActions.Count > 0 && !BattleEnd)
            {
                m_waitAction = m_actionInterval;
                m_battleActions.Pop()?.Invoke();
                yield return WaitTime(m_waitAction);
            }
            yield return WaitTime();
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
            m_waitNow = false;
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
            float timer = m_waitInterval;
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
            m_player.GetFeverEffect.Play();
            SoundManager.PlayBGM(m_feverBGM);
            while (m_normalSlot.CheckNow && !BattleEnd)
            {
                yield return null;
            }
            m_feverCount = 0;
            m_normalSlot.StopAll();
            while (!m_normalSlot.Stop && !BattleEnd)
            {
                yield return null;
            }
            yield return WaitTime(SlotWaitTime);
            m_normalSlot.gameObject.SetActive(false);
            m_sevenSlot.gameObject.SetActive(true);
            yield return WaitTime(SlotWaitTime);
            while (m_feverCount < m_maxFeverCount && !BattleEnd)
            {
                m_sevenSlot.StartSlot();
                m_waitNow = true;
                yield return WaitTime(SlotWaitTime);
                m_sevenSlot.SlotStartInput();
                yield return SlotWait();
                yield return SlotChackFever();
            }
            m_sevenSlot.StopAll();
            m_player.GetFeverEffect.Stop();
            SoundManager.PlayBGM(m_battleBGM);
            while (!m_sevenSlot.Stop && !BattleEnd)
            {
                yield return null;
            }
            m_normalSlot.gameObject.SetActive(true);
            m_sevenSlot.gameObject.SetActive(false);
        }
    }
}