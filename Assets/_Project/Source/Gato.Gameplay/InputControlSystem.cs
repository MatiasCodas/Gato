using Gato.Core;
using UnityEngine;

namespace Gato.Gameplay
{
    public class InputControlSystem : MonoSystem
    {
        private const string HorizontalAxisName = "Horizontal";
        private const string VerticalAxisName = "Vertical";

        //private IeventS  //set as commentary as I wasn't sure of the objective here and was getting a compilation error

        public override void Setup()
        {

        }

        public override void Tick(float deltaTime)
        {
            Vector2 direction = new Vector2(Input.GetAxis(HorizontalAxisName), Input.GetAxis(VerticalAxisName));

            Debug.Log(direction);
        }
    }
}
