using System.Runtime.InteropServices;
using UnityEngine;

public class ShowComments : MonoBehaviour
{



    [DllImport("__Internal")]
    private static extern bool ShowDis();



    public void Show()
    {
        if (!Application.isEditor)
            ShowDis();
    }


}