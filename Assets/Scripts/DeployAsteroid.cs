using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployAsteroid : MonoBehaviour
{
    public int energyInterval = 66;
    private int activations = 0;
    public Energy EnergyPrefab;
    public Asteroid AsteroidPrefab;
    public float respawnTime = 0.4f;
    private Vector2 screenBounds;
    public float astPosition = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));      // Sets Screen Bounds
        StartCoroutine(asteroidWave());         // Starts Co-Routine
    }

    private void spawnAsteroid()
    {
        float variance = Random.Range(-60.0f, 60.0f);       // Variance of the angle of rotation of Asteroid Sprite.
        Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);       // Quaternion that controls the angle axis of rotation using variance
        Vector3 position = new Vector3(screenBounds.x * astPosition, Random.Range(-screenBounds.y, screenBounds.y), 0.0f); // Spawn position of asteroid at random point along Y axis.
        Asteroid ast = Instantiate(this.AsteroidPrefab, position, rotation);        // Instantiation of Asteroid prefab  using previous parameters.
        ast.size = Random.Range(ast.minSize, ast.maxSize);      //Randomization of Asteroid size constrained by given range.
    }

    private void spawnEnergy()
    {
        float variance = Random.Range(-60.0f, 60.0f);
        Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
        Vector3 position = new Vector3(screenBounds.x * astPosition, Random.Range(-screenBounds.y, screenBounds.y), 0.0f);
        Energy ast = Instantiate(this.EnergyPrefab, position, rotation);
        ast.size = Random.Range(ast.minSize, ast.maxSize);
    }

    IEnumerator asteroidWave()      // Co-Routine to control Asteroid spawn  
    {
        while(true)
        {
            yield return new WaitForSeconds(respawnTime);       // Waits for specified time to continue.
            if (activations < energyInterval)                   // Counts no of Asteroid spawns and launches energy on given intervals.
            {
                spawnAsteroid();
                activations ++;
            }                                                               
            else
            {
                spawnEnergy();
                activations = 0;
            }
        }
       
    }
}
