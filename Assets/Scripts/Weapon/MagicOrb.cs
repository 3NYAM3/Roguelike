using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicOrb : MonoBehaviour
{
    [SerializeField] private float detectionRange;

    public float DetectionRange
    {
        get { return detectionRange; }
        private set { detectionRange = value; }
    }

}
