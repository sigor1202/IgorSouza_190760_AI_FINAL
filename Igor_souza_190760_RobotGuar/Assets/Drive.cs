using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Drive : MonoBehaviour {
    //velocidade de movimento
	float speed = 20.0F;
    //velocidade de rotação
    float rotationSpeed = 120.0F;
    //variavel para o prefabe da bala
    public GameObject bulletPrefab;
    //local de spawn
    public Transform bulletSpawn;
    public Slider healthBar;
    float health = 100.0f;
    public Transform spawn;






    void Update() {
        //pega o centro da camera
        Vector3 healthBarPos = Camera.main.WorldToScreenPoint(this.transform.position);
        //valor da barra igual a health
        healthBar.value = (int)health;
        //posiciona a barra de vida
        healthBar.transform.position = healthBarPos + new Vector3(0, 60, 0);


        //movimenta para frente e para tras
        float translation = Input.GetAxis("Vertical") * speed;
        //rotaciona 
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        //deixa a velocidade de rotação e movimentação constante independente da quantidade de fps
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);
        //se apertar espaço
        if(Input.GetKeyDown("space"))
        {   //spawna a bala e adiciona fprça para ir na direção
            GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward*2000);
        }
        //verificar a vida
        if (health == 0)
        {
            Respawn();
        }
    }

    //se colidir e a tag for bullet
    void OnCollisionEnter(Collision col)
    {   //se colidir com um gameObject com a tag bullet
        if (col.gameObject.tag == "bullet")
        {
            //subtrai 10 da vida
            health -= 10;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //se a tag de quem colidiu for area
        if(other.CompareTag("area"))
        {
            //tira 100 da vida
            health -= 100;
            //destroi a outro objeto e seus pais
            Destroy(other.transform.parent.gameObject);
        }
    }


    public void Respawn()
    {
        //iguala a vida a 100
        health = 100;
        //aparece na posição escolhida para o spawn
        transform.position = spawn.position;
    }
}
