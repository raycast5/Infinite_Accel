using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D _rigidbody;
    public float speed = 5.0f;
    public int decreaseRate = 1;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += Vector3.up * this.speed * Time.deltaTime;
            EnergyBar.instance.UseEnergy(decreaseRate);

        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += Vector3.down * this.speed * Time.deltaTime;
            EnergyBar.instance.UseEnergy(decreaseRate);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = 0.0f;
            
            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDied();
        }
    }
}
