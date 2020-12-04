using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonShop : MonoBehaviour
{
    public Tower towerPrefab;

    public void ClickOnButton()
    {
        if (UIInteractionManager.Instance.towerSelected) return;//a tower is already selected
        UIInteractionManager.Instance.selectTowerInShop(towerPrefab);//send select prefab to manager
    }
}
