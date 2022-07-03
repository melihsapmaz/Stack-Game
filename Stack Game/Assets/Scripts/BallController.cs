using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallController : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private TMP_Text ballCountText;
    [SerializeField] private List<GameObject> balls = new List<GameObject>();
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float horizontalLimit;
    private float horizontal;
    [SerializeField] private float moveSpeed;
    private int gateNumber;
    private int targetCount;
    public static bool win, lose;
    void Start(){
        win = false;
        lose = false;
    }

    void Update(){
        if(StartingGame.isGameStarted){
            HorizontalBallMove();
            VerticalBallMove();
            UpdateBallCountText();
            listColliders();
        }
        if(this.transform.position.z < 50 && this.transform.position.z > 49)
            win = true;
        
    }
    private void HorizontalBallMove(){
        float newX;
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            horizontal = touch.deltaPosition.x;
        }
        else{
            horizontal = 0;
        }

        newX = transform.position.x + horizontal * horizontalSpeed * Time.deltaTime;
        newX = Mathf.Clamp(newX, -horizontalLimit, horizontalLimit);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
    private void VerticalBallMove(){
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("BallStack")){
            other.gameObject.transform.SetParent(transform);
            other.gameObject.GetComponent<SphereCollider>().enabled = false;
            other.gameObject.transform.localPosition = new Vector3(0f, 0f, balls[balls.Count -1].transform.localPosition.z - 1f);
            balls.Add(other.gameObject);
        }
        if(other.gameObject.CompareTag("Gate")){
            if((int)other.gameObject.transform.position.z != (int)this.gameObject.transform.position.z)
                return;
            gateNumber = other.gameObject.GetComponent<GateController>().GateNumber();
            targetCount = balls.Count + gateNumber;
            if(gateNumber > 0){
                IncreaseBallCount();
                other.gameObject.GetComponent<Collider>().enabled = false; 
            }
            else if(gateNumber < 0){
                DecreaseBallCount();
                other.gameObject.GetComponent<Collider>().enabled = false; 
            }
        }
        if(other.gameObject.CompareTag("Obstacle")){
            CollideDestroy(other);
        }
    }
    private void UpdateBallCountText(){
        ballCountText.text = balls.Count.ToString();
    }
    private void IncreaseBallCount(){
        for(int i = 0; i < gateNumber; i++){
            GameObject newBall = Instantiate(ballPrefab);
            newBall.transform.SetParent(transform);
            newBall.GetComponent<SphereCollider>().enabled = false;
            newBall.gameObject.transform.localPosition = new Vector3(0f, 0f, balls[balls.Count -1].transform.localPosition.z - 1f);
            balls.Add(newBall);
        }
    }
    private void DecreaseBallCount(){
        if(-balls.Count >= gateNumber){
           lose = true;
           return;
        }
        for(int i = balls.Count - 1; i >= targetCount; i--){
            Destroy(balls[i]);
            balls.RemoveAt(i);
        }
    }
    private void listColliders(){
        foreach (var item in balls){
            item.gameObject.GetComponent<SphereCollider>().enabled = true;
        }
    }
    private void CollideDestroy(Collider other){
        var x = (int)this.gameObject.transform.position.z - (int)other.gameObject.transform.position.z;
        Debug.Log(x);
        if(x == 0){
            lose = true;
        }
        else{
            for(int i = balls.Count - 1; i >= x; i--){
                Destroy(balls[i]);
                balls.RemoveAt(i);
            }
        }
    }
}
