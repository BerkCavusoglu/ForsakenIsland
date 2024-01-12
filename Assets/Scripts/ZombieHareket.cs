using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieHareket : MonoBehaviour
{
    public GameObject kalp;
    private GameObject oyuncu;
    private int zombieCan = 3;
    private int zombidenGelenPuan = 10;
    private float mesafe;
    private OyunKontrol oKontrol;
    private AudioSource aSource;
    private bool zombieOluyor = false;
    private NavMeshAgent navMeshAgent; // NavMeshAgent'i burada tanımlayın.

    void Start()
    {
        aSource = GetComponent<AudioSource>();
        oyuncu = GameObject.FindGameObjectWithTag("Player");

        if (oyuncu == null)
        {
            Debug.LogError("Oyuncu nesnesi bulunamadı! 'Oyuncu' objesinin sahnenizde doğru bir şekilde adlandırıldığından emin olun.");
            return;
        }

        oKontrol = GameObject.Find("_Scripts").GetComponent<OyunKontrol>();

        if (oKontrol == null)
        {
            Debug.LogError("OyunKontrol nesnesi bulunamadı! '_Scripts' objesinin sahnenizde doğru bir şekilde adlandırıldığından emin olun.");
            return;
        }

        // NavMeshAgent bileşenini burada alın.
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent bileşeni bulunamadı! Bu nesnenin üzerinde bu bileşen bulunmalıdır.");
            return;
        }
    }

    void Update()
    {
        if (oyuncu == null)
        {
            Debug.LogError("Oyuncu nesnesi null. Başlangıç metodunda doğru bir şekilde tanımlandığından emin olun.");
            return;
        }

        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent bileşeni null. Bu nesnenin üzerinde bu bileşen bulunmalıdır.");
            return;
        }

        // NavMeshAgent'i kullanmadan önce kontrol edin.
        if (navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.destination = oyuncu.transform.position;
            mesafe = Vector3.Distance(transform.position, oyuncu.transform.position);
        }

        if (mesafe < 10f)
        {
            if (!aSource.isPlaying)
                aSource.Play();

            if (!zombieOluyor)
                GetComponentInChildren<Animation>().Play("Zombie_Attack_01");
        }
        else
        {
            if (aSource.isPlaying)
                aSource.Stop();
        }
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.collider.gameObject.tag.Equals("mermi"))
        {
            Debug.Log("Çarpışma Gerçekleşti");
            zombieCan--;

            if (zombieCan == 0)
            {
                zombieOluyor = true;
                oKontrol.PuanArtir(zombidenGelenPuan);
                Instantiate(kalp, transform.position, Quaternion.identity);
                GetComponentInChildren<Animation>().Play("Zombie_Death_01");
                Destroy(this.gameObject, 1.667f);
            }
        }
    }
}
