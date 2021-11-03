using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace victory7
{
    public class TitleControlα : MonoBehaviour
    {
        bool m_start = false;
        void Start()
        {
            FadeController.Instance.StartFadeIn();
            GameManager.Instance.StartSet();
            MapData.Reset();
        }

        void Update()
        {
            if (m_start)
            {
                return;
            }
            if (Input.GetButtonDown("Jump"))
            {
                GamaStart();
            }
        }
        public void GamaStart()
        {
            if (m_start)
            {
                return;
            }
            m_start = true;
            FadeController.Instance.StartFadeOut(LoadMap);
        }
        void LoadMap()
        {
            SceneManager.LoadScene("MapScene");
        }
    }
}
