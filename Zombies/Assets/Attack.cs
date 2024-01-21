using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.EventSystems;

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

    private float gunDistance = 0.2f;

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;

        Aim.rotation = Quaternion.Euler(new Vector3(0,0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));


        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Aim.position = transform.position + Quaternion.Euler(0, 0, angle) * new Vector3(gunDistance, 0, 0);

        CheckMeleeTimer();
        shootTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnAttack();



        }
        if (Input.GetMouseButton(0))
        {
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
            intBullet.GetComponent<Rigidbody2D>().AddForce(Aim.right *fireForce, ForceMode2D.Impulse);
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
