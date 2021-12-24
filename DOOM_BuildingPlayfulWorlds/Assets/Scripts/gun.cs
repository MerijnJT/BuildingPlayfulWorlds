using UnityEngine;
using System.Collections;

public class gun : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;
    public int shots = 1;
    public float rate = 1f;

    private bool mayShoot = true;

    public Camera fpsCam;

    public ParticleSystem muzzle;
    public GameObject impact;
    public GameObject blood;

    public AudioSource source;
    public AudioClip[] audioClips;


    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && mayShoot)
        {
            StartCoroutine("Shoot");
        }


    }

    public IEnumerator Shoot()
    {
        muzzle.Play();
        source.clip = audioClips[Random.Range(0, audioClips.Length)];
        source.PlayOneShot(source.clip);
        mayShoot = false;

        for (int i = 0; i < shots; i++)
        {
            

            Vector3 bloom = fpsCam.transform.position + fpsCam.transform.forward * 1000f;
            bloom += Random.Range(-70, 70) * fpsCam.transform.up;
            bloom += Random.Range(-70, 70) * fpsCam.transform.right;
            bloom -= fpsCam.transform.position;
            bloom.Normalize();


            RaycastHit hit;

            if (Physics.Raycast(fpsCam.transform.position, bloom, out hit, range))
            {
                Debug.Log(hit.transform.name);
                Enemy target = hit.transform.GetComponent<Enemy>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }

                

                if (hit.transform.tag == "Demon")
                {
                    GameObject wound = Instantiate(blood, hit.point, Quaternion.LookRotation(hit.normal));
                    wound.transform.LookAt(hit.point + hit.normal);
                    wound.transform.parent = hit.transform;
                } else
                {
                    Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
                }
                
            }
        }
        yield return new WaitForSeconds(rate);
        mayShoot = true;
    }
}
