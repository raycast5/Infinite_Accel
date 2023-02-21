using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public ParticleSystem explosion;
    public Player player;
    public float respawnTime = 3.0f;
    public float ghostTime = 3.0f;
    public int score { get; private set; }
    public Text scoreText;
    public int lives { get; private set; }
    public Text livesText;
    public GameObject GameOverUI;
    Vector3  startPos = new Vector3(-6, 0, 0);

    void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (lives <= 0 && Input.GetKeyDown(KeyCode.Return))
        {
            NewGame();
        }
    }
    private void FixedUpdate()
    {
        if (score < int.MaxValue && GameOverUI.activeSelf == false)
        {
            score += 1;
            scoreText.text = score.ToString();
        }
    }
    
    public void NewGame()
    {
        Asteroid[] asteroids = FindObjectsOfType<Asteroid>();
        Energy energies = FindObjectOfType<Energy>();

        for (int i = 0; i < asteroids.Length; i++)
        {
            Destroy(asteroids[i].gameObject);
        } 
        if (energies != null)
        {
            Destroy(energies.gameObject);
        }
        GameOverUI.SetActive(false);

        this.score = 0;
        SetLives(3);
        Respawn();
    }

    public void PlayerDied()
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
        SetLives(this.lives -1);
        if (this.lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), this.respawnTime);
        }
    }

    public void AsteroidDestroyed(Energy energy)
    {
        this.explosion.transform.position = energy.transform.position;
        this.explosion.Play();

    }

    private void Respawn()
    {
        this.player.transform.position = startPos;
        this.player.gameObject.layer = LayerMask.NameToLayer("Ghost");
        this.player.gameObject.SetActive(true);
        Invoke(nameof(GhostOff), ghostTime);

    }

    private void GhostOff()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public void GameOver()
    {
        GameOverUI.SetActive(true);
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = lives.ToString();
    }
}
