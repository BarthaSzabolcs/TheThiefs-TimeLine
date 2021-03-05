using UnityEngine;
using UnityEngine.AI;

namespace BarthaSzabolcs.CutScene.Movement
{
    public class AgentHelper : MonoBehaviour
    {
        #region Datamembers

        #region Editor Settings

        [Header("Goals:")]
        [SerializeField] public Goal[] goals;
        [SerializeField] private float rotationLerp;
        [SerializeField] private float goalThreshold = 0.25f;

        [Header("Gizmo:")]
        [SerializeField] private Color gizmoColor = Color.cyan;
        
        #endregion
        #region Private Properties

        private Goal Goal
        {
            get
            {
                if (goalIndex < 0 || goalIndex >= goals.Length)
                {
                    return null;
                }
                else
                {
                    return goals[goalIndex];
                }
            }
        }

        #endregion
        #region Private Fields

        private NavMeshAgent agent;

        private int goalIndex = 0;

        #endregion

        #endregion


        #region Methods

        #region Unity Callbacks

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.stoppingDistance = goalThreshold;
        }

        private void Update()
        {
            if (Goal != null && agent.remainingDistance < goalThreshold)
            {
                var correctRotation = Quaternion.Euler(
                    transform.rotation.x,
                    Goal.Rotation,
                    transform.rotation.z);

                transform.rotation = Quaternion.Lerp(transform.rotation, correctRotation, rotationLerp);
            }
        }

        private void OnDrawGizmos()
        {
            if (goals != null)
            {
                Gizmos.color = gizmoColor;
                var originalMatrix = Gizmos.matrix;
                foreach (var goal in goals)
                {
                    Gizmos.matrix = Matrix4x4.TRS(
                        pos: goal.Position,
                        q: Quaternion.Euler(0, goal.Rotation, 0),
                        s: Vector3.one);

                    Gizmos.DrawWireSphere(Vector3.zero, 0.25f);
                    Gizmos.DrawCube(Vector3.forward * 0.5f, new Vector3(0.1f, 0.1f, 1));
                }
                Gizmos.matrix = originalMatrix;
            }
        }
        
        #endregion
        #region Public

        public void Advance()
        {
            Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGBA(gizmoColor)}>{name}: Next goal.</color>");

            if (goalIndex + 1 < goals.Length)
            {
                goalIndex++;

                agent.SetDestination(Goal.Position);
                agent.speed = Goal.Speed;
            }
        }

        public void Reset()
        {
            goalIndex = 0;

            agent.Warp(Goal.Position);
            agent.transform.rotation = Quaternion.Euler(
                0,
                Goal.Rotation,
                0);
        }

        #endregion

        #endregion
    }
}
