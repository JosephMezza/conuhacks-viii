using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject Melee;
    bool isAttacking = false;
    float atkDuration = 0.3f;
    float atkTimer = 0f;

    public Transform Aim;
    public GameObject bullet;
    public float fireForce = 10f;
    float shootCooldown = 0.25f;
    float shootTimer = 0.5f;

    // Update is called once per frame
    void Update()
    {
        CheckMeleeTimer();
        shootTimer += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.E) || Input.GetMouseButton(0)){
            OnAttack();
        }

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetMouseButton(1)) {
            OnShoot();
        }
    }

    void OnAttack(){
        if(!isAttacking){
            Melee.SetActive(true);
            isAttacking = true;
        }
    }

    void OnShoot() {
        if (shootTimer > shootCooldown) {
            shootTimer = 0;
            GameObject intBullet = Instantiate(bullet, Aim.position, Aim.rotation);
            intBullet.GetComponent<Rigidbody2D>().AddForce(-Aim.up * fireForce, ForceMode2D.Impulse);
            Destroy(intBullet, 2f);
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