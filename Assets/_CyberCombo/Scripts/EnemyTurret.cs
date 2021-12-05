using UnityEngine;

public class EnemyTurret : Enemy
{
    public float facing = -1.0f;

	void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        gunCoolDown = rateOfFire;
        walkingDirection = facing;
        sprite.flipX = (facing > 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!permaFrost && frozen && timeToMelt < 0)
        {
            UnFreeze();
        }
        else if (!frozen)
        {
            this.gunCoolDown = this.gunCoolDown - Time.deltaTime;            
            if (canShoot && 0 > gunCoolDown)
            {
                Shoot();
                this.gunCoolDown = rateOfFire;
            }
        }
        else
        {
            this.timeToMelt = this.timeToMelt - Time.deltaTime;
        }

    }
}
