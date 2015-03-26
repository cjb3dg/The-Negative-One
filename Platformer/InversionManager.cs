﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class InversionManager
    {
        /* is the world inverted. True = negative world; False = normal world */
        public bool worldInverted { get; private set; }

        /* all non-neutrals */
        public List<Invertible> invertibles { get; private set; }

        public InversionManager()
        {
            worldInverted = false;
            if (invertibles == null)
            {
                invertibles = new List<Invertible>();
            }
        }

        public void invert()
        {
            worldInverted = !worldInverted;
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
