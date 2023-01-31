
using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        public bool isFirstSession = true;
        public string language = "ru";
        public bool feedbackDone;
        public bool promptDone;
        public int graphicsQualityIndex = 2;
        public float soundEffectsVolume = 0.5f;
        public float musicVolume = 0.8f;

        public List<CarData> CarsData;
        public List<RaceData> RacesData;
        public PlayerData PlayerData;
        public GarageData GarageData;
    }
}
