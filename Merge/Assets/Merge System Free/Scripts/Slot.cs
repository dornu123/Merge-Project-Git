﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Slot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI slotIDText;

    public int id;
    public Item currentItem;
    public SlotState state = SlotState.Empty;

    private void Awake() 
    {
        slotIDText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update() 
    {
        slotIDText.text = id.ToString();
    }

    public void CreateItem(int id) 
    {
        var itemGO = (GameObject)Instantiate(Resources.Load("Prefabs/Item"));
        
        itemGO.transform.SetParent(this.transform);
        itemGO.transform.localPosition = Vector3.zero;
        itemGO.transform.localScale = Vector3.one;

        currentItem = itemGO.GetComponent<Item>();
        currentItem.Init(id, this);

        ChangeStateTo(SlotState.Full);
    }

    private void ChangeStateTo(SlotState targetState)
    {
        state = targetState;
    }

    public void ItemGrabbed()
    {
        Destroy(currentItem.gameObject);
        ChangeStateTo(SlotState.Empty);
    }

    private void ReceiveItem(int id)
    {
        switch (state)
        {
            case SlotState.Empty: 

                CreateItem(id);
                ChangeStateTo(SlotState.Full);
                break;

            case SlotState.Full: 
                if (currentItem.id == id)
                {
                    //Merged
                    Debug.Log("Merged");
                }
                else
                {
                    //Push item back
                    Debug.Log("Push back");
                }
                break;
        }
    }

    public int GetID()
    {
        return id;
    }
}

public enum SlotState
{
    Empty,
    Full
}