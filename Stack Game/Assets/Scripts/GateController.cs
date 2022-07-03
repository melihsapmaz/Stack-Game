using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateController : MonoBehaviour
{
    //false for negative gate, true for positive gate
    [SerializeField] private bool gateType;
    [SerializeField] private int gateNumber = 0;
    [SerializeField] private TMP_Text gateNumberText;
    

    void Start(){
        RandomGateNumber();
    }
    public int GateNumber(){
        return gateNumber;
    }

    private void RandomGateNumber(){
        switch (gateType)
        {
            case true:
                gateNumber = Random.Range(1,10);
                gateNumberText.text = gateNumber.ToString();
            break;
            case false:
                gateNumber = Random.Range(-10,-1);
                gateNumberText.text = gateNumber.ToString();
            break;
            default:
        }
    }
}
