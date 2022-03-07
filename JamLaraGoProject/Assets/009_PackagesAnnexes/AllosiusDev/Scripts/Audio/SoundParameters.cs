//
// Updated by Allosius(Yanis Q.) on 17/10/2021.
//

using UnityEngine;

namespace AllosiusDev {
    [System.Serializable]
    public class SoundParameters {
        public float Volume2d => volume;
        public float Volume3d => volume3d;
        public float Pitch => pitch;
        public bool Loop => loop;
        public int Priority => priority;
        public int SpacialBlend => spacialBlend;
        //public bool Is2D => spacialBlend == 0;
        //public bool Is3D => spacialBlend == 1;
        public enum TypeSound
        {
            Sfx,
            Music,
            Ambients,
        }
        public TypeSound typeSound;

        [Range(0f, 5f)]
        [SerializeField] private float volume;
        [Range(0f, 5f)]
        [SerializeField] private float volume3d;
        [Range(.1f, 3)]
        [SerializeField] private float pitch;
        [SerializeField] private bool loop;
        [Range(0, 256)]
        [SerializeField] private int priority;
        [Range(0, 1)]
        [SerializeField] private int spacialBlend;

        public SoundParameters(float volume = 1f, float volume3d = 1f, float pitch = 1f, bool loop = false, int priority = 128, int spacialBlend = 0)
        {
            this.volume = volume;
            this.volume3d = volume3d;
            this.pitch = pitch;
            this.loop = loop;
            this.priority = priority;
            this.spacialBlend = spacialBlend;
        }

        public void SetSpacialBlend(int _value)
        {
            this.spacialBlend = _value;
        }
    }
}