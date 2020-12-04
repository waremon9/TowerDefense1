using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SimpleJSON;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager Instance;
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

    //prefab of all enemy
    public PinkOne prefabPinkOne;
    public PurpleOne prefabPurpleOne;
    public GreenOne prefabGreenOne;
    public MettalicOne prefabMettalicOne;

    //parent to put every new instance in
    public List<Enemy> enemiesList;
    public GameObject TowersParents;
    public GameObject bulletsParents;
    public GameObject ParticlesParents;

    //player lifes and money
    public int Argent;
    public int Vies;

    //text in UI
    public Text ArgentHud;
    public Text HeartHud;

    public GameObject PausePanel;

    private int LeftClick = 0;

    private Tower PlayerSelectedTower = null;//unused yet but will be to display info about tower and upgrade

    private JSONNode data;//script in plugin folder, not mine

    //check to know when a wave is finished and give a reward to player at the end of each wave
    public Level ActualLevel;
    public bool WaveEnded = false;
    public bool RewardObtained = true;

    private bool Pause = false;

    private void Start()
    {
        CreateLevel(Application.streamingAssetsPath + "/Level1.json");//get the data for the  level (and only level)
        ArgentHud.text = " : " + Argent;//set up UI text
        HeartHud.text = " : " + Vies;//set up UI text
    }

    private void getJSONData(string file)
    {
        //get data in a .json file
        string path;
        string jsonString;

        path = file;

        jsonString = File.ReadAllText(path);
        data = JSON.Parse(jsonString);
    }

    private void CreateLevel(string file)
    {
        //Load all level data from .js and put it in EnemyGroup, then in Wave, then in level.

        getJSONData(file);
        List<Wave> levelwaves = new List<Wave>();
        foreach (JSONNode wave in data)
        {
            List<EnemyGroup> enemygroup = new List<EnemyGroup>();
            foreach (JSONNode group in wave)
            {
                Enemy enemyTmp = null;
                string str = group["enemy"];
                switch (str)//from String to Enemy
                {
                    case "PinkOne":
                        enemyTmp = prefabPinkOne;
                        break;
                    case "PurpleOne":
                        enemyTmp = prefabPurpleOne;
                        break;
                    case "GreenOne":
                        enemyTmp = prefabGreenOne;
                        break;
                    case "MettalicOne":
                        enemyTmp = prefabMettalicOne;
                        break;
                    default:
                        break;
                }
                if(enemyTmp==null)Debug.LogError("Enemy pas trouver, erreur dans le json");//in case i missed something
                enemygroup.Add(new EnemyGroup(enemyTmp, group["size"], group["time"]));
            }
            levelwaves.Add(new Wave(enemygroup));
        }
        ActualLevel = new Level(levelwaves);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))//Pause the game by pressing P
        {
            if (Pause)
            {
                UnpauseGame();
            }
            else
            {
                PauseGame();
            }
        }


        //Check variable and give money reward to the player at the end of each wave
        if (!RewardObtained && WaveEnded && enemiesList.Count == 0)
        {
            int MoneyRewardAfterWave = 100 + ActualLevel.ActualWave;//reward is 100 + theWaveNumber
            AddSpendArgent(MoneyRewardAfterWave);
            WaveEnded = false;
            RewardObtained = true;
        }

        if (Vies <= 0) GameLost();//TODO

        //get click on tower to display info like wshowing range. no more yet
        if (Input.GetMouseButtonDown(LeftClick) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100.0f))//raycast
            {
                if (hit.transform.CompareTag("Tower"))//hit a tower
                {
                    if (PlayerSelectedTower) PlayerSelectedTower.ShowRangeIndicator(false);//deactivate lastly selected tower
                    PlayerSelectedTower = hit.transform.GetComponentInParent<Tower>();//get new one
                    PlayerSelectedTower.ShowRangeIndicator(true);//activate it

                    if(PlayerSelectedTower.transform.IsChildOf(TowersParents.transform))UIInteractionManager.Instance.ShowTowerStats(PlayerSelectedTower);
                }
                else
                {
                    unSelectTower();
                }
            }
            else//deactivate when clicking somewhere else.
            {
                unSelectTower();
            }

        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;//Pause
        PausePanel.SetActive(true);
        Pause = true;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;//Unpause
        PausePanel.SetActive(false);
        Pause = false;
    }

    public void unSelectTower()
    {
        if (PlayerSelectedTower) PlayerSelectedTower.ShowRangeIndicator(false);
        PlayerSelectedTower = null;

        UIInteractionManager.Instance.HideTowerStats();
    }

    public void AddEnnemy(Enemy E)
    {
        enemiesList.Add(E);//add enemy to list of alive enemy
    }
    public void DeleteEnnemy(GameObject G, bool killed)
    {
        //enemy is dead, remove it from list and destroy it. but don't forget the money
        if(killed) AddSpendArgent(G.GetComponent<Enemy>().getMaxHP());
        enemiesList.Remove(G.GetComponent<Enemy>());
        Destroy(G);
    }
    
    public bool canBuy(int price)//return true if enough money to buy
    {
        return Argent - price >= 0;
    }

    public void AddSpendArgent(int change)//allow to modifie qte of money
    {
        Argent += change;
        ArgentHud.text = " : " + Argent;//update UI text
    }

    public void looseHealth(int dmg)
    {
        Vies -= dmg;
        if (Vies < 0)
        {
            Vies = 0;
        }
        HeartHud.text = " : " + Vies;//update UI text
    }

    public void ButtonUpgrade1Clicked()//button 1 for path n°1
    {
        if (PlayerSelectedTower.Path1Upgrade >= PlayerSelectedTower.ArrayUpgradeAvailable.GetLength(1)) return;//all bought
        if (canBuy(PlayerSelectedTower.ArrayUpgradeAvailable[0, PlayerSelectedTower.Path1Upgrade].Cost))//check enough money
        {
            AddSpendArgent(-PlayerSelectedTower.ArrayUpgradeAvailable[0, PlayerSelectedTower.Path1Upgrade].Cost);//buy

            PlayerSelectedTower.ApplyUpgrade(PlayerSelectedTower.ArrayUpgradeAvailable[0, PlayerSelectedTower.Path1Upgrade]);//upgrade tower

            PlayerSelectedTower.Path1Upgrade++;//increment upgrade counter

            UIInteractionManager.Instance.ShowTowerStats(PlayerSelectedTower);//update UI
        }
    }
    public void ButtonUpgrade2Clicked()//button 2 for path n°2
    {
        if (PlayerSelectedTower.Path2Upgrade >= PlayerSelectedTower.ArrayUpgradeAvailable.GetLength(1)) return;//all bought
        if (canBuy(PlayerSelectedTower.ArrayUpgradeAvailable[1, PlayerSelectedTower.Path2Upgrade].Cost))//check enough money
        {
            AddSpendArgent(-PlayerSelectedTower.ArrayUpgradeAvailable[1, PlayerSelectedTower.Path2Upgrade].Cost);//buy

            PlayerSelectedTower.ApplyUpgrade(PlayerSelectedTower.ArrayUpgradeAvailable[1, PlayerSelectedTower.Path2Upgrade]);//upgrade tower

            PlayerSelectedTower.Path2Upgrade++;//increment upgrade counter

            UIInteractionManager.Instance.ShowTowerStats(PlayerSelectedTower);//update UI
        }
    }

    public void GameLost()
    {
        Debug.Log("TO DO LOST GAME");
    }
}
