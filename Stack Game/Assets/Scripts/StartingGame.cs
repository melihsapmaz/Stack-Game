using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartingGame : MonoBehaviour
{
    public static bool isGameStarted;
    public static bool isFirstTouch;
    [SerializeField] private TMP_Text levelNumberText;
    void Start(){
        isGameStarted = false;
    }

    void Update(){
        isWin();
        isLose();
        levelNumberText.text = "Level " + SceneManager.GetActiveScene().name;
        if(Input.touchCount > 0){
            Touch touch;
            touch = Input.GetTouch(0);
            isGameStarted = true;
            this.transform.GetChild(0).gameObject.SetActive(false);
        }

    }
    private void isWin(){
        if(BallController.win == true){
            Debug.Log("Win");
            BallController.win = false;
            if(SceneManager.sceneCountInBuildSettings - 1 != SceneManager.GetActiveScene().buildIndex)
                SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
            else
                SceneManager.LoadScene(0);
        }
    }
    private void isLose(){
        if(BallController.lose == true){
            Debug.Log("Lose");
            BallController.lose = false;
            SceneManager.LoadScene(0);
        }
    }

}
