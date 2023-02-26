using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Player player;
    public Slider energyBar;
    public int maxEnergy = 2000;
    private int increaseAmount;
    private int currentEnergy;
    public int decreaseRate = 1;

    public static EnergyBar instance;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        increaseAmount = maxEnergy / 4;
        currentEnergy = maxEnergy;
        energyBar.maxValue = maxEnergy;
        energyBar.value = maxEnergy;
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            UseEnergy(decreaseRate);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            UseEnergy(decreaseRate);
        }
    }

    public void UseEnergy(int amount)
    {
        if(currentEnergy - amount >= 0)
        {
            currentEnergy -= amount;
            energyBar.value = currentEnergy;
        }
        if (energyBar.value <= 0)
        {
            this.player.gameObject.SetActive(false);
            FindObjectOfType<GameManager>().PlayerDied(true);
        }
    }

    public void AddEnergy()
    {
        if (currentEnergy + increaseAmount <= maxEnergy)
        {
            currentEnergy += increaseAmount;
            energyBar.value = currentEnergy;
        } 
        else if (currentEnergy + increaseAmount > maxEnergy)
        {
            energyBar.value = maxEnergy;
        }
    }

    public void ResetEnergy()
    {
        currentEnergy = maxEnergy;
        energyBar.value = maxEnergy;
    }
}
