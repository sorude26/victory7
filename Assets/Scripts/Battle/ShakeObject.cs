using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeObject : MonoBehaviour
{
    [SerializeField]
    float m_shakeDecay = 0.5f;
    [SerializeField]
    float m_startShakeRange = 0.5f;
    [SerializeField]
    float m_shakeSpeed = 10f;
    [SerializeField]
    float m_shakeLevel = 0.02f;
    float m_shakeRange = 0f;
    float m_timer = 0;
    bool m_isShake = default;
    Vector3 m_startPos = default;
    private void Awake()
    {
        m_startPos = transform.localPosition;
        ShakeController.OnShake += StartShake;
    }
    public void StartShake(float time)
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        if (!m_isShake)
        {
            m_shakeRange = m_startShakeRange;
            m_isShake = true;
            m_timer = time;
            StartCoroutine(Shake());
        }
    }
    private IEnumerator Shake()
    {
        float timer = 0;
        Vector3 v = m_startPos;
        while (m_timer > 0 && m_shakeRange > 0)
        {
            timer += Time.deltaTime;
            if (timer > m_shakeLevel)
            {
                timer = 0;
                v = m_startPos;
                v.x += Random.Range(-m_shakeRange, m_shakeRange);
                v.y += Random.Range(-m_shakeRange, m_shakeRange);
            }
            m_timer -= Time.deltaTime;
            transform.localPosition = Vector3.Lerp(m_startPos, v, timer * m_shakeSpeed);
            m_shakeRange -= Time.deltaTime * m_shakeDecay;
            yield return null;
        }
        transform.localPosition = m_startPos;
        m_timer = 0;
        m_shakeRange = 0;
        m_isShake = false;
    }
}
