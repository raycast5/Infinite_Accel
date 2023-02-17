using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ParticleSystem explosion;
    public Player player;
    public int lives = 3;
    public float respawnTime = 3.0f;
    public float ghostTime = 3.0f;
    public int score = 0;
    Vector3  startPos = new Vector3(-6, 0, 0);

    public void PlayerDied()
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
        this.lives--;
        if (this.lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), this.respawnTime);
        }
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

    private void GameOver()
    {
        ///to do
    }
}
