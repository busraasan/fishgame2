using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonBuffs : MonoBehaviour
{
    public GameObject buffObject;
    private Vector2 screenBounds;

    // Use this for initialization
    void Start () {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(buffSummoner());
    }
    
    private void spawnRandomBuff(){
        GameObject a = Instantiate(buffObject) as GameObject;
        a.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), Random.Range(-screenBounds.y, screenBounds.y));
    }
   
    IEnumerator buffSummoner(){
        while(true){
            yield return new WaitForSeconds(Random.Range(0.0f, 2.0f));
            spawnRandomBuff();
        }
    }
}
