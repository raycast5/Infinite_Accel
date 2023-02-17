using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;
    public float size = 1.0f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    public float speed = 10.0f;
    public float turnSpeed = 1.0f;
    private Rigidbody2D _rigidbody;
    private Vector2 screenBounds;
    
    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody= GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        this.transform.localScale = Vector3.one * this.size;
        _rigidbody.mass = this.size;
        _rigidbody.velocity = new Vector2(-speed, 0);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < screenBounds.x - 20)
        {
            Destroy(this.gameObject);
        }
    }
}
