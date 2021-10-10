using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// メニュー画面のボタン操作におけるスクリプト
/// </summary>

public class Menu : MonoBehaviour
{
    [SerializeField] Button btn;
     bool OnMouseEnter;

    // Start is called before the first frame update
    void Start()
    {
        btn.interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && OnMouseEnter == false) // Rキーを押したらキーボード上で操作可能になる
        {
            GameObject go = EventSystem.current.currentSelectedGameObject;

            Selectable sel = GetComponent<Selectable>();
            sel.Select();
        }

        if (OnMouseEnter == true)
        {
            btn.interactable = false;
        }
    }
}
