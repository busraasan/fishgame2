using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public enum BuffType
    {
        Heal,
        SpeedBonus,
        EnemyLengthDebuff
    }

    public int lifeTime = 5;
    public Color color;
    
    public BuffType type;

    private SpriteRenderer sprite;

    void Start()
    {
        type = (BuffType)Random.Range(0, System.Enum.GetValues(typeof(BuffType)).Length); // pick random buff from enum type 'buffType'
        sprite = GetComponent<SpriteRenderer>();
        switch(type){
            case BuffType.Heal:
                color = new Color(0,255,0,255);
                break;
            case BuffType.SpeedBonus:
                color = new Color(0,0,255,255);
                break;
            case BuffType.EnemyLengthDebuff:
                color = new Color(255,0,0,255);
                break;
        }
        sprite.color = color;
        StartCoroutine(destroySelf());
        //kek
    }

    // Update is called once per frame
    void Update()
    {
        
    
    }

    IEnumerator destroySelf(){
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
