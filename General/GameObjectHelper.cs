using System.Collections.Generic;
using UnityEngine;

namespace Utilities.General
{
    public static class GameObjectHelper
    {
        public static void SetActive(Component component, bool status) => SetActive(component.gameObject, status);
        
        public static void SetActive(GameObject gameObject, bool status)
        {
            if (gameObject.activeSelf != status)
                gameObject.SetActive(status);
        }

        public static void SetActive(IEnumerable<GameObject> gameObjects, bool status)
        {
            foreach (var gameObject in gameObjects)
                SetActive(gameObject, status);
        }
    }
}