using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public class EffectManager : MonoBehaviour
    {
        public static EffectManager Instance { get; private set; }
        [SerializeField]
        ViewText m_viewText = default;
        public ViewText Text { get => m_viewText; }
        private void Awake()
        {
            Instance = this;
        }
    }
}
