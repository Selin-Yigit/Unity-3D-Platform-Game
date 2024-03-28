using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using SpawnCoinsNM;
using TMPro;

public class Collection : MonoBehaviour
{
    private SpawnCoin _spawner;
    [SerializeField] private GameObject _gameObj;
    [SerializeField] private TMP_Text _text;
    private int count = 0;
    private AudioSource audio;
    private int totalSpawnPoints;
    int newNumber;
    private void Start(){
        audio = GetComponent<AudioSource>();
        //_gameObj = GameObject.Find("Spawner");
        _spawner = _gameObj.GetComponent(typeof(SpawnCoin)) as SpawnCoin;
        totalSpawnPoints = _spawner.spawnPoints.Count;
    }
   private void OnTriggerEnter(Collider other){
    if (other.gameObject.CompareTag("Coin"))
    {
        //Destroy(other.gameObject);
        count++;
        _text.text = count.ToString();
        other.gameObject.SetActive(false);
        audio.Play();
        StartCoroutine(Spawn(other.gameObject));
    }
   }

    IEnumerator Spawn(GameObject gameObject)
    {
        
        // 3 saniye sonra aktif state dönülecek.
        int index =_spawner.spawnPoints.IndexOf(gameObject.transform.parent.transform);
        _spawner.randomValues.Remove(index);
        yield return new WaitForSeconds(3);
        
        while (_spawner.randomValues.Count < Mathf.CeilToInt(totalSpawnPoints/2.0f))
        {
            newNumber = _spawner.r.Next(0, _spawner.spawnPoints.Count - 1);
            _spawner.randomValues.Add(newNumber);
        }
        gameObject.transform.position = _spawner.spawnPoints[newNumber].transform.position;
        Debug.Log(_spawner.spawnPoints[newNumber]);
        gameObject.SetActive(true);
    }
}
