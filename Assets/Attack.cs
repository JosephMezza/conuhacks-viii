using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject Melee;
    bool isAttacking = false;
    float atkDuration = 0.3f;
    float atkTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        CheckMeleeTimer();

        if(Input.GetKeyDown(KeyCode.E) || Input.GetMouseButton(0)){
            OnAttack();
        }
    }

    void OnAttack(){
        if(!isAttacking){
            Melee.SetActive(true);
            isAttacking = true;
        }
    }

    void CheckMeleeTimer(){
        if(isAttacking){
            atkTimer += Time.deltaTime;
            if(atkTimer > atkDuration){
                atkTimer = 0;
                isAttacking = false;
                Melee.SetActive(false);
            }
        }
    }
}
