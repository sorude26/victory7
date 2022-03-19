using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public class SelectSlotController : MonoBehaviour
    {
        [SerializeField]
        Animator m_animator = default;
        [SerializeField]
        ParticleSystem m_getEffect = default;
        public Action OnGetSlot = default;
        public void PlaySelectAnime(LineType line)
        {
            switch (line)
            {
                case LineType.Left:
                    m_animator.Play("startLeft");
                    break;
                case LineType.Center:
                    m_animator.Play("startCenter");
                    break;
                case LineType.Right:
                    m_animator.Play("startRight");
                    break;
                default:
                    break;
            }
        }
        public void PlayGetSlotAnime()
        {
            m_animator.Play("moveCenterLine");
        }
        public void HideSlot()
        {
            m_animator.Play("hide");
        }
        void PlayEffect()
        {
            m_getEffect.Play();
        }
        void PlayAction()
        {
            OnGetSlot?.Invoke();
            OnGetSlot = null;
        }
    }
}