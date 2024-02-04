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


    void Start()
    {
        EquipWeapon(allWeapons[8], leftArmAttachmentPoint);
        EquipWeapon(allWeapons[8], rightArmAttachmentPoint);
    }

    void Update()
    {
        if (canPickupWeapon && currentEnemyCache != null && Input.GetKey(pickupKey))
        {

            //Debug.Log("Ready to Pickup");

            //Debug.Log(currentEnemyCache.GetCachedWeapons().Count + "and" + equippedWeapons.Count);

            if (Input.GetMouseButtonDown(0))
            {
                SwapAndDropWeapon(0);
                DeleteWeaponAtAttachPoint(enemyObject, "LeftArmAttachPoint");
            }
            else if (Input.GetMouseButtonDown(1))
            {
                SwapAndDropWeapon(1);
                DeleteWeaponAtAttachPoint(enemyObject, "RightArmAttachPoint");
            }
        }
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
            else
            {
                // Optionally, provide feedback that the weapon cannot be swapped because it's of the same kind
                Debug.Log("Cannot swap for a weapon of the same kind.");
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
        if (other.gameObject.CompareTag("Enemy") && currentEnemyCache == null)
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
            Debug.Log("I moved too far away to pickup anything");
        }
    }

    void DeleteWeaponAtAttachPoint(GameObject enemy, string attachPointName)
    {
        Transform attachPoint = enemy.transform.Find(attachPointName);
        if (attachPoint != null && attachPoint.childCount > 0)
        {
            Destroy(attachPoint.GetChild(0).gameObject);
        }
    }


}





/*using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerWeaponController : MonoBehaviour
{
    
    
    
    
    
    public List<WeaponData> allWeapons;
    public List<WeaponData> equippedWeapons;
    public GameObject leftArmAttachmentPoint;
    public GameObject rightArmAttachmentPoint;
    public KeyCode pickupKey = KeyCode.F;

    private bool canPickupWeapon = false;
    public GameObject bloodSplatterPrefab;
    private EnemyDeathCache currentEnemyCache = null;

    void Start()
    {
        EquipWeapon(allWeapons[0], leftArmAttachmentPoint);
        EquipWeapon(allWeapons[0], rightArmAttachmentPoint);
    }

    void Update()
    {
        if (!Input.GetKey(pickupKey)) // Check if the F key is not being held down
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack1();
            }

            if (Input.GetMouseButtonDown(1))
            {
                Attack2();
            }
        }

        if (canPickupWeapon && currentEnemyCache != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SwapAndDropWeapon(0);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                SwapAndDropWeapon(1);
            }
        }
    }

    void Attack1()
    {
        Debug.Log("Attacking with left arm weapon: " + equippedWeapons[0].weaponName);
    }

    void Attack2()
    {
        Debug.Log("Attacking with right arm weapon: " + equippedWeapons[1].weaponName);
    }

    private void EquipWeapon(WeaponData weaponData, GameObject attachmentPoint)
    {
        if (weaponData != null && weaponData.weaponPrefab != null)
        {
            GameObject weaponObj = Instantiate(weaponData.weaponPrefab, attachmentPoint.transform);
            weaponObj.transform.localPosition = Vector3.zero;
            equippedWeapons.Add(weaponData);
        }
    }

    private void SwapWeapon(WeaponData weaponData, GameObject attachmentPoint)
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
        }
    }

    void SwapAndDropWeapon(int weaponIndex)
    {
        // Check if there's a weapon to pick up and if the player has a weapon equipped in the slot
        if (currentEnemyCache.GetCachedWeapons().Count > weaponIndex && equippedWeapons.Count > weaponIndex)
        {
            WeaponData weaponToPickUp = currentEnemyCache.GetCachedWeapons()[weaponIndex];
            WeaponData weaponToDrop = equippedWeapons[weaponIndex];

            // Drop the current weapon at the player's feet
            Instantiate(weaponToDrop.weaponPrefab, transform.position, Quaternion.identity);
            // Optionally, instantiate a blood splatter effect
            Instantiate(bloodSplatterPrefab, transform.position, Quaternion.identity);

            // Remove the old weapon and add the new one
            equippedWeapons[weaponIndex] = weaponToPickUp;

            // Update the weapon attachment point
            GameObject attachmentPoint = weaponIndex == 0 ? leftArmAttachmentPoint : rightArmAttachmentPoint;
            EquipWeapon(weaponToPickUp, attachmentPoint);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && currentEnemyCache == null)
        {
            currentEnemyCache = other.GetComponent<EnemyDeathCache>();
            if (currentEnemyCache != null)
            {
                canPickupWeapon = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<EnemyDeathCache>() == currentEnemyCache)
        {
            canPickupWeapon = false;
            currentEnemyCache = null;
        }
    }
    

}
*/
