using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public int coinsGiven = 10;
    public bool isLooted = false;

    public Transform GetTransform()
    {
        return transform;
    }

    public string GetInteractText()
    {
        return isLooted ? "Chest is empty" : "Open chest";
    }

    public void Interact(PlayerController player)
    {
        if (isLooted) return;

        // Anna kolikot
        PlayerData.Instance.coins += coinsGiven;
        Debug.Log("Chest looted! +" + coinsGiven + " coins");

        isLooted = true;
    }
}
