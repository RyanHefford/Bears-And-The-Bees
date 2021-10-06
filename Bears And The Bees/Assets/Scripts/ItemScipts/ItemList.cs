using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    public enum ITEM
    {
        VANS = 0,
        BELT,
        CHICKEN,
        SMOKE_BOMB,
        ROSE,
        LUNCH_BOX
    }

    public static string GetDescription(ITEM requestedItem)
    {
        switch (requestedItem)
        {
            case ITEM.VANS:
                return "New Vans!!\n\nJump Height Way Up!\nMove Speed Slightly Up!";
            case ITEM.BELT:
                return "Fluorescent Belt!!\n\nMove Speed Way Up!!\nVisibility Up!";
            case ITEM.CHICKEN:
                return "A Full Roast Chicken!?!!\n\nHealth Up!!\nMove Speed Slightly Down!";
            case ITEM.SMOKE_BOMB:
                return "A Single Smoke Bomb!!\n\nTemporary Invisibility\nStun Nearby Enemys!!";
            case ITEM.ROSE:
                return "Super Fragrent Flower!?\n\nTemporary Distraction\nTrick Your Enemys!";
            case ITEM.LUNCH_BOX:
                return "A Full Lunchbox!\n\nInstant Heal!!!\nTemporary Movespeed Down D:";

        }
        return "ERROR: item no description";
    }
}
