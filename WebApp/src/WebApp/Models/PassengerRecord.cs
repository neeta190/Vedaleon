﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class PassengerRecord
    {
        public string RecordTag { get; set; }

        public List<string> Passengers { get; set; }
    }
}