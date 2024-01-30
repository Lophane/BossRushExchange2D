[System.Serializable]
public class Weapon
{
    public enum WeaponType { Crab, Human }
    public WeaponType type;
    public float effectiveRange;

    // Constructor
    public Weapon(WeaponType type, float effectiveRange)
    {
        this.type = type;
        this.effectiveRange = effectiveRange;
    }
}
