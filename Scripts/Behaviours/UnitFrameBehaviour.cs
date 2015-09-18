﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UnitFrameBehaviour : MonoBehaviour, IPointerDownHandler
{

    GameManager gameManager;

    UnitBehaviour linkedUnit; //the unit "linked" to this unit frame, whose attributes we use

    public GameObject selectionOutline; //this is set in the inspector

	// Use this for initialization
	void Start () {

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (selectionOutline == null)
        {
            Debug.LogError("selection outline of unitFrameBehaviour needs to be set in inspector");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (selectionOutline != null)
        {
            if (selectionOutline.activeInHierarchy == false && linkedUnit.GetSelected())
            {
                selectionOutline.SetActive(true);
            }
            if (selectionOutline.activeInHierarchy == true && !linkedUnit.GetSelected())
            {
                selectionOutline.SetActive(false);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameManager.keyboardManager.altModifier) gameManager.ClearIfSelected(linkedUnit);
        if (gameManager.keyboardManager.shiftModifier) gameManager.SetSelectedUnit(linkedUnit);
        if (!gameManager.keyboardManager.altModifier && !gameManager.keyboardManager.shiftModifier)
        {
            Debug.Log("no modifier and clicked frame");
            gameManager.ClearSelection();
            gameManager.SetSelectedUnit(linkedUnit);
        }

    }

    public void LinkUnit(UnitBehaviour unit) //called by interfaceManager when frame is spawned
    {
        if (linkedUnit == null)
        {
            linkedUnit = unit;
        }
        else Debug.LogError("something has gone wrong, UnitFrame already has a linked unit but is being asked to link another");
    }
}
