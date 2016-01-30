using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using GamepadInput;

public class StartButtonWidget : MonoBehaviour
{
    public Action OnButtonPressed;

    private bool check;

    private void Awake()
    {
        Button b = GetComponent<Button>();
        if (b == null) { Debug.Log("No button"); return; }

        b.onClick.AddListener(delegate { if (OnButtonPressed != null) OnButtonPressed(); });
        check = true;
    }

    private void Update()
    {
        if (!check) { return; }
        if (Input.anyKeyDown)
        {
            OnButtonPressed();
            check = false;
        }
    }

}
