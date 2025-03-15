using System;

namespace Utilities.General.Characters
{
    [Serializable]
    public abstract class UpdatableResource : Resource, IUpdatableCharacterResource
    {
        public abstract void Update(float deltaTime);
    }
}