using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class InversionManager
    {
        private List<Invertible> invertibles;
        private bool worldInverted = false;

        public InversionManager()
        {
            if (invertibles == null)
            {
                invertibles = new List<Invertible>();
            }
        }

        public void sendInversionEvent() {
            for (int i = 0; i < invertibles.Count; i++)
            {
                invertibles[i].invert();
            }
        }

        public void registerInvertible(Invertible i)
        {
            invertibles.Add(i);
        }
    }
}
