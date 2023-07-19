using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Gato.Rendering
{
    public class PostProcessingManager : MonoBehaviour
    {
        private PostProcessProfile _ppProfile;

        private void Start()
        {
            _ppProfile = GetComponent<PostProcessVolume>().profile;
        }


        public void FadeIn(float factor)
        {
            _ppProfile.GetSetting<ColorGrading>().brightness.value = Mathf.Lerp(0, -100, Mathf.Abs( factor));

        }

        public void FadeOut(float factor)
        {
            _ppProfile.GetSetting<ColorGrading>().brightness.value = Mathf.Lerp(0, -100, Mathf.Abs(factor));
        }
    }
}
