using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OyunKontrol : MonoBehaviour
{
    public GameObject zombi;
    private float zamanSayaci;
    private float olusumSureci = 1f;
    public Text puanText;
    private int puan;

    // Start is called before the first frame update
    void Start()
    {
        zamanSayaci = olusumSureci;
    }

    // Update is called once per frame
    void Update()
    {
        zamanSayaci -= Time.deltaTime;
        if (zamanSayaci < 0)
        {
            Vector3 pos = new Vector3(Random.Range(-1125f, -1528f), 56f, Random.Range(-458f, -100f));

            // Zombi instantiate ediliyor
            GameObject yeniZombi = Instantiate(zombi, pos, Quaternion.identity);

            if (yeniZombi != null)
            {
                // Nesne baþarýyla instantiate edildi.
                zamanSayaci = olusumSureci;
            }
            else
            {
                // Nesne instantiate edilemedi, bir hata oluþtu.
                Debug.LogError("Zombi instantiate edilemedi!");
            }
        }
    }

    public void PuanArtir(int p)
    {
        puan += p;
        puanText.text = "" + puan;
    }

    public void OyunBitti()
    {
        PlayerPrefs.SetInt("puan", puan);
        SceneManager.LoadScene("OyunBitti");
    }
}
