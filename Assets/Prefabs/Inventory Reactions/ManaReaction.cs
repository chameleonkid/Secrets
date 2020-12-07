using UnityEngine;

public class ManaReaction : MonoBehaviour
{
    public ManaManager manaManager;

    public void UseMana(int amountToIncrease)
    {
        manaManager.AddMana(amountToIncrease);
    }
}
