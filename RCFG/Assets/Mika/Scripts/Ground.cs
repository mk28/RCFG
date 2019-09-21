﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nvp.events;
using System;
using Player;
using UnityEngine.UI;

public partial class Ground : MonoBehaviour
{

    public Material selectedMaterial;
    public Material unselectedMaterial;
    public GameObject content;
    [SerializeField] public bool selected = false;
    public GameObject ore;
    public Text buttonText;
    public GameObject movementMarker;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Events("select").GameEventHandler += onSelect;
        transform.GetChild(0).GetComponent<Canvas>().worldCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onSelect(object sender, System.EventArgs args)
    {
        if (((GameObject)sender).GetComponent<Ground>() == this|| ((GameObject)sender)==content)
        {
            select();
        }
        else
        {
            deselect();
        }


    }

    void select()
    {
        if (!selected)
        {
            this.transform.localPosition += new Vector3(0, 0.5f, 0);
            selected = true;
            this.GetComponent<Renderer>().material = selectedMaterial;
            if (ore != null && content == null)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                //buttonText.GetComponent<Text>().text = content.gameObject.name.Split('(')[0];
            }
        }
    }

    public void deselect()
    {
        if (selected)
        {
            this.transform.localPosition -= new Vector3(0, 0.5f, 0);
            selected = false;
            this.GetComponent<Renderer>().material = unselectedMaterial;
            if (ore != null && content == null)
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public void build()
    {
        if (GetComponentInParent<PlayerManager>().currPlayerIndex == 1)
        {
            this.content = Instantiate(transform.GetChild(1).GetComponent<Ore>().mine, this.transform.position+Vector3.up, Quaternion.Euler(-90, 125, 0), this.transform);
        }
        else
        {
            this.content = Instantiate(transform.GetChild(1).GetComponent<Ore>().mine, this.transform.position + Vector3.up, Quaternion.Euler(-90, -55, 0), this.transform);

        }
        content.GetComponent<Mine>().player = GetComponentInParent<PlayerManager>().CurrPlayer;
        GetComponentInParent<PlayerManager>().CurrPlayer.items.items["Gold"] -= content.GetComponent<Mine>().priceGold;
        GetComponentInParent<PlayerManager>().CurrPlayer.items.items["Eisen"] -= content.GetComponent<Mine>().priceIron;
        if (transform.GetChild(1).GetComponent<Ore>().minetype == "gold")
        {
            GetComponentInParent<PlayerManager>().CurrPlayer.goldMines++;
        }
        else
        {
            GetComponentInParent<PlayerManager>().CurrPlayer.ironMines++;
        }
        
    }

}
