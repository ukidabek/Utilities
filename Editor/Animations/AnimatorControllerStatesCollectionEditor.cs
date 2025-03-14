using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Utilities.General.Animation
{
    [CustomEditor(typeof(AnimatorControllerStatesCollection))]
    public class AnimatorControllerStatesCollectionEditor : AnimatorControllerCollectionEditor
    {
        protected override AnimatorControllerDefinition CreateDefinition(Data parameter)
        {
            return CreateInstance<AnimatorControllerStateDefinition>()
                .Initialize(m_animatorController,
                    ((string)parameter.info[0],
                        (int)parameter.info[1],
                        parameter.name,
                        parameter.hahs));
        }

        protected override Data[] GetData()
        {
            var index = 0;
            return m_animatorController.layers.SelectMany(layer =>
            {
                var layerIndex = index++;
                return layer.stateMachine.states
                    .Select(state => state.state)
                    .Select(state => new Data()
                    {
                        name = state.name,
                        hahs = state.nameHash,
                        info = new object[]
                        {
                            layer.name,
                            layerIndex,
                        }
                    });
            }).ToArray();
        }

        protected override object ConvertParameterDefinition(AnimatorControllerDefinition[] parameterDefinitions) 
            => parameterDefinitions.Cast<AnimatorControllerStateDefinition>().ToArray();

        protected override void OverrideDefinition(AnimatorControllerDefinition destination, 
            AnimatorControllerDefinition source) =>
            (destination as AnimatorControllerStateDefinition).Initialize(
                source as AnimatorControllerStateDefinition);

        protected override bool CompareDataToDefinition(Data data, AnimatorControllerDefinition definition)
        {
            var stateDefinition = definition as AnimatorControllerStateDefinition;
            var layerIndex = (int)data.info[1];
            return base.CompareDataToDefinition(data, definition) && 
                   stateDefinition.LayerIndex== layerIndex;
        }

        protected override IEnumerable<AnimatorControllerDefinition> GetDefinitions()
        {
            var index = 0;
            return m_animatorController.layers.SelectMany(layer =>
                {
                    var currentIndex = index++;
                    return layer.stateMachine.states.Select(state => (
                        layername: layer.name,
                        layerIndex: currentIndex,
                        stateName: state.state.name,
                        nameHash: state.state.nameHash));
                })
                .Select(stateInfo => CreateInstance<AnimatorControllerStateDefinition>()
                    .Initialize(m_animatorController, stateInfo));
        }
    }
}