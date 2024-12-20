using UnityEngine;

namespace Project.Screpts.Services
{
    public class InputHandler : IService
    {
        private BirdController _birdController;

        public InputHandler(BirdController birdController)
        {
            _birdController = birdController;
        }

        public void HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _birdController.Jump();
            }
        }
    }
}