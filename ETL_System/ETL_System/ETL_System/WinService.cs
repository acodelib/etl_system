﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;


namespace ETL_System
{
    public partial class WinService : ServiceBase
    {
        public WinService() {
            InitializeComponent();
        }

        protected override void OnStart(string[] args) {
        }

        protected override void OnStop() {
        }
    }
}
