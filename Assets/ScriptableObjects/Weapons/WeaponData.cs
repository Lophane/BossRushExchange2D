using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon Data", order = 51)]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public GameObject weaponPrefab;
    public AnimationClip attackAnimation;
    public Vector2 hitboxSize;
    public Vector2 hitboxOffset;
    public float hitboxStart;
    public float hitboxEnd;
    public int damage;
    public float effectiveRange;
    public float attackSpeed;
    public float armHealth;
}
