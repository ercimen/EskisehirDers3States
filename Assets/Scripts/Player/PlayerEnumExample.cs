using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnumExample : MonoBehaviour
{

    public PlayerState CurrentPlayerState;

    public float Speed = 3;

    public bool IsGameStarted;

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

        if (GameManager.Instance.CurrentState ==GameState.Play)
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
                Move();
                Jump();
                break;
            case PlayerState.Jump:
                
                break;
            case PlayerState.Ondamage:
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
        Debug.Log("bekliyorum");
    }

    private void Move()
    {
        Debug.Log("hareket ediyorum");
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
            Debug.Log("zipladim");
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 10f, ForceMode.Impulse);
            CurrentPlayerState = PlayerState.Jump;
        }
    }
    private void TakeDamage()
    {
        Debug.Log("Hasar aldým");
    }
    private void Death()
    {
        Debug.Log("Ölüyüm ben ölüyüm.");
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
            CurrentPlayerState = PlayerState.Ondamage;
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
