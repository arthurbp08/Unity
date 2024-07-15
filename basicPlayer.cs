using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed; //velocidade geral
    [SerializeField] private float jumpForce; //força do pulo
    [SerializeField] private int jumpCont; //contagem de pulos
    private int jumpContT; //contagem total de pulos

    public bool isJumping;

    private Rigidbody2D rig;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>(); //pegando a "mascara" de colisão
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        Vector3 move = new Vector3 (Input.GetAxis("Horizontal"), 0, 0); //criando um vetor para pegar o valor de input horzontal que o player vai colocar (1 se ele apertar para a direite e 0 para a esquerda)
        transform.position += move * Time.deltaTime * speed; //Transformando a posição atual do objeto pelo vetor criado anteroirmente junto a velocidade e o tempo media da untiy
    }

    void Jump()
    {
        if (jumpCont > jumpContT) //checa se os pulos atuias são maiores que os totais
        {
            jumpContT = jumpCont; 
        }


        if ((Input.GetButtonDown("Jump")) && (jumpCont > 0)) //se "espaço" for pressionado e ainda tiver pulo sobrando, entra
        {
            rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse); //joga o objeto para cima, adicionando a ele um vetor com y = força do pulo, e forçando um impulso ao modelo
            jumpCont--;
        }
    }
void OnCollisionEnter2D(Collision2D collision) //checa se o player ENTROU em colisão
    {
        if ( jumpCont < jumpContT) //se ele tiver gastado um pulo entra
        {
            jumpCont = jumpContT; //reseta a contagem de pulo
        }
    }
}
