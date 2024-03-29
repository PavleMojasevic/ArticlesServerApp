﻿using System.Security.Cryptography.Xml;

namespace ServerApp.Models;

public class Article
{
    public long Id { get; set; }
    public string Title { get; set; } = "";
    public string Content { get; set; } = "";
    public string? Image { get; set; }
    public long AuthorId { get; set; }
    public User? Author { get; set; }
    public Category? Category { get; set; }
    public long CategoryId { get; set; }
    public DateTime Date { get; set; }
    public List<Comment> Comments { get; set; } = new List<Comment>();
    public List<ArticleTag> Tags { get; set; } = new List<ArticleTag>();
}
