using System.Collections.Generic;

namespace Project.Screpts.Services
{
    public class PauseService : IService
    {
        private List<IPauseItem> _pauseItems = new List<IPauseItem>();

        public void AddPauseItem(IPauseItem pauseItem)
        {
            _pauseItems.Add(pauseItem);
        }

        public void PauseAllItems()
        {
            foreach (var pauseItem in _pauseItems)
            {
                pauseItem.Pause();
            }
        }

        public void ContinueAllItems()
        {
            foreach (var pauseItem in _pauseItems)
            {
                pauseItem.Continue();
            }
        }

        public void ClearPauseItems()
        {
            _pauseItems.Clear();
        }
    }

    public interface IPauseItem
    {
        public void Pause();
        public void Continue();
    }
}