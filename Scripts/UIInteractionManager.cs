using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInteractionManager : MonoBehaviour
{
    //Singleton
    public static UIInteractionManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }
    //End Singleton

    private int LeftClick = 0;

    private Tower TowerShop;
    public bool towerSelected = false;

    public GameObject TowerStatPanel;

    public Text Button1;
    public Text Button2;
    public Text TourInfo;

    public void selectTowerInShop(Tower prefab)
    {
        if (!GameManager.Instance.canBuy(prefab.Cost)) return;//not enough money
        GameManager.Instance.unSelectTower();
        towerSelected = true;//update variable
        Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);//get mouse position
        temp.y = 0;
        TowerShop = Instantiate(prefab, temp, Quaternion.identity, transform);//create tower
        TowerShop.ShowRangeIndicator(true);
    }

    private void Update()
    {
        if (towerSelected)//if a tower is selected
        {
            //move the tower with the mouse cursor.
            Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            temp.y = 0;
            TowerShop.transform.position = temp;

            if (Input.GetMouseButtonDown(LeftClick))
            {
                if (EventSystem.current.IsPointerOverGameObject())//click on shop UI
                {
                    //cancel buy
                    Destroy(TowerShop.gameObject);
                    towerSelected = false;
                }
                else
                {
                    if (TowerShop.IsColliding) return;//on the path or hitting another tower

                    TowerShop.transform.parent = GameManager.Instance.TowersParents.transform;
                    TowerShop.ActivateTower();//bool become true and tower can now shoot
                    ShowTowerStats(TowerShop);
                    towerSelected = false;//tower no longer selected
                    GameManager.Instance.AddSpendArgent(-TowerShop.Cost);//buying cost money
                }
            }
        }
    }

    public void ShowTowerStats(Tower tower)
    {
        TowerStatPanel.SetActive(true);//show panel
        if(tower.Path1Upgrade >= tower.ArrayUpgradeAvailable.GetLength(1))//first path
        {
            Button1.text = "No more upgrade";
        }
        else
        {
            Button1.text = tower.ArrayUpgradeAvailable[0, tower.Path1Upgrade].ToString();//Upgrade from path 1
        }
        if (tower.Path2Upgrade >= tower.ArrayUpgradeAvailable.GetLength(1))//second path
        {
            Button2.text = "No more upgrade";
        }
        else
        {
            Button2.text = tower.ArrayUpgradeAvailable[1, tower.Path2Upgrade].ToString();//Upgrade from path 2
        }
        TourInfo.text = tower.ToString();//display tower info
    }

    public void HideTowerStats()
    {
        TowerStatPanel.SetActive(false);//hide panel
    }
}
