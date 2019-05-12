using UnityEngine;
using System.Collections;

namespace CircleSurvival
{
    /***
    * Spawns circles provided by circle providers
    ***/
    public class CircleSpawner: ISpawner
    {
        private readonly IPositionProvider positionProvider;
        private readonly ICircleProvider greenCircleProvider;
        private readonly ICircleProvider blackCircleProvider;
        private readonly ICoroutineRunner coroutineRunner;

        private float minSpawnTime;
        private float maxSpawnTime;
        private float deltaSpawnTime;
        private float deltaSpawnAcceleration;

        private Coroutine spawnCoroutine;

        public CircleSpawner(
            ICircleProvider greenCircleProvider, ICircleProvider blackCircleProvider,
            IPositionProvider positionProvider, ICoroutineRunner coroutineRunner,
            float minSpawnTime, float maxSpawnTime, float deltaSpawnTime, float deltaSpawnAcceleration)
        {
            this.greenCircleProvider = greenCircleProvider;
            this.blackCircleProvider = blackCircleProvider;
            this.positionProvider = positionProvider;
            this.coroutineRunner = coroutineRunner;

            this.minSpawnTime = minSpawnTime;
            this.maxSpawnTime = maxSpawnTime;
            this.deltaSpawnTime = deltaSpawnTime;
            this.deltaSpawnAcceleration = deltaSpawnAcceleration;
        }

        public void StartSpawning()
        {
            spawnCoroutine = coroutineRunner.StartCoroutine(SpawnCircles());
        }

        public void StopSpawning()
        {
            coroutineRunner.StopCoroutine(spawnCoroutine);
        }

        private IEnumerator SpawnCircles()
        {
            System.Random random = new System.Random();
            while(true)
            {
                GenerateCircle(random);
                float timeInterval = UnityEngine.Random.Range(minSpawnTime, maxSpawnTime);
                yield return new WaitForSeconds(timeInterval);
                minSpawnTime = Mathf.Max(0.1f, minSpawnTime - deltaSpawnTime);
                maxSpawnTime = Mathf.Max(0.1f, maxSpawnTime - deltaSpawnTime);
                deltaSpawnTime = Mathf.Max(0.002f, deltaSpawnTime - deltaSpawnAcceleration);
                Debug.Log("min spawn time =" + minSpawnTime);
                Debug.Log("deltaSpawnTimee =" + deltaSpawnTime);
            }
        }


        private void GenerateCircle(System.Random random)
        {
            int randValue = random.Next(10);
            GameObject circle = (randValue == 0) ? blackCircleProvider.GetCircle() : greenCircleProvider.GetCircle();
            circle.transform.position = positionProvider.GetPosition();
        }
    }
}
