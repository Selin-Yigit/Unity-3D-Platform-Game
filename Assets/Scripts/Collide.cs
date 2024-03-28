using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collide : MonoBehaviour
{
    private int heart = 3;
    [SerializeField] GameObject[] _heartUI;
    [SerializeField] GameObject _gameOver;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            heart--;
            _heartUI[heart].gameObject.SetActive(false);
            if(heart == 0)
            {
                _gameOver.SetActive(true);
                StartCoroutine(Fade());
            }
        }
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(1.6f);
        // time scale 0 oldu�u zaman hi�bir logic hesaplanmaz. Yani oyunu durmas�n� sa�lad�k.
        Time.timeScale = 0;
    }
}
