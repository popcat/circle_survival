using UnityEngine;
using System.Collections;

namespace CircleSurvival
{
    public class CircleSpawner: ISpawner
    {
        private readonly IPositionProvider positionProvider;
        private readonly ICircleProvider greenCircleGenerator;
        private readonly ICircleProvider blackCircleGenerator;
        private readonly ICoroutineRunner coroutineRunner;

        private float spawnTimeInterval;
        private readonly float deltaSpawnTime;

        private Coroutine spawnCoroutine;

        public CircleSpawner(
            ICircleProvider greenCircleGenerator, ICircleProvider blackCircleGenerator,
            IPositionProvider positionProvider, ICoroutineRunner coroutineRunner,
            float spawnTimeInterval, float deltaSpawnTime)
        {
            this.greenCircleGenerator = greenCircleGenerator;
            this.blackCircleGenerator = blackCircleGenerator;
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
                spawnTimeInterval = Mathf.Max(0, spawnTimeInterval - deltaSpawnTime);
            }
        }


        private void GenerateCircle(System.Random random)
        {
            int randValue = random.Next(10);
            ICircleController circleController;
            if(randValue == 0)
            {
                circleController = greenCircleGenerator.GetCircle();
            }
            else
            {
                circleController = blackCircleGenerator.GetCircle();
            }
            circleController.SetPosition(positionProvider.GetPosition());
            circleController.Activate();
        }
    }
}
