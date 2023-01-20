using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
  
    public Questionaire mesQuestion ;

    public TextMeshPro intituleQuestion;

    public List<Toggle> mesReponse;

    public ToggleGroup ToggleGroup;

    private int curentQuenstion = 0;


    private void Start()
    {
        foreach(var toggle in mesReponse)
        {
            toggle.onValueChanged.AddListener(ToggleChanged);
        }
        questionaireChange();
    }

    private void questionaireChange()
    {
        
    }
    


    private void ToggleChanged(bool arg0)
    {
        //Suivant.iterable = ToggleGroup.AnyTogglesOn();
    }





}
