using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider energyBar;
    public int maxEnergy = 500;
    private int currentEnergy;

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
        currentEnergy = maxEnergy;
        energyBar.maxValue = maxEnergy;
        energyBar.value = maxEnergy;
    }

    public void UseEnergy(int amount)
    {
        if(currentEnergy - amount >= 0)
        {
            currentEnergy -= amount;
            energyBar.value = currentEnergy;
        }
    }
}
