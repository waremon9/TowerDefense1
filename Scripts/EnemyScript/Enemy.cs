using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected int HP;
    protected int MaxHP;
    protected int Atk;
    protected float Speed;
    protected bool Flying;

    protected FollowPath followPath;

    public virtual void Start()
    {
        //comme Start, mais différent. posez pas de questions.
        followPath = gameObject.GetComponent<FollowPath>();//get the component
        followPath.getPath();//get the path
        followPath.Move(Speed);//make it move
    }

    public int getMaxHP()//return max hp of enemy
    {
        return MaxHP;
    }
    public int getHP()//return actual hp of enemy
    {
        return HP;
    }

    public void Hit(int dmg)//reduce hp after hit
    {
        HP -= dmg;
        gameObject.GetComponent<HealthBar>().updateHealthBar();
    }

    public float DistanceLeft()//return distance left to end of path.
    {
        return followPath.GetDistanceLeft();
    }

    public int getAttaque()//return the damage dealt by enemy to player lifes
    {
        return Atk;
    }

    private void OnDestroy()
    {
        SFXManager.Instance.PlayMonsterDeathSfx();
    }
}
