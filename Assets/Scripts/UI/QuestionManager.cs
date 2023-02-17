using System.Collections.Generic;
using Koboct.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Koboct.UI
{
    public class QuestionManager : MonoBehaviour
    {
        public bool nopQuestionaire;

        public Questionnaire MonQuestionnaire;

        public TMP_Text Titre;

        public TMP_Text TitreQuestion;

        public List<Toggle> MesReponses;

        public Button Suivant;

        public Button Game;

        public ToggleGroup MyGroup;

        private int CurrentQuestion = 0;

        public Dictionary<Toggle, Reponse> MyDictionnay = new Dictionary<Toggle, Reponse>();

        public ScoreManager score;

        



        public PlayerData PlayerData;

        // Start is called before the first frame update
        void Start()
        {

            if (nopQuestionaire || PlayerData.QuestionRepondu )
            {
                Finish();
            }
            else
            {
                foreach (var toggle in MesReponses)
                {
                    MyDictionnay.Add(toggle, null);
                    toggle.onValueChanged.AddListener(ToggleChanged);
                }


                QuestionChanged();
                Suivant.onClick.AddListener(NextQuestion);
            }

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

        public void information()
        {
            Debug.Log("j'en crée un nouveau");

            score.appel();
            Debug.Log(score.DisplayScoreList());
            TitreQuestion.text = "votre resultat est " + PlayerData.Name + " avec " + PlayerData.Score;
            Titre.text = "LeaderBoard";

            for (int i = 0; i < 5; i++)
            {
                MesReponses[i].gameObject.SetActive(false);
            }
        }

        private void Finish()
        {
            if (PlayerData.Score == MonQuestionnaire.MesQuestions.Count)
            {
                TitreQuestion.text = "Bravo pour le sans faute à ce questionnaire. Bon Jeu";
            }
            else
            {
                TitreQuestion.text = "Merci d'avoir repondu à ce questionnaire. Bon Jeu";
            }

            PlayerData.QuestionRepondu = true;

            Game.transform.gameObject.SetActive(true);
            Suivant.transform.gameObject.SetActive(false);

            for (int i = 0; i < MesReponses.Count; i++)
            {
                MesReponses[i].gameObject.SetActive(false);

            }




            //PlayerData.Score = 666;
        }

        public void FinishButton()
        {
            PlayerData.Score = 0;
            // SceneManager.LoadScene("niv", LoadSceneMode.Additive);
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
                    MyDictionnay[MesReponses[i]] = question.MesReponses[i];
                }
                else
                    MesReponses[i].gameObject.SetActive(false);
            }
        }

        private void ToggleChanged(bool arg0)
        {
            Suivant.interactable = MyGroup.AnyTogglesOn();
        }


        public List<dreamloLeaderBoard.Score> scoreList;

        public void leaderBoard(List<dreamloLeaderBoard.Score> list)
        {
            scoreList = list;

            Game.gameObject.GetComponentInChildren<TMP_Text>().text = "retenter";


            for (int i = 0; i < 5; i++)
            {
                if (i < scoreList.Count)
                {
                    MesReponses[i].gameObject.SetActive(true);
                    MesReponses[i].gameObject.GetComponentInChildren<TMP_Text>().text = scoreList[i].playerName + " avec " + scoreList[i].score;
                    //MesReponses[i].gameObject.GetComponentInChildren<Image>().gameObject.SetActive(false);
                    MesReponses[i].transform.Find("Background").gameObject.SetActive(false);
                }
                else
                { MesReponses[i].gameObject.SetActive(false); }
            }

            TitreQuestion.text = "votre resultat est " + PlayerData.Name + " avec " + PlayerData.Score;
            Titre.text = "LeaderBoard";

        }



    }
}

    
