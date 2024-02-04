using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public List<WeaponData> allWeapons; // Assuming this contains all possible weapon types
    public List<WeaponData> equippedWeapons; // Currently equipped weapons
    public GameObject leftArmAttachmentPoint;
    public GameObject rightArmAttachmentPoint;
    public KeyCode pickupKey = KeyCode.F;
    public List<GameObject> bloodSplatterPrefabs;
    public bool canPickupWeapon = false;

    private EnemyDeathCache currentEnemyCache;
    private GameObject enemyObject;
    public PlayerHitboxController pHitbox;


    public bool lastAttack0 = true;


    void Awake()
    {
        //pHitbox = GetComponent<PlayerHitboxController>();
        EquipWeapon(allWeapons[8], leftArmAttachmentPoint);
        EquipWeapon(allWeapons[8], rightArmAttachmentPoint);

    }

    void Update()
    {

        if (!Input.GetKey(pickupKey))
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastAttack0 = true;
                Attack1();
            }

            if (Input.GetMouseButtonDown(1))
            {
                lastAttack0 = false;
                Attack2();
            }
        }

        if (canPickupWeapon && currentEnemyCache != null && Input.GetKey(pickupKey))
        {

            //Debug.Log("Ready to Pickup");

            //Debug.Log(currentEnemyCache.GetCachedWeapons().Count + "and" + equippedWeapons.Count);

            if (Input.GetMouseButtonDown(0))
            {
                SwapAndDropWeapon(0);
                pHitbox.UpdateHitbox(0);
                DeleteWeaponAtAttachPoint(enemyObject, "LeftArmAttachPoint");
            }
            else if (Input.GetMouseButtonDown(1))
            {
                SwapAndDropWeapon(1);
                pHitbox.UpdateHitbox(1);
                DeleteWeaponAtAttachPoint(enemyObject, "RightArmAttachPoint");
            }
        }
    }

    void Attack1()
    {
        //animator.Play(equippedWeapons[0].attackAnimation.name);
        StartCoroutine(EnableHitboxAfterDelay(0, equippedWeapons[0].hitboxStart));
        StartCoroutine(DisableHitboxAfterDelay(0, equippedWeapons[0].hitboxEnd));

    }

    void Attack2()
    {
        //animator.Play(equippedWeapons[1].attackAnimation.name);
        StartCoroutine(EnableHitboxAfterDelay(1, equippedWeapons[1].hitboxStart));
        StartCoroutine(DisableHitboxAfterDelay(1, equippedWeapons[1].hitboxEnd));

    }

    void SwapAndDropWeapon(int weaponIndex)
    {

        if (currentEnemyCache.GetCachedWeapons().Count > weaponIndex && equippedWeapons.Count > weaponIndex)
        {

            WeaponData weaponToPickUp = currentEnemyCache.GetCachedWeapons()[weaponIndex];
            WeaponData weaponToDrop = equippedWeapons[weaponIndex];

            if (weaponToPickUp.weaponName != weaponToDrop.weaponName)
            {

                Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                Instantiate(weaponToDrop.weaponPrefab, transform.position, randomRotation);

                if (bloodSplatterPrefabs.Count > 0)
                {
                    GameObject randomBloodSplatter = bloodSplatterPrefabs[Random.Range(0, bloodSplatterPrefabs.Count)];
                    Quaternion bloodSplatterRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                    Instantiate(randomBloodSplatter, transform.position, bloodSplatterRotation);
                }

                // Update the equipped weapon list with the new weapon
                equippedWeapons[weaponIndex] = weaponToPickUp;

                // Update the weapon attachment point
                GameObject attachmentPoint = weaponIndex == 0 ? leftArmAttachmentPoint : rightArmAttachmentPoint;
                EquipWeapon(weaponToPickUp, attachmentPoint);

            }
        }
    }

    private void EquipWeapon(WeaponData weaponData, GameObject attachmentPoint)
    {
        // Destroy the current weapon GameObject if one exists
        foreach (Transform child in attachmentPoint.transform)
        {
            Destroy(child.gameObject);
        }

        // Instantiate the new weapon and set it as a child of the attachment point
        if (weaponData != null && weaponData.weaponPrefab != null)
        {
            GameObject weaponObj = Instantiate(weaponData.weaponPrefab, attachmentPoint.transform);
            weaponObj.transform.localPosition = Vector3.zero;
            equippedWeapons.Add(weaponData);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Corpse") && currentEnemyCache == null)
        {
            enemyObject = other.gameObject;
            currentEnemyCache = other.GetComponent<EnemyDeathCache>();
            if (currentEnemyCache != null)
            {
                canPickupWeapon = true;
                //Debug.Log("I can pick up: " + currentEnemyCache.GetCachedWeapons()[0] + " and " + currentEnemyCache.GetCachedWeapons()[1]);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<EnemyDeathCache>() == currentEnemyCache)
        {
            enemyObject = null;
            canPickupWeapon = false;
            currentEnemyCache = null;
            //Debug.Log("I moved too far away to pickup anything");
        }
    }

    void DeleteWeaponAtAttachPoint(GameObject enemy, string attachPointName)
    {
        Transform attachPoint = enemy.transform.Find(attachPointName);
        //Debug.Log("Deleting" + attachPoint + " " + enemy);

        if (attachPoint != null && attachPoint.childCount > 0)
        {
            Destroy(attachPoint.GetChild(0).gameObject);
        }
    }

    IEnumerator EnableHitboxAfterDelay(int index, float delay)
    {
        yield return new WaitForSeconds(delay);
        pHitbox.EnableHitbox(index);
    }

    IEnumerator DisableHitboxAfterDelay(int index, float delay)
    {
        yield return new WaitForSeconds(delay);
        pHitbox.DisableHitbox(index);
    }

    

}
