using System.Linq;
using System.Text;
using Grenaders.Editor.CodeGeneration.Utils;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Grenaders.Editor.CodeGeneration.MyCodeGenerators
{
    public class AnimationStatesCodeGenerator : BaseCodeGenerator
    {
        public override string FileName()
        {
            return "AnimationStates";
        }

        public override string Code()
        {
            var allAnimationControllersPaths = AssetPathFinder.FindAssetsByType("t:animatorcontroller");
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("using UnityEngine;");
            builder.AppendFormatLine("public static class {0}", FileName());
            builder.AppendLine("{");

            foreach (var path in allAnimationControllersPaths)
            {
                var animatorName = System.IO.Path.GetFileNameWithoutExtension(path);
                animatorName = animatorName.ToLower().FirstCharToUpper();

                AnimatorController animatorController = AssetDatabase.LoadAssetAtPath<AnimatorController>(path);

                animatorController.layers.ToList()
                    .ForEach(layer =>
                    {
                        layer.stateMachine.states.ToList().ForEach(eachChildAnimatorState =>
                        { 
                            var pascalSnakeStateName = eachChildAnimatorState.state.name.ToPascalSnake();
                            var fieldStrName = animatorName + "_Id" + pascalSnakeStateName;
                            var fileldStrValue = eachChildAnimatorState.state.name;
                            builder.AppendFormatLine("public const string {0} = \"{1}\";", fieldStrName, fileldStrValue);


                            var fieldHashName = animatorName + "_Hash" + pascalSnakeStateName;
                            var fieldHashValue = Animator.StringToHash(eachChildAnimatorState.state.name);
                            builder.AppendFormatLine("public const int {0} = {1};", fieldHashName, fieldHashValue.ToString());
                        });
                    });
            }


            builder.AppendLine("}");
            return builder.ToString();
        }
    }
}