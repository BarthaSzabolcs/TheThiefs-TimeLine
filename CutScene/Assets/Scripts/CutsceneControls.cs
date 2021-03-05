using UnityEngine;
using UnityEngine.Playables;

using TMPro;

using BarthaSzabolcs.CutScene.Movement;

namespace BarthaSzabolcs.CutScene
{
    public class CutsceneControls : MonoBehaviour
    {
        #region Datamembers

        #region Editor Settings

        [Header("Components:")]
        [SerializeField] private AgentHelper[] agents;
        [SerializeField] private PlayableDirector director;

        [Header("GUI:")]
        [SerializeField] private GameObject help;
        [SerializeField] private TextMeshProUGUI timeScale_text;

        #endregion
        #region Private Fields

        private KeyCode[] numberKeyCodes = {
         KeyCode.Alpha0,
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
     };

        #endregion

        #endregion


        #region Methods

        private void Start()
        {
            director.stopped += (playbleDirector) =>
            {
                Debug.Log("Press Space to replay.");
            };
        }

        private void Update()
        {
            timeScale_text.text = $"Time scale: {Time.timeScale}";

            if (Input.GetKeyDown(KeyCode.H))
            {
                help.SetActive(!help.activeSelf);
            }

            for (int i = 0; i < numberKeyCodes.Length; i++)
            {
                if (Input.GetKeyDown(numberKeyCodes[i]))
                {
                    Time.timeScale = i;
                    Debug.Log($"<color=yellow>Time scale set to: {Time.timeScale}</color>");

                    return;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                foreach (var agent in agents)
                {
                    agent.Reset();
                }

                Time.timeScale = 1;
                director.time = 0;
                director.Stop();
                director.Evaluate();
                director.Play();

                Debug.Log($"<color=yellow>Cutscene started.</color>");
            }
        }

        #endregion
    }
}