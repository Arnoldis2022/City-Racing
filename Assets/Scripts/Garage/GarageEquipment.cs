using UnityEngine;

public abstract class GarageEquipment : MonoBehaviour
{
    [SerializeField] private EquipmentConfig _config;
    protected bool _isActive;

    public EquipmentConfig Config => _config;
    public bool IsActive => _isActive;

    public abstract void Init(GarageData garageData);

    public bool TryOpenEquipment(Player player)
    {
        if (player.Level >= _config.OpeningLevel)
        {
            var purchased = player.TryDecreaseCredits(_config.OpeningPrice);
            if (purchased)
            {
                gameObject.SetActive(true);
                _isActive = true;
                return true;
            }
            throw new PriceException();
        }

        throw new LevelException();
    }
}