using System.Collections.Generic;
using System.Threading;
using Model;

namespace View.Input.AI
{
    class ChosenColorQueue
    {
        #region Singleton
        private static ChosenColorQueue _instance;
        private ChosenColorQueue() {}

        public static ChosenColorQueue Instance()
        {
            if (_instance == null)
                _instance = new ChosenColorQueue();
            return _instance;
        }
        #endregion

        private Queue<Color> _queue = new Queue<Color>();
        public void Enqueue(Color color)
        {
            Monitor.Enter(_queue);
            _queue.Enqueue(color);
            Monitor.Exit(_queue); //signal that something has been inserted
        }
        public Color Dequeue()
        {
            Color color;
            Monitor.Enter(_queue);
            if (_queue.Count < 1)
                Monitor.Wait(_queue);
            color = _queue.Dequeue();
            Monitor.Exit(_queue);
            return color;
        }
    }
}
