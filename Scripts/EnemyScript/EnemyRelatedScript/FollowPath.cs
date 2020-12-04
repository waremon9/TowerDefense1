using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FollowPath : MonoBehaviour
{
    private Vector3[] path;

    public void getPath()
    {
        //get the path to follow
        path = iTweenPath.GetPath("EnemyPath");
    }

    public float GetDistanceLeft()
    {
        float dist = Vector3.Distance(transform.position, path[0]);
        for (int i = 0; i < path.Length - 1; i++)
        {
            dist += Vector3.Distance(path[i], path[i + 1]);
        }
        return dist;
    }

    private void Update()
    {
        //need to modify path every time a waypoint is reached because stop and continue will make it redo all and doing position and not path don't have smooth curves.
        if (path.Length>1 && Vector3.Distance(transform.position, path[0])<0.3f)//if waypoint reached and not end of path
        {
            //delete passed waypoint from path to allow stop and continue as well as speed changing
            path = path.Skip(1).ToArray();
            
        }
        if(path.Length  == 1)//end of path
        {
            if(path[0] == transform.position)
            {
                GameManager.Instance.looseHealth(gameObject.GetComponent<Enemy>().getAttaque());
                GameManager.Instance.DeleteEnnemy(gameObject, false);
                Destroy(gameObject);
            }
        }

    }

    public void Stop()//Stop the movement of the enemy
    {
        iTween.Stop(gameObject);
    }
    public void Move(float speed)//the enemy move again
    {
        PathLenghtCheckAndDo(speed);
    }

    private void PathLenghtCheckAndDo(float speed)
    //check size of path and use either path or position to not crash
    {
        if (path.Length == 1)//path not accepted, need at least 2 values
        {
            iTween.MoveTo(gameObject, iTween.Hash("Position", path[0], "time", CalculateSpeed(speed), "easeType", iTween.EaseType.linear, "orienttopath", true, "looktime", 0.5f));//finish with "position"
        }
        else
        {
            iTween.MoveTo(gameObject, iTween.Hash("Path", path, "time", CalculateSpeed(speed), "easeType", iTween.EaseType.linear, "orienttopath", true, "looktime", 0.5f));//more than 1 waypoint left, continue with "path"
        }
    }

    private float CalculateSpeed(float speed)
    //because itween can't have a constant speed, it depend heavily on the distance with the waypoint.
    {
        float duration = GetDistanceLeft() / speed;
        return duration;
    }
}
