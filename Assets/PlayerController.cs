using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool canPlayerMove = true;
    public float speed = 5.0f;
    public float baseSpeed;
    public float rotationSpeed = 15.0f;
    public GameObject rootBone;
    public List<GameObject> bones;
    public int boneCount;
    private Vector3[] positions; 
    private Vector3[] tempPositions; 

    public float[] lengths; 

    public int bufferSize = 18;
    public Vector3 lastpos;
    public float dist;
    public float fishLengthFactor = 1f;
    public float moveHorizontal = 0;
    public float moveVertical = 0;
    public float fishLength;
    public float basefishLength;

    private bool onCooldown = false;

    // Reset variables
    public Quaternion headStartRotation;
    public List<Vector3> boneStartPositions;

    public void setCooldown(bool t){
        onCooldown = t;
    }

    public void setSpeed(float s){
        speed = s;
    }
    
    public void changeDirection(int i){
        moveHorizontal = i;
    }
    
    public float getSpeed(){
        return speed;
    }

    public float getBaseSpeed(){
        return baseSpeed;
    }

    public float getBaseFishLength(){
        return basefishLength;
    }
    public void setFishLength(float f){
        fishLengthFactor = f;
    }
    
    void getBones(){
        bones = new List<GameObject>();
        GameObject currentBone;
        for(int i = 0; i < transform.childCount; i++){
            currentBone = transform.GetChild(i).gameObject;
            bones.Add(currentBone);
            boneStartPositions.Add(currentBone.transform.position);
        }
        boneCount = bones.Count;
    }

    void getLengths(){
        lengths = new float[bufferSize-1];
        for (int i = 0; i< lengths.Length;i++){
            lengths[i] = Vector2.Distance(bones[i].transform.position, bones[i+1].transform.position);
        }
    }

    void moveOneBehind(){
        for (int i = 0; i< positions.Length-1;i++){
            positions[i] = positions[i+1];
        }
        
        lastpos = positions[boneCount-1];
    }
    
    public void resetRound(){
        for (int i = 0; i< boneCount;i++){
            bones[i].transform.position = boneStartPositions[i];
            positions[i] = boneStartPositions[i];
        }
        
        for (int i = 0; i< boneCount-1;i++){
            Vector2 loc = bones[i+1].transform.position - bones[i].transform.position;
            bones[i].transform.rotation = Quaternion.FromToRotation(Vector3.right, loc);
        }

        bones[boneCount-1].transform.rotation = headStartRotation;
    }

    // Start is called before the first frame update
    void Start()
    {
        baseSpeed = speed;
        basefishLength = fishLengthFactor;
        getBones();
        headStartRotation = bones[boneCount-1].transform.rotation;
        getLengths();
        positions = new Vector3[bufferSize];
        tempPositions = new Vector3[bufferSize];
        for (int i = 0; i< positions.Length;i++){
            positions[i] = rootBone.transform.position;
        }

        fishLength = 0;
        for (int i = 0; i< boneCount-1;i++){
            fishLength += Vector2.Distance(bones[i].transform.position, bones[i+1].transform.position);
        }
    }

    void FixedUpdate()
    {
        if(!onCooldown){
            boneCount = bones.Count;
            // Debug stuff
            fishLength = 0;
            for (int i = 0; i< boneCount-1;i++){
                fishLength += Vector2.Distance(bones[i].transform.position, bones[i+1].transform.position);
            }

            // Move head bone
            bones[boneCount-1].transform.position += bones[boneCount-1].transform.right * speed * Time.deltaTime;
            bones[boneCount-1].transform.Rotate(0,0, -moveHorizontal * rotationSpeed * Time.deltaTime);

            positions[boneCount-1] =  bones[boneCount-1].transform.position;
            for (int i=boneCount-1;i>0;i--){
                float distance = Vector2.Distance(positions[i], positions[i-1]);
                if(distance > lengths[i-1]*fishLengthFactor){
                    float ratio = lengths[i-1]*fishLengthFactor/distance;
                    Vector3 newPoint = (1-ratio)*positions[i] + ratio*positions[i-1];
                    positions[i-1] = newPoint;
                }
            }

            //
            for (int i = 0; i< boneCount;i++){
                bones[i].transform.position = positions[i];
            }


            // make bones look at other bones
            for (int i = 0; i< boneCount-1;i++){
                Vector2 loc = bones[i+1].transform.position - bones[i].transform.position;
                bones[i].transform.rotation = Quaternion.FromToRotation(Vector3.right, loc);
            }

        }
    }
  
}
