using UnityEngine;
using System.Collections;
using GamepadInput;
using System;
using System.Collections.Generic;

public class MainMenuView : MonoBehaviour
{
    [SerializeField]
    private int countDownTime = 15;

    private GameObject MenuScreen;
    private GameObject CharacterScreen;

    private ChoosableCharacterWidget[] choosableCharacters;
    private PlayerSelectorWidget[] playerWidgets;

    private bool countdown = false;

    private void Awake()
    {
        MenuScreen = GameObject.FindGameObjectWithTag("MenuScreen");
        CharacterScreen = GameObject.FindGameObjectWithTag("CharacterScreen");

        FindObjectOfType<StartButtonWidget>().OnButtonPressed += ShowCharacterSelection;

        choosableCharacters = FindObjectsOfType<ChoosableCharacterWidget>();
        playerWidgets = FindObjectsOfType<PlayerSelectorWidget>();
    }

    private void ShowCharacterSelection()
    {
        ShowScreen(MenuScreen, false);
        StartCoroutine(StartCountdown());
        countdown = true;
    }

    private void ShowScreen(GameObject rt, bool enabled)
    {
        Canvas c = FindObjectOfType<Canvas>();
        Vector3 addPosition = Camera.main.ScreenToWorldPoint(new Vector3(c.pixelRect.width / 2, c.pixelRect.height * 1.5f, 0));
        rt.MoveBy(enabled ? -addPosition : addPosition, .2f, 0, EaseType.easeOutBack);
    }

    private void Update()
    {
        if (countdown)
            CheckInputs();

    }

    private IEnumerator StartCountdown()
    {
        GameObject cdi = GameObject.Find("r_CountdownIndicator");
        GameObject cdf = GameObject.Find("r_CountdownFinish");

        Vector3 newPosition = new Vector3(cdf.transform.position.x, cdf.transform.position.y, 0);
        cdi.MoveBy(new Vector3(0, 4, 0), countDownTime, 0f, EaseType.linear);

        yield return new WaitForSeconds(countDownTime);
        CountdownDone();
    }

    private void CountdownDone()
    {
        countdown = false;
        Application.LoadLevel("scene");
    }

    private void CheckInputs()
    {
        if (Input.GetButtonDown("Controller1_LeftShoulder"))
            PlayerSelectCharacter(1, true);

        if (Input.GetButtonDown("Controller1_RightShoulder"))
            PlayerSelectCharacter(2, true);

        if (Input.GetButtonDown("Controller2_LeftShoulder"))
            PlayerSelectCharacter(3, true);

        if (Input.GetButtonDown("Controller2_RightShoulder"))
            PlayerSelectCharacter(4, true);

        //if (gamepad.gettrigger(gamepad.trigger.lefttrigger, gamepad.index.one) > .2f)
        //    playerselectcharacter(1, true);
        //else if (gamepad.getbutton(gamepad.button.leftshoulder, gamepad.index.one))
        //    playerselectcharacter(1, false);

        //if (gamepad.gettrigger(gamepad.trigger.righttrigger, gamepad.index.one) > .2f)
        //    playerselectcharacter(2, true);
        //else if (gamepad.getbutton(gamepad.button.rightshoulder, gamepad.index.one))
        //    playerselectcharacter(2, false);

        //if (gamepad.gettrigger(gamepad.trigger.lefttrigger, gamepad.index.two) > .2f)
        //    playerselectcharacter(3, true);
        //else if (gamepad.getbutton(gamepad.button.leftshoulder, gamepad.index.two))
        //    playerselectcharacter(3, false);

        //if (gamepad.gettrigger(gamepad.trigger.righttrigger, gamepad.index.two) > .2f)
        //    playerselectcharacter(4, true);
        //else if (gamepad.getbutton(gamepad.button.rightshoulder, gamepad.index.two))
        //    playerselectcharacter(4, false);

    }

    private void PlayerSelectCharacter(int player, bool next)
    {
        if ((int)GlobalSelectedCharacters.instance.chosenCharacters[player - 1] == 0)
        {
            GlobalSelectedCharacters.instance.SetPlayerCharacter(player, 1);
            UpdatePlayerSelector(player, 1);
            return;
        }

        int nextIndex = (int)GlobalSelectedCharacters.instance.chosenCharacters[player - 1];
        if (next)
        {
            if (nextIndex + 1 == choosableCharacters.Length + 1)
            {
                nextIndex = 1;
            }
            else
                nextIndex += 1;
        }
        else
        {
            if (nextIndex - 1 == 0)
                nextIndex = choosableCharacters.Length;
            else
                nextIndex -= 1;

        }
        GlobalSelectedCharacters.instance.SetPlayerCharacter(player, nextIndex);
        UpdatePlayerSelector(player, nextIndex);
    }

    private void UpdatePlayerSelector(int player, int newIndex)
    {
        PlayerSelectorWidget playerWidget = GetPlayerWidgetOnIndex(player);
        ChoosableCharacterWidget newTarget = GetCharacterWidgetOnIndex(newIndex);
        if (playerWidget != null && newTarget != null)
        {
            playerWidget.SetSelectedCharacter(newTarget.gameObject);
        }
    }

    private ChoosableCharacterWidget GetCharacterWidgetOnIndex(int index)
    {
        foreach (ChoosableCharacterWidget c in choosableCharacters)
        {
            if (c.Index == index)
                return c;
        }
        return null;
    }

    private PlayerSelectorWidget GetPlayerWidgetOnIndex(int index)
    {
        foreach (PlayerSelectorWidget c in playerWidgets)
        {
            if (c.PlayerIndex == index)
                return c;
        }
        return null;
    }
}
