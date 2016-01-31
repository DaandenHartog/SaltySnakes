using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controller : MonoBehaviour {

    public List<Racer> racers = new List<Racer>();
    private List<RacerData> racersData = new List<RacerData>();

    private CheckpointController checkpointController;

	private void Awake () {
        checkpointController = GetComponent<CheckpointController>();

        //for (int i = 0; i < GlobalSelectedCharacters.instance.chosenCharacters.Length; i++ )
        //{
        //    GameObject racerObject = GameObject.Find("snail" + i);

        //    if (GlobalSelectedCharacters.instance.chosenCharacters[i] == GlobalSelectedCharacters.CharacterType.None)
        //    {
        //        GameObject.Destroy(racerObject);
        //        continue;
        //    }

        //    Racer racer = racerObject.GetComponent<Racer>();
        //    racer.name = GlobalSelectedCharacters.instance.chosenCharacters[i].ToString();

        //    racer.Initialize(this, i + 1);
        //    racersData.Add(new RacerData(racer));

        //    racers.Add(racer);                
        //}

        GameObject racerObject = GameObject.Find("snail0");
        Racer racer = racerObject.GetComponent<Racer>();
        racer.name = "Daan";
        racer.Initialize(this, 1);
        racersData.Add(new RacerData(racer));

        StartRace();
	}

    private void StartRace() {
        foreach (Racer racer in racers)
            racer.StartRace();
    }

    private void EndRace() {
        foreach (Racer racer in racers)
            racer.StopRace();

        List<RacerData> score = GetScore();

        for (int i = 0; i < score.Count; i++)
            Debug.Log(score[i].racer.name + " finished at place " + (i + 1));
    }

    private List<RacerData> GetScore()
    {
        List<RacerData> score = new List<RacerData>();

        while (racersData.Count != 0)
        {
            RacerData data = racersData[0];

            for (int i = 0; i < racersData.Count; i++)
            {
                if(data.round < racersData[i].round)
                {
                    data = racersData[i];
                    continue;
                }

                if(data.round == racersData[i].round)
                {
                    if(data.racer.checkpoints.Count < racersData[i].racer.checkpoints.Count)
                    {
                        data = racersData[i];
                        continue;
                    }
                }
            }
            
            score.Add(data);
            racersData.Remove(data);
        }

        return score;
    }

    public bool CheckRound(List<CheckPoint> list)
    {
        return checkpointController.CheckRound(list);
    }

    public void FinishedLap(Racer racer) {
        RacerData data = GetRacerData(racer);

        data.round++;

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
