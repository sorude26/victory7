using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using victory7;
public class CharacterImageChanger : MonoBehaviour
{
    [SerializeField]
    GameObject[] m_models = default;
    void Start()
    {
        foreach (var item in m_models)
        {
            item.SetActive(false);
        }
        m_models[PlayerData.ID].SetActive(true);
    }
}
