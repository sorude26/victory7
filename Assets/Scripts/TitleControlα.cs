using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace victory7
{
    public class TitleControlα : MonoBehaviour
    {
        [SerializeField]
        string m_target = "MapScene";
        [SerializeField]
        BGMType m_bgm = BGMType.Title;
        bool m_start = false;
        void Start()
        {
            FadeController.Instance.StartFadeIn(() => m_start = true);
            //GameManager.Instance.StartSet();
            SoundManager.PlayBGM(m_bgm);
            MapData.ClearReset();
        }

        void Update()
        {
            if (!m_start)
            {
                return;
            }
            if (Input.GetButtonDown("Submit"))
            {
                GamaStart();
            }
        }
        public void GamaStart()
        {
            if (!m_start)
            {
                return;
            }
            m_start = false;
            SoundManager.Play(SEType.Decision);
            FadeController.Instance.StartFadeOut(LoadMap);
        }
        void LoadMap()
        {
            SceneManager.LoadScene(m_target);
        }
    }
}
