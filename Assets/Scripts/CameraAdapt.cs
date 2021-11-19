using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdapt : MonoBehaviour
{

    Camera cam;
    float num;
    float width, height;
    float originalRes, actualRes;
    [SerializeField]
    float originalSize =  3.55f;
    [SerializeField]
    float originalWidth = 800f;
    [SerializeField]
    float originalHeight = 1280f;
    private void Awake()
    {
        cam = GetComponent<Camera>();
        width = Screen.width;
        height = Screen.height;


        originalRes = (originalWidth / originalHeight);
        actualRes = (width / height);

        num = originalRes / actualRes;

        print("NUM: " + num);
        cam.orthographicSize = num * originalSize;

        print("RESACTUAL: " + (width / height));
        print("RESORIGINAL: " + (800f / 1280f));
    }
}
