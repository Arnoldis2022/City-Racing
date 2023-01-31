using UnityEngine;

[CreateAssetMenu(menuName = "Mega Racing/Music data")]
public class MusicData : ScriptableObject
{
    [SerializeField] private string _nameMusician;
    [SerializeField] private string _songTitle;
    [SerializeField] private AudioClip _audioclip;

    public string FullNameToString()
    {
        return _nameMusician + " - " + _songTitle;
    }
}
