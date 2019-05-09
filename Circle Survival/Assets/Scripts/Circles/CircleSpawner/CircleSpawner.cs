using UnityEngine;
using System.Collections;

namespace CircleSurvival
{
    public class CircleSpawner: ISpawner
    {
        private readonly IPositionProvider positionProvider;
        private readonly ICircleProvider greenCircleProvider;
        private readonly ICircleProvider blackCircleProvider;
        private readonly ICoroutineRunner coroutineRunner;

        private float spawnTimeInterval;
        private readonly float deltaSpawnTime;

        private Coroutine spawnCoroutine;

        public CircleSpawner(
            ICircleProvider greenCircleProvider, ICircleProvider blackCircleProvider,
            IPositionProvider positionProvider, ICoroutineRunner coroutineRunner,
            float spawnTimeInterval, float deltaSpawnTime)
        {
            this.greenCircleProvider = greenCircleProvider;
            this.blackCircleProvider = blackCircleProvider;
            this.positionProvider = positionProvider;
            this.coroutineRunner = coroutineRunner;

            this.spawnTimeInterval = spawnTimeInterval;
            this.deltaSpawnTime = deltaSpawnTime;
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
                yield return new WaitForSeconds(spawnTimeInterval);
                spawnTimeInterval = Mathf.Max(0.25f, spawnTimeInterval - deltaSpawnTime);
            }
        }


        private void GenerateCircle(System.Random random)
        {
            int randValue = random.Next(10);
            GameObject circle;
            
            if(randValue == 0)
            {
                circle = blackCircleProvider.GetCircle();
            }
            else
            {
                circle = greenCircleProvider.GetCircle();
            }
            //circle = greenCircleProvider.GetCircle();
            circle.transform.position = positionProvider.GetPosition();
        }
    }
}
