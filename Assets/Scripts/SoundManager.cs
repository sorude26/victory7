using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SEType
{
    StopSpin,//リールを止める：通常
    StopSpin7,//絵柄「7」揃う
    StartSpin,//リールが回っている

    PaylineAttack,//「攻撃」絵柄を実行
    PaylineHeel,//「回復」絵柄を実行
    PaylineGuard,//「ガード」絵柄を実行
    PaylineCharge,//「チャージ」絵柄を実行

    Jackpot,//絵柄が揃う
    Attack,//被弾：通常
    Guard,//被弾：ガード
    Dead,//倒れる

    SkillHeel,//スキル発動：回復系
    SkillAttack,//スキル発動：攻撃系
    SkillGuard,//スキル発動：ガード系

    WeakAttackDragon,//弱攻撃：ドラゴン
    StrongAttackDragon,//強攻撃：ドラゴン

    WeakAttackGoblin,//弱攻撃：ゴブリン
    StrongAttackGoblin,//強攻撃：ゴブリン
    
    WeakAttackGolem,//弱攻撃：ゴーレム
    StrongAttackGolem,//強攻撃：ゴーレム
    
    WeakAttackSlime,//弱攻撃：スライム
    StrongAttackSlime,//強攻撃：スライム
    
    WeakAttackSpecial,//弱攻撃：特殊
    StrongAttackSpecial,//強攻撃：特殊
    
    DragonDeath,//死亡ボイス：ドラゴン
    GoblinDeath,//死亡ボイス：ゴブリン
    GolemDeath,//死亡ボイス：ゴーレム
    SlimeDeath,//死亡ボイス：スライム
    SpecialDeath,//死亡ボイス：特殊
    
    HeelSquares,//回復マス
    LevelUPSquares,//レベルアップマス
    
    Decision,//決定
    Choice,//選択
    Cancel,//キャンセル
    Deployment//展開
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    static SoundManager instance = default;

    [SerializeField]
    AudioClip[] m_seClips = default;

    AudioSource m_audio = default;
    Dictionary<SEType, AudioClip> m_seDic = default;
    private void Awake()
    {
        instance = this;
        m_audio = GetComponent<AudioSource>();
        m_seDic = new Dictionary<SEType, AudioClip>();
        for (int i = 0; i < m_seClips.Length; i++)
        {
            m_seDic.Add((SEType)i, m_seClips[i]);
        }
    }

    public static void Play(SEType type)
    {
        if (instance)
        {
            instance.m_audio.PlayOneShot(instance.m_seDic[type]);
            //Debug.Log(type);
        }
    }
    public static void PlayBGM()
    {
        instance.m_audio.Play();
    }
}
