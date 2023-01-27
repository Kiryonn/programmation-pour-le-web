using System.Collections.Generic;
using Koboct.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Koboct.UI
{
    public class QuestionManager : MonoBehaviour
    {
        public Questionnaire MonQuestionnaire;

        public TMP_Text TitreQuestion;

        public List<Toggle> MesReponses;

        public Button Suivant;

        public ToggleGroup MyGroup;

        private int CurrentQuestion = 0;

        public Dictionary<Toggle, Reponse> MyDictionnay = new Dictionary<Toggle, Reponse>();



        public PlayerData PlayerData;
        
        // Start is called before the first frame update
        void Start()
        {
            foreach (var toggle in MesReponses)
            {
                MyDictionnay.Add(toggle,null);
                toggle.onValueChanged.AddListener(ToggleChanged);
            }

            QuestionChanged();
            Suivant.onClick.AddListener(NextQuestion);
            
        }

        private void NextQuestion()
        {
            foreach (var reponse in MyDictionnay)
            {
                if (reponse.Key.isOn)
                {
                    if (reponse.Value.EstCorrect)
                        PlayerData.ScoreReponseQuestion++;
                }
            }
            CurrentQuestion++;
            if (CurrentQuestion < MonQuestionnaire.MesQuestions.Count)
            {
                QuestionChanged();
            }
            else
                Finish();
        }

        private void Finish()
        {
            if ( PlayerData.Score==MonQuestionnaire.MesQuestions.Count)
            {
                PlayerData.QuestionRepondu= true;
            }

            PlayerData.Score = 666;
        }

        private void QuestionChanged()
        {
            var question = MonQuestionnaire.MesQuestions[CurrentQuestion];

            TitreQuestion.text = question.Intitule;

            for (int i = 0; i < MesReponses.Count; i++)
            {
                MesReponses[i].isOn = false;
                
                if (i < question.MesReponses.Count)
                {
                    MesReponses[i].gameObject.SetActive(true);
                    MesReponses[i].gameObject.GetComponentInChildren<TMP_Text>().text =
                        question.MesReponses[i].Intitule;
                    MyDictionnay[MesReponses[i]] =  question.MesReponses[i];
                }
                else
                    MesReponses[i].gameObject.SetActive(false);
            }
        }

        private void ToggleChanged(bool arg0)
        {
            Suivant.interactable = MyGroup.AnyTogglesOn();
        }

     
    }

    
}