﻿using System.ComponentModel.DataAnnotations;

namespace ReleaseCrawler.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public Genre()
        {

        }

        public Genre(string name)
        {
            Name = name;
        }
    }
}