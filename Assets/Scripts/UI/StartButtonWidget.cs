using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class StartButtonWidget : MonoBehaviour
{
    public Action OnButtonPressed;

    private void Awake()
    {
        Button b = GetComponent<Button>();
        if (b == null) { Debug.Log("No button"); return; }

        b.onClick.AddListener(delegate { if (OnButtonPressed != null) OnButtonPressed(); });
    }

}
