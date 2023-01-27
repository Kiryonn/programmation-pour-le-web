namespace Koboct.Score
{
    public class ScormGetter : ScormSender
    {
        public override void DoSomethingInScorm()
        {
            Mydata.Name=  scormService.GetLearnerName();
        }
    }
}