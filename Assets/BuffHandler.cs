using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffHandler : MonoBehaviour
{
    private PlayerController playerController;
    private Health health;
    public GameObject Enemy;
    private PlayerController enemyController;

    void Start(){
        playerController = GetComponent<PlayerController>();
        enemyController = Enemy.GetComponent<PlayerController>();
        health = GetComponent<Health>();
    }

    public IEnumerator speedBonus(){
        playerController.setSpeed(8);
        yield return new WaitForSeconds(2);
        playerController.setSpeed(playerController.getBaseSpeed());
    }

    public IEnumerator healBonus(){
        if(health.getcurHealth() < 1000){
            health.DamagePlayer(-10);
            yield return new WaitForSeconds(2);
        }
    }


    
    public IEnumerator LengthDebuff(){
        enemyController.setFishLength(1.8f);
        yield return new WaitForSeconds(2);
        enemyController.setFishLength(enemyController.getBaseFishLength());
        }

    }

