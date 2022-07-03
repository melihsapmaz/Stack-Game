using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour{
    [SerializeField] private GameObject obstacle;
    [SerializeField] private float speed;
    private bool dirRight = true;
    void Update(){
        ObstacleMove();
    }
    private void ObstacleMove(){
        if (dirRight)
            transform.Translate (Vector2.right * speed * Time.deltaTime);
        else
            transform.Translate (-Vector2.right * speed * Time.deltaTime);
        if(transform.position.x >= 5.0f)
            dirRight = false;
        if(transform.position.x <= -5.0f)
            dirRight = true;
    }



}
