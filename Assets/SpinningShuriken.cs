using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningShuriken : MonoBehaviour
{
    public float rotationSpeed = 100f; // Tốc độ xoay của shuriken

    // Update is called once per frame
    void Update()
    {
        // Xoay sprite theo trục Z (hướng lên trên) với tốc độ và thời gian từ Time.deltaTime
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
