using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController Instance;

    public CharacterController charController;

    private void Awake()
    {
        Instance = this;
    }

}
