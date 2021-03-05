using System;

using UnityEngine;

namespace BarthaSzabolcs.CutScene.Movement
{
    [Serializable]
    public class Goal
    {
        [field: SerializeField] 
        public Vector3 Position { get; set; }
        
        [field: SerializeField, Range(0, 360)]
        public float Rotation { get; set; }

        [field: SerializeField]
        public float Speed { get; set; } = 1.5f;
    }
}
