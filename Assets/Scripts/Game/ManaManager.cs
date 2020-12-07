using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour
{
    public Slider manaSlider;
    public PlayerInventory playerInventory;

    // Start is called before the first frame update
    private void Start()
    {
        manaSlider.maxValue = playerInventory.maxMana;
        manaSlider.value = playerInventory.maxMana;
    }

    public void AddMana(int amountToIncrease)
    {
        if (manaSlider.value + amountToIncrease > manaSlider.maxValue)   
        {
            manaSlider.value = manaSlider.maxValue;                   
        }
        else
        {
            manaSlider.value += amountToIncrease;                         
        }
    }

    public void DecreaseMana(int amountToDecrease)
    {
        if (manaSlider.value - amountToDecrease <= 0)   
        {
            manaSlider.value = 0;
        }
        else
        {
            manaSlider.value -= amountToDecrease;                         
        }
    }
}
