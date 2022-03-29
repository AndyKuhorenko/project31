using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanManager : MonoBehaviour
{
    public float wavesHeight = 30f;
    public float wavesFrequency = 6f;
    public float wavesSpeed = 2f;

    public Transform ocean;

    Material oceanMat;

    Texture2D displacementTexture;


    void Start()
    {
        SetVariables();
    }

    private void SetVariables()
    {
        oceanMat = ocean.GetComponent<Renderer>().sharedMaterial;
        displacementTexture = (Texture2D)oceanMat.GetTexture("_WavesHeightTexture");
    }

    public float GetWaveHeightAtPos(Vector3 position)
    {
        float oceanPosY = ocean.position.y;

        return oceanPosY + displacementTexture.GetPixelBilinear(position.x * wavesFrequency / 100, position.z * wavesFrequency / 100 + Time.time * wavesSpeed / 100).g * wavesHeight / 100 * ocean.localScale.x;
    }

    void OnValidate()
    {
        if (!oceanMat) SetVariables();

        UpdateMaterial();
    }

    private void UpdateMaterial()
    {
        oceanMat.SetFloat("_WavesFrequency", wavesFrequency / 100);
        oceanMat.SetFloat("_WavesSpeed", wavesSpeed / 100);
        oceanMat.SetFloat("_WavesHeight", wavesHeight / 100);
    }
}
