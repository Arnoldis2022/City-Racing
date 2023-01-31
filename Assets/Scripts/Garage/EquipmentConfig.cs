using UnityEngine;

[CreateAssetMenu(menuName = "Mega Racing/Equipment config")]
public class EquipmentConfig : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _nameLocalizationKey;
    [SerializeField] private string _descriptionLocalizationKey;
    [SerializeField] private Sprite _previewSprite;
    [SerializeField] private int _openingLevel;
    [SerializeField] private int _openingPrice;

    public string Name => _name;
    public string NameLocalizationKey => _nameLocalizationKey;
    public string DescriptionLocalizationKey => _descriptionLocalizationKey;
    public Sprite PreviewSprite => _previewSprite;
    public int OpeningLevel => _openingLevel;
    public float OpeningPrice => _openingPrice;
}