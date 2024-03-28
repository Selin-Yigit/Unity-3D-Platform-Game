using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace SpawnCoinsNM
{
    public class SpawnCoin : MonoBehaviour
    {
        public List<Transform> spawnPoints = new List<Transform>();
        [SerializeField] private GameObject coin;
        public HashSet<int> randomValues = new HashSet<int>();
        public Random r = new Random();
        private void Start()
        {
            RandomSpawn();
        }
        private void RandomSpawn()
        {
            int a = Mathf.CeilToInt(spawnPoints.Count / 2.0f);
            while (randomValues.Count < a)
            {
                randomValues.Add(r.Next(0, spawnPoints.Count() - 1));
            }
            //var randomValues = Enumerable.Range(0, a).Select(e => spawnPoints[r.Next(spawnPoints.Count)]);
            //Debug.Log(randomValues.Count());
            foreach (var trns in randomValues)
            {
                Instantiate(coin, spawnPoints[trns]);
            }
        }
    }
}
