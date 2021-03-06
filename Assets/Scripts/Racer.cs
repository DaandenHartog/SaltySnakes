﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GamepadInput;

public class Racer : MonoBehaviour {

    [HideInInspector]
    public List<CheckPoint> checkpoints = new List<CheckPoint>();
    [HideInInspector]
    public string name;

    private Controller controller;

    private Rigidbody2D rigidBody;

    private SpriteRenderer body;
    private Sprite body0, body1;

    private Vector2 input;

    private float currentVelocity;
    private float maxForwardVelocity = 0.5f, maxBackwardVelocity = 2f;
    private float timeForwardVelocity = 0.01f, timeBackwardVelocity = 0.01f;

    private float currentRotation;
    private float maxRotation = 40f;
    private float timeRotation = 1f;

    private bool canRace = true;
    private bool canControl = true;

    private IEnumerator animation;

    int playerID = 0;

    public void Initialize(Controller controller, int playerID) {
        this.controller = controller;
        this.playerID = playerID;

        body0 = ResourceLoader._instance.GetAsset<Sprite>("body" + name + "0");
        body1 = ResourceLoader._instance.GetAsset<Sprite>("body" + name + "1");
        body = transform.FindChild("body").GetComponent<SpriteRenderer>();
        body.sprite = body0;

        transform.FindChild("shell").GetComponent<SpriteRenderer>().sprite = ResourceLoader._instance.GetAsset<Sprite>("shell" + name);
        transform.FindChild("legs").FindChild("legLeft").GetComponent<SpriteRenderer>().sprite = ResourceLoader._instance.GetAsset<Sprite>("leg" + name + "Left");
        transform.FindChild("legs").FindChild("legRight").GetComponent<SpriteRenderer>().sprite = ResourceLoader._instance.GetAsset<Sprite>("leg" + name + "Right");

        Debug.Log(name + " looking for rb");
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void StartRace() {
        canRace = true;

        animation = AnimateBody(body0, body1);
        StartCoroutine(animation);
    }

    public void StopRace() {
        StopCoroutine(animation);
        input = Vector2.zero;
        canRace = false;
    }

    private IEnumerator AnimateBody(Sprite current, Sprite next)
    {
        yield return new WaitForSeconds(1f);

        body.sprite = next;
        animation = AnimateBody(next, current);
        StartCoroutine(animation);
    }
	
	private void Update () {
        if (canControl)
        {
            if (canRace)
                DetectInput();

            DetectValues();
        }
	}

    private void DetectInput() {
        input = Vector2.zero;

        switch(playerID)
        {
            default:
            case 1:
                if (GamePad.GetButtonDown(GamePad.Button.LeftShoulder, GamePad.Index.One))
                    input += Vector2.up;
                input = new Vector2(Input.GetAxis("Controller1_LeftAxis"), input.y);
                break;
            case 2:
                if (GamePad.GetButtonDown(GamePad.Button.RightShoulder, GamePad.Index.One))
                    input += Vector2.up;
                input = new Vector2(Input.GetAxis("Controller1_RightAxis"), input.y);
                break;
            case 3:
                if (GamePad.GetButtonDown(GamePad.Button.LeftShoulder, GamePad.Index.Two))
                    input += Vector2.up;
                input = new Vector2(Input.GetAxis("Controller2_LeftAxis"), input.y);
                break;
            case 4:
                if (GamePad.GetButtonDown(GamePad.Button.RightShoulder, GamePad.Index.Two))
                    input += Vector2.up;
                input = new Vector2(Input.GetAxis("Controller2_RightAxis"), input.y);
                break;
        }

        //if (Input.GetKey(KeyCode.W)) input += Vector2.up;
        //if (Input.GetKey(KeyCode.S)) input += Vector2.down;

        //if (Input.GetKey(KeyCode.A)) input += Vector2.left;
        //if (Input.GetKey(KeyCode.D)) input += Vector2.right;

        
    }

    private void DetectValues() {
        if (input.y == 1) currentVelocity = AddVelocity(currentVelocity, maxForwardVelocity, timeForwardVelocity);
        if (input.y == -1) currentVelocity = AddVelocity(currentVelocity, maxBackwardVelocity, timeBackwardVelocity, -1);
        if (input.y == 0) currentVelocity = DecreaseVelocity(currentVelocity);

        //if (input.x == 1) currentRotation = AddVelocity(currentRotation, maxRotation, timeRotation, -1);
        //if (input.x == -1) currentRotation = AddVelocity(currentRotation, maxRotation, timeRotation);
        if (input.x != 0)
        {
            //Debug.Log(input.x);

            int direction = 1;
            if (input.x > 0) direction = -1;
            currentRotation = AddVelocity(currentRotation, maxRotation * Mathf.Abs(input.x), timeRotation, direction);
        }
        if (input.x == 0) currentRotation = DecreaseRotation(currentRotation);
    }

    private float AddVelocity(float velocity, float max, float time, int direction = 1) {
        velocity += max * (1f / time) * Time.deltaTime * direction;
        velocity = Mathf.Clamp(velocity, -max, max);

        return velocity;
    }

    private float DecreaseVelocity(float velocity) {
        if (velocity == 0) return velocity;

        if(velocity > 0) {
            velocity -= maxForwardVelocity * (1f / 0.75f) * Time.deltaTime;
            velocity = Mathf.Clamp(velocity, 0f, maxForwardVelocity);
        }
        else {
            velocity += maxBackwardVelocity * (1f / 0.75f) * Time.deltaTime;
            velocity = Mathf.Clamp(velocity, -maxBackwardVelocity, 0f);
        }
        
        return velocity;
    }

    private float DecreaseRotation(float rotation) {
        if (rotation == 0) return rotation;

        if (rotation > 0)
        {
            rotation -= maxRotation * (1f / timeRotation) * Time.deltaTime;
            rotation = Mathf.Clamp(rotation, 0f, maxRotation);
        }
        else
        {
            rotation += maxRotation * (1f / timeRotation) * Time.deltaTime;
            rotation = Mathf.Clamp(rotation, -maxRotation, 0f);
        }
        
        return rotation;
    }

    private void FixedUpdate() {
        ApplyInput();
    }

    private void ApplyInput() {
        rigidBody.velocity = (Vector2)transform.up * currentVelocity;
        transform.eulerAngles += new Vector3(0, 0, currentRotation) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "checkpoint")
            AddCheck(collider.GetComponent<CheckPoint>());

        if (collider.gameObject.tag == "finish")
            CheckForFinish();

        if (collider.gameObject.tag == "oil")
            AddSpill();
    }

    private void AddSpill() {
        canControl = false;

        StartCoroutine(Spill());
    }

    private IEnumerator Spill() {

        float timer = 0f;
        float totalVelocity = 5f;
        
        while(true)
        {
            timer += Time.deltaTime * 1.5f;
            currentVelocity = totalVelocity - timer;

            if(currentVelocity <= 0)
                break;

            yield return null;
        }

        currentVelocity = 0f;
        canControl = true;
    }

    private void AddCheck(CheckPoint checkPoint) {
        if (checkpoints.Contains(checkPoint))
            checkpoints.Remove(checkPoint);
            
        checkpoints.Add(checkPoint);
    }

    private void CheckForFinish()
    {
        Debug.Log("Checking finish..");

        if (!controller.CheckRound(checkpoints))
            return;

        Debug.Log("Applied finish");

        checkpoints = new List<CheckPoint>();
        controller.FinishedLap(this);
        
    }
}
