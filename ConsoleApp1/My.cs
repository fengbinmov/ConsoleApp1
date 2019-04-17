using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    public class My
    {
        private Timer timer = null;
        public bool enable = false;

        public void OnEnable()
        {

            if (!enable)
            {
                enable = true;
                timer = new Timer(new TimerCallback(Enable), enable, 0, 10);
                Start();
            }
        }
        private void Enable(object acit)
        {

            if (enable)
            {
                Update();
            }
            else
            {
                timer.Dispose();
                Destroy();
            }
        }
        public virtual void Start()
        {
        }
        public virtual void Update()
        {
        }
        public virtual void Destroy()
        {
        }
    }
}
