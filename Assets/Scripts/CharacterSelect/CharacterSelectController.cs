using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Vertical"))
        {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                //TargetChange(1);
            }
            else
            {
                //TargetChange(-1);
            }
        }
        else if (Input.GetButtonDown("Horizontal"))
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                //m_currentEvent?.MoveLine(1);
            }
            else
            {
                //m_currentEvent?.MoveLine(-1);
            }
        }
        if (Input.GetButtonDown("Jump"))
        {
            //;
        }
    }
}
