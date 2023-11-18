using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum CALCULATIONTYPE{
    PLUS,
    MINUS,
    EQUAL
}


[CreateAssetMenu(fileName = "New Calculation", menuName = "Calculations/Calculation")]
public class Calculation : ScriptableObject{
    [SerializeField]
    public string description;
    [SerializeField]
    float modifier;
    [SerializeField]
    CALCULATIONTYPE type;
    

    public int ApplyCaluclation(int value){
        switch (type)
        {
            case CALCULATIONTYPE.PLUS:
                return Plus(value);
            case CALCULATIONTYPE.MINUS:
                return Minus(value);
            case CALCULATIONTYPE.EQUAL:
                return Equal(value);
            default:
                return 0;
        }
    }


    private int Plus(int value){
        return (int)Math.Floor( value + modifier);
    }

    private int Minus(int value){
        return (int)Math.Floor( value - modifier);
    }

    private int Equal(int value){
        return (int)Math.Floor( modifier);
    }




}
