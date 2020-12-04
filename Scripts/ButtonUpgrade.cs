using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUpgrade : MonoBehaviour
{
    public void Button1Clicked()
    {
        GameManager.Instance.ButtonUpgrade1Clicked();
    }
    public void Button2Clicked()
    {
        GameManager.Instance.ButtonUpgrade2Clicked();
    }
}
