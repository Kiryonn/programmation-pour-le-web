using System;
using System.Collections.Generic;
using UnityEngine;

namespace Koboct.Data
{
    [Serializable]
    [CreateAssetMenu()]
    public class Questionnaire:ScriptableObject
    {
        public List<Question> MesQuestions = new List<Question>();
       
    }
}

[Serializable]
public class Question
{
  
    public string Intitule;
    public List<Reponse> MesReponses = new List<Reponse>();

}  

[Serializable]
public class Reponse
{
    public string Intitule;
    public bool EstCorrect;
}


