using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ButtonCacheHandler : MonoBehaviour
{
    private List<Transform> childs;

    private void Start()
    {
        childs = transform.Cast<Transform>().ToList();//get all childs of the buttonShop panel
    }

    private void Update()
    {
        foreach (Transform button in childs)
        {
            //for every button, if tower cost is higher than actual money, activate semi transparent label on top of the tower icon to mark it as unavailable
            if (GameManager.Instance.canBuy(button.GetComponent<ButtonShop>().towerPrefab.Cost))
            {
                button.Find("Cache").gameObject.SetActive(false);
            }
            else
            {
                button.Find("Cache").gameObject.SetActive(true);
            }
        }
    }
}
