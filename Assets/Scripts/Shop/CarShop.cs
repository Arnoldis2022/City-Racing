using UnityEngine;
using UnityEngine.UI;

public class CarShop : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameDataBase _carsDataBase;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Text _priceText;
    [SerializeField] private Button _selectButton;
    [SerializeField] private InfoWindow _infoWindow;
    [SerializeField] private CarInfo _carInfo;
    private Car[] _carPool;
    private int _carIndex = 0;

    private void OnEnable()
    {
        var cars = _carsDataBase.Cars;
        if (_carPool == null)
        {
            _carIndex = cars.IndexOf(_player.Car.Config);
            _carPool = new Car[cars.Count];
            _carPool[_carIndex] = _player.Car;
        }
        _carPool[_carIndex].gameObject.SetActive(false);
        _carIndex = cars.IndexOf(_player.Car.Config);
        _carPool[_carIndex].gameObject.SetActive(true);
        _carInfo.Init(cars[_carIndex]);
        EnableButtons(cars[_carIndex].name);
    }

    private void OnDisable()
    {
        _carPool[_carIndex].gameObject?.SetActive(false);
        _player.Car.gameObject.SetActive(true);
    }

    public void PreviousCar()
    {
        _carPool[_carIndex].gameObject.SetActive(false);
        _carIndex--;
        if (_carIndex < 0)
        {
            _carIndex = _carPool.Length - 1;
        }
        TryEnableCar();
        var cars = _carsDataBase.Cars;
        _carInfo.Init(cars[_carIndex]);
        EnableButtons(cars[_carIndex].name);
    }

    public void NextCar()
    {
        _carPool[_carIndex].gameObject.SetActive(false);
        _carIndex++;
        if (_carIndex >= _carPool.Length)
        {
            _carIndex = 0;
        }
        TryEnableCar();
        var cars = _carsDataBase.Cars;
        _carInfo.Init(cars[_carIndex]);
        EnableButtons(cars[_carIndex].name);
    }

    private void TryEnableCar()
    {
        try
        {
            _carPool[_carIndex].gameObject.SetActive(true);
        }
        catch
        {
            var cars = _carsDataBase.Cars;
            var carConfig = cars[_carIndex];
            var playerTransform = _player.transform;
            var car = Instantiate(carConfig.CarPrefab, playerTransform.position, playerTransform.rotation, playerTransform);
            var carData = Game.Instance.GetCarData(carConfig);
            if(carData is null)
                carData = new CarData(carConfig);
            car.ConfigureCar(carData);
            car.KillEngine();
            car.CarController.canControl = false;
            _carPool[_carIndex] = car;
        }
    }

    private void EnableButtons(string carID)
    {
        if (_player.PurchasedCarsID.IndexOf(carID) == -1)
        {
            _buyButton.gameObject.SetActive(true);
            _selectButton.gameObject.SetActive(false);
            _priceText.text = _carsDataBase.Cars[_carIndex].Price.ToString();
        }
        else if(carID != _player.CarID)
        {
            _buyButton.gameObject.SetActive(false);
            _selectButton.gameObject.SetActive(true);
        }
        else
        {
            _buyButton.gameObject.SetActive(false);
            _selectButton.gameObject.SetActive(false);
        }
    }

    public void Buy()
    {
        var carConfig = _carsDataBase.Cars[_carIndex];
        var carPrice = carConfig.Price;
        bool isPurchaised = _player.TryDecreaseCredits(carPrice);
        Debug.Log(carPrice);
        if(isPurchaised)
        {
            _buyButton.gameObject.SetActive(false);
            _player.SetCar(_carPool[_carIndex]);
            Game.Instance.UpdateCarData(_carPool[_carIndex]);
            Game.Instance.UpdatePlayerData(_player);
        }
        else
        {
            var localization = Game.Instance.Localization;
            var message = localization.GetText("{ui_error_price}");
            _infoWindow.OpenInfoWindow(message);
        }
    }

    public void Select()
    {
        _selectButton.gameObject.SetActive(false);
        _player.SetCar(_carPool[_carIndex]);
        Game.Instance.UpdatePlayerData(_player);
    }
}
