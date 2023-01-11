using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerEnumExample : MonoBehaviour
{

    public PlayerState CurrentPlayerState;

    public float Speed = 3;

    public bool IsGameStarted;

    public int Hp = 10;

    private float _damageDelay = 0.2f;


    private void Awake()
    {
        CurrentPlayerState = PlayerState.Idle;

        Debug.Log(CurrentPlayerState);
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.Play)
        {
            return;
        }

        if (GameManager.Instance.CurrentState ==GameState.Play && !IsGameStarted)
        {
            CurrentPlayerState = PlayerState.Move;
            IsGameStarted = true;
        }

        StateCheck();
    }



    private void StateCheck()
    {

        switch (CurrentPlayerState)
        {
            case PlayerState.Idle:
                Idle();
                break;
            case PlayerState.Move:
                DeathCheck();
                Move();
                Jump();
                break;
            case PlayerState.Jump:
                
                break;
            case PlayerState.OnDamage:
                DeathCheck();
                TakeDamage();
                Move();
                break;
            case PlayerState.Dead:
                Death();
                break;
            default:
                break;
        }
    }

    private void Idle()
    {
    }

    private void Move()
    {


        if (Input.GetKey(KeyCode.UpArrow))
        {

            transform.position += transform.forward * Speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {

            transform.position -= transform.forward * Speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {

            transform.position -= transform.right * Speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {

            transform.position += transform.right * Speed * Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 10f, ForceMode.Impulse);
            CurrentPlayerState = PlayerState.Jump;
        }
    }
    private void TakeDamage()
    {

        _damageDelay -= Time.deltaTime;

        if (_damageDelay <= 0)
        {
            Hp -= 1;
            Debug.Log(Hp);
            _damageDelay = 0.2f;

            if (Hp <= 0)
            {
                Death();
            }
        }
    }
    private void Death()
    {

        Hp = 0;
        CurrentPlayerState = PlayerState.Dead;

        GameManager.Instance.CurrentState = GameState.Finish;
    }


    private void DeathCheck()
    {
        if (Hp<=0)
        {
            CurrentPlayerState = PlayerState.Dead;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground") && IsGameStarted)
        {
            CurrentPlayerState = PlayerState.Move;
        }


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Lava"))
        {
            CurrentPlayerState = PlayerState.OnDamage;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Lava"))
        {
            CurrentPlayerState = PlayerState.Move;
        }

    }
}
