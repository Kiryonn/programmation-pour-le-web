using System.Collections;
using Koboct.Data;
//using Koboct.Data;
using Scorm;
using UnityEngine;

namespace Asset.Score
{
    public class ScormSender : MonoBehaviour
    {
        public PlayerData Mydata;
        
        public static bool result;

        protected IScormService scormService;

        private void Start()
        {
#if UNITY_EDITOR
            scormService = new ScormPlayerPrefsService(); // PlayerPrefs implementation (for editor testing)
#else
scormService = new ScormService(); // Real implementation
#endif
        Debug.Log(Mydata.Score);

            if (result)
                DoSomethingInScorm();              
            else 
                StartCoroutine(Delay());


        }

        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(3);

             result = scormService.Initialize(Scorm.Version.Scorm_2004); // Begins communication with the LMS
            int tentative = 3;
            while (!result)
            {
                result = scormService.Initialize(Scorm.Version.Scorm_2004);
                
                if (tentative <= 0)
                    break;

                tentative--;
                
                yield return new WaitForSeconds(3);
            }

            if (result)
            {
                Debug.Log("Something happens");
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
}