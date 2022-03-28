using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class FloatingObject : MonoBehaviour
{
    public Transform[] floaters;

    public float underWaterDrag = 3f;
    public float underWaterAngularDrag = 1f;

    public float airDrag = 0;
    public float airAngularDrag = 0.05f;

    public float floatingPower = 15f;

    public float waveHeight = 0f;

    private Rigidbody rigidbody;

    private int floatersUnderwater;

    bool isUnderwater;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        floatersUnderwater = 0;

        for (int i = 0; i < floaters.Length; i++)
        {
            float underwaterDifference = floaters[i].position.y - waveHeight;

            if (underwaterDifference < 0)
            {
                rigidbody.AddForceAtPosition(Vector3.up * floatingPower * Mathf.Abs(underwaterDifference), floaters[i].position, ForceMode.Force);

                floatersUnderwater++;

                if (!isUnderwater)
                {
                    isUnderwater = true;
                    SwitchState();
                }
            }
        }

        if (isUnderwater && floatersUnderwater == 0)
        {
            isUnderwater = false;

            SwitchState();
        }
    }

    public void SwitchState()
    {
        if (isUnderwater)
        {
            rigidbody.drag = underWaterDrag;
            rigidbody.angularDrag = underWaterAngularDrag;
        }
        else
        {
            rigidbody.drag = airDrag;
            rigidbody.angularDrag= airAngularDrag;
        }
    }
}
