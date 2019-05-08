using UnityEngine;
using System.Collections;

namespace CircleSurvival
{
    public class GameSceneInitializer : MonoBehaviour
    {
        PositionProvider positionProvider;
        private void Awake()
        {
            positionProvider = new PositionProvider(1, -2, 2, -2, 2);
        }
    }
}
