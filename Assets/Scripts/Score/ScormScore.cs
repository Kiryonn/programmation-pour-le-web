using Asset.Score;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScormScore : ScormSender
{

    public override void DoSomethingInScorm()
    {
        if (scormService.GetRawScore()>Mydata.Score) return;
       
        scormService.SetRawScore(Mydata.Score);
        scormService.Commit();
    }

}
