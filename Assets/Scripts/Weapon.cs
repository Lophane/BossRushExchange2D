using UnityEngine;

[System.Serializable]
public class Weapon : MonoBehaviour
{
    public string name;
    public GameObject weaponPrefab;
    public float damage;
    public float effectiveRange;
    public float attackSpeed;

    public Weapon(string name, GameObject weaponPrefab, float damage, float effectiveRange, float attackSpeed)
    {
        this.name = name;
        this.weaponPrefab = weaponPrefab;
        this.damage = damage;
        this.effectiveRange = effectiveRange;
        this.attackSpeed = attackSpeed;
    }
    /*
    public static Weapon CreateHumanWeapon()
    {
        GameObject humanWeaponPrefab = 
        return new Weapon("Human", humanWeaponPrefab, 10f, 2f, 1.5f);
    }

    public static Weapon CreateBearWeapon()
    {
        return new Weapon("Bear", weaponPrefab, 15f, 1f, 2f);
    }

    public static Weapon CreatePorcupineWeapon()
    {
        return new Weapon("Porcupine", weaponPrefab, 10f, 2f, 1.5f);
    }

    public static Weapon CreateMantisWeapon()
    {
        return new Weapon("Mantis", weaponPrefab, 15f, 1f, 2f);
    }
    public static Weapon CreateScorpionWeapon()
    {
        return new Weapon("Scorpion", weaponPrefab, 10f, 2f, 1.5f);
    }

    public static Weapon CreateOctopusWeapon()
    {
        return new Weapon("Octopus", weaponPrefab, 15f, 1f, 2f);
    }

    public static Weapon CreateLionfishWeapon()
    {
        return new Weapon("Lionfish", weaponPrefab, 10f, 2f, 1.5f);
    }

    public static Weapon CreateChickenWeapon()
    {
        return new Weapon("Chicken", weaponPrefab, 15f, 1f, 2f);
    }

    public static Weapon CreateBatWeapon()
    {
        return new Weapon("Bat", weaponPrefab, 15f, 1f, 2f);
    }
    */
}
