using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{

    public int curHealth = 0;
    public int maxHealth = 100;
    public List<Vector3> positions;
    public int boneCount;
    public PlayerController playercontroller;
    private Quaternion headBoneRotation;

    public HealthBar healthBar;

    public int getcurHealth(){
        return curHealth;
    }

    void getBones(){
        positions = new List<Vector3>();
        Vector3 currentBone;
        for(int i = 0; i < transform.childCount; i++){
            currentBone = transform.GetChild(i).gameObject.transform.position;
            positions.Add(currentBone);
        }
        boneCount = positions.Count;

        headBoneRotation = playercontroller.bones[boneCount-1].transform.rotation;
    }

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        getBones();
    }

    public void DamagePlayer( int damage )
    {
        curHealth -= damage;

        healthBar.SetHealth( curHealth );
    }

    public void NewRound()
    {
        for (int i = 0; i< boneCount;i++){
            playercontroller.bones[i].transform.position = positions[i];
        }
        for (int i = 0; i< boneCount-1;i++){
            Vector2 loc = playercontroller.bones[i+1].transform.position - playercontroller.bones[i].transform.position;
            playercontroller.bones[i].transform.rotation = Quaternion.FromToRotation(Vector3.right, loc);
        }

        playercontroller.bones[boneCount-1].transform.rotation = headBoneRotation;
    }

}
