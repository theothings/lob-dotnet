﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lob.Net.Resouces
{
    public class Addresses : Resource
    {
        public Addresses(Lob lob) : base(lob)
        {
        }

        protected override string ResourcePathName
        {
            get
            {
                return "addresses";
            }
        }

    }
}
