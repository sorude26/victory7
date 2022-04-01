using UnityEngine;

namespace victory7
{
    /// <summary>
    /// マップの配置・編成データ
    /// </summary>
    [CreateAssetMenu]
    public class MapPatternData : ScriptableObject
    {
        #region SerializeField
        [Header("マップ背景画像")]
        [SerializeField]
        Sprite m_background = default;
        [Header("戦闘背景画像")]
        [SerializeField]
        Sprite m_battlebackground = default;
        [Header("スロットデザイン画像")]
        [SerializeField]
        Sprite m_slotFrame = default;
        [Header("配置ラインデータ")]
        [SerializeField]
        MapLine[] m_mapLines = default;
        [Header("通常戦闘パターンデータ")]
        [SerializeField]
        MapPointData[] m_battlePointData = default;
        [Header("特殊戦闘パターンデータ")]
        [SerializeField]
        MapPointData m_battlePointDataEx = default;
        [Header("ボス戦闘パターンデータ")]
        [SerializeField]
        MapPointData m_bossPointData = default;
        [Header("遷移先パターンデータ")]
        [SerializeField]
        MapPatternData[] m_randomNextMap = default;
        [Header("マップの大きさ")]
        [SerializeField]
        Vector2 m_mapSize = new Vector2(10, 6);
        [Header("スタートとゴール地点の距離")]
        [SerializeField]
        float m_pointRange = 2f;
        #endregion

        #region Property
        /// <summary> 背景画像 </summary>
        public Sprite Background { get => m_background; }
        /// <summary> 戦闘背景画像 </summary>
        public Sprite BattleBackground { get => m_battlebackground; }
        /// <summary> スロット画像 </summary>
        public Sprite SlotFrame { get => m_slotFrame; }
        /// <summary> 配置ライン </summary>
        public MapLine[] MapLines { get => m_mapLines; }
        /// <summary> 通常戦闘 </summary>
        public MapPointData[] NormalBattleData { get => m_battlePointData; }
        /// <summary> 特殊戦闘 </summary>
        public MapPointData ExBattleData { get => m_battlePointDataEx; }
        /// <summary> ボス戦闘 </summary>
        public MapPointData BossBattleData { get => m_bossPointData; }
        /// <summary> 次マップデータ </summary>
        public MapPatternData[] NextMapData { get => m_randomNextMap; }
        /// <summary> マップのサイズ </summary>
        public Vector2 MapSize { get => m_mapSize; }
        /// <summary> スタートとゴール地点の距離 </summary>
        public float PointRange { get => m_pointRange; }
        #endregion

        #region Getter
        /// <summary>
        /// 通常戦闘の敵を返す
        /// </summary>
        /// <returns></returns>
        public MapPointData GetNormalBattle() 
        {
            if (m_battlePointData.Length > 0)
            {
                return m_battlePointData[Random.Range(0, m_battlePointData.Length)];
            }
            else
            {
                Debug.LogError("通常戦闘データが未設定です");
                return null;
            }
        }
        /// <summary>
        /// 次のマップデータを返す
        /// </summary>
        /// <returns></returns>
        public MapPatternData GetNextMap()
        {
            if (m_randomNextMap.Length > 0)
            {
                return m_randomNextMap[Random.Range(0, m_randomNextMap.Length)];
            }
            else
            {
                Debug.LogError("遷移先パターンが未設定です");
                return null;
            }
        }
        #endregion
    }
}