using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG {
    public class CharacterAnimationManager : MonoBehaviour
    {
        [SerializeField] CharacterManager character;

        protected virtual void Awake()
        { }
        public void UpdateAnimatorMovementParameters(float horizontalValue, float verticalValue)
        {
            character.animator.SetFloat("Horizontal", horizontalValue);
            character.animator.SetFloat("Vertical", verticalValue);
        }
        public virtual void PlayerTargetActionAnimation(string targetAnimation, bool isPerformingAction, bool applyRootMotion = true)
        {
            character.animator.applyRootMotion = applyRootMotion;
            character.animator.CrossFade(targetAnimation, 0.2f);
            character.isPerformingAction = isPerformingAction;
        }
    }
}
