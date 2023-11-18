using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum CALCULATIONTYPE{
    PLUS,
    MINUS,
    EQUAL,
    DIVIDE,
    MULTIPLY,
    MODULO,
    
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
            case CALCULATIONTYPE.DIVIDE:
                return Divide(value);
            case CALCULATIONTYPE.MULTIPLY:
                return Multiply(value);
            case CALCULATIONTYPE.MODULO:
                return Modulo(value);
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

    private int Divide(int value){
        return (int)Math.Floor( value / modifier);
    }
    private int Multiply(int value){
        return (int)Math.Floor( value * modifier);
    }
    private int Modulo(int value){
        return (int)Math.Floor( value % modifier);
    }




}
