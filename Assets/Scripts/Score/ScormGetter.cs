using Asset.Score;

namespace Koboct.Score
{
    public class ScormGetter : ScormSender
    {

        public override void DoSomethingInScorm()
        {
            Mydata.Name=  scormService.GetLearnerName();
            Mydata.Name = scormService.GetLearnerName();
            //Mydata.QuestionRepondu = scormService.;
            scormService.SetMinScore(0); // Sets a min score of 0
            scormService.SetMaxScore(16);// Sets a max score of 10
        }

        

    }
}