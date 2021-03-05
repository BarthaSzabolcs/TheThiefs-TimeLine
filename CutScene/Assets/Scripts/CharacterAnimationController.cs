using UnityEngine;
using UnityEngine.AI;

namespace BarthaSzabolcs.CutScene
{
    public class CharacterAnimationController : MonoBehaviour
    {
        #region Datamembers

        #region Private Fields

        private NavMeshAgent agent;
        private Animator animator;

        #endregion

        #endregion


        #region Methods

        #region Unity Callbacks

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }

        #endregion
        #region Public

        public void Take()
        {
            animator.SetTrigger("Take");
        }

        #endregion

        #endregion
    }
}