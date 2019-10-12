using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager instance;
    public DamagePopup DamagePopUpPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Returns a reference to the damageprefab, so you dont have to
    /// set it manually
    /// </summary>
    public DamagePopup GetDamagePopUp()
    {
        return DamagePopUpPrefab;
    }



}
