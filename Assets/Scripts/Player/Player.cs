using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// Kötü kod örneðidir. Daha sonra hatalar giderileceði için mümkün olduðunda kural ihlali vardýr.
    /// </summary>
    public float Speed;
    public int Hp = 100;
    private bool _isMoving;
    private bool _isJumping;
    private bool _isGrounded = true;
    private bool _onDamage;
    private bool _isDead;

    private float _damageDelay = 0.2f;



    private void Awake()
    {

    }
    private void Update()
    {

        //if (_isDead)  Etkili bir if kullanýmý. 
        //{
        //    return;
        //}

        if (Input.GetKey(KeyCode.UpArrow) && !_isDead && !_isJumping)
        {
            _isMoving = true;
            transform.position += transform.forward * Speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow) && !_isDead && !_isJumping)
        {
            _isMoving = true;
            transform.position -= transform.forward * Speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && !_isDead && !_isJumping)
        {
            _isMoving = true;
            transform.position -= transform.right * Speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow) && !_isDead && !_isJumping)
        {
            _isMoving = true;
            transform.position += transform.right * Speed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded && !_onDamage && !_isDead && !_isJumping) // _onDamage ==false
        {
            Debug.Log("Space pressed");
            _isJumping = true;
            _isGrounded = false;
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 10f, ForceMode.Impulse);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
           _isGrounded = true;
            _isJumping = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Lava"))
        {
            _onDamage = true;
            TakeDamage();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Lava"))
        {
            _onDamage = false;

        }
    }

    private void TakeDamage()
    {
        _damageDelay -= Time.deltaTime;

        if (_damageDelay<=0)
        {
            Hp -= 1;
            Debug.Log(Hp);
            _damageDelay = 0.2f;

            if (Hp<=0)
            {
                _isDead = true;
                Hp = 0;
            }
        }
    }
}
