using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    [SerializeField]
    bool m_endDestroy = true;
    [SerializeField]
    float m_shakeTime = 0.1f;
    void PlayEnd()
    {
        if (m_endDestroy)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public void PlayShake()
    {
        ShakeController.PlayShake(m_shakeTime);
    }
}
