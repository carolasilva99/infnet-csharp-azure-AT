﻿namespace MVC.Models.States
{
    public class CreateStateDto
    {
        public string Name { get; set; }
        public string FlagBase64 { get; set; }
        public int CountryId { get; set; }
    }
}
