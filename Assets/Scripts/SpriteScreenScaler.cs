using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScreenScaler : MonoBehaviour
{
    public float customScale = 245f;
    void Start()
    {
        float width = SpriteScreenSize.GetScreenToWorldWidth;
        transform.localScale = new Vector3(customScale, customScale, 1) * width;
    }
}
