using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource gameOverSound;
    [SerializeField] AudioSource explosionSound;
    [SerializeField] AudioSource energyUp;
    public EnergyBar energy;
    public ParticleSystem explosion;
    public Player player;
    public float respawnTime = 3.0f;
    public float ghostTime = 3.0f;
    public float highscore = 0;
    public float score { get; private set; }
    public Text scoreText;
    public Text highscoreText;
    public int lives { get; private set; }
    public Text livesText;
    public GameObject GameOverUI;
    Vector3  startPos = new Vector3(-6, 0, 0);

    void Start()
    {
        highscore = PlayerPrefs.GetFloat("highScore");
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
        bgm.Play();

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
        EnergyBar.instance.ResetEnergy();
        SetLives(3);
        Respawn();
    }

    public void PlayerDied(bool energyDepl)
    {
        this.explosion.transform.position = this.player.transform.position;
        explosionSound.Play();
        this.explosion.Play();
        if (energyDepl == true)
        {
            SetLives(0);
        }
        else
        {
            SetLives(this.lives -1);
        }
        if (this.lives <= 0)
        {
            GameOver();
            bgm.Stop();
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
        energyUp.Play();

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
        if (score > highscore)
        {
            PlayerPrefs.SetFloat("highScore", score);
            highscore = score;
        }
        highscoreText.text = ("High Score: " + highscore.ToString());
        gameOverSound.Play();
        GameOverUI.SetActive(true);
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = lives.ToString();
    }
}
