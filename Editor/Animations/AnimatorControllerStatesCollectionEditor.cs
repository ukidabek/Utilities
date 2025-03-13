using System.Collections.Generic;
using UnityEditor;

namespace Utilities.General.Animation
{
    [CustomEditor(typeof(AnimatorControllerStatesCollection))]
    public class AnimatorControllerStatesCollectionEditor : AnimatorControllerCollectionEditor
    {
        protected override AnimatorControllerDefinition CreateDefinition(Data parameter)
        {
            throw new System.NotImplementedException();
        }

        protected override Data[] GetData()
        {
            throw new System.NotImplementedException();
        }

        protected override object ConvertParameterDefinition(AnimatorControllerDefinition[] parameterDefinitions)
        {
            throw new System.NotImplementedException();
        }

        protected override void OverrideDefinition(AnimatorControllerDefinition destination,
            AnimatorControllerDefinition animatorControllerDefinition)
        {
            throw new System.NotImplementedException();
        }

        protected override IEnumerable<AnimatorControllerDefinition> GetDefinitions()
        {
            throw new System.NotImplementedException();
        }
    }
}