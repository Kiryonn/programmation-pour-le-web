using JetBrains.Annotations;
using Koboct.Data;
using Scorm;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScormSender : MonoBehaviour
{
    public PlayerData Mydata;

    protected IScormService scormService;

    private void Start()
    {
#if UNITY_EDITOR
        scormService = new ScormPlayerPrefsService(); // PlayerPrefs implementation (for editor testing)
#else
scormService = new ScormService(); // Real implementation
#endif

        StartCoroutine(Delay());


    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(3);


        bool result = scormService.Initialize(Scorm.Version.Scorm_1_2); // Begins communication with the LMS
        int tentative = 3;
        while (!result)
        {
            result = scormService.Initialize(Scorm.Version.Scorm_1_2);

            if (tentative <= 0)
                break;

            tentative--;

            yield return new WaitForSeconds(3);
        }

        if (result)
        {
            DoSomethingInScorm();
        }
    }

    public virtual void DoSomethingInScorm()
    {
        scormService.SetMinScore(0); // Sets a min score of 0
        scormService.SetMaxScore(1000000); // Sets a max score of 10
        scormService.SetRawScore(Mydata.Score); // Sets a score of 6.5
        scormService.Commit(); // Commit the pending changes
        scormService.Finish(); // Ends communication with the LMS
    }
}
