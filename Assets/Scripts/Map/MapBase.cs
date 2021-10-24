using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace victory7
{
    public class MapBase : MonoBehaviour
    {
        [SerializeField]
        GameObject m_player = default;
        [SerializeField]
        GameObject m_target = default;
        [Header("配置ラインデータ")]
        [SerializeField]
        MapLine[] m_mapLines = default;
        [SerializeField]
        Vector2 m_mapSize = Vector2.one;
        List<MapPoint>[] m_mapData = default;
        [SerializeField]
        MapPoint m_bossPoint = default;
        [SerializeField]
        GameObject m_startPoint = default;
        [SerializeField]
        float m_startAndGoalPos = 1.5f;
        int m_currentPos = 0;
        List<MapPoint> m_targetPos = default;
        Vector2 m_bossPos = default;
        bool m_create = default;
        Vector2Int m_playerPos = default;
        bool m_load = default;
        [Header("通常戦闘パターンデータ")]
        [SerializeField]
        MapPointData[] m_battlePointData = default;
        [Header("ボス戦闘パターンデータ")]
        [SerializeField]
        MapPointData m_bossPointData = default;
        bool m_gard = false;
        private void Start()
        {
            m_create = MapData.Create;
            m_playerPos = MapData.PlayerPos;
            CreateMap(); 
            LodeMap();
            FadeController.Instance.StartFadeIn(() => m_gard = false);
            m_gard = true;
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
            if (Input.GetButtonDown("Jump"))
            {
                Next();
            }
        }
        void Next()
        {
            if (m_targetPos.Count <= 0 || m_load)
            {
                m_player.transform.position = m_bossPos;
                MapData.Reset();
                BattleData.SetData(m_bossPointData);
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
            MapData.SetData(new Vector2Int(map.LineNumber, map.PosNumber));
            int b = map.TypeNumber;
            if (b >= m_battlePointData.Length)
            {
                b = 0;
            }
            BattleData.SetData(m_battlePointData[b]);
            FadeController.Instance.StartFadeOut(Battle);
        }
        void TargetChange(int a)
        {
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
            m_target.transform.position = m_targetPos[m_currentPos].transform.position;
        }
        void CreateMap()
        {
            if (m_mapLines.Length <= 0)
            {
                Debug.LogError("マップ横軸データが足りません");
                return;
            }
            m_mapData = new List<MapPoint>[m_mapLines.Length];
            for (int i = 0; i < m_mapData.Length; i++)
            {
                m_mapData[i] = new List<MapPoint>();
            }
            float lineSpan = m_mapSize.y / (m_mapLines.Length - 1);
            Vector2 startPos = -m_mapSize / 2;
            var boss = Instantiate(m_bossPoint);
            boss.transform.position = new Vector2(m_mapSize.x + startPos.x + m_startAndGoalPos, 0);
            boss.transform.SetParent(transform);
            m_bossPos = boss.transform.position;
            var start = Instantiate(m_startPoint);
            start.transform.position = new Vector2(startPos.x - m_startAndGoalPos, 0);
            start.transform.SetParent(transform);
            m_player.transform.position = start.transform.position;
            for (int i = 0; i < m_mapLines.Length; i++)
            {
                Vector2 front = start.transform.position;
                for (int k = 0; k < m_mapLines[i].Points.Length; k++)
                {
                    var map = Instantiate(m_mapLines[i].Points[k]);
                    map.transform.position = new Vector2(m_mapSize.x / (m_mapLines[i].Points.Length - 1) * k, i * lineSpan) + startPos;
                    map.SetData(i, k);
                    map.SetUpTarget(m_mapLines[i].TargetPointU[k]);
                    map.SetDownTarget(m_mapLines[i].TargetPointD[k]);
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
                            var map = m_mapData[i - 1][m_mapData[i][k].DownTarget];
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
            SceneManager.LoadScene("BattleTest");
        }
    }
}
