using System;
using System.Collections.Generic;
using System.Text;

namespace FindABook.Models.UtillityModels
{
    public class DbConnectionSettings
    {
        public string Server { get; set; }
        public string Port { get; set; }
        public bool WindowsAuth { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public string ServerType { get; set; }
    }
}
