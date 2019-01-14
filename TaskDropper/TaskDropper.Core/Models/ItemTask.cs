﻿using SQLite;

namespace TaskDropper.Core.Models
{
    public class ItemTask
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }

        public ItemTask() { }

        public ItemTask(int id, int userId, string title, string description, bool status)
        {
            Id = id;
            UserId = userId;
            Title = title;
            Description = description;
            Status = status;
        }
    }
}