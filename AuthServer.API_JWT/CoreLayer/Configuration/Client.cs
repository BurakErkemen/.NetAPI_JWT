﻿namespace CoreLayer.Configuration
{
    public class Client
    {
        public string? Id { get; set; }

        public string? Secret { get; set; } 

        public List<string>? Audiences { get; set; } // www.myapi.com gibi
    }
}
