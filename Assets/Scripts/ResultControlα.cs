using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace victory7
{
    public class ResultControlα : MonoBehaviour
    {
        bool m_start = false;
        void Start()
        {
            FadeController.Instance.StartFadeIn();
        }

        void Update()
        {
            if (m_start)
            {
                return;
            }
            if (Input.GetButtonDown("Jump"))
            {
                ReturnTitle();
            }
        }
        public void ReturnTitle()
        {
            if (m_start)
            {
                return;
            }
            m_start = true;
            FadeController.Instance.StartFadeOut(LoadTitle);
        }
        void LoadTitle()
        {
            SceneManager.LoadScene("αTitle");
        }
    }
}