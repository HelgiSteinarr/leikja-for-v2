using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawning : MonoBehaviour {

    // Public breytur svo þær eru breytanlegar innan editorsins
    public int psfb;                          // Stendur fyrir Points to Spawn From Beginning
    public int spawnDelay = 10;
    public Vector2 center = new Vector2(0,0); // Miðja mapsins sem er í notkun
    public Vector2 size = new Vector2(76,76); // Svæðið sem punktarnir eiga að spawna innan frá miðju
    public GameObject pointPrefab;
    public GameObject evilPointPrefab;


    private void Start () { 
		for (int i = 0; i < psfb; i++) // Spawnar öllum byrjunnar punktunum
        {
            SpawnPoint();
        }

        // Lætur punkta spawna endalaust hverjar "spawnDelay" sekúndur eftir að byrjunar punktarnir hafa spawnað 
        InvokeRepeating("SpawnPoint", spawnDelay, spawnDelay); 
	}

    // Spawnar punktunum
    private void SpawnPoint()
    {
        bool isEvil;
        if (Random.value > 0.8)
        {
            isEvil = true;
        }
        else
        {
            isEvil = false;
        }

        // Finnur random staðsetningu fyrir punktinn
        var spawnPos = center + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
        if (!isEvil)
        {
            Instantiate(pointPrefab, spawnPos, Quaternion.identity);  // Spawnar punktinum
        }else
        {
            Instantiate(evilPointPrefab, spawnPos, Quaternion.identity);  // Spawnar punktinum
        }
        
        Debug.Log("-- SPAWNED POINT --");
    }


    // Svo hægt er að sjá svæðið sem punktarnir spawna á þegar þú velur 
    // GameObjectið sem scriptan er bundin við í editornum
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);  // Litur svæðisins
        Gizmos.DrawCube(center, size); // Býr til svæðið notandi sömu breytur og er notað fyrir punktana
    }
}
