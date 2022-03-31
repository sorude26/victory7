using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGMType
{
    Battle1,
    Battle2,
    Battle3,
    Battle4,
    Battle5,
    Battle6,
    Battle7,
    Battle8,
    Map1,
    Map2,
    Result,
    Select,
    Title,
    Build,
}
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
    const int MaxVolume = 1;
    [SerializeField]
    AudioClip[] m_seClips = default;
    [SerializeField]
    AudioClip[] m_bgmClips = default;
    [SerializeField]
    AudioSource m_bgmSource = default;

    AudioSource m_audio = default;
    Dictionary<SEType, AudioClip> m_seDic = default;
    Dictionary<BGMType, AudioClip> m_bgmDic = default;
    bool m_isPlaying = false;
    float m_bgmVolume = MaxVolume;
    float m_seVolume = MaxVolume;

    public static BGMType CurrentBGM { get; private set; }
    public static float BGMVolume
    {
        get => instance.m_bgmVolume;
        set
        {
            if (value > MaxVolume)
            {
                instance.m_bgmVolume = MaxVolume;
            }
            else if (value < 0)
            {
                instance.m_bgmVolume = 0;
            }
            else { instance.m_bgmVolume = value; }
            instance.m_bgmSource.volume = instance.m_bgmVolume;
        }
    }
    public static float SEVolume
    {
        get => instance.m_seVolume;
        set
        {
            if (value > MaxVolume)
            {
                instance.m_seVolume = MaxVolume;
            }
            else if (value < 0)
            {
                instance.m_seVolume = 0;
            }
            else { instance.m_seVolume = value; }
            instance.m_audio.volume = instance.m_seVolume;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            m_audio = GetComponent<AudioSource>();
            m_seDic = new Dictionary<SEType, AudioClip>();
            m_bgmDic = new Dictionary<BGMType, AudioClip>();
            for (int i = 0; i < m_seClips.Length; i++)
            {
                m_seDic.Add((SEType)i, m_seClips[i]);
            }
            for (int i = 0; i < m_bgmClips.Length; i++)
            {
                m_bgmDic.Add((BGMType)i, m_bgmClips[i]);
            }
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
    public static void PlayBGM(BGMType type, float fadeTime = 1f)
    {
        if (instance)
        {
            if (instance.m_isPlaying)
            {
                instance.ChangeBGM(type, fadeTime);
            }
            else
            {
                instance.SetBGM(type);
            }
        }
    }
    void SetBGM(BGMType type)
    {
        m_bgmSource.clip = m_bgmDic[type];
        m_bgmSource.Play();
        m_bgmSource.loop = true;
        CurrentBGM = type;
        m_isPlaying = true;
    }
    void ChangeBGM(BGMType type, float fadeTime)
    {
        if (type != CurrentBGM)
        {
            StartCoroutine(FadeChangeBGM(type, fadeTime));
        }
    }
    IEnumerator FadeChangeBGM(BGMType type, float fadeChangeTime)
    {
        float timer = 0f;
        while (timer < fadeChangeTime)
        {
            timer += Time.deltaTime;
            m_bgmSource.volume = m_bgmVolume * (1 - timer / fadeChangeTime);
            yield return null;
        }
        m_bgmSource.volume = m_bgmVolume;
        SetBGM(type);
    }
}
