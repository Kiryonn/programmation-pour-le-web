using UnityEngine;

namespace Koboct.Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData", order = 0)]
    public class PlayerData : ScriptableObject
    {
        public string Name = "Toto";

        public bool QuestionRepondu=false;
        public int ScoreReponseQuestion;

        public int Score;

        private void OnEnable()
        {
            ScoreReponseQuestion = 0;
            Score = 0;
        }
    }
}