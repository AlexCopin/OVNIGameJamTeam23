using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RPS_Sign : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    public List<Material> materials = new();
    
    public enum SignType
    {
        DogeSign = 0,
        LamastiSign = 1,
        EteRhumSign = 2,
        ShitSign = 3
    }
    public SignType typeSign;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material = materials[(int) typeSign];
    }
}
