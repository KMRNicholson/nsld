﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace NeverSkipLegDay.Models
{
    class Workout : Model
    {
        [PrimaryKey, AutoIncrement]
        public string Name { get; set; }
    }
}
