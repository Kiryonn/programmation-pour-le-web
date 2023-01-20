using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;


[Serializable]
[CreateAssetMenu]
public class Questionaire : ScriptableObject
{

    private List<Question> MesQestion =new List<Question>();
}




[Serializable]
public class Question
{

    public string intitule;
    public List<Reponse> MesReponses=new List<Reponse>();
}


[Serializable]
public class Reponse
{
    public string intitule;
    public bool estCorrect;
}