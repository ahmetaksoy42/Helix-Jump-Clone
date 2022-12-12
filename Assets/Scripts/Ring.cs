using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RingType
{
    Unsafe,
    Safe,
    Trigger,
    Finish,
}
public class Ring : MonoBehaviour
{
    [SerializeField] private RingType ringType;

    private Rigidbody rb;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    public void Break(int x)
    {
        GetComponent<MeshCollider>().enabled = false;

        rb = gameObject.AddComponent<Rigidbody>();

        Vector3 a = (meshRenderer.bounds.center - transform.parent.position).normalized;

        //  rb.AddForce(a * 300+(Vector3.down * Random.Range(-250, 250)+(Vector3.forward* Random.Range(-250,250))));

        rb.AddForce((a * x) + (Vector3.down * 100));

        transform.SetParent(null);

        Destroy(gameObject, 0.5f);
    }

}
