using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum Mode
{
    normalMode,

    FireMode,
}

public class Ball : MonoBehaviour
{
    private Mode ballMode;

    private int powerCount = 0;

    private bool fireMode = false;

    private MeshRenderer meshRenderer;

    private CanvasManager canvasManager;

    private GameManager gameManager;

    private AudioManager audioManager;

    private PrefabPool prefabPool;

    private TrailRenderer trailRenderer;
   
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float jump;

    [SerializeField] private RingType ringType;
    
    [SerializeField] private Transform ballTransform;

    [SerializeField] private GameObject splashPrefab;

    [SerializeField] private ParticleSystem particlePrefab;

    [SerializeField] private ParticleSystem FireModePartical;

    [SerializeField] private Material ballMaterial;

    [SerializeField] private Material fireMaterial;

    [SerializeField] private Material fireTrail;

    [SerializeField] private Material ballTrail; 

    public static Ball Instance { get; set; }

    public Mode BallMode { get => ballMode; set => ballMode = value; }

    private Queue<GameObject> Splashs = new Queue<GameObject>();

    //public Animator ballAnim; SİLME (rapor için)

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        audioManager = AudioManager.Instance;

        canvasManager = CanvasManager.Instance;

        gameManager = GameManager.Instance;

        prefabPool = PrefabPool.Instance;

        particlePrefab.Stop();

        meshRenderer = GetComponent<MeshRenderer>();

        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(StringVariables.ADDSCORE_TAG))
        {
            powerCount++;
            if (powerCount >= 3)
            {
                fireMode = true;
                if (powerCount % 3 == 0)
                    AddExtraScore();
            }
            else
                fireMode = false;

            switch (fireMode)
            {
                case true:

                    SetMode(Mode.FireMode);

                    break;

                case false:

                    SetMode(Mode.normalMode);

                    break;
            }

            gameManager.NumOfPassedRings++;

            audioManager.PlayAudio(AudioType.Pass);

            GameObject mainRing = other.transform.parent.gameObject;

            mainRing.GetComponent<RingPiecer>().Breaker(300);

            Destroy(mainRing.transform.parent.parent.gameObject, 0.5f);

            canvasManager.Score += 20;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        particlePrefab.Play();

        //ballAnim.SetBool("Touch", true); buraları raporda animasyon kısmında koymak gerektiği için silme!!

        //Invoke("ResetAnim", 0.5f);

        if (collision.transform.parent != null)
        {
            GameObject mainRing = collision.transform.parent.gameObject;
            switch (collision.collider.tag)
            {
                case "Safe":

                    rb.velocity = new Vector3(rb.velocity.x, jump + powerCount * 0.3f, rb.velocity.z);

                    powerCount = 0;

                    ballTransform.DOScale(new Vector3(0.38f, 0.3f, 0.22f), 0.1f).SetLoops(2, LoopType.Yoyo);

                    /*
                    GameObject splash = Instantiate(splashPrefab, transform.position + new Vector3(0, -0.1f, 0), transform.rotation);
                    
                    splash.transform.SetParent(collision.gameObject.transform);
                    
                    Destroy(splash, 1f); */

                    GameObject splash = prefabPool.GetSplashFromPool(collision);

                    Splashs.Enqueue(splash);

                    if (prefabPool.PooledObject.Count == 0) return;

                    StartCoroutine(prefabPool.SetPool(splash));

                   // GameObject usedSplash = Splashs.Dequeue();

                   // rb.velocity = new Vector3(rb.velocity.x, jump, rb.velocity.z);

                    if (fireMode)
                    {
                        for (int i = 0; i < mainRing.transform.childCount; i++)
                        {
                            mainRing.transform.GetChild(i).GetComponent<MeshRenderer>().material.color = Color.red;
                        }

                        SetMode(Mode.normalMode);

                        audioManager.PlayAudio(AudioType.Boom);

                        mainRing.GetComponent<RingPiecer>().Breaker(500);

                        canvasManager.Score += 20;

                        Destroy(mainRing.transform.parent.parent.gameObject, 0.5f);


                        fireMode = false;

                        gameManager.NumOfPassedRings++;
                    }
                    else
                        if (collision.transform.parent != null)
                        audioManager.PlayAudio(AudioType.Bounce);

                    break;

                case "Unsafe":

                    rb.velocity = new Vector3(rb.velocity.x, jump + powerCount * 0.3f, rb.velocity.z);
                    powerCount = 0;
                    /*
                    meshRenderer.material = ballMaterial;

                    gameObject.GetComponent<TrailRenderer>().material = ballTrail; */

                    if (fireMode)
                    {
                        for (int i = 0; i < mainRing.transform.childCount; i++)
                        {
                            mainRing.transform.GetChild(i).GetComponent<MeshRenderer>().material.color = Color.red;
                        }

                        SetMode(Mode.normalMode);

                        audioManager.PlayAudio(AudioType.Boom);

                        mainRing.GetComponent<RingPiecer>().Breaker(500);

                        canvasManager.Score += 20;

                        Destroy(mainRing.transform.parent.parent.gameObject, 0.5f);

                        fireMode = false;

                        gameManager.NumOfPassedRings++;
                    }
                    else
                        gameManager.GameOver = true;
                    break;
            }
        }
        if (collision.collider.CompareTag(StringVariables.FINISH_TAG))
        {
            gameManager.LevelCompleted = true;
        }

        /*
        if (collision.collider.tag == "Safe")
        {               
            //rb.AddForce(Vector3.up * jump);
            powerCount =0;

            meshRenderer.material = ballColor;

            rb.velocity = new Vector3(rb.velocity.x, jump, rb.velocity.z);
           

            if (fireMode == true)
            {
                mainRing.GetComponent<RingPiecer>().Breaker(fireMode);
                Destroy(mainRing.transform.parent.parent.gameObject, 0.5f);
                fireMode = false;
            }
        }

        if (collision.collider.tag == "Unsafe")
        {
            
            GameManager.gameOver = true;
        }
        if (collision.collider.tag == "Finish")
        {
            
            GameManager.levelCompleted = true;
        }
        */


    }
    void ResetAnim()
    {
        //ballAnim.SetBool("Touch", false);
    }

    public void AddExtraScore()
    {
        canvasManager.Score += 20;
    }

    public void SetMode(Mode ballMode)
    {
        switch (ballMode)
        {
            case Mode.normalMode:

                FireModePartical.gameObject.SetActive(false);

                meshRenderer.material = ballMaterial;

                trailRenderer.enabled = true;

                trailRenderer.material = ballTrail;

                transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.2f);

                break;

            case Mode.FireMode:

                FireModePartical.gameObject.SetActive(true);

                meshRenderer.material = fireMaterial;

                trailRenderer.material = fireTrail;

                transform.DOScale(new Vector3(0.4f, 0.4f, 0.4f), 0.2f);

                break;


        }
    }


}
