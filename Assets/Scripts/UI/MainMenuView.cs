﻿using UnityEngine;
using System.Collections;

public class MainMenuView : MonoBehaviour
{

    private GameObject MenuScreen;
    private GameObject CharacterScreen;

    private float ppu;

    private void Awake()
    {
        MenuScreen = GameObject.FindGameObjectWithTag("MenuScreen");
        CharacterScreen = GameObject.FindGameObjectWithTag("CharacterScreen");

        ppu = FindObjectOfType<Canvas>().referencePixelsPerUnit;

        FindObjectOfType<StartButtonWidget>().OnButtonPressed += ShowCharacterSelection;
    }

    private void ShowCharacterSelection()
    {
        ShowScreen(MenuScreen, false);
    }

    private void ShowScreen(GameObject rt, bool enabled)
    {
        Vector3 addPosition = new Vector3(0, Camera.main.pixelHeight / ppu * 2, 0);
        rt.MoveBy(enabled ? -addPosition : addPosition, .2f, 0, EaseType.easeOutBack);
    }
}
