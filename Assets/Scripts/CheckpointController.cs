using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckpointController : MonoBehaviour {

    public List<CheckPoint> checkpointOrder;

    public bool CheckRound(List<CheckPoint> racerOrder)
    {
        if (racerOrder.Count != checkpointOrder.Count)
            return false;

        for (int i = 0; i < racerOrder.Count; i++)
            if (racerOrder[i] != checkpointOrder[i])
                return false;

        return true;
    }

}
