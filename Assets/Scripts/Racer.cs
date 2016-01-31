﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Racer : MonoBehaviour {

    private List<CheckPoint> checkpoints = new List<CheckPoint>();

    private CheckpointController roundController;

    private Rigidbody2D rigidBody;

    private Vector2 input;

    private float currentVelocity;
    private float maxForwardVelocity = 4f, maxBackwardVelocity = 2f;
    private float timeForwardVelocity = 0.5f, timeBackwardVelocity = 0.3f;

    private float currentRotation;
    private float maxRotation = 40f;
    private float timeRotation = 1f;

    private bool canRace = true;

    public void Initialize(CheckpointController roundController) {
        this.roundController = roundController;

        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void StartRace() {
        canRace = true;
    }

    public void StopRace() {
        canRace = false;
    }
	
	private void Update () {
        if (canRace)
            DetectInput();

        DetectValues();
	}

    private void DetectInput() {
        input = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) input += Vector2.up;
        if (Input.GetKey(KeyCode.S)) input += Vector2.down;

        if (Input.GetKey(KeyCode.A)) input += Vector2.left;
        if (Input.GetKey(KeyCode.D)) input += Vector2.right;
    }

    private void DetectValues() {
        if (input.y == 1) currentVelocity = AddVelocity(currentVelocity, maxForwardVelocity, timeForwardVelocity);
        if (input.y == -1) currentVelocity = AddVelocity(currentVelocity, maxBackwardVelocity, timeBackwardVelocity, -1);
        if (input.y == 0) currentVelocity = DecreaseVelocity(currentVelocity);

        if (input.x == 1) currentRotation = AddVelocity(currentRotation, maxRotation, timeRotation, -1);
        if (input.x == -1) currentRotation = AddVelocity(currentRotation, maxRotation, timeRotation);
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
            velocity -= maxForwardVelocity * (1f / timeForwardVelocity) * Time.deltaTime;
            velocity = Mathf.Clamp(velocity, 0f, maxForwardVelocity);
        }
        else {
            velocity += maxBackwardVelocity * (1f / timeBackwardVelocity) * Time.deltaTime;
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
    }

    private void AddCheck(CheckPoint checkPoint) {
        if (checkpoints.Contains(checkPoint))
            checkpoints.Remove(checkPoint);
            
        checkpoints.Add(checkPoint);

        Debug.Log("Added " + checkPoint.name);
    }

    private void CheckForFinish()
    {
        Debug.Log("Ran over the finish");

        if (!roundController.CheckRound(checkpoints))
            return;

        Debug.Log("Cleared the track!");
    }
}