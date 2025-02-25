using System;

namespace Utilities.General.Characters
{
    [Serializable]
    public abstract class UpdatableCharacterResource : CharacterResource, IUpdatableCharacterResource
    {
        public abstract void Update(float deltaTime);
    }
}