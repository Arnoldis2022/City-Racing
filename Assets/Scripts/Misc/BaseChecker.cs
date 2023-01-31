using UnityEngine;

public abstract class BaseChecker : MonoBehaviour
{
    public abstract void Init(Player player);
    public abstract void Check();
}
