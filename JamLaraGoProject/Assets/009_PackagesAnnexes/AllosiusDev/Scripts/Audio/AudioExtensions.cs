//
// Updated by Allosius(Yanis Q.) on 17/10/2021.
//

using UnityEngine;

namespace AllosiusDev {
    public partial class Extensions {

        public static AudioSource SetSoundToSource(this AudioSource source, Sound sound) {
            source.clip = sound.Clip;
            source.loop = sound.Parameters.Loop;
            source.pitch = sound.Parameters.Pitch;

            if (sound.Parameters.SpacialBlend != 0)
            {
                source.volume = sound.Parameters.Volume3d;
            }
            else
            {
                source.volume = sound.Parameters.Volume2d;
            }

            source.spatialBlend = sound.Parameters.SpacialBlend;
            return source;
        }
    }
}