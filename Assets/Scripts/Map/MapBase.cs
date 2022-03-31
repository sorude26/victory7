using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace victory7
{
    public class MapBase : MonoBehaviour
    {
        [SerializeField]
        GameObject m_player = default;
        [SerializeField]
        GameObject m_target = default;
        [SerializeField]
        Image m_background = default;
        [Header("遷移先シーン名")]
        [SerializeField]
        string m_targetScene = "BattleTest";
        [SerializeField]
        MapPoint m_bossPoint = default;
        [SerializeField]
        GameObject m_startPoint = default;
        [SerializeField]
        RemoveSlotControl m_removeSlot = default;
        [SerializeField]
        LevelUpControl m_levelUp = default;
        [SerializeField]
        HeelControl m_heel = default;
        [SerializeField]
        MaxHPUpControl m_maxHPUp = default;
        [SerializeField]
        MapPlayerDataPanel m_playerDataPanel = default;
        [SerializeField]
        Text m_mapCount = default;
        [SerializeField]
        MapPlayerData m_mapPlayerData = default;
        [SerializeField]
        float m_bgmChangeTime = 0.5f;
        [Header("BGM変更マップ走破数")]
        [SerializeField]
        int m_bgmChangeCount = 4;
        [SerializeField]
        BGMType m_startMapBGM = BGMType.Map1;
        [SerializeField]
        BGMType m_hardMapBGM = BGMType.Map2;
        [SerializeField]
        BGMType m_normalBattleBGM = BGMType.Battle1;
        [SerializeField]
        BGMType m_hardBattleBGM = BGMType.Battle3;
        [SerializeField]
        BGMType m_bossBattleBGM = BGMType.Battle5;
        [SerializeField]
        BGMType m_exBattleBGM = BGMType.Battle6;

        int m_currentPos = 0;
        bool m_gard = false;
        bool m_create = default;
        bool m_load = default;
        bool m_event = false;
        Vector2 m_bossPos = default;
        Vector2Int m_playerPos = default;
        MapEventControlBase m_currentEvent = default;
        List<MapPoint> m_targetPos = default;
        List<MapPoint>[] m_mapData = default;
        BGMType m_battleBGM = BGMType.Battle1;
        private void Start()
        {
            m_create = MapData.Create;
            m_playerPos = MapData.PlayerPos;
            CreateMap();
            LodeMap();
            m_removeSlot.OnEventEnd += () => FadeController.Instance?.StartFadeOutIn(() => m_event = false);
            m_levelUp.OnEventEnd += () => FadeController.Instance?.StartFadeOutIn(() => m_event = false);
            m_heel.OnEventEnd += () => FadeController.Instance?.StartFadeOutIn(() => { m_event = false; m_mapPlayerData.DataUpdate(); });
            m_maxHPUp.OnEventEnd += () => FadeController.Instance?.StartFadeOutIn(() => { m_event = false; m_mapPlayerData.DataUpdate(); });
            m_playerDataPanel.OnEventEnd += () => m_event = false;
            FadeController.Instance?.StartFadeIn(() => TutorialController.Instance.PlayTutorial(TutorialType.Map, () => m_gard = false));
            m_gard = true;
            m_background.sprite = MapData.CurrentMap.Background;
            m_mapCount.text = (1 + MapData.ClearStageCount).ToString();
            if (MapData.ClearStageCount < m_bgmChangeCount)
            {
                SoundManager.PlayBGM(m_startMapBGM, m_bgmChangeTime);
            }
            else
            {
                SoundManager.PlayBGM(m_hardMapBGM, m_bgmChangeTime);
            }
        }
        private void Update()
        {
            if (m_gard || m_load)
            {
                return;
            }
            if (Input.GetButtonDown("Vertical"))
            {
                if (Input.GetAxisRaw("Vertical") > 0)
                {
                    TargetChange(1);
                }
                else
                {
                    TargetChange(-1);
                }
            }
            else if (Input.GetButtonDown("Horizontal") && m_event)
            {
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    m_currentEvent?.MoveLine(1);
                }
                else
                {
                    m_currentEvent?.MoveLine(-1);
                }
            }
            if (Input.GetButtonDown("Submit"))
            {
                if (m_event)
                {
                    m_currentEvent?.SelectAction();
                    return;
                }
                Next();
            }
            if (Input.GetButtonDown("Cancel"))
            {
                if (m_event)
                {
                    m_currentEvent?.CancelAction();
                    return;
                }
            }
            if (Input.GetKeyDown(KeyCode.Q) && !m_event)
            {
                m_currentEvent = m_playerDataPanel;
                m_currentEvent.SelectEvent();
                m_event = true;
                m_load = false;
                return;
            }
        }
        void Next()
        {
            if (m_targetPos.Count <= 0)
            {
                m_player.transform.position = m_bossPos;
                MapData.PositionReset();
                BattleData.SetData(MapData.CurrentMap.BossBattleData);
                BattleData.Next = true;
                m_battleBGM = m_bossBattleBGM;
                FadeController.Instance.StartFadeOut(Battle);
                return;
            }
            m_load = true;
            var map = m_targetPos[m_currentPos];
            m_player.transform.position = map.transform.position;
            m_targetPos.Clear();
            if (map.PosNumber < m_mapData[map.LineNumber].Count - 1)
            {
                m_targetPos.Add(m_mapData[map.LineNumber][map.PosNumber + 1]);
                if (map.LineNumber < m_mapData.Length - 1 && map.UpTarget > 0 && map.UpTarget < MapData.CurrentMap.MapLines[map.LineNumber + 1].Points.Length)
                {
                    m_targetPos.Add(m_mapData[map.LineNumber + 1][map.UpTarget]);
                }
                if (map.LineNumber > 0 && map.DownTarget > 0 && map.DownTarget < MapData.CurrentMap.MapLines[map.LineNumber - 1].Points.Length)
                {
                    m_targetPos.Add(m_mapData[map.LineNumber - 1][map.DownTarget]);
                }
            }
            m_currentPos = 0;
            if (m_targetPos.Count > 0)
            {
                m_target.transform.position = m_targetPos[m_currentPos].transform.position;
            }
            else
            {
                m_target.transform.position = m_bossPos;
            }
            switch (map.Type)
            {
                case MapPointType.Enemy:
                    BattlePoint(map.LineNumber, map.PosNumber, map.TypeNumber);
                    break;
                case MapPointType.Remove:
                    m_currentEvent = m_removeSlot;
                    m_currentEvent.SelectEvent();
                    m_event = true;
                    m_load = false;
                    break;
                case MapPointType.LevelUp:
                    m_currentEvent = m_levelUp;
                    m_currentEvent.SelectEvent();
                    m_event = true;
                    m_load = false;
                    break;
                case MapPointType.Heel:
                    m_currentEvent = m_heel;
                    m_currentEvent.SelectEvent();
                    m_event = true;
                    m_load = false;
                    break;
                case MapPointType.MaxHPUp:
                    m_currentEvent = m_maxHPUp;
                    m_currentEvent.SelectEvent();
                    m_event = true;
                    m_load = false;
                    break;
                default:
                    break;
            }
            SoundManager.Play(SEType.Deployment);
        }

        void BattlePoint(int lineNumber, int posNumber, int typeNumber)
        {
            MapData.SetData(new Vector2Int(lineNumber, posNumber));
            if (typeNumber < 0)
            {
                BattleData.SetData(MapData.CurrentMap.ExBattleData);
                m_battleBGM = m_exBattleBGM;
            }
            else
            {
                BattleData.SetData(MapData.CurrentMap.GetNormalBattle());
                if (MapData.ClearStageCount < m_bgmChangeCount)
                {
                    m_battleBGM = m_normalBattleBGM;
                }
                else
                {
                    m_battleBGM = m_hardBattleBGM;
                }
            }
            FadeController.Instance.StartFadeOut(Battle);
        }
        void TargetChange(int a)
        {
            if (m_event)
            {
                m_currentEvent.MoveSlot(a);
                return;
            }
            if (m_targetPos.Count <= 1)
            {
                return;
            }
            if (a > 0)
            {
                m_currentPos++;
                if (m_currentPos >= m_targetPos.Count)
                {
                    m_currentPos = 0;
                }
            }
            else if (a < 0)
            {
                m_currentPos--;
                if (m_currentPos < 0)
                {
                    m_currentPos = m_targetPos.Count - 1;
                }
            }
            SoundManager.Play(SEType.Choice);
            m_target.transform.position = m_targetPos[m_currentPos].transform.position;
        }
        void CreateMap()
        {
            var mapLines = MapData.CurrentMap.MapLines;
            var mapSize = MapData.CurrentMap.MapSize;
            if (mapLines.Length <= 0)
            {
                Debug.LogError("マップ横軸データが足りません");
                return;
            }
            m_mapData = new List<MapPoint>[mapLines.Length];
            for (int i = 0; i < m_mapData.Length; i++)
            {
                m_mapData[i] = new List<MapPoint>();
            }
            float lineSpan = mapLines.Length > 1 ? mapSize.y / (mapLines.Length - 1) : 0;
            Vector2 startPos = -mapSize / 2;
            var boss = Instantiate(m_bossPoint);
            boss.transform.position = new Vector2(mapSize.x + startPos.x + MapData.CurrentMap.PointRange, 0);
            boss.transform.SetParent(transform);
            m_bossPos = boss.transform.position;
            var start = Instantiate(m_startPoint);
            start.transform.position = new Vector2(startPos.x - MapData.CurrentMap.PointRange, 0);
            start.transform.SetParent(transform);
            m_player.transform.position = start.transform.position;
            for (int i = 0; i < mapLines.Length; i++)
            {
                Vector2 front = start.transform.position;
                for (int k = 0; k < mapLines[i].Points.Length; k++)
                {
                    var map = Instantiate(mapLines[i].Points[k]);
                    float pointSpan = mapLines[i].Points.Length > 1 ? mapSize.x / (mapLines[i].Points.Length - 1) : 0;
                    map.transform.position = new Vector2(k * pointSpan, i * lineSpan) + startPos;
                    map.SetData(i, k);
                    map.SetUpTarget(mapLines[i].TargetPointU[k]);
                    map.SetDownTarget(mapLines[i].TargetPointD[k]);
                    map.transform.SetParent(transform);
                    m_mapData[i].Add(map);
                    map.DrawLine(front, map.transform.position);
                    front = map.transform.position;
                }
                boss.DrawLine(front, boss.transform.position);
            }
            m_targetPos = new List<MapPoint>();
            for (int i = 0; i < m_mapData.Length; i++)
            {
                for (int k = 0; k < m_mapData[i].Count; k++)
                {
                    if (i > 0)
                    {
                        int pos = m_mapData[i][k].DownTarget;
                        if (pos > 0 && pos < m_mapData[i - 1].Count)
                        {
                            var map = m_mapData[i - 1][pos];
                            map.DrawLine(m_mapData[i][k].transform.position, map.transform.position);
                        }
                    }
                    if (i < m_mapData.Length - 1)
                    {
                        int pos = m_mapData[i][k].UpTarget;
                        if (pos > 0 && pos < m_mapData[i + 1].Count)
                        {
                            var map = m_mapData[i + 1][pos];
                            map.DrawLine(m_mapData[i][k].transform.position, map.transform.position);
                        }
                    }
                }
                if (!m_create)
                {
                    m_targetPos.Add(m_mapData[i][0]);
                }
            }
            if (!m_create)
            {
                m_target.transform.position = m_targetPos[0].transform.position;
            }
        }
        void LodeMap()
        {
            if (!m_create)
            {
                return;
            }
            m_targetPos = new List<MapPoint>();
            var map = m_mapData[m_playerPos.x][m_playerPos.y];
            m_player.transform.position = map.transform.position;
            if (map.PosNumber < m_mapData[map.LineNumber].Count - 1)
            {
                m_targetPos.Add(m_mapData[map.LineNumber][map.PosNumber + 1]);
                if (map.LineNumber < m_mapData.Length - 1 && map.UpTarget > 0)
                {
                    m_targetPos.Add(m_mapData[map.LineNumber + 1][map.UpTarget]);
                }
                if (map.LineNumber > 0 && map.DownTarget > 0)
                {
                    m_targetPos.Add(m_mapData[map.LineNumber - 1][map.DownTarget]);
                }
            }
            m_currentPos = 0;
            if (m_targetPos.Count > 0)
            {
                m_target.transform.position = m_targetPos[m_currentPos].transform.position;
            }
            else
            {
                m_target.transform.position = m_bossPos;
            }
        }
        void Battle()
        {
            SoundManager.PlayBGM(m_battleBGM, m_bgmChangeTime);
            SceneManager.LoadScene(m_targetScene);
        }
    }
}
