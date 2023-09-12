
using UnityEngine;
using UnityEngine.EventSystems;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject hitEffect;
    private float fireTime = 2f;
    public GameObject logicHandler;
    LogicHandler logic;

    public AudioSource shootSound;
    public AudioSource hitSound;
    // Start is called before the first frame update
    private void Awake()
    {
        logic = logicHandler.GetComponent<LogicHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetButton("Fire1") && Time.time >= fireTime)
        {
            fireTime = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        shootSound.Play();
        muzzleFlash.Play();
        RaycastHit hit;
        
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range) )
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.TakeDamage(damage);
                hitSound.Play();
                GameObject impactObject = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactObject, 1f);
            }
        }
    }
}
