using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public enum Action
    { 
        None,
        Move,
        Jump,
        Attack,
        Power
    }

    public Action type { get; set; }

}
