using UnityEngine;

public class CarChecker : MonoBehaviour
{
    [SerializeField] private FuelChecker _fuelChecker;
    [SerializeField] private DriftChecker _driftChecker;
    // Start is called before the first frame update
    public void Init(Player player)
    {
        _fuelChecker.Init(player);
        _driftChecker.Init(player);
    }

    private void Update()
    {
        _fuelChecker.Check();
        _driftChecker.Check();
    }
}
