﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public CharacterAttackSystem attackSystem;
    public CharacterStatsSystem statsSystem;
    public CharacterMovement movement;
    public MonoBehaviour[] skills;
    public GameObject target;
    public GameObject girlGameObject;
    public GameObject robotGameObject;
    public Animator animator;
    private bool isStun = false;
    private bool isRobot = true;
    public string playableCharacterTagName = "Player";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(gameObject.tag);
        if (isRobot)
        {
            if (Input.GetMouseButtonDown(0)) attackSystem.AddAtackToQueue();
            if (Input.GetKeyDown(KeyCode.Space)) UseSkill(0);
            if (Input.GetKeyDown(KeyCode.Q)) UseSkill(1);
            if (Input.GetKeyDown(KeyCode.E)) UseSkill(2);
            if (Input.GetKeyDown(KeyCode.R)) UseSkill(3);
            if (Input.GetKeyDown(KeyCode.Tab)) SwitchPlayerCharacter();
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (GameObject.FindGameObjectWithTag("Girl"))
                {
                    return;
                }
                girlGameObject.SetActive(true);
                girlGameObject.transform.position = new Vector3(robotGameObject.transform.position.x - 200, robotGameObject.transform.position.y, 0);
                playableCharacterTagName = "Girl";
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0)) attackSystem.AddAtackToQueue();
            if (Input.GetKeyDown(KeyCode.Space)) UseSkill(0);
            if (Input.GetKeyDown(KeyCode.Tab)) SwitchPlayerCharacter();
        }

    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (playableCharacterTagName == "Girl" && gameObject.tag == "Player")
        {
            Transform rootT = other.transform.root;
            GameObject go = rootT.gameObject;
            Debug.Log("Enter to Trigger, " + go.tag);
            if (Input.GetKeyDown(KeyCode.Y) && go.tag == "Girl")
            {
                go.SetActive(false);
                playableCharacterTagName = "Player";
                isRobot = true;
                Debug.Log("Switch player");
            }
        }
    }

    private void UseSkill(int index)
    {
        if (skills[index] is ISkills) (skills[index] as ISkills).UseSkill();
    }
    IEnumerator Stun()
    {
        isStun = true;
        yield return new WaitForSeconds(0.7f);
        isStun = false;
    }

    public void ChangeStunState(bool isStun)
    {
        this.isStun = isStun;
    }

    public void SwitchPlayerCharacter()
    {
        if (GameObject.FindGameObjectWithTag("Girl") && GameObject.FindGameObjectWithTag("Player"))
        {
            if (isRobot)
            {
                isRobot = false;
                playableCharacterTagName = "Girl";
            }
            else if(!isRobot)
            {
                isRobot = true;
                playableCharacterTagName = "Player";
            }
        }
        
    }
}
