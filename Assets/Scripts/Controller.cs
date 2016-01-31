using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controller : MonoBehaviour {

    public List<Racer> racers;
    private List<RacerData> racersData = new List<RacerData>();

    private CheckpointController checkpointController;

	private void Awake () {
        checkpointController = GetComponent<CheckpointController>();

        foreach (Racer racer in racers)
        {
            racer.Initialize(checkpointController);
            racersData.Add(new RacerData(racer));
        }

        StartRace();
	}

    private void StartRace() {
        foreach (Racer racer in racers)
            racer.StartRace();
    }

    private void EndRace() {
        foreach (Racer racer in racers)
            racer.StartRace();
    }

    public void FinishedLap() {

    }

    private class RacerData
    {
        public Racer racer;
        public int round;

        public RacerData(Racer racer)
        {
            this.racer = racer;
        }
    }
	
}
