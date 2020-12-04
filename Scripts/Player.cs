using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject PlayerSprite;
    public GameObject ReactorSprite;

    public float StratingHeight;

    public float maxSpeed;
    public float acceleration;
    public float deceleration;
    public Vector3 Velocity;
    public float DeadZone;

    public float minx, maxx, miny, maxy;

    public float reduction;

    private void Start()
    {
        Vector3 startingPosition = Vector3.zero;
        startingPosition.y = StratingHeight;
        transform.position = startingPosition;
        _lastShot = Time.time;
    }

    private void Update()
    {

        //movement code
        if (Input.GetKey(KeyCode.Q))
        {
            Velocity.x -= acceleration;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Velocity.x += acceleration;
        }
        if (Input.GetKey(KeyCode.Z))
        {
            Velocity.z += acceleration;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Velocity.z -= acceleration;
        }

        if (Velocity.x > maxSpeed) Velocity.x = maxSpeed;
        if (Velocity.x < -maxSpeed) Velocity.x = -maxSpeed;
        if (Velocity.z > maxSpeed) Velocity.z = maxSpeed;
        if (Velocity.z < -maxSpeed) Velocity.z = -maxSpeed;

        if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            if (Velocity.x >= DeadZone) Velocity.x -= deceleration;
            else if (Velocity.x <= -DeadZone) Velocity.x += deceleration;
            else Velocity.x = 0;
        }
        if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {

            if (Velocity.z >= DeadZone) Velocity.z -= deceleration;
            else if (Velocity.z <= -DeadZone) Velocity.z += deceleration;
            else Velocity.z = 0;
        }

        float pythagoras = ((Velocity.x * Velocity.x) + (Velocity.z * Velocity.z));
        if (pythagoras > (maxSpeed * maxSpeed))
        {
            float magnitude = Mathf.Sqrt(pythagoras);
            float multiplier = maxSpeed / magnitude;
            Velocity.x *= multiplier;
            Velocity.z *= multiplier;
        }

        transform.Translate(Velocity * Time.deltaTime);

        Vector3 tmp = transform.position;

        if (tmp.x < minx) tmp.x = minx;
        if (tmp.x > maxx) tmp.x = maxx;
        if (tmp.z < miny) tmp.z = miny;
        if (tmp.z > maxy) tmp.z= maxy;

        transform.position = tmp;

        Vector3 movement = new Vector3(Velocity.x, 0.0f, Velocity.z);
        if(movement != Vector3.zero) PlayerSprite.transform.rotation = Quaternion.LookRotation(movement);

        ReactorSprite.transform.localScale = new Vector3(pythagoras/(maxSpeed*maxSpeed*reduction), pythagoras/(maxSpeed* maxSpeed* reduction), pythagoras/(maxSpeed* maxSpeed* reduction));

        int RightClick = 1;

        if(Input.GetMouseButton(RightClick)) if (canShoot()) Shoot();
    }

    private bool TowerColliding = false;
    private Tower TowerBelow = null;

    private bool side = false;
    public Bullet bullet;
    public int Atk;
    public float Range, Speed, reload;
    private float _lastShot;
    public float _offset;

    public GameObject Cannon1, Cannon2;
    private void Shoot()
    {
        Vector3 OffSet;
        if (side)
        {
            side = false;
            OffSet = Cannon1.transform.position;
        }
        else
        {
            OffSet = Cannon2.transform.position;
            side = true;
        }
        Bullet B = Instantiate(bullet, PlayerSprite.transform.forward + OffSet - new Vector3(0,StratingHeight,0), PlayerSprite.transform.rotation, GameManager.Instance.bulletsParents.transform);//create bullet
        B.GiveBulletParam(Atk, PlayerSprite.transform.localRotation.eulerAngles, Range, Speed);

        _lastShot = Time.time;//cooldown is up
    }

    private bool canShoot()
    {
        return _lastShot + reload <= Time.time;
    }
}
