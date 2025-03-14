using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Utilities.General.Animation
{
    [CustomEditor(typeof(AnimatorControllerParametersCollection))]
    public class AnimatorControllerParametersCollectionEditor : AnimatorControllerCollectionEditor
    {
        protected override IEnumerable<AnimatorControllerDefinition> GetDefinitions()
        {
            return m_animatorController.parameters
                .Select(parameter => CreateInstance<AnimatorControllerParameterDefinition>()
                    .Initialize(m_animatorController, (parameter.name, parameter.nameHash, (int)parameter.type)))
                .Select(parameter =>
                {
                    AssetDatabase.AddObjectToAsset(parameter, target);
                    return parameter;
                });
        }
        
        protected override Data[] GetData()
        {
            return m_animatorController.parameters.Select(parameter => new Data()
            {
                name = parameter.name,
                hahs = parameter.nameHash,
                info = new object [] {(int)parameter.type}
            }).ToArray();
        }
        
        protected override object ConvertParameterDefinition(AnimatorControllerDefinition[] parameterDefinitions) 
            => parameterDefinitions.Cast<AnimatorControllerParameterDefinition>().ToArray();

        protected override void OverrideDefinition(AnimatorControllerDefinition destination,
            AnimatorControllerDefinition source) =>
            (destination as AnimatorControllerParameterDefinition).Initialize(
                source as AnimatorControllerParameterDefinition);

        protected override AnimatorControllerDefinition CreateDefinition(Data parameter) =>
            CreateInstance<AnimatorControllerParameterDefinition>()
                .Initialize(m_animatorController, (parameter.name, parameter.hahs, (int)parameter.info[0]));
    }
}