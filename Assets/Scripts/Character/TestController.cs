using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public class TestController : MonoBehaviour
    {
        [SerializeField]
        EnemyControl enemy;
        void Start()
        {
            enemy.StartSet();
        }

        void Update()
        {
            enemy.CharacterUpdate();
        }
    }
}