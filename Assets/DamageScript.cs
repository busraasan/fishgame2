using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Buff.BuffType;

public class DamageScript : MonoBehaviour
{   
    public string self;
    public string target;
    private BuffHandler buffHandler;
    private int i;
    public CountdownController countdownController;
    public int numRounds;
    public int currentRound;
    public finishscript Finishscript;

    void Start(){
        buffHandler = transform.parent.gameObject.GetComponent<BuffHandler>();
        currentRound = 1;
        numRounds = 3;
    }
    public IEnumerator countdown(GameObject enemy, GameObject player){

            if (currentRound < numRounds){
                enemy.GetComponent<PlayerController>().resetRound();
                player.GetComponent<PlayerController>().resetRound();

                enemy.GetComponent<PlayerController>().setCooldown(true);
                player.GetComponent<PlayerController>().setCooldown(true);
                StartCoroutine(countdownController.CountdownToStart());
                yield return new WaitForSeconds(4);
                enemy.GetComponent<PlayerController>().setCooldown(false);
                player.GetComponent<PlayerController>().setCooldown(false);

                currentRound++;
            } else if (currentRound == numRounds)
            {
                Finishscript.viewText(true);
            }
            
        }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == self)
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        if(collision.gameObject.tag == target){
            StartCoroutine(countdown(collision.gameObject.transform.parent.gameObject, gameObject.transform.parent.gameObject)); // enemy, player
        }
        if(collision.gameObject.tag == "Buff"){
            switch(collision.gameObject.GetComponent<Buff>().type){
                case Buff.BuffType.SpeedBonus:
                    Destroy(collision.gameObject);
                    Debug.Log("speed");
                    StartCoroutine(buffHandler.speedBonus());
                    break;
                case Buff.BuffType.Heal:
                    Destroy(collision.gameObject);
                    Debug.Log("heal");
                    StartCoroutine(buffHandler.healBonus());
                    break;
                case Buff.BuffType.EnemyLengthDebuff:
                    Destroy(collision.gameObject);
                    Debug.Log("enemyhoho");
                    StartCoroutine(buffHandler.LengthDebuff());
                    break;
            }
            Destroy(collision.gameObject);
        }
    }
}
