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
        LUNCH_BOX,
        COFFEE,
        GUMMY_BEAR,
        COKE,
        STATUE,
        HIGH_HEELS
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
            case ITEM.COFFEE:
                return "Triple Shot Almond Milk Extra Hot Latte \n With Extra Foam And 4 Sugars\nTemporary All Positive Stat Boost!\nTemporary All Negitive Stats Afterwards!!";
            case ITEM.GUMMY_BEAR:
                return "Bouncy Gummy Bear!?\n\nYou Feel Springy!\nVisibility Up D:";
            case ITEM.COKE:
                return "Product Placement!!! #NotSponsored\n\nAll Stats Up!\nIncluding Visibility :(!";
            case ITEM.STATUE:
                return "Stone Statue!?\n\nBecome Indestructable!!\nThey Know You're There!!";
            case ITEM.HIGH_HEELS:
                return "Fasionable Heels! Gotta Get Used To Them!!\n\nHealth Up!\nTemporary Visibilty and Noise Up! :(";

        }
        return "ERROR: item no description";
    }
}
