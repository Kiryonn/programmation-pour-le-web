using UnityEngine;

namespace Koboct.Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData", order = 0)]
    public class PlayerData : ScriptableObject
    {
        public string Name = "Toto";

        public bool QuestionReponduCorrectement=false;

        public int Score;

        private void OnEnable()
        {
            Score = 0;
            QuestionReponduCorrectement = false;
        }
    }
}