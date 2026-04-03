
using UnityEngine;


public class SlicingScript : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] commonSounds;
    public AudioClip errorSound;
    public AudioClip RareSound;

    public GameObject whole;
    public GameObject sliced;
    public int scoreValue;
    private Rigidbody cakeRigidbody;
    private Collider cakeCollider;
    private ParticleSystem dropsParticleEffect;
    public GameObject floatingTextPrefab;
    private bool isSliced = false;

    private void Awake()
    {
        cakeRigidbody = GetComponent<Rigidbody>();
        cakeCollider = GetComponent<Collider>();
        dropsParticleEffect = GetComponentInChildren<ParticleSystem>();
        audioSource = Camera.main.GetComponentInChildren<AudioSource>(); // Лучше для всех послудующих указывать так
    }


    private void Slice(Vector3 direction, Vector3 position, float force)
    {
        whole.SetActive(false);
        sliced.SetActive(true);

        cakeCollider.enabled = false;
        dropsParticleEffect.Play(); // партиклы

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0, 0, angle);

        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody slice in slices)
        {
            slice.linearVelocity = cakeRigidbody.linearVelocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }
    }
    private void PlaySound()
    {
        int index = Random.Range(0, commonSounds.Length);
        audioSource.PlayOneShot(commonSounds[index]);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isSliced) return;
        if (!other.CompareTag("Player")) return;

        isSliced = true;

        KnifeScript knife = other.GetComponent<KnifeScript>();
        Slice(knife.direction, knife.transform.position, knife.sliceForce);

        GameObject textObject = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);     

        PlaySound();

        if (CompareTag("Rotten"))
        {
            ScoreScript.Instance.TakeScore(10);
            StreakScript.BreakStreak();
            textObject.GetComponent<FloatingText>().SetText("- 1");
            DamageFlash.Instance.Flash();
            audioSource.PlayOneShot(errorSound);

        }
        else if (CompareTag("Chocolate"))
        {
            ScoreScript.Instance.AddChocolate(1);
            textObject.GetComponent<FloatingText>().SetText("+ 1");
            audioSource.PlayOneShot(RareSound);
        }
        else
        {
            ScoreScript.Instance.AddScore(scoreValue);
            StreakScript.AddStreak();
            textObject.GetComponent<FloatingText>().SetText("+ " + scoreValue.ToString());
        }

    }
}
