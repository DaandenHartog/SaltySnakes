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
            racer.Initialize(this);
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
            racer.StopRace();
    }

    public bool CheckRound(List<CheckPoint> list)
    {
        return checkpointController.CheckRound(list);
    }

    public void FinishedLap(Racer racer) {
        RacerData data = GetRacerData(racer);

        data.round++;

        Debug.Log("Calling finished lap. Current round: " + data.round);

        if(data.round >= 2)
        {
            EndRace();
            return;
        }
    }

    private RacerData GetRacerData(Racer racer)
    {
        foreach (RacerData data in racersData)
            if (data.racer == racer)
                return data;

        return null;
    }

    private class RacerData
    {
        public Racer racer;
        public int round = 1;

        public RacerData(Racer racer)
        {
            this.racer = racer;
        }
    }
	
}
