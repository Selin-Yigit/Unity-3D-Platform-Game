using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MusicFilesNM;

public class Finish : MonoBehaviour
{

    private GameObject _music;
    private MusicFiles _musicFiles;
    [SerializeField] int listIndex;
    private void Start()
    {
        _music = GameObject.Find("AudioManager");
        _musicFiles = _music.GetComponent(typeof(MusicFiles)) as MusicFiles;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            //Destroy(other.gameObject);
            AudioSource.PlayClipAtPoint(_musicFiles.audioSources[listIndex], gameObject.transform.position);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
