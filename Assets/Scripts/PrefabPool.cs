using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabPool : MonoBehaviour
{
    [SerializeField] private GameObject splashPrefab;

    public static PrefabPool Instance { get; set; }

    public Queue<GameObject> PooledObject { get => pooledObject; set => pooledObject = value; }

    private Queue<GameObject> pooledObject = new Queue<GameObject>();

    private int PrefabCount = 10;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        for (int i = 0; i < PrefabCount; i++)
        {

            GameObject splash = Instantiate(splashPrefab, transform.position + new Vector3(0, -0.1f, 0), Quaternion.Euler(90, 0, 0));

            splash.SetActive(false);

            PooledObject.Enqueue(splash);
        }
    }
    public GameObject GetSplashFromPool(Collision other)
    {
        if (PooledObject.Count == 0)
            AddSizePool(5);

        GameObject obj = PooledObject.Dequeue();

        if (obj != null)
        {
            obj.SetActive(true);

            obj.transform.position = transform.position + new Vector3(0, -0.1f, 0);

            obj.transform.SetParent(other.gameObject.transform);
        }
        return obj;

    }
    public IEnumerator SetPool(GameObject usedPrefab)
    {
        yield return new WaitForSecondsRealtime(0.4f);

        PooledObject.Enqueue(usedPrefab);
        if (usedPrefab != null)
        {
            usedPrefab.SetActive(false);

            usedPrefab.transform.SetParent(null);
        }

    }

    public void AddSizePool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject splash = Instantiate(splashPrefab, transform.position + new Vector3(0, -0.1f, 0), transform.rotation);

            splash.SetActive(false);

            PooledObject.Enqueue(splash);
        }
    }

}
