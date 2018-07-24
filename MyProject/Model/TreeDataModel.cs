﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyProject.Model
{
    public class TreeDataModel
    {
        public string id { get; set; }
        public string label { get; set; }
        public bool? collapsed { get; set; }
        public TreeDataModel[] children { get; set; }
    }
}