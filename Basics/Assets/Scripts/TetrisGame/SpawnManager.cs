using UnityEngine;

public class TetrisManager : MonoBehaviour
{
    [Header("Spawn")]
    public GameObject[] tetrisPrefabs;
    public float spawnAreaSize = 200f;
    public float spawnHeight = 200f;
    public float spawnInterval = 1.5f;

    [Header("Distruzione")]
    public float destroyY = -10f;

    [Header("Raccolta")]
    public string playerTag = "Player";

    private float spawnTimer;

    void Start()
    {
        spawnTimer = spawnInterval;
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            SpawnPiece();
            spawnTimer = spawnInterval;
        }
    }

    void SpawnPiece()
    {
        if (tetrisPrefabs == null || tetrisPrefabs.Length == 0)
        {
            Debug.LogWarning("Nessun prefab assegnato!");
            return;
        }

        float x = Random.Range(-spawnAreaSize / 2f, spawnAreaSize / 2f);
        float z = Random.Range(-spawnAreaSize / 2f, spawnAreaSize / 2f);
        Vector3 spawnPos = new Vector3(x, spawnHeight, z);

        int index = Random.Range(0, tetrisPrefabs.Length);
        GameObject piece = Instantiate(tetrisPrefabs[index], spawnPos, Quaternion.identity);

        Rigidbody rb = piece.GetComponent<Rigidbody>();
        if (rb == null)
            rb = piece.AddComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;

        Collider col = piece.GetComponent<Collider>();
        if (col == null)
            col = piece.AddComponent<BoxCollider>();
        col.isTrigger = false;

        DestroyBelowY destroyScript = piece.GetComponent<DestroyBelowY>();
        if (destroyScript == null)
        {
            destroyScript = piece.AddComponent<DestroyBelowY>();
            destroyScript.destroyY = destroyY;
        }

        Transform triggerChild = piece.transform.Find("CollectTrigger");
        if (triggerChild == null)
        {
            GameObject triggerObj = new GameObject("CollectTrigger");
            triggerObj.transform.parent = piece.transform;
            triggerObj.transform.localPosition = Vector3.zero;
            triggerObj.transform.localRotation = Quaternion.identity;

            SphereCollider triggerCol = triggerObj.AddComponent<SphereCollider>();
            triggerCol.isTrigger = true;
            triggerCol.radius = 1.5f;

            CollectTrigger collectTrigger = triggerObj.AddComponent<CollectTrigger>();
            collectTrigger.playerTag = playerTag;
            // Non assegno più manager perché la raccolta la gestisci altrove
        }
    }
}

public class DestroyBelowY : MonoBehaviour
{
    public float destroyY = -10f;

    void Update()
    {
        if (transform.position.y < destroyY)
            Destroy(gameObject);
    }
}

public class CollectTrigger : MonoBehaviour
{
    public string playerTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            // Ora la raccolta la gestisce lo script esterno TetrisCollector
            // Quindi chiamiamo quello (puoi modificare se vuoi passare un riferimento diretto)

            TetrisCollector collector = Object.FindFirstObjectByType<TetrisCollector>();
            if (collector != null)
            {
                collector.RaccogliPezzo();
            }

            Destroy(transform.parent.gameObject);
        }
    }
}
