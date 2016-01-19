﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litmus.Domain
{
    public interface IScreenshot
    {
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        string Url { get; set; }
    }
}
