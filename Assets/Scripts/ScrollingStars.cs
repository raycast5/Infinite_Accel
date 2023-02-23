using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingStars : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private Material material;
    private Vector2 offset;
    public float scrollSpeed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        material = _meshRenderer.material;
        offset = material.mainTextureOffset;
    }

    // Update is called once per frame
    void Update()
    {
        offset.x += Time.deltaTime / scrollSpeed;
        material.mainTextureOffset = offset;
    }
}
