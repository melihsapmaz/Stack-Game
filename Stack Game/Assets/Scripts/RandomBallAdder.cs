using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBallAdder : MonoBehaviour{

    [SerializeField] private GameObject ballPrefab, gatePrefab;
    [SerializeField] private float xBoundary;

    void Start(){
        xBoundary = 1.8f;
        AddRandomBalls();
    }
    
    private void AddRandomBalls(){
        for(int i = -43; i < 43; i += 3){
            Instantiate(ballPrefab, new Vector3(Random.Range(-xBoundary, + xBoundary), 1, (float)i), Quaternion.identity);
        }
        for(int i = -41; i < 41; i += (int)Random.Range(15,20)){
            var go = Instantiate(gatePrefab, new Vector3(0, 0, i), Quaternion.identity);
            if(i % 2 == 0 || i % 4 == 0  || i % 8 == 0){
                go.transform.GetChild(0).position = new Vector3(go.transform.GetChild(0).position.x * -1,
                                                                go.transform.GetChild(0).position.y,
                                                                go.transform.GetChild(0).position.z);
                go.transform.GetChild(1).position = new Vector3(go.transform.GetChild(0).position.x * -1,
                                                                go.transform.GetChild(0).position.y,
                                                                go.transform.GetChild(0).position.z);
                                                                
            }
        }
    }
}
